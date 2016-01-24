using System.Speech.Synthesis;
using NAudio.Wave;

namespace SharpTTS
{
    public class SpeechSynthesizerWrapper
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();
        private readonly WaveStream _waveStream;

        public SpeechSynthesizerWrapper(WaveStream waveStream)
        {
            _waveStream = waveStream;
            _speechSynthesizer.SetOutputToWaveStream(_waveStream);
        }

        public virtual void Speak(string message)
        {
            _speechSynthesizer.Speak(message);
            _waveStream.Flush();
        }
    }
}