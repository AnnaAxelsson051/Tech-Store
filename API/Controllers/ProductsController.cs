using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{

		private readonly StoreContext context;
		public ProductsController(StoreContext context)
		{
			this.context = context;
		}

		//Return all products 

		[HttpGet]
		public async Task <ActionResult<List<Product>>> GetProducts()
		{
			return await context.Products.ToListAsync();

		}

		//Return individual product

		[HttpGet("{id}")]
		public async Task <ActionResult<Product>> GetProduct(int id)
		{
			return await context.Products.FindAsync(id);
		}
		
	}
}

