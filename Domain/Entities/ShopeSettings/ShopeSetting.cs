using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Employees;

namespace Domain.Entities.ShopeSettings;

public class ShopeSetting:BaseAuditableEntity
{
    public string ShopeName { get; set; }
    public string OnerName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public int GstNumber  { get; set; }
    [ForeignKey("Employee")]
    public int? EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public string? UserId { get; set; }
}