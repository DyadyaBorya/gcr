using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GCR.Web.Models;
using GCR.Core;
using GCR.Core.Services;
using GCR.Core.Security;

namespace GCR.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private IUserService userService;

        public AccountController(IUserService service)
        {
            userService = service;
        }
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && userService.LoginLocal(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            userService.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    userService.CreateLocalAccount(model.UserName, model.Password);
                    userService.LoginLocal(model.UserName, model.Password, false);
                    return RedirectToAction("Index", "Home");
                }
                catch (UserCreationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            bool disassociate = userService.Disassociate(provider, providerUserId);
            return RedirectToAction("Manage", new { Message = disassociate ? "The external login was removed." : null });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(string message)
        {
            ViewBag.StatusMessage = message;
            ViewBag.HasLocalPassword = userService.HasLocalAccount(CurrentUser.Identity.UserId);
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = userService.HasLocalAccount(CurrentUser.Identity.UserId);
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = userService.ChangeLocalPassword(CurrentUser.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = "Your password has been changed." });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        userService.CreateLocalAccount(CurrentUser.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = "Your password has been set." });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, GetExternalCallbackUrl(returnUrl), userService);
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = userService.GetOAuthResultFromRequest(GetExternalCallbackUrl(returnUrl));
            if (!result.IsValid)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (userService.LoginOAuth(result.Provider, result.ProviderUserId))
            {
                return RedirectToLocal(returnUrl);
            }

            if (CurrentUser.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                userService.UpdateOAuthAccount(CurrentUser.Identity.Name, result.Provider, result.ProviderUserId);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                ViewBag.ProviderDisplayName = result.ProviderDisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = result.EncryptedLoginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            var result = userService.GetOAuthResult(model.ExternalLoginData);
            if (ModelState.IsValid)
            {
                if (!userService.UsernameExists(model.UserName))
                {
                    userService.CreateOAuthAccount(model.UserName, result.Provider, result.ProviderUserId);
                    userService.LoginOAuth(result.Provider, result.ProviderUserId);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                } 
            }

            ViewBag.ProviderDisplayName = result.ProviderDisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", userService.GetOAuthProviders());
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            var accounts = userService.GetOAuthAccountsForUser(CurrentUser.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (var account in accounts)
            {
                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.ProviderName,
                    ProviderDisplayName = account.ProviderDisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || userService.HasLocalAccount(CurrentUser.Identity.UserId);
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private string GetExternalCallbackUrl(string internalReturnUrl)
        {
            return Url.Action("ExternalLoginCallback", new { ReturnUrl = internalReturnUrl });
        }

        internal class ExternalLoginResult : ActionResult
        {
            private IUserService userService;
            
            public ExternalLoginResult(string provider, string returnUrl, IUserService service)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
                userService = service;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                userService.RequestAuthentication(Provider, ReturnUrl);
            }
        }
        #endregion
    }
}
