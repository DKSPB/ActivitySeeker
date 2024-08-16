using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ImageDto
{
    public Guid Id { get; set; }

    public byte[] Content { get; set; }
        
    public ActivityDto Activity { get; set; }

    public ImageDto(){}
    public ImageDto(Image image)
    {
        Id = image.Id;
        Content = image.Content;
        Activity = new ActivityDto(image.Activity);
    }

    public Image ToEntity()
    {
        return new Image
        {
            Content = Content,
        };
    }
}