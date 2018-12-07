using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Chemicals
    {
        public int ChemicalsId { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public string Properties { get; set; }

        public List<Allergen> Allergens { get; set; }

        public List<Compound> Compounds { get; set; } 

        public Chemicals()
        {
            Allergens = new List<Allergen>();
            Compounds = new List<Compound>();
        }
    }
}
