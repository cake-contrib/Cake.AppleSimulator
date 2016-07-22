namespace Cake.AppleSimulator
{
    public class AppleSimulatorRuntime
    {
        /// <summary>
        /// The status of the simulator (
        /// </summary>
        /// <example>(available)</example>
        public string Availability { get; set; }

        /// <summary>
        /// </summary>
        /// <example>13E230</example>
        public string BuildVersion { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>com.apple.CoreSimulator.SimRuntime.iOS-9-3</example>
        public string Identifier { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>iOS 9.3</example>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>9.3</example>
        public string Version { get; set; }
    }
}