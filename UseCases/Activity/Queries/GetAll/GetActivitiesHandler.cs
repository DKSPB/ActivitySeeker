using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Activity.Models;

namespace UseCases.Activity.Queries.GetAll;

public class GetActivitiesHandler : IRequestHandler<GetActivitiesCommand, List<ActivityDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetActivitiesHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ActivityDto>> Handle(GetActivitiesCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.Activities.ToListAsync(cancellationToken);
        return _mapper.Map<List<ActivityDto>>(entities);
    }
}