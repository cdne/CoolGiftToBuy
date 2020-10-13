namespace API.Models
{
    /// <summary>
    /// Product data transfer object class without Tags collection
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AffiliateLink { get; set; }
        public string ImageSource { get; set; }
        public string ProductUrl { get; set; }
        public int CategoryId { get; set; }
    }
}