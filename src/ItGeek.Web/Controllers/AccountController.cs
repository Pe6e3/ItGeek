﻿using ItGeek.BLL;
using ItGeek.DAL.Entities;
using ItGeek.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ItGeek.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UnitOfWork _uow;

        public AccountController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _uow.UserRepository.ValidateLoginPasswordAsync(loginViewModel.Email, loginViewModel.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Нет такого пользователя");
            }
            return View(loginViewModel);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
    };

            if (user.Role != null) // Проверка наличия роли у пользователя
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName.ToString()));
            }

            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                }
            );
        }



        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User? user = await _uow.UserRepository.GetByEmailAsync(registerViewModel.Email);
                if (user == null)
                {
                    User newuser = new User()
                    {
                        Email = registerViewModel.Email,
                        Password = _uow.UserRepository.PasswordHash(registerViewModel.Password),
                        RoleId = await _uow.RoleRepository.GetBasicAsync()
                    };
                    await _uow.UserRepository.InsertAsync(newuser);

                    await Authenticate(newuser); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
            }
            return View(registerViewModel);
        }
        
    }
}
