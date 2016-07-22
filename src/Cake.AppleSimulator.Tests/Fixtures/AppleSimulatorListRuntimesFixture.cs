using Cake.AppleSimulator.SimCtl;
using System.Collections.Generic;
using System.IO;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal sealed class AppleSimulatorListSimulatorsFixture : AppleSimulatorFixture<SimCtlSettings>
    {
        public AppleSimulatorListSimulatorsFixture()
        {
            var standardOutput = File.ReadAllLines(@"Fixtures\SimCtlListDevices.json");
            ProcessRunner.Process.SetStandardOutput(standardOutput);
        }

        public IReadOnlyList<AppleSimulator> ToolResult { get; set; }

        protected override void RunTool()
        {
            var runner = new SimCtlRunner(FileSystem, Environment, ProcessRunner, Tools, Log, Settings);
            ToolResult = runner.ListSimulators();
        }
    }
}