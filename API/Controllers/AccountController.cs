﻿using System;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AccountController : BaseApiController
     {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
		{
		_userManager = userManager;
		}

		[HttpPost("login")]
		public async Task <ActionResult<User>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByNameAsync(loginDto.UserName);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
				return Unauthorized();

			return user;
					}
	}
}

