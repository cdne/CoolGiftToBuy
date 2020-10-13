namespace API.Models
{
    /// <summary>
    /// Class used for creating tags on HttpPost request
    /// </summary>
    public class TagForCreationDto
    {
         public string Name { get; set; }
         public int ProductId { get; set; }
    }
}