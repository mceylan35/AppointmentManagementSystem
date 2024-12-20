﻿using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.Features.Commands.Users.CreateUser;
using AppointmentManagementSystem.Infrastructure.Identity.Services.Abstract; 
using AppointmentManagementSystem.WebApp.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentManagementSystem.WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResult>> Login([FromBody] LoginViewModel request, string returnUrl = "/")
        {
            var result = await _identityService.AuthenticateAsync(request.Username, request.Password);


            if (result.Success)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = returnUrl
                     
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(result.Claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                    authProperties);

                
                 
            }
            return Ok(result);
        }

        [Authorize] 
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             
            
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            Response.Headers["Pragma"] = "no-cache";
             

            return RedirectToAction("Index", "Home");
        }

       
        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}