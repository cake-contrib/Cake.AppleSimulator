using Cake.AppleSimulator.Simulator;

namespace Cake.AppleSimulator
{
    /// <summary>
    /// not implemented, for future expansion.
    /// </summary>
    public class AppleSimulatorLaunchSettings : SimulatorSettings
    {
        public int? ConnectHardwareKeyboard { get; set; }
        public AppleSimulatorGraphicsQuality? GraphicsQuality { get; set; }
        public AppleSimulatorWindowScale? WindowScale { get; set; }
    }
}