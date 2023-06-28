using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
	public class AccountController : BaseApiController
     {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
		{
		_userManager = userManager;
		}
	}
}

