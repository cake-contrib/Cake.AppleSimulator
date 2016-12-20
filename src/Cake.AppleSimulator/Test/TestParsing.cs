using System;
using System.Linq;
using System.Text.RegularExpressions;
using Cake.Core.IO;

namespace Cake.AppleSimulator.UnitTest
{
    public static class TestParsing
    {
        public static void TestResultsFromStdOut(IProcess process, TestResults testResults)
        {
            foreach (var line in process.GetStandardOutput().Reverse())
            {
                // Unit for Devices = "Tests run: 0 Passed: 0 Failed: 0 Skipped: 0"
                // NUnit for Devices = "Tests run: 2 Passed: 1 Inconclusive: 0 Failed: 1 Ignored: 1
                if (line.Contains("Tests run:"))
                {
                    var testLine = line.Substring(line.IndexOf("Tests run:", StringComparison.Ordinal));
                    var testArray = Regex.Split(testLine, @"\D+").Where(s => s != string.Empty).ToArray();
                    testResults.Run = int.Parse(testArray[0]);
                    testResults.Passed = int.Parse(testArray[1]);
                    if (testArray.Length == 4)
                    {
                        testResults.Failed = int.Parse(testArray[2]);
                        testResults.Skipped = int.Parse(testArray[3]);
                    }
                    else
                    {
                        testResults.Inconclusive = int.Parse(testArray[2]);
                        testResults.Failed = int.Parse(testArray[3]);
                        testResults.Skipped = int.Parse(testArray[4]);
                    }
                    break;
                }
            }
        }
    }
}
