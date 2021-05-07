using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Privacy.Interfaces
{
    public interface IPrivacyNotificationsService
    {
        Task NotifyUsersIfDataHolderPrivacyTimeExpired(CancellationToken cancellationToken = default);

        Task NotifyUsersIfImagePrivacyTimeExpired(CancellationToken cancellationToken = default);

        Task NotifyUsersIfVideoPrivacyTimeExpired(CancellationToken cancellationToken = default);

    }
}
