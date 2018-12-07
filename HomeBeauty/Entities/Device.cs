using System;
using System.Collections.Generic;

namespace HomeBeauty.Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public DateTime ProductionDate { get; set; }
        public string IMEI { get; set; }

        public List<WaterReception> WaterReceptions { get; set; }
        
        public Device()
        {
            WaterReceptions = new List<WaterReception>();
        }
    }
}
