using System;

namespace HomeBeauty.Models
{
    public class UserModel : AuthModel
    {
        public int UserId;
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
