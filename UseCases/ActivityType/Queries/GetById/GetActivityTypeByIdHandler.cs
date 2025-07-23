using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetById
{
    internal class GetActivityTypeByIdHandler : IRequestHandler<GetActivityTypeByIdQuery, ActivityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivityTypeByIdHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ActivityTypeDto> Handle(GetActivityTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ActivityTypes
                .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken) ?? 
                throw new NullReferenceException($"Тип активности с идентификатором {request.Id} не найден");
            return _mapper.Map<ActivityTypeDto>(entity);
        }
    }
}
