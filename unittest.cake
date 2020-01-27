using System.Threading;

//////////////////////////////////////////////////////////////////////
// ADDINS
//////////////////////////////////////////////////////////////////////
#addin "Cake.FileHelpers"
#addin "Cake.AppleSimulator"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
if (string.IsNullOrWhiteSpace(target))
{
    target = "Default";
}
//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Build configuration
//(Context.Environment.Platform.Family == PlatformFamily.OSX); // Still broken
var isRunningOnMacOS = IsRunningOnUnix(); 
var isRunningOnUnix = IsRunningOnUnix();
var isRunningOnWindows = IsRunningOnWindows();
var solution = "./DeviceTests/DeviceTests.sln";
var buildConfiguration = "Release";
var buildTarget = "Build";

// Macros
Action Abort = () => { throw new Exception("A non-recoverable fatal error occurred."); };
Action TestFailuresAbort = () => { throw new Exception("Testing revealed failed unit tests"); };
Action NonMacOSAbort = () => { throw new Exception("Running on platforms other macOS is not supported."); };
Action<string, string, string> buildThisApp = (p,c,t) =>
{
    Information(string.Format($"{t}|{c}|{p}"));
    if (isRunningOnMacOS)
    {
        var settings = new XBuildSettings()
            .WithProperty("SolutionDir", new string[] { @"./" })
            .WithProperty("OutputPath", new string[] { @"../../artifacts/" })
            .SetConfiguration(c)
            .SetVerbosity(Verbosity.Quiet)
            .WithTarget(t);
        XBuild(p, settings);
    };
};
Action<string, string> unitTestApp = (bundleId, appPath) =>
{
    Information("Shutdown");
    ShutdownAllAppleSimulators();

    var setting = new SimCtlSettings() { ToolPath = FindXCodeTool("simctl") };
    var simulators = ListAppleSimulators(setting);
    var device = simulators.First(x => x.Name == "xUnit Runner" & x.Runtime == "iOS 10.1");
    Information(string.Format($"Name={device.Name}, UDID={device.UDID}, Runtime={device.Runtime}, Availability={device.Availability}"));

    Information("LaunchAppleSimulator");
    LaunchAppleSimulator(device.UDID);
    Thread.Sleep(60 * 1000);

    Information("UninstalliOSApplication");
    UninstalliOSApplication(
        device.UDID, 
        bundleId,
        setting);
	Thread.Sleep(5 * 1000);

    Information("InstalliOSApplication");
    InstalliOSApplication(
        device.UDID,
        appPath,
        setting);
	// Delay to allow simctl install to finish, otherwise you can receive the following error:
	// The request was denied by service delegate (SBMainWorkspace) for reason: 
	// Busy ("Application "cake.applesimulator.test-xunit" is installing or uninstalling, and cannot be launched").     
	Thread.Sleep(5 * 1000);

    Information("TestiOSApplication");
    var testResults = TestiOSApplication(
        device.UDID, 
        bundleId,
        setting);
    Information("Test Results:");
    Information(string.Format($"Tests Run:{testResults.Run} Passed:{testResults.Passed} Failed:{testResults.Failed} Skipped:{testResults.Skipped} Inconclusive:{testResults.Inconclusive}"));    

    Information("UninstalliOSApplication");
    UninstalliOSApplication(
        device.UDID, 
        bundleId,
        setting);

    Information("Shutdown");
    ShutdownAllAppleSimulators();

    if (testResults.Run > 0 && testResults.Failed > 0) 
    {
	    Information(string.Format($"Tests Run:{testResults.Run} Passed:{testResults.Passed} Failed:{testResults.Failed} Skipped:{testResults.Skipped} Inconclusive:{testResults.Inconclusive}"));    
		TestFailuresAbort();
    }
};

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup((context) =>
{
    // Executed BEFORE the first task.
});

Teardown((context) =>
{
    // Executed AFTER the last task.
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("RunningOnMacOS")
    .Does (() =>
{
	Information(string.Format($"{Context.Environment.Platform.Family}")); // Still reports Linux... :-(
    if (!isRunningOnMacOS)
	{
		NonMacOSAbort();
	} 
	else 
	{
		Information("Running on MacOS");
	}
});

Task("RestorePackages")
	.IsDependentOn("RunningOnMacOS")
    .Does (() =>
{
    NuGetRestore(solution);
});

Task("UnitTestingFailExample")
	.IsDependentOn("RunningOnMacOS")
    .IsDependentOn("RestorePackages")
    .Does (() =>
{
    buildThisApp(
        "./DeviceTests/Test.NUnit/Test.NUnit.csproj",
        "UnitTesting",
        "Clean"    
    );
    buildThisApp(
        "./DeviceTests/Test.NUnit/Test.NUnit.csproj",
        "UnitTesting",
        "Build"    
    );

    unitTestApp(
        "cake.applesimulator.test-nunit",
        "./artifacts/Test.NUnit.app"
    );
});

Task("UnitTestingPassExample")
	.IsDependentOn("RunningOnMacOS")
    .IsDependentOn("RestorePackages")
    .Does (() =>
{
    buildThisApp(
        "./DeviceTests/Test.XUnit/Test.XUnit.csproj",
        "UnitTesting",
        "Clean"    
    );
    buildThisApp(
        "./DeviceTests/Test.XUnit/Test.XUnit.csproj",
        "UnitTesting",
        "Build"    
    );

    unitTestApp(
        "cake.applesimulator.test-xunit",
        "./artifacts/Test.XUnit.app"
    );
});

RunTarget(target);

