using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public DateTime StartDate { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int IllnessId { get; set; }
        public Illness Illness { get; set; }

        public List<Cure> Cures { get; set; }

        public Treatment()
        {
            Cures = new List<Cure>();
        }
    }
}
