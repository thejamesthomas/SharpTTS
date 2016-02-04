using System.Collections.Generic;
using NAudio.Wave;

namespace SharpTTS
{
    public class OutputDevice
    {
        private readonly WaveOutCapabilities _outputDevice;

        public string Name => _outputDevice.ProductName;
        public int DeviceNumber { get; }

        public OutputDevice(WaveOutCapabilities outputDevice, int deviceNumber)
        {
            _outputDevice = outputDevice;
            DeviceNumber = deviceNumber;
        }

        public static List<OutputDevice> GetInstalledOutputDevices()
        {
            var outputDevices = new List<OutputDevice>();

            for (var i = 0; i < WaveOut.DeviceCount; i++)
            {
                outputDevices.Add(new OutputDevice(WaveOut.GetCapabilities(i), i));
            }

            return outputDevices;
        }
    }
}
