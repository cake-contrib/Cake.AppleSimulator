using System;
using System.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppleSimulator.XCRun
{
    internal sealed class XCRunRunner : XCRunTool<XCRunSettings>
    {
        private readonly ICakeLog _log;

        public XCRunRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log, XCRunSettings settings) : base(fileSystem, environment, processRunner, tools, settings)
        {
            _log = log;
        }

        public string Find(string command)
        {
            var arguments = CreateArgumentBuilder(Settings).Append("--find").Append(command);

            var stdOutput = RunAndRedirectStandardOutput(Settings, arguments);
            if (stdOutput.StartsWith("xcrun: error:", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FileNotFoundException();
            }
            return stdOutput;
        }
    }
}