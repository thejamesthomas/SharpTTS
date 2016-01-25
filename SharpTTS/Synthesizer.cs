using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

using static System.Speech.AudioFormat.AudioBitsPerSample;
using static System.Speech.AudioFormat.AudioChannel;

namespace SharpTTS
{
    public class Synthesizer
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();
        private readonly SpeechAudioFormatInfo _speechAudioFormatInfo = new SpeechAudioFormatInfo(44100, Sixteen, Stereo);
        
        public Stream Stream { get; private set; }

        public Synthesizer(SynthesizerVoice synthesizerVoice)
        {
            SelectVoice(synthesizerVoice);
        }

        public void SelectVoice(SynthesizerVoice synthesizerVoice)
        {
            _speechSynthesizer.SelectVoice(synthesizerVoice.Name);
        }

        public virtual void PrepareMessageInStream(string message)
        {
            Stream = new MemoryStream();
            
            _speechSynthesizer.SetOutputToAudioStream(Stream, _speechAudioFormatInfo);
            _speechSynthesizer.Speak(message);

            Stream.Position = 0;
        }
    }
}