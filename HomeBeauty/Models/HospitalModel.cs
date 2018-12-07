using System.Collections.Generic;

namespace HomeBeauty.Models
{
    public class HospitalModel
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<DoctorModel> Doctors { get; set; }
    }
}
