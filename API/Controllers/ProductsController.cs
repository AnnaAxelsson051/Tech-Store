using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
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
		public async Task <ActionResult<PagedList<Product>>> GetProducts(ProductParams productParams)
		{
			var query = _context.Products
			.Sort(productParams.OrderBy)
			.Search(productParams.SearchTerm)
			.Filter(productParams.Brands, productParams.Types)
			.AsQueryable();

			var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

			Response.Headers.Add("Pagination", JsonSerializer.Serialize(products.MetaData));
			return products;
		}

		//Return individual product

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}

