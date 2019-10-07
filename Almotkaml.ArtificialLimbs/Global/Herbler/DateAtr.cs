using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public class DateAtr
    {
        public Category Notify { get; private set; }
        public DateTime BeginOfYear => new DateTime(Year, 1, 1);
        public DateTime BeginOfMonth => new DateTime(Year, DateTime.Now.Month, 1);
        public int Year { get; private set; }
        public DateTime NowDate => new DateTime(Year, DateTime.Now.Month, DateTime.Now.Day);

        public DateTime EndOfYear => new DateTime(Year, 12, 31);
    }
}