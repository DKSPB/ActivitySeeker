using MediatR;
using DataAccess.Interfaces;
using UseCases.Common;
using AutoMapper;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Commands.Update
{
    public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand, ActivityTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public UpdateActivityTypeHandler(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ActivityTypeDto> Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ActivityTypes
                .FindAsync(new object?[] { request.Id }, cancellationToken) ??
                throw new ObjectNotFoundException(nameof(Domain.Entities.ActivityType), request.Id);
            
            entity.TypeName = request.TypeName;
            entity.ParentId = request.ParentId;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityTypeDto>(entity);
        }
        
    }
}
