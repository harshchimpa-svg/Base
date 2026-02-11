using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;

namespace Domain.Entities.Notifications;

public class Notification:BaseAuditableEntity
{
    public string Title { get; set; }
    public string Message { get; set; }
    [ForeignKey("UserId")]
    public string? UserId { get; set; }
    public User? User { get; set; }
}