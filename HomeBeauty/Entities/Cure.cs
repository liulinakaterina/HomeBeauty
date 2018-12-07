using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Cure
    {
        public int CureId { get; set; }
        public double DosageValue { get; set; }
        public string DosageType { get; set; }
        
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

        public int CareProductId { get; set; }
        public CareProduct CareProduct { get; set; }
    }
}
