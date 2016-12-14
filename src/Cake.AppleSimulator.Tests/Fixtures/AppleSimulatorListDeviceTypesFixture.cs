using Cake.AppleSimulator.SimCtl;
using System.Collections.Generic;
using System.IO;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal sealed class AppleSimulatorListDeviceTypesFixture : AppleSimulatorFixture<SimCtlSettings>
    {
        public AppleSimulatorListDeviceTypesFixture()
        {
            var standardOutput = File.ReadAllLines(Path.Combine("Fixtures", "SimCtlListDeviceTypes.json"));
            ProcessRunner.Process.SetStandardOutput(standardOutput);
        }

        public IReadOnlyList<AppleSimulatorDeviceType> ToolResult { get; set; }

        protected override void RunTool()
        {
            var runner = new SimCtlRunner(FileSystem, Environment, ProcessRunner, Tools, Log, Settings);
            ToolResult = runner.ListDeviceTypes();
        }
    }
}