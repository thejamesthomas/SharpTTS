using NAudio.Wave;

namespace SharpTTS
{
    public class Voice
    {
        private readonly Synthesizer _synthisizer;
        private readonly IWavePlayer _outputDevice;

        public string LastMessage { get; set; }

        public Voice(IWavePlayer outputDevice, SynthesizerVoice synthesizerVoice)
        {
            _outputDevice = outputDevice;
            _synthisizer = new Synthesizer(synthesizerVoice);
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
    }
}