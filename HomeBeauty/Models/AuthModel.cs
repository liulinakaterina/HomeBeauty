using System.ComponentModel.DataAnnotations;

namespace HomeBeauty.Models
{
    public class AuthModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe;
    }
}
