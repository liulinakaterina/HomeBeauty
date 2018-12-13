using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeBeauty.Options
{
    public class AuthOptions
    {
        public const string Issuer = "HomeBeautyAuthServer"; // издатель токена
        public const string Audience = "http://localhost:5000/"; // потребитель токена
        public const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 60;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
