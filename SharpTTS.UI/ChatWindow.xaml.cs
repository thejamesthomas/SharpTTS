using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

namespace SharpTTS.UI
{
    public partial class ChatWindow
    {
        private readonly Voice _voice;

        public ChatWindow()
        {
            InitializeComponent();
            DataContext = this;

            _voice = new Voice(new SpeechSynthesizerWrapper());
        }
    }
}
