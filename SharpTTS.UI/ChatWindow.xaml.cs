using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NAudio.Wave;

namespace SharpTTS.UI
{
    public partial class ChatWindow
    {
        private readonly Voice _voice;
        private IEnumerable<SynthesizerVoice> _voices;

        public ChatWindow()
        {
            InitializeComponent();
            DataContext = this;

            var waveOut = new WaveOut { DeviceNumber = 5 };
            _voices = SynthesizerVoice.GetInstalledVoices();
            _voice = new Voice(waveOut, _voices.First());

            VoiceComboBox.ItemsSource = _voices;

            SendButton.Click += SendButton_Click;
            MessageTextBox.KeyUp += MessageTextBox_KeyUp;
        }

        private void MessageTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _voice.Speak(MessageTextBox.Text);
                MessageTextBox.Text = "";
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            _voice.Speak(MessageTextBox.Text);
        }
    }
}
