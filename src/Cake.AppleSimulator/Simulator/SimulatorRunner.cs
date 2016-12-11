using System;
using Cake.AppleSimulator.SimCtl;
using Cake.AppleSimulator.XCRun;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppleSimulator.Simulator
{
    internal sealed class SimulatorRunner : SimulatorTool<SimulatorSettings>
    {
        private readonly ICakeLog _log;
        private readonly SimCtlRunner _simCtlRunner;
        private readonly XCRunRunner _xcrunRunner;

        public SimulatorRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log, SimulatorSettings settings) : base(fileSystem, environment, processRunner, tools, settings)
        {
            _log = log;
            _simCtlRunner = new SimCtlRunner(fileSystem, environment, processRunner, tools, log, new SimCtlSettings());
            _xcrunRunner = new XCRunRunner(fileSystem, environment, processRunner, tools, log, new XCRunSettings());
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

            const string SimulatorAppSubPath = "Contents/Developer/Applications/Simulator.app";
            var toolPath = _xcrunRunner.Find("simctl");
            var appIndex = toolPath.IndexOf(".app/", StringComparison.Ordinal);
            var app = toolPath.Remove(appIndex + 5) + SimulatorAppSubPath;
            var arguments =
                CreateArgumentBuilder(Settings)
                    .Append("-Fgn")
                    .Append(app)
                    .Append("--args")
                    .Append("-CurrentDeviceUDID")
                    .AppendQuoted(deviceIdentifier);

            Run(Settings, arguments);
        }
    }
}