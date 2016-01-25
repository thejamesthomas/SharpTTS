using NAudio.Wave;

namespace SharpTTS
{
    public class Voice
    {
        private readonly Synthesizer _synthisizer;
        private IWavePlayer _outputDevice;

        public string LastMessage { get; set; }

        public Voice(IWavePlayer outputDevice)
        {
            _outputDevice = outputDevice;
            _synthisizer = new Synthesizer();
        }

        public void Speak(string message)
        {
            if (_outputDevice.PlaybackState == PlaybackState.Playing)
            {
                _outputDevice.Stop();
                return;
            }

            LastMessage = message;
            _synthisizer.PrepareMessageInStream(message);

            _outputDevice.Init(new RawSourceWaveStream(_synthisizer.Stream, new WaveFormat()));
            _outputDevice.Play();
        }

        public void SelectVoice(SynthesizerVoice synthesizerVoice)
        {
            _synthisizer.SelectVoice(synthesizerVoice);
        }

        public void SelectOutput(OutputDevice outputDevice)
        {
            _outputDevice = new WaveOut { DeviceNumber = outputDevice.DeviceNumber };
        }
    }
}