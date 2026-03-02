using Application.Dto.Notifications;
using Shared;

namespace Application.Interfaces.Repositories.Notifications
{
    public interface INotificationRepository
    {
        Task<Result<bool>> SendNotificationAsync(GetNotificationDto dto, string connectionId);

        Task<Result<bool>> SetConnectionIdByNameAsync(string Id, string connectionId);
    }
}