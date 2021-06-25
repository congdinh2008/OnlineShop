using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    [Table("Products", Schema = "Common")]
    public class Product : Entity
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}