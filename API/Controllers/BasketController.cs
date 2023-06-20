

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
           [HttpGet]
           public async Task<ActionResult>Basket>> GetBasket()
           {
            
           var basket = await RetrieveBasket();
           
           if (basket == null) return NotFound();

           return basket;
           }

           [HttpPost]
           public async Task <ActionResult> AddItemToBasket(int productId, int quantity)
           {
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();

            return StatusCode(201);
           }


           [HttpDelete]
           public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
           {
            return Ok();
           }
             private async Task <BasketController> RetrieveBasket()
           {
            return await _context.Baskets
            .Include(in => int.Items)
            .ThenInclude(p => p.Product) //info
            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
           }

        //Creating a basket with a global unique identifyer for the basket from db
        //Adding basket
        private Basket CreateBasket()
           {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId, cookieOptions)
          var basket = new Basket{BuyerId = buyerId};
          _context.Baskets.Add(basket);
          return basket;
          }
    }
}