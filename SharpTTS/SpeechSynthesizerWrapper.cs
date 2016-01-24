using System.Speech.Synthesis;

namespace SharpTTS
{
    public class SpeechSynthesizerWrapper
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public virtual void Speak(string message)
        {
            _speechSynthesizer.Speak(message);
        }
    }
}