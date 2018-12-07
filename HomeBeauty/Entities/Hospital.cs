using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Hospital
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public List<Doctor> Doctors { get; set; }

        public Hospital()
        {
            Doctors = new List<Doctor>();
        }
    }
}
