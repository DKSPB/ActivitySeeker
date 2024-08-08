namespace ActivitySeeker.Bll.Models;

public class NewActivityDto
{
    public string ActivityName { get; set; }
    
    public string ActivityDescription { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public FileStream image { get; set; } 
}