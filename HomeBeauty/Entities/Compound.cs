using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Compound
    {
        public int CompoundId { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }

        public int ChemicalId { get; set; }
        public Chemicals Chemicals { get; set; }

        public int CareProductId { get; set; }
        public CareProduct CareProduct { get; set; }
    }
}
