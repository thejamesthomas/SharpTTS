﻿using System;
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

        private readonly OutputDevice _outputDevice;
        private readonly List<OutputDevice> _outputDevices;

        public ChatWindow()
        {
            InitializeComponent();
            DataContext = this;

            _voices = SynthesizerVoice.GetInstalledVoices();
            _outputDevices = OutputDevice.GetInstalledOutputDevices();

            _outputDevice = _outputDevices.First();
            _voice = new Voice(new WaveOut { DeviceNumber = _outputDevice.DeviceNumber });

            _voice.SelectVoice(_voices.First());
            VoiceComboBox.ItemsSource = _voices;
            VoiceComboBox.DisplayMemberPath = "Name";
            VoiceComboBox.SelectionChanged += VoiceComboBox_SelectionChanged;

            _voice.SelectOutput(_outputDevice);
            OutputComboBox.SelectedItem = _outputDevices;
            OutputComboBox.ItemsSource = _outputDevices;
            OutputComboBox.DisplayMemberPath = "Name";
            OutputComboBox.SelectionChanged += OutputComboBox_SelectionChanged;

            SendButton.Click += SendButton_Click;
            MessageTextBox.KeyUp += MessageTextBox_KeyUp;
        }

        private void OutputComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _voice.SelectOutput(e.AddedItems[0] as OutputDevice);
        }

        private void VoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _voice.SelectVoice(e.AddedItems[0] as SynthesizerVoice);
        }

        private void MessageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            SendMessage();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            _voice.Speak(MessageTextBox.Text);
            MessageTextBox.Text = "";
        }

    }
}
