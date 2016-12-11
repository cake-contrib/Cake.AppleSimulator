using Cake.AppleSimulator.SimCtl;
using System.Collections.Generic;
using System.IO;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal sealed class AppleSimulatorListRuntimesFixture : AppleSimulatorFixture<SimCtlSettings>
    {
        public AppleSimulatorListRuntimesFixture()
        {
            var standardOutput = File.ReadAllLines(Path.Combine("Fixtures", "SimCtlListRuntimes.json"));
            ProcessRunner.Process.SetStandardOutput(standardOutput);
        }

        public IReadOnlyList<AppleSimulatorRuntime> ToolResult { get; set; }

        protected override void RunTool()
        {
            var runner = new SimCtlRunner(FileSystem, Environment, ProcessRunner, Tools, Log, Settings);
            ToolResult = runner.ListRuntimes();
        }
    }
}