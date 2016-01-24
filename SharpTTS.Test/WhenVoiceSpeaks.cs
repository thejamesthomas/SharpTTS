using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using ItToo = Moq.It;

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
            MockSpeechSynthesizerWrapper = new Mock<SpeechSynthesizerWrapper>();
            MockSpeechSynthesizerWrapper.Setup(s => s.Speak(ItToo.IsAny<string>()));

            Subject = new Voice(MockSpeechSynthesizerWrapper.Object);
        };

        Because of = () => Subject.Speak("Hello people!");

        It should_save_the_last_message = () => Subject.LastMessage.ShouldEqual("Hello people!");
        private It should_invoke_the_speech_synthesizer = () => MockSpeechSynthesizerWrapper.Verify(s => s.Speak("Hello people!"), Times.Once);

        static Voice Subject;
        private static Mock<SpeechSynthesizerWrapper> MockSpeechSynthesizerWrapper;
    }
}
