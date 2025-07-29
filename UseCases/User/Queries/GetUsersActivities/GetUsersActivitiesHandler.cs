using MediatR;
using AutoMapper;
using UseCases.Common;
using DataAccess.Interfaces;
using UseCases.Activity.Models;
using Microsoft.EntityFrameworkCore;

namespace UseCases.User.Queries.GetUsersActivities
{
    internal class GetUsersActivitiesHandler : IRequestHandler<GetUsersActivitiesQuery, PagedResult<ActivityDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        
        public GetUsersActivitiesHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<PagedResult<ActivityDto>> Handle(GetUsersActivitiesQuery query, CancellationToken cancellationToken)
        {
            var entities = _context.Activities.Where(x => x.UserId == query.UserId);

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
