using System;

namespace HomeBeauty.Entities
{
    public class WaterReception
    {
        public int WaterReceptionId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public DateTime Time { get; set; }
        public string Information { get; set; }
    }
}
