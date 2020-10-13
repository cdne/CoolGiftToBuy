namespace API.Models
{
    /// <summary>
    /// Category data transfer object without Product collection
    /// Contains 
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}