using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBeauty.Options
{
    public class AuthOptions
    {
        public const string Issuer = "HomeBeautyAuthServer"; // издатель токена
        public const string Audience = "http://localhost:44356/"; // потребитель токена
        public const string Key = "mysupersecret_secretkey!123";
        public const int Lifetime = 60;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
