
using MediatR;
using TechTalk.SpecFlow.CommonModels;

namespace Application.Features.Locations.Command;

public class CreateLocationCommand : IRequest<Result<int>>
{
}
