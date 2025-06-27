using MediatR;
using DataAccess.Interfaces;

namespace UseCases.ActivityType.Commands.Update
{
    public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand>
    {
        private readonly IDbContext _context;
        public UpdateActivityTypeHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateActivityTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ActivityTypes
                .FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken);

            if(entity is null)
            {
                throw new NullReferenceException($"Тип активности с идентификатором {request.Id} не найден");
            }

            entity.TypeName = request.TypeName;
            entity.ParentId = request.ParentId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
