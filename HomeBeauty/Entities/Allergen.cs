using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Allergen
    {
        public int AllergenId { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ChemicalId { get; set; }
        public Chemicals Chemicals { get; set; }
    }
}
