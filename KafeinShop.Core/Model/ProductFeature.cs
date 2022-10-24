﻿namespace KafeinShop.Core.Model
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}