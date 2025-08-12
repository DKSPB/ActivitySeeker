using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.ActivityType.Models;
using UseCases.Common;

namespace UseCases.ActivityType.Queries.GetAll
{
    public class GetActivityTypesHandler : IRequestHandler<GetActivityTypesQuery, PagedResult<ActivityTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivityTypesHandler(IMapper mapper, IDbContext contex)
        {
            _mapper = mapper;
            _context = contex;
        }
        public async Task<PagedResult<ActivityTypeDto>> Handle(GetActivityTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.ActivityTypes;
            
            var total = await entities.CountAsync(cancellationToken);

            var items = await entities
                .Skip(Math.Max(0, (request.Offset - 1) * request.Limit))
                .Take(request.Limit)
                .ToListAsync(cancellationToken);
        
            return new PagedResult<ActivityTypeDto>
            {
                Items = _mapper.Map<List<ActivityTypeDto>>(items),
                Total = total
            };
        }
    }
}
