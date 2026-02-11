using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Users.UserRoles;
using Domain.Entities.Employees;

namespace Application.Dto.Employees;

public class GetEmployeeDTO: BaseDto,IMapFrom<Employee>
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
    public string? RoleId { get; set; }
    public GetRoleDto Role { get; set; } 
}