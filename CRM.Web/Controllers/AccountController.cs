using CRM.Application.Services.Interface;
using CRM.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Login

        [Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["WarningMessage"] = "اطلاعات وارد شده معتبر نمی باشد";
                return View(loginUserViewModel);
            }

            var result = await _userService.LoginUser(loginUserViewModel);

            switch (result)
            {
                case LoginUserResult.Success:
                    var loginUser = _userService.GetUserByUserName(loginUserViewModel.UserName);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginUser.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = loginUserViewModel.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    TempData["SuccessMessage"] = "عملیات با موفقیت انجام شد";

                    return RedirectToAction("Index", "Home");

                case LoginUserResult.NotFound:
                    TempData["SuccessMessage"] = "اکانت شما با این مشخصات یافت نشد";
                    break;

                case LoginUserResult.PasswordNotCorrect:
                    TempData["SuccessMessage"] = "رمز عبور شما درست نمی باشد";
                    break;
            }

            return View(loginUserViewModel);
        }

        #endregion

        #region Logout

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData["SuccessMessage"] = "خروج با موفقیت انجام شد";
            return RedirectToAction("Login");
        }

        #endregion

    }
}
