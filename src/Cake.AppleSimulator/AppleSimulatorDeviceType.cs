namespace Cake.AppleSimulator
{
    public sealed class AppleSimulatorDeviceType
    {
        /// <summary>
        /// The runtime identifier of the simulator (i.e. com.apple.CoreSimulator.SimDeviceType.iPad-Air)
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// The simulator name (i.e. iPad Air)
        /// </summary>
        public string Name { get; set; }
    }
}