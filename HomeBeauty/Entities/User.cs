using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Doctor> Doctors { get; set; } 

        public List<Allergen> Allergens { get; set; }

        public List<Illness> Illnesses { get; set; }

        public List<WaterReception> WaterReceptions { get; set; } 

        public User()
        {
            Allergens = new List<Allergen>();
            Illnesses = new List<Illness>();
            WaterReceptions = new List<WaterReception>();
            Doctors = new List<Doctor>();
        }
    }
}
