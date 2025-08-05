using MediatR;
using AutoMapper;
using UseCases.Common;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var entity = await _context.Activities.Include(x => x.ActivityType)
                             .FirstAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ?? 
                throw new ObjectNotFoundException(nameof(Entities.Activity), request.Id);

            return _mapper.Map<ActivityDto>(entity);
        }
    }
}
