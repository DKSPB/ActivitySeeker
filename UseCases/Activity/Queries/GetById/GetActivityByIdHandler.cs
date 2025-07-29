using MediatR;
using AutoMapper;
using UseCases.Common;
using DataAccess.Interfaces;
using UseCases.Activity.Models;
using Entities = Domain.Entities;


namespace UseCases.Activity.Queries.GetById
{
    internal class GetActivityByIdHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivityByIdHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ActivityDto> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Activities
                .FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken: cancellationToken) ?? 
                throw new ObjectNotFoundException(nameof(Entities.Activity), request.Id);

            return _mapper.Map<ActivityDto>(entity);
        }
    }
}
