using Application.Dto.GetUserIdAndOrganizationIds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.UserIdAndOrganizationIds;

public interface IUserIdAndOrganizationIdRepository
{
    Task<GetUserIdAndOrganizationIdDto> Get();
}
