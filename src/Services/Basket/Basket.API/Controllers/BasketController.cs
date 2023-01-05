using Basket.API.Entities;
using Basket.API.Repositories.ba_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _repo;

        public BasketController(IBasketRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{userName}", Name="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repo.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket) 
        {
            return Ok(await _repo.UpdateBasket(basket));
        }
        
        [HttpDelete("{userName}",Name ="DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
             await _repo.DeleteBasket(userName);
            return Ok();
        }

    }
}
