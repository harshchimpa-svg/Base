using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.ApplicationUsers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Tranners.Commands;

public class DeleteTranerCommand : IRequest<Result<string>>
{
    public string Id { get; set; }

    public DeleteTranerCommand(string id)
    {
        Id = id;
    }
}

internal class DeleteTrannerCommandHandler : IRequestHandler<DeleteTranerCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;


    public DeleteTrannerCommandHandler(IUnitOfWork unitOfWork,UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(DeleteTranerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return Result<string>.NotFound("User not found.");
        }

        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
        {
            var errors = string.Join(", ", deleteResult.Errors.Select(e => e.Description));
            return Result<string>.BadRequest($"Failed to delete user: {errors}");
        }

        return Result<string>.Success(request.Id, "User deleted successfully.");
    }
    
}