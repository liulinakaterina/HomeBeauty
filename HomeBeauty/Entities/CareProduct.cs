using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class CareProduct
    {
        public int CareProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Compound> Compounds { get; set; }

        public List<Cure> Cures { get; set; }

        public CareProduct()
        {
            Compounds = new List<Compound>();
            Cures = new List<Cure>();
        }
    }
}
