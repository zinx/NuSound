using NAudio.Wave;
using System;

namespace ACT_Plugin
{
    class AudioPlayer
    {
        static Guid deviceId = DirectSoundOut.DSDEVID_DefaultPlayback;
        public static Guid DeviceIdentifier {
            get { return deviceId; }
            set { deviceId = value; }
        }

        static void PlayProvider(IWaveProvider wave)
        {
            IWavePlayer player = new DirectSoundOut(deviceId);
            player.Init(wave);
            player.Play();
        }

        public static void PlayWaveFile(string file, Int32 volumePercent)
        {
            AudioFileReader wave = new AudioFileReader(file);
            if (volumePercent != 100)
                wave.Volume = volumePercent / 100.0f;
            PlayProvider(wave);
        }

        public static void PlayTTS(string text, Int32 volumePercent)
        {
            IWaveProvider wave = TTSProvider.SpeakAsync(text, volumePercent);
            PlayProvider(wave);
        }
    }
}
