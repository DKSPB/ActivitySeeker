namespace UseCases.Activity.Queries.GetImage.Models
{
    public class FileResult
    {
        public string Extension { get; set; } = string.Empty;
        public Stream Content { get; set; } = default!;
    }
}
