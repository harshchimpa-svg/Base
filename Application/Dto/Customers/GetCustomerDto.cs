using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Contacts;
using Domain.Entities.Customers;

namespace Application.Dto.Customers;

public class GetCustomerDto: BaseDto, IMapFrom<Customer>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Notes { get; set; }
}