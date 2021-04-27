using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.DataHolders.ViewModels;
using FamilyTree.Application.Privacy.Interfaces;
using FamilyTree.Application.Privacy.ViewModels;
using FamilyTree.Domain.Enums.Privacy;
using FamilyTree.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Services
{
    public class PrivacyNotificationsService : IPrivacyNotificationsService
    {
        private readonly IApplicationDbContext _context;

        private readonly IHubContext<PrivacyHub> _privacyHubContext;

        private readonly IDateTimeService _dateTimeService;

        public PrivacyNotificationsService(IApplicationDbContext context, 
            IHubContext<PrivacyHub> privacyHubContext, 
            IDateTimeService dateTimeService)
        {
            _context = context;
            _privacyHubContext = privacyHubContext;
            _dateTimeService = dateTimeService;
        }

        public async Task NotifyUsersIfPrivacyTimeExpired(CancellationToken cancellationToken = default)
        {
            var nowDateTime = _dateTimeService.Now;
            var dataHoldersPrivacies = await _context.DataHolderPrivacies
                .Include(dhp => dhp.DataHolder)
                .Where(dhp => !dhp.IsAlways.Value &&
                              nowDateTime > dhp.EndDate)
                .ToListAsync(cancellationToken);

            dataHoldersPrivacies.ForEach(item => 
            {
                item.IsAlways = true;
                item.PrivacyLevel = PrivacyLevel.PublicUse;
            });

            await _context.SaveChangesAsync(cancellationToken);

            dataHoldersPrivacies.ForEach(item =>
            {
                DataHolderDto dataHolder = new DataHolderDto() 
                {
                    Id = item.DataHolder.Id,
                    Title = item.DataHolder.Title,
                    Data = item.DataHolder.Data,
                    DataHolderType = item.DataHolder.DataHolderType,
                    IsDeletable = item.DataHolder.IsDeletable.Value,
                    Privacy = new DataHolderPrivacyDto() 
                    {
                        Id = item.Id,
                        BeginDate = item.BeginDate,
                        EndDate = item.EndDate,
                        IsAlways = item.IsAlways.Value,
                        PrivacyLevel = item.PrivacyLevel
                    }
                };

                _privacyHubContext.Clients
                    .User(item.CreatedBy)
                    .SendAsync("ReceiveDataHolderPrivacyNotification", dataHolder);
            });
        }
    }
}
