namespace HomeBeauty.Models
{
    public class CompoundModel
    {
        public int CompoundId { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }

        public int ChemicalId { get; set; }
    }
}
