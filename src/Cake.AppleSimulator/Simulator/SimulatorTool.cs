using Cake.AppleSimulator.SimCtl;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;

namespace Cake.AppleSimulator.Simulator
{
    /// <summary>
    ///     Base class for all AppleSimulator related tools.
    /// </summary>
    /// <typeparam name="TSettings">The Settings type.</typeparam>
    public abstract class SimulatorTool<TSettings> : Tool<TSettings>
        where TSettings : SimulatorSettings
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SimCtlTool{TSettings}" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="settings">The Settings.</param>
        protected SimulatorTool(
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
            return new[] { new FilePath("/usr/bin/open") };
        }

        /// <summary>
        ///     Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[]
            {
                "open"
            };
        }

        /// <summary>
        ///     Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "AppleSimulator";
        }
    }
}