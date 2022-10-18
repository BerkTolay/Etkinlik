using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Etkinlik.API.Helper
{
    public class TokenHandler:ITokenHandler
    {       
        
        public string CreateAccessToken(List<string> userRoles)
        {
            Token token = new Token();
            //Security Key simetriği
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("RTE"));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


            //ayarlar

            JwtSecurityToken jwtSecurityToken = new(
                audience: "localhost",
                issuer: "localhost",
                expires: DateTime.UtcNow.AddDays(50),
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                ); 

            //token olşturucu
            JwtSecurityTokenHandler tokenHandler = new();
            string bisi = tokenHandler.WriteToken(jwtSecurityToken);


            return bisi;
        }

        public Token CreateAccessToken()
        {
            throw new NotImplementedException();
        }
    }
}
