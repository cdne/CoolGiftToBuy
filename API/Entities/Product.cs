using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    /// <summary>
    /// Product class entity used in Entity Framework
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }

        public string ImageSource { get; set; }

        public string AffiliateLink { get; set; }

        public string ProductUrl { get; set; }
        
        public ICollection<Tag> Tags { get; set; } = new List<Tag>(); 
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}