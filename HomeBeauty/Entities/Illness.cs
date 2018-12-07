using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBeauty.Entities
{
    public class Illness
    {
        public int IllnessId { get; set; }
        public string Name { get; set; }
        public string Symptoms { get; set; }
        public bool IsCured { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Treatment> Treatments { get; set; }

        public Illness()
        {
            Treatments = new List<Treatment>();
        }
    }
}
