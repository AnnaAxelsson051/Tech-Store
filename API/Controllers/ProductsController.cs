using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	public class ProductsController : BaseApiController
	{

		private readonly StoreContext _context;
		public ProductsController(StoreContext context)
		{
			_context = context;
		}

		//Return all products 

		[HttpGet]
		public async Task <ActionResult<List<Product>>> GetProducts(string orderBy)
		{
			var query = _context.Products
			.Sort(orderBy)
			.AsQueryable();

			return await query.ToListAsync();
		}

		//Return individual product

		[HttpGet("{id}")]
		public async Task <ActionResult<Product>>GetProduct(int id)
		{
			return product = await _context.Products.FindAsync(id);
		
		if (product == null) return NotFound();

		return product;
		}
		
	}
}

