using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace ACT_Plugin
{
    class SAPITTS : ISpeechSynthesizer
    {
        SpeechSynthesizer synth;
        SpeechAudioFormatInfo format;
        WaveFormat waveFormat;

        public WaveFormat WaveFormat { get { return waveFormat; } }
        public int Rate {
            get { return synth.Rate; }
            set { synth.Rate = value; }
        }
        public event EventHandler SpeakCompleted;

        class VoiceInfo : ACT_Plugin.VoiceInfo
        {
            public VoiceInfo(System.Speech.Synthesis.VoiceInfo info)
            {
                Description = info.Description;
                Name = info.Name;
            }
        }

        public ACT_Plugin.VoiceInfo Voice { get { return new VoiceInfo(synth.Voice); } }

        class VoiceInfoEnumerable : IEnumerable<VoiceInfo>
        {
            class VoiceInfoEnumerator : IEnumerator<VoiceInfo>
            {
                IEnumerator<InstalledVoice> i;
                public VoiceInfoEnumerator(IEnumerator<InstalledVoice> enumerator)
                {
                    i = enumerator;
                    while (i.Current != null && (!i.Current.Enabled || !i.MoveNext()))
                        ;
                }

                VoiceInfo currentObj = null;
                public VoiceInfo Current { get { return currentObj ?? (currentObj = new VoiceInfo(i.Current.VoiceInfo)); } }
                object IEnumerator.Current { get { return currentObj ?? (currentObj = new VoiceInfo(i.Current.VoiceInfo)); } }

                public bool MoveNext()
                {
                    bool success;
                    while ((success = i.MoveNext()) && i.Current != null && !i.Current.Enabled)
                        ;
                    currentObj = null;
                    return success;
                }

                public void Reset()
                {
                    i.Reset();
                    while (i.Current != null && (!i.Current.Enabled || !i.MoveNext()))
                        ;
                    currentObj = null;
                }

                public void Dispose()
                {
                    i.Dispose();
                    i = null;
                    currentObj = null;
                }
            }

            IEnumerable<System.Speech.Synthesis.InstalledVoice> installedVoices;
            public VoiceInfoEnumerable(IEnumerable<InstalledVoice> ivs)
            {
                installedVoices = ivs;
            }

            public IEnumerator<VoiceInfo> GetEnumerator()
            {
                return new VoiceInfoEnumerator(installedVoices.GetEnumerator());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new VoiceInfoEnumerator(installedVoices.GetEnumerator());
            }
        }

        public static IEnumerable<ACT_Plugin.VoiceInfo> GetVoices()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            return new VoiceInfoEnumerable(synth.GetInstalledVoices());
        }

        public SAPITTS()
        {
            synth = new SpeechSynthesizer();
            synth.SpeakCompleted += synth_SpeakCompleted;
            SelectVoice(null);
        }

        void synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            SpeakCompleted(sender, e);
        }

        public void SelectVoice(string name)
        {
            if (name != null)
                synth.SelectVoice(name);
            format = synth.Voice.SupportedAudioFormats[0];
            waveFormat = new WaveFormat(format.SamplesPerSecond, format.BitsPerSample, format.ChannelCount);
        }

        public void SetOutputToAudioStream(Stream audioDestination)
        {
            synth.SetOutputToAudioStream(audioDestination, format);
        }

        public void SetOutputToNull()
        {
            synth.SetOutputToNull();
        }

        public void SpeakAsync(string text, int volumePercent)
        {
            synth.Volume = volumePercent;
            synth.SpeakAsync(text);
        }

        public void Dispose()
        {
            synth.Dispose();
            synth = null;
            format = null;
            waveFormat = null;
        }
    }

    class SAPITTSFactory : ISpeechSynthesizerFactory
    {
        public string Name { get { return "SAPI"; } }
        public Type SynthType { get { return typeof(SAPITTS); } }
        public ISpeechSynthesizer Create() { return new SAPITTS(); }
        public IEnumerable<VoiceInfo> GetVoices() { return SAPITTS.GetVoices(); }
    }
}
