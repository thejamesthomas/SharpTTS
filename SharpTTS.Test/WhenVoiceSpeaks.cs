using System.Speech.Synthesis;
using Machine.Specifications;
using Moq;
using NAudio.Wave;
using It = Machine.Specifications.It;

using static Moq.It;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable UnusedMember.Local

namespace SharpTTS.Test
{
    [Subject(typeof(Voice))]
    public class WhenVoiceSpeaks
    {
        Establish context = () =>
        {
            MockWaveOut = new Mock<IWavePlayer>();
            MockVoiceInfo = new Mock<VoiceInfo>();

            Subject = new Voice(MockWaveOut.Object);
        };



        Because of = () => Subject.Speak("Hello people!");

        It should_save_the_last_message = () => Subject.LastMessage.ShouldEqual("Hello people!");
        It should_init_the_wave_out = () => MockWaveOut.Verify(w => w.Init(IsAny<IWaveProvider>()), Times.Once);
        It should_invoke_the_wave_out = () => MockWaveOut.Verify(w => w.Play(), Times.Once);

        static Voice Subject;
        static Mock<IWavePlayer> MockWaveOut;
        static Mock<VoiceInfo> MockVoiceInfo;
    }
}
