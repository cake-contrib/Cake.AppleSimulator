using System.Collections.Generic;

namespace Cake.AppleSimulator.SimCtl
{
    internal sealed class SimCtlListDevicesResponse
    {
        /// <summary>
        /// ["iPhone 6s"].Devices[0..12].Name
        /// </summary>
        public IDictionary<string, IEnumerable<AppleSimulator>> Devices { get; set; }
    }
}