using System;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Controllers
{
	public class AccountController : BaseApiController
     {
        private readonly UserManager<User> _userManager;
		private readonly TokenService _tokenService;
		private readonly StoreContext _context;

        public AccountController(UserManager<User> userManager, TokenService tokenService, StoreContext context)
		{
	    _context = context;
		_userManager = userManager;
        _tokenService = tokenService;
        }

		[HttpPost("login")]
		public async Task <ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByNameAsync(loginDto.UserName);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
				return Unauthorized();

			var userBasket = await RetrieveBasket(loginDto.UserName);
			var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

			if (anonBasket != null)
            {
            if (userBasket != null) _context.Baskets.Remove(userBasket);
			anonBasket.BuyerId = user.UserName;
			ResponseCacheAttribute.Cookies.Delete("buyerId");
			await _context.SaveChangesAsync();
            }

            return new UserDto
			{
				Email = user.Email,
				Token = await _tokenService.GenerateToken(user),
                Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket.MapBasketToDto()

            };
					}

		[HttpPost("Register")]
		public async Task <ActionResult> Register(RegisterDto registerDto)
		{
			var user = new User { UserName = registerDto.UserName, Email = registerDto.Email };

			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if(!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return ValidationProblemDetails();
			}
			await _userManager.AddToRoleAsync(user, "Member");
			return StatusCode(201);
		}

		[Authorize]
		[HttpGet("currentUser")]
		public async Task<ActionResult<UserDto>>GetCurrentUser()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			return new UserDto
			{
				Email = user.Email,
				Token = await _tokenService.GenerateToken(user)
			};
		}

        private async Task<BasketController> RetrieveBasket(string buyerId)
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
    }
}

