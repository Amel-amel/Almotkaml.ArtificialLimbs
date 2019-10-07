using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository 
    {

        public DeviceRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }

        public bool DeviceExist(string DeviceName = null)
        {
            return _context.Devices.Any(d => d.ArabicName.ToString() == DeviceName);
        }
    }
}