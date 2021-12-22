using System.Runtime.InteropServices;

namespace OP_Engine.Sounds.WaveOut
{
    [StructLayout(LayoutKind.Sequential)]
    public class WaveFormat
    {
        public short FormatTag;
        public short Channels;
        public int SamplesPerSec;
        public int AvgBytesPerSec;
        public short BlockAlign;
        public short BitsPerSample;
        public short Size;

        public WaveFormat(int rate, int bits, int channels)
        {
            FormatTag = (short)WaveFormats.Pcm;
            Channels = (short)channels;
            SamplesPerSec = rate;
            BitsPerSample = (short)bits;
            Size = 0;

            BlockAlign = (short)(channels * (bits / 8));
            AvgBytesPerSec = SamplesPerSec * BlockAlign;
        }
    }
}
