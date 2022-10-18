using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Etkinlik.Core.DTOs;
using Etkinlik.MVC.Models;

namespace Etkinlik.MVC.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto LoginDto)
        {
            if (ModelState.IsValid)
            {
                //password passwordagain check eklenecekti
                var httpclient = new HttpClient(); 
                StringContent context = new StringContent(JsonConvert.SerializeObject(LoginDto), Encoding.UTF8, "application/json");

                var response = await httpclient.PostAsync("https://localhost:7256/api/user/login", context);             


                
                var token = JsonConvert.DeserializeObject<TokenModel>(response.Content.ReadAsStringAsync().Result);

                if (response.IsSuccessStatusCode)
                {

                    HttpContext.Session.SetString("JWToken", token.data);

                    var handler = new JwtSecurityTokenHandler();

                    var jwtSecurityToken = handler.ReadJwtToken(token.data);



                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
                        new Claim(ClaimTypes.Email,  jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value ),
                        new Claim(ClaimTypes.Name,  jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value ),
                        new Claim("access_token",  token.data ),
                        new Claim(ClaimTypes.Role, "User")
                    };

                    var roleClaims = jwtSecurityToken.Claims.Where(x => x.Type == ClaimTypes.Role);
                    claims.AddRange(roleClaims);

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
               
                var ErrMsg = JsonConvert.DeserializeObject<ErrorData>(response.Content.ReadAsStringAsync().Result);
                foreach (var error in ErrMsg.errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            
            return View(ModelState);
        }
    }
}
