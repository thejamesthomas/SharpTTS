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
    [Subject(typeof(SpeechSynthesizerWrapper))]
    public class WhenSpeechSyntesizerWrapperSpeaks
    {
        Establish context = () =>
        {
            MockWaveStream = new Mock<WaveStream>();
            MockWaveStream.Setup(s => s.Write(IsAny<byte[]>(), IsAny<int>(), IsAny<int>()));
            MockWaveStream.Setup(s => s.Flush());

            Subject = new SpeechSynthesizerWrapper(MockWaveStream.Object);
        };

        Because of = () => Subject.Speak("Hey y'all!");

        It should_invoke_write_on_wave_stream = () => MockWaveStream.Verify(s => s.Write(IsAny<byte[]>(), IsAny<int>(), IsAny<int>()), Times.AtLeastOnce);
        It should_flush_the_output_stream_when_done = () => MockWaveStream.Verify(s => s.Flush(), Times.Once);

        static SpeechSynthesizerWrapper Subject;
        static Mock<WaveStream> MockWaveStream;
    }
}
