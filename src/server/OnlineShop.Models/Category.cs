using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    [Table("Categories", Schema = "Common")]
    public class Category : Entity
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}