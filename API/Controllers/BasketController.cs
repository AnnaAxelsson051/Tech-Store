

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
            var basket = await _context.Baskets
            .Include(in => int.Items)
            .ThenInclude(p => p.Product) //info
            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
           
           if (basket == null) return NotFound();

           return basket;
           }
        
    }
}