using System.Speech.Synthesis;
using Machine.Specifications;
using Moq;
using NAudio.Wave;
using It = Machine.Specifications.It;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable UnusedMember.Local

namespace SharpTTS.Test
{
    [Subject(typeof(Voice))]
    public class WhenVoiceSpeaksWhileAlreadySpeaking
    {
        Establish context = () =>
        {
            MockWaveOut = new Mock<IWavePlayer>();
            MockWaveOut.Setup(w => w.PlaybackState).Returns(PlaybackState.Playing);
            
            MockSynthesizerVoice = new Mock<SynthesizerVoice>(null);

            Subject = new Voice(MockWaveOut.Object);
        };

        Because of = () => Subject.Speak("Hello people!");

        It should_stop_playback = () => MockWaveOut.Verify(w => w.Stop(), Times.Once);
        It should_not_invoke_the_wave_out = () => MockWaveOut.Verify(w => w.Play(), Times.Never);

        static Voice Subject;
        static Mock<IWavePlayer> MockWaveOut;
        static Mock<SynthesizerVoice> MockSynthesizerVoice;
    }
}
