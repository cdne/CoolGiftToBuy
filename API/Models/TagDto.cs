namespace API.Models
{
    /// <summary>
    /// Tag DTO without Product property
    /// </summary>
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}