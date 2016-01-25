using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NAudio.Wave;

namespace SharpTTS.UI
{
    public partial class ChatWindow
    {
        private readonly Voice _voice;
        private readonly List<SynthesizerVoice> _voices;
        private readonly List<OutputDevice> _outputs;

        public ChatWindow()
        {
            InitializeComponent();
            DataContext = this;

            var waveOut = new WaveOut { DeviceNumber = 5 };
            _voices = SynthesizerVoice.GetInstalledVoices();
            _voice = new Voice(waveOut, _voices.First());

            VoiceComboBox.ItemsSource = _voices;
            VoiceComboBox.DisplayMemberPath = "Name";
            VoiceComboBox.SelectionChanged += VoiceComboBox_SelectionChanged;

            _outputs = OutputDevice.GetInstalledOutputDevices();
            OutputComboBox.ItemsSource = _outputs;
            OutputComboBox.DisplayMemberPath = "Name";

            SendButton.Click += SendButton_Click;
            MessageTextBox.KeyUp += MessageTextBox_KeyUp;
        }

        private void VoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _voice.SelectVoice(e.AddedItems[0] as SynthesizerVoice);
        }

        private void MessageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            _voice.Speak(MessageTextBox.Text);
            MessageTextBox.Text = "";
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            _voice.Speak(MessageTextBox.Text);
        }
    }
}
