using Etkinlik.Core.DTOs;
using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Models;
using Etkinlik.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Etkinlik.Service.Services
{
    public class AuthService:IAuthService
    {
        protected UserManager<AppUser> userManager { get; }
        protected IUnitOfWork unitOfWork { get; }

        public AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }        
              

        private IEnumerable<Claim> GetClaims(AppUser user, List<string> userRoles, string audience)
        {
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Aud, audience),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };            
            userList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));          

            return userList;
        }

        public async Task<CustomResponseDto<string>> LoginSync(LoginDto loginDto)
        {
            var errors = new List<string>();
            if (loginDto == null)
            {

                errors.Add("Kullacı bilgisi bulunamadı");
                return CustomResponseDto<string>.Fail(404, errors);
            }

            //kullanıcı var mı kontrolü
            var user = userManager.FindByEmailAsync(loginDto.Email).Result;

            if (user == null)
            {
                errors.Add("Email adresi veya şifre hatalı");
               
                return CustomResponseDto<string>.Fail(400, errors);
            }

            //kullanıcı adı şifre kontrolü
            if (!userManager.CheckPasswordAsync(user, loginDto.Password).Result)
            {
                errors.Add("Email adresi veya şifre hatalı");
                return CustomResponseDto<string>.Fail(400, errors);
            }
            var userRoles = (userManager.GetRolesAsync(user).Result).ToList();
            string token = CreateToken(user, userRoles);           
                     

            //return token;
            return CustomResponseDto<string>.Success(200,token);
        }

        private string CreateToken(AppUser user, List<string> userRoles)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddDays(50);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyforapp12"));
            
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
               issuer: "localhost",
               audience: "localhost",
               expires: DateTime.Now.AddDays(50),
               notBefore: DateTime.UtcNow,
               claims: GetClaims(user, userRoles, "localhost"),
               signingCredentials: signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            

            return token;
        }


        /// <summary>
        /// şirktler için
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<CustomResponseDto<string>> SingUp(Company company)
        {     
            string token = CreateTokenForSellers(company);
            
            return CustomResponseDto<string>.Success(200, token);
        }

        private string CreateTokenForSellers(Company company)
        {
            
           
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyforapp12"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
               issuer: "localhost",
               audience: "localhost",
               expires: DateTime.Now.AddDays(50),
               notBefore: DateTime.UtcNow,
               claims: GetClaimsForSellers(company, "localhost"),
               signingCredentials: signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);



            return token;
        }

        private IEnumerable<Claim> GetClaimsForSellers(Company company, string audience)
        {
            var userList = new List<Claim> {
            new Claim(ClaimTypes.Name,company.Name),
            new Claim(JwtRegisteredClaimNames.Aud, audience),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,"Company")
            };

            return userList;
        }


    }
}
