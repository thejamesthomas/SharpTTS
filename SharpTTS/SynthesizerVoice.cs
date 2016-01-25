using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace SharpTTS
{
    public class SynthesizerVoice
    {
        private static readonly SpeechSynthesizer SpeechSynthesizer = new SpeechSynthesizer();
        private readonly VoiceInfo _voiceInfo;

        public string Name => _voiceInfo.Name;

        public SynthesizerVoice(VoiceInfo voiceInfo)
        {
            _voiceInfo = voiceInfo;
        }

        public static List<SynthesizerVoice> GetInstalledVoices()
        {
            var installedVoices = SpeechSynthesizer.GetInstalledVoices();
            return installedVoices.Select(v => new SynthesizerVoice(v.VoiceInfo)).ToList();
        }
    }
}
