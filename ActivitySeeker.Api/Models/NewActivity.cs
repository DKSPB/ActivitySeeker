namespace ActivitySeeker.Api.Models;

public class NewActivity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public IFormFile Image { get; set; }
}