#addin "nuget:http://nuget:3128/api/odata?package=Cake.AppleSimulator""
using System.Threading;

Task("RunIntegrationTests")
    .Does (() =>
{
    var simulators = ListAppleSimulators();

    var device = simulators.First();
    var deviceIdentifier = device.UDID;

    Information(string.Format("Name={0}, UDID={1}, State={2}, Availability={3}", device.Name, device.UDID, device.State, device.Availability));

    Information("Erase");
    EraseAppleSimulator(deviceIdentifier);

    Information("Launch");
    LaunchAppleSimulator(deviceIdentifier);

    Thread.Sleep(15000);

    Information("Shutdown");
    ShutdownAllAppleSimulators();

});

RunTarget("RunIntegrationTests");
