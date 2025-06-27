using MediatR;
using AutoMapper;
using DataAccess.Interfaces;
using UseCases.Activity.Models;

namespace UseCases.Activity.Queries.GetById
{
    internal class GetActivityByIdHandler : IRequestHandler<GetActivityByIdCommand, ActivityDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivityByIdHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ActivityDto> Handle(GetActivityByIdCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Activities
                .FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken: cancellationToken) ?? 
                throw new NullReferenceException($"Активность с идентификатором {request.Id} не найдена");

            return _mapper.Map<ActivityDto>(entity);
        }
    }
}
