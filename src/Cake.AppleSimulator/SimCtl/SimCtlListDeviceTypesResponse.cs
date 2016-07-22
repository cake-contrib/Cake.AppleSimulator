using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.AppleSimulator.SimCtl
{
    internal sealed class SimCtlListDeviceTypesResponse
    {
        public IReadOnlyList<AppleSimulatorDeviceType> DeviceTypes { get; set; }
    }
}