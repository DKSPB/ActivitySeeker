using Newtonsoft.Json;

namespace Controllers.ViewModels
{
    [JsonObject]
    public class NewActivityType
    {
        public Guid? Id { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
    }
}
