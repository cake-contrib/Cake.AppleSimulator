using Cake.AppleSimulator.SimCtl;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing.Fixtures;
using NSubstitute;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal abstract class AppleSimulatorFixture<TSettings> : ToolFixture<TSettings, ToolFixtureResult>
        where TSettings : SimCtlSettings, new()
    {
        protected AppleSimulatorFixture()
            : base("simctl")
        {
            Log = Substitute.For<ICakeLog>();
            ProcessRunner.Process.SetStandardOutput(new string[] { });
        }

        public ICakeLog Log { get; private set; }

        protected override ToolFixtureResult CreateResult(FilePath path, ProcessSettings process)
        {
            return new ToolFixtureResult(path, process);
        }
    }
}