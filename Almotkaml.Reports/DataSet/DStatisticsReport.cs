using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almotkaml.Reports
{
    public class DStatisticsReport
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public int AboveAgeM { get; set; }//----فوق العمر المحدد(Male) 
        public int AboveAgeF { get; set; }//----فوق العمر المحدد(FeMale)
        public int UnderAgeM { get; set; }//----تحت العمر المحدد(Male)
        public int UnderAgeF { get; set; }//----تحت العمر المحدد(FeMale)
    }
}
