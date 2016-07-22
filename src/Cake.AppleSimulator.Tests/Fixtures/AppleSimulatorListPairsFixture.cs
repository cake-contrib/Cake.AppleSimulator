using Cake.AppleSimulator.SimCtl;
using System.Collections.Generic;
using System.IO;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal sealed class AppleSimulatorListPairsFixture : AppleSimulatorFixture<SimCtlSettings>
    {
        public AppleSimulatorListPairsFixture()
        {
            var standardOutput = File.ReadAllLines(@"Fixtures\SimCtlListPairs.json");
            ProcessRunner.Process.SetStandardOutput(standardOutput);
        }

        public IReadOnlyList<AppleSimulatorPair> ToolResult { get; set; }

        protected override void RunTool()
        {
            var runner = new SimCtlRunner(FileSystem, Environment, ProcessRunner, Tools, Log, Settings);
            ToolResult = runner.ListPairs();
        }
    }
}