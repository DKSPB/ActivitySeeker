using ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;
using ActivitySeeker.UseCases.ActivityType.Dto;
using ActivitySeeker.UseCases.ActivityType.Queries.GetById;
using ActivitySeeker.UseCases.Utils;
using AutoMapper;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UploadActivityTypeImage;

public class UploadActivityTypeImageHandler : IRequestHandler<UploadActivityTypeImageCommand>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UploadActivityTypeImageHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task Handle(UploadActivityTypeImageCommand command, CancellationToken cancellationToken)
    {
        var imageModel = command.UploadActivityTypeImage;

        var activityType = await _mediator.Send(new GetActivityTypeByIdQuery(imageModel.ActivityTypeId), cancellationToken);

        activityType.ImagePath = imageModel.Path;

        await FileProvider.UploadImage(imageModel.Path, imageModel.Image);

        var activityTypeDto = _mapper.Map<UpdateActivityTypeDto>(activityType);
        
        await _mediator.Send(new UpdateActivityTypeCommand(activityTypeDto), cancellationToken);
    }
}