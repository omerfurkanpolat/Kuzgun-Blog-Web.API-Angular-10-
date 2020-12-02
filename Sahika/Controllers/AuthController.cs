using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Sahika.App_Start;
using Sahika.Dtos;
using Sahika.Helper.Filters;
using Sahika.Helper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using System.Web.Routing;
using static Sahika.App_Start.IdentityConfig;
using static Sahika.Models.IdentityModels;

namespace Sahika.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/{auth}")]
    public class AuthController : ApiController
    {
        public AuthController()
        {

        }
        public AuthController( IEmailService emailService)
        {
            
            _emailService = emailService;
           
        }

        private readonly IMapper _mapper = AutoMapperConfing._mapper;
        private IEmailService _emailService;

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserForRegisterDTO model)
        {

            if (ModelState.IsValid)
            {
                var userName =await UserManager.FindByNameAsync(model.UserName);
                if(userName!=null)
                {
                    var email = await UserManager.FindByEmailAsync(model.Email);
                    if (email != null)
                    {
                        return BadRequest("Kullanıcı adı ve Email daha önce kullanılmış");
                    }
                    return BadRequest("Kullanıcı adı daha önce kullamış");
                }
                var emailAddres = await UserManager.FindByEmailAsync(model.Email);
                if (emailAddres != null)
                {
                    return BadRequest("Email adresi daha önce kullanılmış");
                }

                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email, IsDeleted=false };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!RoleManager.RoleExistsAsync("user").Result)
                    {
                        var role = new ApplicationRole("user");
                        var roleResult = await RoleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest("rol oluşturulamadı");
                        }


                    }
                    var addRole = await UserManager.AddToRoleAsync(user.Id, "user");
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var url = "http://localhost:4200/confirmEmail?";
                    var callbackUrl = $"{url}" + $"UserId={user.Id}&" + $"Code={code}";
                    _emailService.SendMail(user.UserName, user.Email, "Hesap onaylama maili", $"Hesabınızı onaylamak için lütfen linkle tıklayın: {callbackUrl} ");


                    return Ok("Hesabınıza gelen maili onaylayınız");
                }
                
                return BadRequest();
                                
            }
                       
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(UserForLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await UserManager.FindByNameAsync(model.UserName);
            
            
            if(user==null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            if (user.IsDeleted == true)
            {
                return BadRequest("Hesap kullanıma kapatılmıştır");
               throw new HttpResponseException(HttpStatusCode.Unauthorized);

            }
            if(user.EmailConfirmed==false)
            {
                return BadRequest("Email hesabınızı onaylamadan giriş yapamazsınız");
            }
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);
                       

            switch (result)
            {
                case SignInStatus.Success:
                    user.LastActive = DateTime.Now;
                     UserManager.Update(user);
                    var userRole = UserManager.GetRoles(user.Id).First();
                    var createdToken = Get(user, userRole);

                    return Ok(new { token = createdToken });
                case SignInStatus.LockedOut:
                    return BadRequest();
                case SignInStatus.RequiresVerification:
                    return BadRequest();
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return BadRequest("Şifreniz hatalı");
                    
            }
            
        }


        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(UserForConfirmEmailDTO model)
        {
            var userId = model.UserId;
            var c = model.Code.Replace(" ", "+");            
            var code = c;
            if (userId > 0 || code == null)
            {
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                if(result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest("Email adresiniz onaylanamadı");
            }
            return BadRequest("Bir hata oluştu");
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(UserForForgotPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null )
                {
                    
                    return BadRequest("Kullanıcı Bulunamadı");
                }
                if(!(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return BadRequest("Email Adresinizi Onaylayın");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var url = "http://localhost:4200/resetpassword?";
                var callbackUrl = $"{url}" + $"UserId={user.Id}&" + $"Code={code}";
                _emailService.SendMail(user.UserName, user.Email, "Parola Sıfırlama Maili", $"Parolanızı sıfırlamak için lütfen linkle tıklayın {callbackUrl} ");

                return Ok();
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("resetPassword")]
        public async Task<IHttpActionResult> ResetPassword(  UserForResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                
                return BadRequest("Kullanıcı Bulunamadı");
            }

            var revisedCode = model.Code.Replace(" ", "+");
            var result = await UserManager.ResetPasswordAsync(user.Id, revisedCode, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            
            return BadRequest("Bir Hata Oluştu");
        }

        [JwtAuthentication]
        [HttpPut]
        [Route("changePassword/{id}")]
        public async Task<IHttpActionResult> ChangePassword(UserForChangePasswordDTO model, int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var result = await UserManager.ChangePasswordAsync(id, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                return Ok();
            }
            return BadRequest("Mecvut şifrenizi hatalı girdiniz");
        }

        [JwtAuthentication]
        [HttpGet]
        [Route("changeEmail/{id}")]
        public async Task<IHttpActionResult> ChangeEmail(int id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var result = _mapper.Map<UserForChangeEmailDTO>(user);
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            return Ok(result);
        }
        [JwtAuthentication]
        [HttpPut]
        [Route("changeEmail/{id}")]
        public IHttpActionResult ChangeEmail(UserForChangeEmailDTO model, int id)
        {

            var user = UserManager.FindById(id);
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Email Adresi Değiştirilemedi");
            }
            user.Email = model.Email;
            UserManager.Update(user);
            return Ok();

        }
        [JwtAuthentication]
        [HttpPut]
        [Route("changeProfilePicture/{id}")]
        public IHttpActionResult ChangeProfilPicture(UserForChangeProfilePictureDTO model, int id)
        {
            var user = UserManager.FindById(id);
            if(user==null|| !ModelState.IsValid)
            {
                return BadRequest("Profil Resmi Değiştirilemedi");
            }
            user.ImageUrl = model.ImageUrl;
            UserManager.Update(user);
            return Ok();
        }

        public string Get(ApplicationUser user, string userRole)
        {

            return JwtManager.GenerateToken(user, userRole);
            

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }




    }
}
