
using System.Collections.Generic;

namespace Cake.AppleSimulator.UnitTest
{
	// XUnit for Devices = "Tests run: 0 Passed: 0 Failed: 0 Skipped: 0"
	// Nunit for Devices = "Tests run: 2 Passed: 1 Inconclusive: 0 Failed: 1 Ignored: 1
	public sealed class TestResults
	{
		public int Run { get; set; }
		public int Passed { get; set; }
		public int Failed { get; set; }
		public int Skipped { get; set; }
		public int Inconclusive { get; set; }
        public List<string> FailedList { get; set; }
        public List<string> PassedList { get; set; }
        public List<string> SkippedList { get; set; }
    }
}
