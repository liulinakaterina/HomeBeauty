using System;

namespace HomeBeauty.Models
{
    public class WaterReceptionModel
    {
        public int WaterReceptionId { get; set; }

        public int UserId { get; set; }

        public int DeviceId { get; set; }

        public DateTime Time { get; set; }
        public string Information { get; set; }
    }
}
