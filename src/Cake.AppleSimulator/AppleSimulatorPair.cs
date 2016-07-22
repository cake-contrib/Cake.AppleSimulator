namespace Cake.AppleSimulator
{
    public sealed class AppleSimulatorPair
    {
        public AppleSimulatorPairedPhone Phone { get; set; }
        public string State { get; set; }
        public string UDID { get; set; }
        public AppleSimulatorPairedWatch Watch { get; set; }
    }

    public sealed class AppleSimulatorPairedPhone
    {
        /// <summary>
        /// </summary>
        /// <example>iPhone 6s Plus</example>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        /// <example>Shutdown, Shutting Down, Booted</example>
        public string State { get; set; }

        /// <summary>
        /// </summary>
        /// <example>1F647CAB-89E4-40BB-95C8-590AE755AD58</example>
        public string UDID { get; set; }
    }

    public sealed class AppleSimulatorPairedWatch
    {
        /// <summary>
        /// </summary>
        /// <example>Apple Watch - 42mm</example>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        /// <example>Shutdown, Shutting Down, Booted</example>
        public string State { get; set; }

        /// <summary>
        /// </summary>
        /// <example>8D4F5981-15D7-47BF-9A26-0FC9DCDD637F</example>
        public string UDID { get; set; }
    }
}