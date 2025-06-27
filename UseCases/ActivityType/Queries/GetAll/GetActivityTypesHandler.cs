using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetAll
{
    public class GetActivityTypesHandler : IRequestHandler<GetActivityTypesCommand, List<ActivityTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public GetActivityTypesHandler(IMapper mapper, IDbContext contex)
        {
            _mapper = mapper;
            _context = contex;
        }
        public async Task<List<ActivityTypeDto>> Handle(GetActivityTypesCommand request, CancellationToken cancellationToken)
        {
            var entities = await _context.ActivityTypes.ToListAsync(cancellationToken);
            return _mapper.Map<List<ActivityTypeDto>>(entities);
        }
    }
}
