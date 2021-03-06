﻿namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models.ShoppingCarts;

    public class ShoppingCartService : BaseService<ShoppingCart>, IShoppingCartService
    {
        public ShoppingCartService(ApplicationDbContext db, IMapper mapper)
            : base(db, mapper)
        {
        }

        public async Task AddAsync(int productId, int quantity, string userId)
        {
            var shoppingCart = new ShoppingCart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            await this.Data.AddAsync(shoppingCart);
            await this.Data.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int productId, int quantity, string userId)
        {
            var shoppingCart = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (shoppingCart == null)
            {
                return false;
            }

            shoppingCart.Quantity = quantity;

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAsync(int productId, string userId)
        {
            var shoppingCart = await this.GetByProductIdAndUserIdAsync(productId, userId);
            if (shoppingCart == null)
            {
                return false;
            }

            this.Data.Remove(shoppingCart);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> GetByUserIdAsync(string userId)
            => await this.Mapper
                .ProjectTo<ShoppingCartProductsResponseModel>(this
                    .AllByUserId(userId)
                    .AsNoTracking())
                .ToListAsync();

        private async Task<ShoppingCart> GetByProductIdAndUserIdAsync(int productId, string userId)
            => await this
                .AllByUserId(userId)
                .FirstOrDefaultAsync(c => c.ProductId == productId);

        private IQueryable<ShoppingCart> AllByUserId(string userId)
            => this
                .All()
                .Where(c => c.UserId == userId);
    }
}
