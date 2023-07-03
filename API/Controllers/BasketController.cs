

using API.Data;
using API.DTOs;
using API.Entities;
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

           //Getting basket with items and info about the products
           // (with cookies)
           [HttpGet(Name = "GetBasket")]
           public async Task<ActionResult<BasketDto>> GetBasket()
        {

            var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) return NotFound();

            return basket.MapBasketToDto();
        }


        //Checks if there is already a basket and retrieves it
        //Otherwise creates a basket, looks for product by id and adds it
        //Saves changes and ensures so changes were made
        [HttpPost]
           public async Task <ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
           {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket = CreateBasket();
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return BadRequest(new ProblemDetails{Title = "Product Not Found"});
            basket.AddItem(product, quantity)
            
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails{Title = "Error while saving item to basket" });
           }


           [HttpDelete]
           public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
           {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Error while removing item from basket" });
           }


        //If buyer id is empty delete cookie
        //otherwise retrieve basket
             private async Task <BasketController> RetrieveBasket(string buyerId)
           {
             if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            return await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(p => p.Product) //info
            .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
           }

        //Checkong if there is a username or a cookie for buyer id
        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"];
        }

        //Creating a basket with a global unique identifyer /
        // anonymous basket if user is not logged in
        //If user is logged in creating a basket setting buyer id to
        //the user name 
        private Basket CreateBasket()
           {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);
            return basket;
          }
    }
}