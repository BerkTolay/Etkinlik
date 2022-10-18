using Etkinlik.API.Helper;
using Etkinlik.API.ViewModels;
using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;
using Etkinlik.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected UserManager<AppUser> userManager { get; }
        protected SignInManager<AppUser> signInManager { get; }
        protected readonly ITokenHandler tokenHandler;
        protected IAuthService authService;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IAuthService authService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenHandler = tokenHandler;
            this.authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            
                AppUser user = new AppUser();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {                   
                    return CreateActionResult(CustomResponseDto<UserViewModel>.Success(201, userViewModel));
                }
                
              return CreateActionResult(CustomResponseDto<UserViewModel>.Fail(400, ""));
                               
            
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(LoginDto loginViewModel)
        {            
            var response = await authService.LoginSync(loginViewModel);
            return CreateActionResult(response);           
        }
               

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response )
        {
            if (response.StatusCode == 204)//204 no content

                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
        [NonAction]
        public IActionResult TokenResponse(Token token)
        {
            return (IActionResult)token;
        }
    }
}
