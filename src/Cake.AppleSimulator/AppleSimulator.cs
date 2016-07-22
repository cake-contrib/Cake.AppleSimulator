namespace Cake.AppleSimulator
{
    public sealed class AppleSimulator
    {
        /// <summary>
        /// The status of the simulator (shutdown, booted, unavailable, unaviable, reason xxxx)
        /// </summary>
        /// <example>
        ///     (unavailable, failed to open liblaunch_sim.dylib)
        ///     (unavailable, runtime profile not found)
        ///     (unavailable, Mac OS X 10.11.5 is not supported)
        /// </example>
        public string Availability { get; set; }

        /// <summary>
        /// The human name of the simulator (i.e. iPhone 6s, Apple TV 1080p, Apple Watch - 38mm)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The simulator runtime (i.e. iOS 9.3, tvOS 9.2, watchOS 2.2)
        /// </summary>
        public string Runtime { get; set; }

        /// <summary>
        /// </summary>
        /// <example>Shutdown, Shutting Down, Booted</example>
        public string State { get; set; }

        /// <summary>
        /// The unique identifier, that identifies the simulator and is used for operations such as start, create, shutdown, pair.
        /// </summary>
        public string UDID { get; set; }
    }
}