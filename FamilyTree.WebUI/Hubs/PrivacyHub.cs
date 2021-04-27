using FamilyTree.Domain.Entities.Privacy;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Hubs
{
    public class PrivacyHub : Hub
    {
        public async Task SendDataHolderPrivacyNotification(DataHolderPrivacy privacy)
        {            
            await Clients.User(privacy.CreatedBy)
                .SendAsync("ReceiveDataHolderPrivacyNotification", privacy);
        }
    }
}
