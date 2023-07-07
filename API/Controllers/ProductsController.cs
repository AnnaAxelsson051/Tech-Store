using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
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
		public async Task <ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
		{
			var query = _context.Products
			.Sort(productParams.OrderBy)
			.Search(productParams.SearchTerm)
			.Filter(productParams.Brands, productParams.Types)
			.AsQueryable();

			var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

			Response.AddPaginationHeader(products.MetaData);
			return products;
		}

		//Return individual product

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }

		[HttpGet("filters")]
		public async Task<IActionResult> GetFilters()
		{
			var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
			return Ok(new { brands, types });
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task <ActionResult<Product>> CreateProduct(CreateProductDto productDto)
		{
			_context.Products.Add(product);
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);
			return BadRequestObjectResult(new ProblemDetails { Title = "Problem creating new product" });
		}
    }
}

