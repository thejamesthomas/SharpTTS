namespace SharpTTS
{
    public class Voice
    {
        private readonly SpeechSynthesizerWrapper _speechSynthisizerWrapper;

        public Voice(SpeechSynthesizerWrapper speechSynthisizerWrapper)
        {
            _speechSynthisizerWrapper = speechSynthisizerWrapper;
        }

        public string LastMessage { get; set; }

        public void Speak(string message)
        {
            LastMessage = message;
            _speechSynthisizerWrapper.Speak(message);
        }
    }
}