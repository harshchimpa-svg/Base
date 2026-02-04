using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entities.ApplicationRoles;

namespace Domain.Entities.Employees;

public class Employee:BaseAuditableEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }  
    public string Alterphonenumber  { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; } 
    public string City { get; set; }
    public string State { get; set; }
    public string country { get; set; }
    [ForeignKey("RoleId")]
    public string? RoleId { get; set; }
    public Role Role { get; set; }
    
}