using Cake.AppleSimulator.Simulator;

namespace Cake.AppleSimulator
{
    /// <summary>
    /// not implemented yet, these will be options to customize settings of the simulator before boot. Stuff like disabling auto-correction...
    /// see https://github.com/plu/simctl/blob/master/lib/simctl/device_settings.rb
    /// </summary>
    public class AppleSimulatorConfigurationSettings : SimulatorSettings
    {
        public bool? KeyboardAllowPaddle { get; set; }
        public bool? KeyboardAssistant { get; set; }
        public bool? KeyboardAutocapitalization { get; set; }
        public bool? KeyboardAutocorrection { get; set; }
        public bool? KeyboardCapsLock { get; set; }
        public bool? KeyboardCheckSpelling { get; set; }
        public bool? KeyboardPeriodShortcut { get; set; }
        public bool? KeyboardPrediction { get; set; }
        public bool? KeyboardShowPredictionBar { get; set; }
    }
}