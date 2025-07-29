using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Activity.Models;
using UseCases.Common;

namespace UseCases.ActivityType.Queries.GetActivitiesByType
{
    internal class GetActivitiesByTypeHandler : IRequestHandler<GetActivitiesByTypeQuery, PagedResult<ActivityDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivitiesByTypeHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<PagedResult<ActivityDto>> Handle(GetActivitiesByTypeQuery query, CancellationToken cancellationToken)
        {
            var entities = _context.Activities.Where(x => x.ActivityTypeId == query.Id);

            var total = await entities.CountAsync(cancellationToken);

            var items = await entities.OrderBy(x => x.StartDate)
                .Skip((query.Offset - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync(cancellationToken);

            return new PagedResult<ActivityDto>
            {
                Items = _mapper.Map<List<ActivityDto>>(items),
                Total = total
            };
        }
    }
}
