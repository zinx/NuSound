using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ACT_Plugin
{
    class TTSProvider : Stream, IWaveProvider
    {
        public static string VoiceIdentifier { get; set; }
        public static int VoiceRate { get; set; }
        public static ISpeechSynthesizerFactory TTSFactory;

        public static List<ISpeechSynthesizerFactory> TTSFactories = new List<ISpeechSynthesizerFactory>
        {
            new SAPITTSFactory()
        };

        ISpeechSynthesizer synth;

        bool filling = true;
        bool finishing = false;
        bool finished = false;
        CircularBuffer waveBuffer;

        static Queue<TTSProvider> providers = new Queue<TTSProvider>();

        static TTSProvider()
        {
            TTSFactory = TTSFactories[0];
            if (MSSTTSFactory.IsSupported())
                TTSFactories.Add(new MSSTTSFactory());
        }

        TTSProvider()
        {
            CreateSynth();
        }

        void CreateSynth()
        {
            synth = TTSFactory.Create();
            synth.SpeakCompleted += synth_SpeakCompleted;
            SetVoice();
        }

        void SetVoice()
        {
            try { synth.SelectVoice(VoiceIdentifier); }
            catch (ArgumentException) { }
            waveBuffer = new CircularBuffer(synth.WaveFormat.ConvertLatencyToByteSize(500));
        }

        void synth_SpeakCompleted(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(synth_Cleanup);
        }

        void synth_Cleanup(object state)
        {
            Flush();
            Close();
            synth.Dispose();
            CreateSynth();
            if (TTSFactory.SynthType.IsInstanceOfType(synth))
                lock (providers) providers.Enqueue(this);
        }

        static public void EarlyInit()
        {
            lock (providers)
                if (providers.Count <= 0)
                    providers.Enqueue(new TTSProvider());
        }

        static TTSProvider GetProvider()
        {
            TTSProvider provider = null;

            lock (providers)
                while (provider == null && providers.Count > 0)
                {
                    provider = providers.Dequeue();
                    if (!TTSFactory.SynthType.IsInstanceOfType(provider.synth))
                        provider = null;
                }
            if (provider == null)
                provider = new TTSProvider();

            if (VoiceIdentifier != null && !provider.synth.Voice.Name.Equals(VoiceIdentifier))
                provider.SetVoice();

            provider.synth.Rate = VoiceRate;

            provider.Reset();

            return provider;
        }

        void Reset()
        {
            filling = true;
            finishing = false;
            finished = false;
            waveBuffer.Reset();
            synth.SetOutputToAudioStream(this);
        }

        public static TTSProvider SpeakAsync(string text, Int32 volumePercent) {
            TTSProvider provider = GetProvider();
            string fixtext = text.Replace("/", " ");
            provider.synth.SpeakAsync(fixtext, volumePercent);
            return provider;
        }

        override public int Read(byte[] buffer, int offset, int count)
        {
            if (filling) lock (waveBuffer)
                {
                    while (filling && !finishing && waveBuffer.Count < waveBuffer.Capacity / 2)
                        Monitor.Wait(waveBuffer);
                    filling = false;
                }

            lock (waveBuffer)
            {
                while (waveBuffer.Count <= 0 && !finishing)
                    Monitor.Wait(waveBuffer);
                int amount = waveBuffer.Read(buffer, offset, count);
                if (amount <= 0)
                    finished = true;
                Monitor.PulseAll(waveBuffer);
                return amount;
            }
        }

        public WaveFormat WaveFormat
        {
            get { return synth.WaveFormat; }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            lock (waveBuffer)
                while (waveBuffer.Count > 0)
                    Monitor.Wait(waveBuffer);
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                return 0;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (count <= 0 || finishing)
                return;
            lock (waveBuffer) while (true)
            {
                int wroteLen = waveBuffer.Write(buffer, offset, count);
                count -= wroteLen;
                offset += wroteLen;
                Monitor.PulseAll(waveBuffer);
                if (count <= 0 || finishing)
                    break;
                Monitor.Wait(waveBuffer);
            }
        }

        public override void Close()
        {
            lock (waveBuffer)
            {
                finishing = true;
                Monitor.PulseAll(waveBuffer);
                while (!finished)
                    Monitor.Wait(waveBuffer);
            }
        }
    }
}
