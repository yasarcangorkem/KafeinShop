using System.Collections.Generic;

namespace KafeinShop.Core.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}