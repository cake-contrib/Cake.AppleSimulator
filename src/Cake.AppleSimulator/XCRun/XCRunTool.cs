using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppleSimulator.XCRun
{
	public abstract class XCRunTool<TSettings> : Tool<TSettings> where TSettings : XCRunSettings
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="XCRunTool{TSettings}" /> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		/// <param name="environment">The environment.</param>
		/// <param name="processRunner">The process runner.</param>
		/// <param name="tools">The tool locator.</param>
		/// <param name="settings">The Settings.</param>
		protected XCRunTool(
			IFileSystem fileSystem,
			ICakeEnvironment environment,
			IProcessRunner processRunner,
			IToolLocator tools, TSettings settings)
				: base(fileSystem, environment, processRunner, tools)
		{
			Settings = settings;
		}

		protected TSettings Settings { get; set; }

		/// <summary>
		///     Creates a <see cref="ProcessArgumentBuilder" /> and adds common commandline arguments.
		/// </summary>
		/// <param name="settings">The Settings.</param>
		/// <returns>Instance of <see cref="ProcessArgumentBuilder" />.</returns>
		protected ProcessArgumentBuilder CreateArgumentBuilder(TSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException(nameof(settings));
			}

			var builder = new ProcessArgumentBuilder();
			return builder;
		}

		/// <summary>
		///     Gets alternative file paths which the tool may exist in
		/// </summary>
		/// <returns>The alternative locations for the tool.</returns>
		protected override IEnumerable<FilePath> GetAlternativeToolPaths(TSettings settings)
		{
			return new[] { new FilePath("/usr/bin/xcrun") };
		}

		/// <summary>
		///     Gets the possible names of the tool executable.
		/// </summary>
		/// <returns>The tool executable name.</returns>
		protected override IEnumerable<string> GetToolExecutableNames()
		{
			return new[]
			{
				"xcrun"
			};
		}

		/// <summary>
		///     Gets the name of the tool.
		/// </summary>
		/// <returns>The name of the tool.</returns>
		protected override string GetToolName()
		{
			return "XcodeRun";
		}

		/// <summary>
		///     Runs the specified process, using the specified Settings/arguments and returns the process StandardOutput.
		/// </summary>
		/// <param name="settings">The Settings.</param>
		/// <param name="arguments">The arguments.</param>
		protected string RunAndRedirectStandardOutput(TSettings settings, ProcessArgumentBuilder arguments)
		{
			var stdOutput = string.Empty;

			Run(settings, arguments,
				new ProcessSettings
				{
					RedirectStandardOutput = true
				},
				process => stdOutput = string.Join(Environment.NewLine, process.GetStandardOutput()));

			return stdOutput;
		}
	}
}
