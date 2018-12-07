using System.Collections.Generic;

namespace HomeBeauty.Entities
{
    public class MedicalCard
    {
        public int MedicalCardId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Allergy Allergy { get; set; }

        public List<Illness> Illnesses { get; set; }

        public MedicalCard()
        {
            Illnesses = new List<Illness>();
        }
    }
}
