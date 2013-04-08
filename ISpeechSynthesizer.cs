using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace ACT_Plugin
{
    interface ISpeechSynthesizer : IDisposable
    {
        WaveFormat WaveFormat { get; }
        ACT_Plugin.VoiceInfo Voice { get; }
        int Rate { get; set; }
        event EventHandler SpeakCompleted;

        void SelectVoice(string name);
        void SetOutputToAudioStream(Stream audioDestination);
        void SetOutputToNull();
        void SpeakAsync(string text, Int32 volumePercent);
    }

    interface ISpeechSynthesizerFactory
    {
        string Name { get; }
        Type SynthType { get; }
        ISpeechSynthesizer Create();
        IEnumerable<VoiceInfo> GetVoices();
    }

    public class VoiceInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
