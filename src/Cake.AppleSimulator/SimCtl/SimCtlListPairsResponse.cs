using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.AppleSimulator.SimCtl
{
    internal sealed class SimCtlListPairsResponse
    {
        public Dictionary<string, AppleSimulatorPair> Pairs { get; set; }
    }
}