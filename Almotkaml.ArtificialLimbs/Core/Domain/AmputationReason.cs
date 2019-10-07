using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Domain
{
    public class AmputationReason
    {
        private AmputationReason() { }

        public int AmputationReasonID { get; private set; }
        public string Reason { get; private set; }

        public ICollection<AmputationStatues> AmputationStatues { get; } = new HashSet<AmputationStatues>();

        public void Modify(string _reason)
        {
            Check.NotEmpty(Reason, nameof(_reason));

            Reason =_reason;
        }
        public static AmputationReason New(string _reason)
        {

            Check.NotEmpty(_reason, nameof(_reason));

            var reason = new AmputationReason()
            {
                Reason = _reason,
            };

            return reason;
        }

      
    }
}