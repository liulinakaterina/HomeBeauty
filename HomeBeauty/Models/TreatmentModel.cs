using System;
using System.Collections.Generic;

namespace HomeBeauty.Models
{
    public class TreatmentModel
    {
        public int TreatmentId { get; set; }
        public DateTime StartDate { get; set; }
        public List<CureModel> Cures { get; set; }

        public TreatmentModel()
        {
            this.Cures = new List<CureModel>();
        }
    }
}
