﻿namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Wishlist> Wishlists { get; } = new HashSet<Wishlist>();

        public ICollection<ShoppingCart> ShoppingCarts { get; } = new HashSet<ShoppingCart>();

        public ICollection<OrderProduct> Orders { get; } = new HashSet<OrderProduct>();
    }
}