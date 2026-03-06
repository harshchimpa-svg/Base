using Application.Dto.Users.UserRoles;
using Application.Interfaces.Repositories.UserIdAndOrganizationIds;
using AutoMapper;
using Domain.Entities.ApplicationRoles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Command
{
    public class UpdateRoleCommand : IRequest<Result<GetRoleDto>>
    {
        public Guid Id { get; set; }
        public CreateRoleCommand RoleCommand { get; set; }

        public UpdateRoleCommand(Guid id, CreateRoleCommand RoleCommand)
        {
            Id = id;
            this.RoleCommand = RoleCommand;
        }
    }
    internal class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<GetRoleDto>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserIdAndOrganizationIdRepository _organizationIdRepository;
        public UpdateRoleCommandHandler(RoleManager<Role> roleManager, IMapper mapper, IUserIdAndOrganizationIdRepository organizationIdRepository)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _organizationIdRepository = organizationIdRepository;
        }

        public async Task<Result<GetRoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var orgId = await _organizationIdRepository.Get();
            var roleId = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.Id.ToString() && x.OrganizationId == orgId.OrganizationId);
            if (roleId == null)
            { 
                return Result<GetRoleDto>.BadRequest("Sorry role id not found");
            }
            var mapRole = _mapper.Map(request.RoleCommand, roleId);
            await _roleManager.UpdateAsync(mapRole);
            return Result<GetRoleDto>.Success("Update Role...");    
        }
    }
}
