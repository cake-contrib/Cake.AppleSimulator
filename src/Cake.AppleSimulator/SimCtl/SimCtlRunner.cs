using Cake.AppleSimulator.Extensions;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Cake.AppleSimulator.SimCtl
{
    internal sealed class SimCtlRunner : SimCtlTool<SimCtlSettings>
    {
        private readonly ICakeLog _log;

        public SimCtlRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log, SimCtlSettings settings) : base(fileSystem, environment, processRunner, tools, settings)
        {
            _log = log;
        }

        public void BootSimulator(string deviceIdentifier)
        {
            var arguments = CreateArgumentBuilder(Settings).Append("boot").Append(" ").AppendQuoted(deviceIdentifier);
            Run(Settings, arguments);
        }

        public void EraseSimulator(string deviceIdentifier)
        {
            var arguments = CreateArgumentBuilder(Settings).Append("erase").Append(" ").AppendQuoted(deviceIdentifier);
            Run(Settings, arguments);
        }

        public IReadOnlyList<AppleSimulatorDeviceType> ListDeviceTypes()
        {
            var arguments = CreateArgumentBuilder(Settings).Append("list devicetypes").Append(" --json");

            var stdOutput = RunAndRedirectStandardOutput(Settings, arguments);

            return JsonConvert.DeserializeObject<SimCtlListDeviceTypesResponse>(stdOutput).DeviceTypes;
        }

        public IReadOnlyList<AppleSimulatorPair> ListPairs()
        {
            var arguments = CreateArgumentBuilder(Settings).Append("list devices").Append(" --json");

            var stdOutput = RunAndRedirectStandardOutput(Settings, arguments);

            var pairs = JsonConvert.DeserializeObject<SimCtlListPairsResponse>(stdOutput).Pairs;

            return pairs.Select(kvp =>
            {
                kvp.Value.UDID = kvp.Key;
                return kvp.Value;
            }).ToList().AsReadOnly();
        }

        public IReadOnlyList<AppleSimulatorRuntime> ListRuntimes()
        {
            var arguments = CreateArgumentBuilder(Settings).Append("list runtimes").Append(" --json");

            var stdOutput = RunAndRedirectStandardOutput(Settings, arguments);

            return JsonConvert.DeserializeObject<SimCtlListRuntimesResponse>(stdOutput).Runtimes;
        }

        public IReadOnlyList<AppleSimulator> ListSimulators()
        {
            var arguments = CreateArgumentBuilder(Settings).Append("list devices").Append(" --json");

            var stdOutput = RunAndRedirectStandardOutput(Settings, arguments);

            var simulators = JsonConvert.DeserializeObject<SimCtlListDevicesResponse>(stdOutput).Devices;

            return simulators.SelectMany(kvp => kvp.Value.Select(simulator =>
            {
                simulator.Runtime = kvp.Key;
                return simulator;
            })).ToList().AsReadOnly();
        }

        public void ShutdownSimulators()
        {
            foreach (var process in Process.GetProcessesByName("Simulator"))
            {
                //_log.Information("----------");
                //_log.Information(process.ProcessName);
                //_log.Information(process.Id.ToString());
                //_log.Information("----------");

                // don't kill ghosts
                if (process.Id > 0)
                {
                    process.Kill();

                    // rando/made up - needs to be enough to allow process to clean up.
                    Thread.Sleep(500);
                }
            };

            //var arguments = CreateArgumentBuilder(Settings).Append("shutdown").Append(" ").AppendQuoted(deviceIdentifier);

            //    Run(Settings, arguments);
        }
    }
}