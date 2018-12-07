using System.Collections.Generic;

namespace HomeBeauty.Models
{
    public class Patient
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<AllergenModel> Allergens { get; set; }

        public int DoctorUserId;
        public DoctorModel Doctor;
    }
}
