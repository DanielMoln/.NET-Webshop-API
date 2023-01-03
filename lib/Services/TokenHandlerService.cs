using Microsoft.IdentityModel.Tokens;

namespace WebshopAPI.lib.Services
{
    public class TokenHandlerService
    {
        public static AccessToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.UserID),
                new Claim(ClaimTypes.Name, user.Name)
            };
            using (SQL sql = new SQL())
            {
                sql.UserRoles.Where(a => a.UserID == user.UserID)
                    .Select(a => a.RoleID)
                    .ToList()
                    .ForEach(a =>
                    {
                        claims.Add(new Claim(ClaimTypes.Role, a.ToString()));
                    });
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = Program.Audience,
                Issuer = Program.Issuer,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Program.JWT_KEY),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessToken()
            {
                Token = tokenHandler.WriteToken(token),
                ExpireAt = token.ValidTo
            };
        }   
    }
}
