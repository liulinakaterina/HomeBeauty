namespace HomeBeauty.Models
{
    public class ExtentendedTreatment : TreatmentModel
    {
        public int DoctorId { get; set; }
        public int IllnessId { get; set; }
    }
}
