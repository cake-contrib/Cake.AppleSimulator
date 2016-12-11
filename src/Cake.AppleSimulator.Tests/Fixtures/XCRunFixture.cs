using Cake.AppleSimulator.XCRun;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing.Fixtures;
using NSubstitute;

namespace Cake.AppleSimulator.Tests.Fixtures
{
    internal abstract class XCRunFixture<TSettings> : ToolFixture<TSettings, ToolFixtureResult>
		where TSettings : XCRunSettings, new()
	{
		protected XCRunFixture() : base("xcrun")
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