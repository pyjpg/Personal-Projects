using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;
            
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        




        // How To Get Basket:
        // Step 1. Get basket.
        // Step 2. Create the basket.
        // Step 3. Get Product/(s) from basket
        // Step 4. Add Item to basket
        // Step 5. Save Changes.
        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            //Step 1:
            var basket = await RetrieveBasket();
            // Step 2:
            if (basket == null) basket = CreateBasket();
            // Step 3:
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return BadRequest(new ProblemDetails{Title = "Product not Found"});
            // Step 4:
            basket.AddItem(product, quantity);
            // Step 5:
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }

        

        // How To Get Basket:
        // Step 1. Get basket.
        // Step 2. Remove item and reduce quantity.
        // Step 3. Save changes.


        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            //Step 1:
            var basket = await RetrieveBasket();
            //Step 1 extend:
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            // Step 3:
            basket.RemoveItem(product, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();
            return BadRequest(new ProblemDetails{Title = "Problem removing item from basket"});
        }

        private  BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }
        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);
            return basket;
        }
    }
}