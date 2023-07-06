using System;
using API.Data;
using API.DTOs;
using API.Entities.OrderAggregate;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace API.Controllers
{
	public class PaymentsController : BaseApiController
	{
		private readonly PaymentService _paymentService;
        private readonly StoreContext _context;
        private readonly IConfiguration _config;

        public PaymentsController(PaymentService paymentService, StoreContext context, IConfiguration config)
		{
			_config = config;
			_context = context;
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

		[HttpPost("webhook")]
		public async Task<ActionResult>StripeWebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
				_config["StripeSettings:WhSecret"]);

			var charge = (Charge)stripeEvent.Data.Object;
			var order = await _context.Orders.FirstOrDefaultAsync(x =>
			x.PaymentIntentId == charge.PaymentIntentId);

			if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.PaymentReceived;
			await _context.SaveChangesAsync();

			return new EmptyResult();
		}
	}
}

