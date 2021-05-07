using FamilyTree.Application.PersonContent.DataHolders.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Hubs
{
    public class PrivacyHub : Hub
    {
        public async Task SendDataHolderPrivacyNotification(DataHolderDto dataHolder, string userId)
        {            
            await Clients.User(userId)
                .SendAsync("ReceiveDataHolderPrivacyNotification", dataHolder);
        }
    }
}
