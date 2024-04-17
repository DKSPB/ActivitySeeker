namespace ActivitySeeker.Domain;

public class Activity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Company { get; set; } = default!;
}