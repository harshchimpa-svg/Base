    using Application.Dto.Customers;
    using Application.Dto.GymTrainers;
    using Application.Features.Customers.Queries;
    using Application.Interfaces.UnitOfWorkRepositories;
    using AutoMapper;
    using Domain.Entities.Services;
    using MediatR;
    using Shared;

    namespace Application.Features.GymTrainers.Queries;

    public class GetGymTrainerByIdQuery: IRequest<Result<GetGymTrainerDto>>
    {
        public int Id { get; set; }

        public GetGymTrainerByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetGymTrainerByIdQueryHandler : IRequestHandler<GetGymTrainerByIdQuery, Result<GetGymTrainerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGymTrainerByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetGymTrainerDto>> Handle(GetGymTrainerByIdQuery request, CancellationToken cancellationToken)
        {
            var gymTrainers = await _unitOfWork.Repository<Service>().GetByID(request.Id);

            if (gymTrainers == null)
            {
                return Result<GetGymTrainerDto>.BadRequest("GymTrainers not found.");
            }

            var mapData = _mapper.Map<GetGymTrainerDto>(gymTrainers);

            return Result<GetGymTrainerDto>.Success(mapData, "GymTrainers");
        }
    }