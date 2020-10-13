namespace API.Models
{
    /// <summary>
    /// Model used for creating Category on HttpPost request
    /// </summary>
    public class CategoryForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}