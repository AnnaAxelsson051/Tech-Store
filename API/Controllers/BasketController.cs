

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

        //Checks if there is already a basket and retrieves it
        //Otherwise creates a basket, looks for product by id and adds it
        //Saves changes and ensures so changes were made
           [HttpPost]
           public async Task <ActionResult> AddItemToBasket(int productId, int quantity)
           {
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();
            basket.AddItem(product, quantity)
            
            var result = await _context.SaveChangesAsync() > 0;
            return (result) (201);

            return BadRequest(new ProblemDetails{Title = 'Error while saving item to basket'});
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