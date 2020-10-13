namespace API.Models
{
    /// <summary>
    /// Class used for creating Product on HttpPost request
    /// </summary>
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public string AffiliateLink { get; set; }
        public string ProductUrl { get; set; }
        public int CategoryId { get; set; }
    }
}