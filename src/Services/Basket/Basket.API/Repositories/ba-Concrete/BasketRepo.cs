using Basket.API.Entities;
using Basket.API.Repositories.ba_Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories.ba_Concrete
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepo(IDistributedCache redisCache)
        {
              _redisCache =redisCache;
        }

        public async Task DeleteBasket(string userName) 
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            //It is a key-value pair implementation(UserName is the Key while object basket is the value
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
