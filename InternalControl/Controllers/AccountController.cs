using Business.Utilities;
using Business.ViewModels.Account;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternalControl.Controllers
{
    public class AccountController : Controller
    {
        #region Injects

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;

        }

        #endregion




        #region Register

        //Register Operation

        public IActionResult Register()
        {
            return RedirectToAction("Login", "Account");
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LogRegViewModel registerViewModel)
        {
            Random random = new Random();

            if (!ModelState.IsValid) return View(registerViewModel);

            ApplicationUser newUser = new ApplicationUser
            {
                

                UserName = "user"+random.Next(),
                Email = registerViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);


            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = registerViewModel.Email }, Request.Scheme);



                var msgArea =
                    $"<!DOCTYPE html><html><head> <title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /></head><body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"></head> <!-- HIDDEN PREHEADER TEXT --> <div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Montserrat'Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> ToG Net-dən bildiriş!</div> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- LOGO --> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr style=\"border-top-left-radius: 14px; border-top-right-radius: 14px;\"> <td  bgcolor=\"#00bfd8\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 2px 2px 0px 0px; color: white; font-family: 'Londrina Solid'Helvetica, Arial, sans-serif; font-size: 45px; font-weight: 700; letter-spacing: 2px; line-height: 48px;\"> <h1 style=\"font-size: 40px; font-weight:700; margin: w-50;\">Xoş Gəlmisiniz!</h1> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 40px 30px; color: white; font-family:'Montserrat bold' Helvetica, Arial, sans-serif; font-size: 22px; font-weight:600; line-height: 25px;\"> <p >Qeydiyyatdan keçdiyiniz üçün təşəkkür edirik!</p> </td> </tr> <tr> <td bgcolor=\"#00bfd8\" align=\"left\"> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td align=\"center\" style=\"border-radius: 30px;\" bgcolor=\"transparent\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"text-decoration: none;border-radius: 20px;border: 1px solid white;background-color: transparent;color: #FFFFFF;font-size: 18px;font-weight: bold;padding: 12px 45px;letter-spacing: 1px;text-transform: uppercase;transition: transform 80ms ease-in;\">Hesabımı Doğrula</a></td> </tr> </table> </td> </tr> </table> </td> </tr> <!-- COPY --> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 0px 30px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight:550; line-height: 25px;\"> <p style=\"margin: 0;\">Hesabınızı doğrulamaq üçün bu linkdən də istifadə edə bilərsiniz : </p> </td> </tr> <!-- COPY --> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 550; line-height: 25px;\"> <p style=\"margin: 0;\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"color: black;\">{confirmationLink}</a></p> </td> </tr> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 20px 30px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 25px;\"> <p style=\"margin: 0;\">Diqqətiniz üçün təşəkkürlər!</p> </td> </tr> <tr style=\"border-bottom-left-radius: 14px; border-bottom-right-radius: 14px;\"> <td  bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 25px;\"> <p style=\"margin: 0;\">Əlaqə üçün: <a href=\"#\" target=\"_blank\" style=\"color: #000000;\">tognet.helper@gmail.com</a></p> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br> <p style=\"margin: ;\"><a href=\"#\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\"</p> </td> </tr> </table> </td></tr></table></body></html>";

                var subject = "ToG Net - Hesab Doğrulama";

                bool emailResponse = Helper.SendEmail(registerViewModel.Email, msgArea, subject);


                if (emailResponse)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User.ToString());
                    return RedirectToAction("ConfirmedEmail", "Account");
                }
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerViewModel);
            }


            return RedirectToAction("Login", "Account");

        }

        //Register Operation - End

        #endregion


        #region Login

        //Login Operation

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogRegViewModel loginViewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır !");
                return View(loginViewModel);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Zəhmət olmasa emailinizi təsdiqləyin! Təsdiq mesajı emailə göndərilmişdir !");
                return View(loginViewModel);
            }

            var signInResult =
                await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe,
                    true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Zəhmət olmasa bir neçə dəqiqə gözləyin !");
                return View(loginViewModel);
            }


            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır !");
                return View(loginViewModel);
            }


            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Login Operation - End

        #endregion


        #region Logout

        //Logout Operation

        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            await _signInManager.SignOutAsync();

            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Logout Operation - End

        #endregion


        #region Confirmation user email on register

        //Registration Success
        public ActionResult ConfirmedEmail()
        {
            return View();
        }

        //Confirmation Success
        public ActionResult ConfirmationEmail()
        {
            return View();
        }

        //Confirm Email Operation

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmationEmail" : "Error");
        }

        //Confirm Email Operation - End

        #endregion


        #region Forget password and reset password


        //Reset password
        public IActionResult ResetPass(string token, string email)
        {
            if (token == null && email == null)
            {
                ModelState.AddModelError("", "Axtardığınız email ilə bağlanmış hesab yoxdur ! ");
            }

            return View();
        }

        //Reset password operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPass(ResetPassViewModel reset)
        {
            if (!ModelState.IsValid) return View(reset);

            var user = await _userManager.FindByEmailAsync(reset.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email ilə bağlanmış hesab tapılmadı !  Doğru emaili daxil etdiyinizdən əmin olun !");
                return View(user);
            }



            var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            return RedirectToAction("ResetSuccess", "Account");

        }
        //Reset password operation - End

        //Reset password succesful
        public IActionResult ResetSuccess()
        {
            return View();
        }

        //Forget password
        public IActionResult ForgetPass()
        {
            return View();
        }

        //Forget password operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPass(ForgetPassViewModel forget)
        {
            if (!ModelState.IsValid) return View(forget);

            var user = await _userManager.FindByEmailAsync(forget.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email ilə bağlanmış hesab tapılmadı !  Doğru emaili daxil etdiyinizdən əmin olun !");
                return View(user);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string confirmationLink = Url.Action("ResetPass", "Account", new
            {
                email = user.Email,
                token = token
            }, protocol: HttpContext.Request.Scheme);



            var msg =
                    $"<!DOCTYPE html><html><head> <title></title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /></head><body style=\"background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"></head> <!-- HIDDEN PREHEADER TEXT --> <div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Montserrat'Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> ToG Net-dən bildiriş!</div> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- LOGO --> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr style=\"border-top-left-radius: 14px; border-top-right-radius: 14px;\"> <td  bgcolor=\"#00bfd8\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 2px 2px 0px 0px; color: white; font-family: 'Londrina Solid'Helvetica, Arial, sans-serif; font-size: 45px; font-weight: 700; letter-spacing: 2px; line-height: 48px;\"> <h1 style=\"font-size: 40px; font-weight:700; margin: w-50;\">Xoş Gəlmisiniz!</h1> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 40px 30px; color: white; font-family:'Montserrat bold' Helvetica, Arial, sans-serif; font-size: 22px; font-weight:600; line-height: 25px;\"> <p >ToG Net-ə müraciət etdiyiniz üçün təşəkkür edirik. Aşağıdakı keçidə vuraraq şifrəni yeniləyənin siz olduğunuzu təsdiqləyin!!</p> </td> </tr> <tr> <td bgcolor=\"#00bfd8\" align=\"left\"> <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> <tr> <td align=\"center\" style=\"border-radius: 30px;\" bgcolor=\"transparent\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"text-decoration: none;border-radius: 20px;border: 1px solid white;background-color: transparent;color: #FFFFFF;font-size: 18px;font-weight: bold;padding: 12px 45px;letter-spacing: 1px;text-transform: uppercase;transition: transform 80ms ease-in;\">Təsdiqlə</a></td> </tr> </table> </td> </tr> </table> </td> </tr> <!-- COPY --> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 0px 30px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight:550; line-height: 25px;\"> <p style=\"margin: 0;\">Şifrənizi yeniləmək üçün bu linkdən də istifadə edə bilərsiniz : </p> </td> </tr> <!-- COPY --> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 550; line-height: 25px;\"> <p style=\"margin: 0;\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"color: black;\">{confirmationLink}</a></p> </td> </tr> <tr> <td bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 20px 30px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 25px;\"> <p style=\"margin: 0;\">Diqqətiniz üçün təşəkkürlər!</p> </td> </tr> <tr style=\"border-bottom-left-radius: 14px; border-bottom-right-radius: 14px;\"> <td  bgcolor=\"#00bfd8\" align=\"center\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: white; font-family:'Montserrat'Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 25px;\"> <p style=\"margin: 0;\">Əlaqə üçün: <a href=\"#\" target=\"_blank\" style=\"color: #000000;\">tognet.helper@gmail.com</a></p> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr> <td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br> <p style=\"margin: ;\"><a href=\"#\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\"</p> </td> </tr> </table> </td></tr></table></body></html>";

            var subject = "ToG Shopping - Şifrə yeniləmə";

            //SendMailHelper sendEmailHelper = new SendMailHelper();
            //bool emailResponse = SendEmail(forget.Email, msg);
            bool emailResponse = Helper.SendEmail(forget.Email, msg, subject);



            if (emailResponse)
            {
                return RedirectToAction("PassVerification", "Account");
            }

            return View();
        }
        //Forget password operation - End

        //Send email for reset forget account password successful
        public IActionResult PassVerification()
        {
            return View();
        }


        #endregion


        #region External login register

        //Login with Facebook

        public IActionResult FacebookLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        //Login with Facebook - End

        //Login with Google

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        //Login with Google - End




        // Social network login/register operation

        public async Task<IActionResult> SocialMediaResponse(string returnUrl)
        {

            Random random = new Random();

            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Register");
            }
            else
            {
                var result =
                    await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (loginInfo.Principal.HasClaim(scl => scl.Type == ClaimTypes.Email))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                            FullName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name),
                            UserName = "user" + random.Next(),
                            EmailConfirmed = true
                        };
                        var createResult = await _userManager.CreateAsync(user);

                        await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

                        if (createResult.Succeeded)
                        {
                            var identityLogin = await _userManager.AddLoginAsync(user, loginInfo);
                            if (identityLogin.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, true);
                                return Redirect("Login");
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Register");
        }

        // Social network login/register operation - End


        #endregion




        #region for create roles

        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}

        #endregion




    }








}
