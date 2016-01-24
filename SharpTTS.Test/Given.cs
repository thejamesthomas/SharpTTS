using System.IO;
using NAudio.Wave;

namespace SharpTTS.Test
{
    class Given
    {
        public static WaveStream AWaveStream()
        {
            return new RawSourceWaveStream(Stream.Null, new WaveFormat());
        }
    }
}
