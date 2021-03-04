using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RobotLegs.Web.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotLegs.Web.Controllers
{
    public class Accounts : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public Accounts(SignInManager<CognitoUser> signInManager, UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task<IActionResult> SignUp_Post(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);

                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User with this email already exists");
                    return View(model);
                }

                var createdUser = await _userManager.CreateAsync(user, model.Password);

                if (createdUser.Succeeded) 
                    return RedirectToAction("Confirm");
                else
                {
                    foreach (var error in createdUser.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }                    
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Confirm")]
        public async Task<IActionResult> Confirm_Post(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "Unable to find a user with that email address.");
                    return View(model);
                }

                var result = await (_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync(user, model.ConfirmationCode, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login_Post(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Email is not recognised or the password is incorrect");
                }
            }

            return View("Login", model);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            var model = new ResetPasswordModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ResetPassword_Post(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "Unable to find a user with that email address.");
                    return View(model);
                }
                
                await user.ForgotPasswordAsync();

                return RedirectToAction("ConfirmResetPassword");
            }

            return View("ResetPassword", model);
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            var model = new ConfirmResetPasswordModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword_Post(ConfirmResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                
                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "Unable to find a user with that email address.");
                    return View(model);
                }

                await user.ConfirmForgotPasswordAsync(model.ConfirmResetPasswordCode, model.NewPassword);

                return RedirectToAction("Index", "Home");
            }

            return View("ConfirmResetPassword", model);
        }
    }
}
