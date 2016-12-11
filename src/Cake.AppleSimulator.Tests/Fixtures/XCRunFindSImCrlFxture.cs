using Cake.AppleSimulator.XCRun;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal sealed class XCRunFindSimCtlFixture : XCRunFixture<XCRunSettings>
    {
        public string ToolResult { get; set; }

        protected override void RunTool()
        {
            var runner = new XCRunRunner(FileSystem, Environment, ProcessRunner, Tools, Log, Settings);
            ToolResult = runner.Find("simctl");
        }
    }
}