using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Qualification { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Treatment> Treatments { get; set; }

        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public Doctor()
        {
            Treatments = new List<Treatment>();
        }
    }
}
