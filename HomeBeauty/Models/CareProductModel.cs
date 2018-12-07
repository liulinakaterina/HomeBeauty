using System.Collections.Generic;

namespace HomeBeauty.Models
{
    public class CareProductModel
    {
        public int CareProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<CompoundModel> Compounds { get; set; }

        public CareProductModel()
        {
            this.Compounds = new List<CompoundModel>();
        }
    }
}
