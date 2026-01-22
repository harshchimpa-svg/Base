    /*using Application.Dto.Customers;
    using Application.Dto.GymTraners;
    using Application.Features.Customers.Queries;
    using Application.Interfaces.UnitOfWorkRepositories;
    using AutoMapper;
    using Domain.Entities.Services;
    using MediatR;
    using Shared;

    namespace Application.Features.GymTraners.Queries;

    public class GetGymTranerByIdQueries: IRequest<Result<GetGymTranerDto>>
    {
        public int Id { get; set; }

        public GetGymTranerByIdQueries(int id)
        {
            Id = id;
        }
    }
    internal class GetGymTranerByIdQueriesHandler : IRequestHandler<GetGymTranerByIdQueries, Result<GetGymTranerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGymTranerByIdQueriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetGymTranerDto>> Handle(GetGymTranerByIdQueries request, CancellationToken cancellationToken)
        {
            var Service = await _unitOfWork.Repository<Service>().GetByID(request.Id);

            if (Service == null)
            {
                return Result<GetGymTranerDto>.BadRequest("Service not found.");
            }

            var mapData = _mapper.Map<GetGymTranerDto>(Service);

            return Result<GetGymTranerDto>.Success(mapData, "Service");
        }
    }*/