﻿namespace BlazorShop.Models.Products
{
    using Common.Mapping;
    using Data.Models;

    public class ProductListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
