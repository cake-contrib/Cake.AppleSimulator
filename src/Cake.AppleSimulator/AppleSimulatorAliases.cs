using System;
using System.Collections.Generic;
using Cake.AppleSimulator.SimCtl;
using Cake.AppleSimulator.Simulator;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.AppleSimulator.UnitTest;
using Cake.AppleSimulator.XCRun;
using Newtonsoft.Json;

namespace Cake.AppleSimulator
{
    [CakeAliasCategory("AppleSimulator")]
    [CakeNamespaceImport("Cake.AppleSimulator")]
    [CakeNamespaceImport("Cake.AppleSimulator.SimCtl")]
    [CakeNamespaceImport("Cake.AppleSimulator.Simulator")]
    public static class AppleSimulatorAliases
    {
        /// <summary>
        ///     Boot the specified simulator, headless in the background.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        [CakeMethodAlias]
        public static void BootAppleSimulator(this ICakeContext context, string deviceIdentifier)
        {
            BootAppleSimulator(context, deviceIdentifier, new SimCtlSettings());
        }

        /// <summary>
        ///     Boot the specified simulator, headless in the background.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void BootAppleSimulator(this ICakeContext context, string deviceIdentifier,
            SimCtlSettings settings)
        {
            if (string.IsNullOrWhiteSpace(deviceIdentifier))
            {
                throw new ArgumentException(nameof(deviceIdentifier));
            }

            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            runner.BootSimulator(deviceIdentifier);
        }

        /// <summary>
        ///     Not implemented yet. Send in a pull-request!
        /// </summary>
        /// <param name="context"></param>
        [CakeMethodAlias]
        public static void EraseAllAppleSimulators(this ICakeContext context)
        {
            EraseAllAppleSimulators(context, new SimCtlSettings());
        }

        /// <summary>
        ///     Not implemented yet. Send in a pull-request!
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void EraseAllAppleSimulators(this ICakeContext context, SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Erases and factory resets the specified simulator.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        [CakeMethodAlias]
        public static void EraseAppleSimulator(this ICakeContext context, string deviceIdentifier)
        {
            EraseAppleSimulator(context, deviceIdentifier, new SimCtlSettings());
        }

        /// <summary>
        ///     Erases and factory resets the specified simulator.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void EraseAppleSimulator(this ICakeContext context, string deviceIdentifier,
            SimCtlSettings settings)
        {
            if (string.IsNullOrWhiteSpace(deviceIdentifier))
            {
                throw new ArgumentException(nameof(deviceIdentifier));
            }

            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            runner.EraseSimulator(deviceIdentifier);
        }

        /// <summary>
        ///     Boot the specified simulator, headless in the background. This operation returns immiedately and does not block,
        ///     you need to put the cake thread to sleep/pause execution to allow time for the simulator to boot.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        [CakeMethodAlias]
        public static void LaunchAppleSimulator(this ICakeContext context, string deviceIdentifier)
        {
            LaunchAppleSimulator(context, deviceIdentifier, new SimulatorSettings());
        }

