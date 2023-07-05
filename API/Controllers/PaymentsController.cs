using System;
using API.Data;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class PaymentsController : BaseApiController
	{
		private readonly PaymentService _paymentService;
		public PaymentsController(PaymentService paymentService, StoreContext context)
		{
			_paymentService = paymentService;
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult<BasketDto>>CreateOrUpdatePaymentIntent()
		{
			var basket = await _context.Baskets
				.RetrieveBasketWithItems(UserDto.Identity.Name)
				.FirstOrDefaultAsync();

			if (basket == null) return NotFound();

			var intent = await _paymentService.CreateOrUpdatePaymentIntent(basket);
			if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem creting payment intent" });

			basket.PaymentIntentId = basket.PaymentIntentId ?? intent.Id;
			basket.ClientSecret = basket.ClientSecret ?? intent.ClientSecret;
			if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });
			return basket.MapBasketToDto();
		}
	}
}

