using System;
using System.Collections.Generic;
using System.Text;

namespace KafeinShop.Core.DTOs
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public List<ProductDto> Products { get; set; }


    }
}
