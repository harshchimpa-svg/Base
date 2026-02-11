using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Diets;
using Domain.Entities.Notifications;

namespace Application.Dto.Notifications;

public class GetNotificationDto: BaseDto,IMapFrom<Notification>
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string? UserId { get; set; }
}