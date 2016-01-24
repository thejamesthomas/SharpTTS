using Machine.Specifications;
using Moq;
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
            MockSpeechSynthesizerWrapper = new Mock<SpeechSynthesizerWrapper>(Given.AWaveStream());
            MockSpeechSynthesizerWrapper.Setup(s => s.Speak(IsAny<string>()));

            Subject = new Voice(MockSpeechSynthesizerWrapper.Object);
        };

        Because of = () => Subject.Speak("Hello people!");

        It should_save_the_last_message = () => Subject.LastMessage.ShouldEqual("Hello people!");
        It should_invoke_the_speech_synthesizer = () => MockSpeechSynthesizerWrapper.Verify(s => s.Speak("Hello people!"), Times.Once);

        static Voice Subject;
        private static Mock<SpeechSynthesizerWrapper> MockSpeechSynthesizerWrapper;
    }
}
