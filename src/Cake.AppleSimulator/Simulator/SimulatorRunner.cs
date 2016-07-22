using Cake.AppleSimulator.SimCtl;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;

namespace Cake.AppleSimulator.Simulator
{
    internal sealed class SimulatorRunner : SimulatorTool<SimulatorSettings>
    {
        private readonly ICakeLog _log;
        private readonly SimCtlRunner _simCtlRunner;

        public SimulatorRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log, SimulatorSettings settings) : base(fileSystem, environment, processRunner, tools, settings)
        {
            _log = log;
            _simCtlRunner = new SimCtlRunner(fileSystem, environment, processRunner, tools, log, new SimCtlSettings());
        }

        public void LaunchSimulator(string deviceIdentifier)
        {
            try
            {
                // Launching the same device twice does not work and results in Simulator.app hanging.
                // removed until way to shutdown just this simulator, not all is simulators implemented.
                //_simCtlRunner.ShutdownSimulators();
            }
            catch (Exception)
            {
                // ignore
            }

            var arguments =
                CreateArgumentBuilder(Settings)
                    .Append("-Fgn")
                    .Append("/Applications/Xcode.app/Contents/Developer/Applications/Simulator.app")
                    .Append("--args")
                    .AppendQuoted(deviceIdentifier);

            Run(Settings, arguments);
        }
    }
}