        /// <summary>
        ///     Boot the specified simulator, headless in the background. This operation returns immiedately and does not block,
        ///     you need to put the cake thread to sleep/pause execution to allow time for the simulator to boot.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceIdentifier"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void LaunchAppleSimulator(this ICakeContext context, string deviceIdentifier,
            SimulatorSettings settings)
        {
            if (string.IsNullOrWhiteSpace(deviceIdentifier))
            {
                throw new ArgumentException(nameof(deviceIdentifier));
            }

            ThrowIfNotRunningOnMac(context);

            var runner = new SimulatorRunner(context.FileSystem, context.Environment, context.ProcessRunner,
                context.Tools, context.Log, settings);
            runner.LaunchSimulator(deviceIdentifier);
        }

		/// <summary>
        /// Installs an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="applicationPath"></param>
		[CakeMethodAlias]
		public static void InstalliOSApplication(this ICakeContext context, string deviceIdentifier, string applicationPath)
		{
			InstalliOSApplication(context, deviceIdentifier, applicationPath, new SimCtlSettings());
		}

		/// <summary>
        /// Installs an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="applicationPath"></param>
		/// <param name="settings"></param>
		[CakeMethodAlias]
		public static void InstalliOSApplication(this ICakeContext context, string deviceIdentifier,
			string applicationPath, SimCtlSettings settings)
		{
			if (string.IsNullOrWhiteSpace(deviceIdentifier))
			{
				throw new ArgumentException(nameof(deviceIdentifier));
			}

			ThrowIfNotRunningOnMac(context);

			var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner,
				context.Tools, context.Log, settings);
			runner.InstallApplication(deviceIdentifier, applicationPath);
		}

		/// <summary>
        /// Uninstalls an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		[CakeMethodAlias]
		public static void UninstalliOSApplication(this ICakeContext context, string deviceIdentifier, string appIdentifier)
		{
			UninstalliOSApplication(context, deviceIdentifier, appIdentifier, new SimCtlSettings());
		}

		/// <summary>
        /// Uninstalls an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		/// <param name="settings"></param>
		[CakeMethodAlias]
		public static void UninstalliOSApplication(this ICakeContext context, string deviceIdentifier,
			string appIdentifier, SimCtlSettings settings)
		{
			if (string.IsNullOrWhiteSpace(deviceIdentifier))
			{
				throw new ArgumentException(nameof(deviceIdentifier));
			}

			ThrowIfNotRunningOnMac(context);

			var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner,
				context.Tools, context.Log, settings);
			runner.UninstallApplication(deviceIdentifier, appIdentifier);
		}

		/// <summary>
        /// Launches an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		[CakeMethodAlias]
		public static void LaunchiOSApplication(this ICakeContext context, string deviceIdentifier, string appIdentifier)
		{
			LaunchiOSApplication(context, deviceIdentifier, appIdentifier, new SimCtlSettings());
		}

		/// <summary>
        /// Launches an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		/// <param name="settings"></param>
		[CakeMethodAlias]
		public static void LaunchiOSApplication(this ICakeContext context, string deviceIdentifier,
			string appIdentifier, SimCtlSettings settings)
		{
			if (string.IsNullOrWhiteSpace(deviceIdentifier))
			{
				throw new ArgumentException(nameof(deviceIdentifier));
			}

			ThrowIfNotRunningOnMac(context);

			var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner,
				context.Tools, context.Log, settings);
			runner.LaunchApplication(deviceIdentifier, appIdentifier);
		}

		/// <summary>
        /// Launches and returns the unit test results of an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		[CakeMethodAlias]
		public static TestResults TestiOSApplication(this ICakeContext context, string deviceIdentifier, string appIdentifier)
		{
			return TestiOSApplication(context, deviceIdentifier, appIdentifier, new SimCtlSettings());
		}

		/// <summary>
        /// Launches and returns the unit test results of an application on the specified simulator.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="deviceIdentifier"></param>
		/// <param name="appIdentifier"></param>
		/// <param name="settings"></param>
		[CakeMethodAlias]
		public static TestResults TestiOSApplication(this ICakeContext context, string deviceIdentifier,
			string appIdentifier, SimCtlSettings settings)
		{
			if (string.IsNullOrWhiteSpace(deviceIdentifier))
			{
				throw new ArgumentException(nameof(deviceIdentifier));
			}

			ThrowIfNotRunningOnMac(context);

			var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner,
				context.Tools, context.Log, settings);
			return runner.TestApplication(deviceIdentifier, appIdentifier);
		}

		/// <summary>
		/// Fetch list of installed simulator device types.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="settings"></param>
		/// <returns></returns>
		[CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorDeviceType> ListAppleSimulatorDeviceTypes(this ICakeContext context,
            SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            return runner.ListDeviceTypes();
        }

        /// <summary>
        /// Fetch list of installed simulator device types.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorDeviceType> ListAppleSimulatorDeviceTypes(this ICakeContext context)
        {
            ThrowIfNotRunningOnMac(context);

            return ListAppleSimulatorDeviceTypes(context, new SimCtlSettings());
        }

        /// <summary>
        /// Fetch list of simulators pairs (iphone+watch) available for administrative operations.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorPair> ListAppleSimulatorPairs(this ICakeContext context,
            SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            return runner.ListPairs();
        }

        /// <summary>
        /// Fetch list of simulators pairs (iphone+watch) available for administrative operations.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorPair> ListAppleSimulatorPairs(this ICakeContext context)
        {
            ThrowIfNotRunningOnMac(context);

            return ListAppleSimulatorPairs(context, new SimCtlSettings());
        }

        /// <summary>
        /// Fetch list of installed simulator runtimes.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorRuntime> ListAppleSimulatorRuntimes(this ICakeContext context,
            SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            return runner.ListRuntimes();
        }

        /// <summary>
        /// Fetch list of installed simulator runtimes.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulatorRuntime> ListAppleSimulatorRuntimes(this ICakeContext context)
        {
            ThrowIfNotRunningOnMac(context);

            return ListAppleSimulatorRuntimes(context, new SimCtlSettings());
        }

        /// <summary>
        /// Fetch list configured simulators that are defined and available for administrative operations (boot, launch, shutdown, etc)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulator> ListAppleSimulators(this ICakeContext context,
            SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            return runner.ListSimulators();
        }

        /// <summary>
        /// Fetch list configured simulators that are defined and available for administrative operations (boot, launch, shutdown, etc)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static IReadOnlyList<AppleSimulator> ListAppleSimulators(this ICakeContext context)
        {
            ThrowIfNotRunningOnMac(context);

            return ListAppleSimulators(context, new SimCtlSettings());
        }

        /// <summary>
        /// Shutdowns all running simulators.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void ShutdownAllAppleSimulators(this ICakeContext context, SimCtlSettings settings)
        {
            ThrowIfNotRunningOnMac(context);

            var runner = new SimCtlRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
                context.Log, settings);
            runner.ShutdownSimulators();
        }

        /// <summary>
        /// Shutdowns all running simulators.
        /// </summary>
        /// <param name="context"></param>
        [CakeMethodAlias]
        public static void ShutdownAllAppleSimulators(this ICakeContext context)
        {
            ShutdownAllAppleSimulators(context, new SimCtlSettings());
        }

		/// <summary>
		/// Shutdowns all running simulators.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="settings"></param>
		[CakeMethodAlias]
		public static string FindXCodeTool(this ICakeContext context, string tool, XCRunSettings settings)
		{
			ThrowIfNotRunningOnMac(context);

			var runner = new XCRunRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools,
				context.Log, settings);
			return runner.Find(tool);
		}

		/// <summary>
		/// Shutdowns all running simulators.
		/// </summary>
		/// <param name="context"></param>
		[CakeMethodAlias]
		public static string FindXCodeTool(this ICakeContext context, string tool)
		{
			return FindXCodeTool(context, tool, new XCRunSettings());
		}

        private static void ThrowIfNotRunningOnMac(ICakeContext context)
        {
            // I've always used IsRunningOnUnix() from ICakeEnvironment, but I know @patriksvensson is currently working
            // on a PlatformFamily as part of #1008 , which actually distinguishes between Linux and OS X. He might be
            // able to chime in on when we can expect those changes..
            if (!context.Environment.Platform.IsUnix())
            {
                throw new PlatformNotSupportedException("This Addin only works on OSX.");
            }
        }
	}
}