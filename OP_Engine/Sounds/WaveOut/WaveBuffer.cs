using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace OP_Engine.Sounds.WaveOut
{
    public class WaveBuffer : IDisposable
    {
        public Stream Stream;
        public WaveBuffer NextBuffer;

        public AutoResetEvent PlayEvent = new AutoResetEvent(false);
        public IntPtr WaveOut;

        public WaveNative.WaveHdr Header;
        public byte[] HeaderData;
        public GCHandle HeaderHandle;
        public GCHandle HeaderDataHandle;

        public bool Playing;

        public int Size { get { return Header.dwBufferLength; } }

        public IntPtr Data { get { return Header.lpData; } }

        public WaveBuffer(IntPtr waveOutHandle, int size)
        {
            WaveOut = waveOutHandle;

            HeaderHandle = GCHandle.Alloc(Header, GCHandleType.Pinned);
            Header.dwUser = (IntPtr)GCHandle.Alloc(this);
            HeaderData = new byte[size];
            HeaderDataHandle = GCHandle.Alloc(HeaderData, GCHandleType.Pinned);
            Header.lpData = HeaderDataHandle.AddrOfPinnedObject();
            Header.dwBufferLength = size;
            WaveNative.waveOutPrepareHeader(WaveOut, ref Header, Marshal.SizeOf(Header));
        }

        internal static void WaveOutProc(int uMsg, ref WaveNative.WaveHdr wavhdr)
        {
            if (uMsg == WaveNative.MM_WOM_DONE)
            {
                try
                {
                    GCHandle h = (GCHandle)wavhdr.dwUser;
                    WaveBuffer buf = (WaveBuffer)h.Target;
                    buf.OnCompleted();
                }
                catch
                {

                }
            }
        }

        public bool Play()
        {
            lock (this)
            {
                PlayEvent.Reset();
                Playing = WaveNative.waveOutWrite(WaveOut, ref Header, Marshal.SizeOf(Header)) == WaveNative.MMSYSERR_NOERROR;
                return Playing;
            }
        }

        public void WaitFor()
        {
            if (Playing)
            {
                Playing = PlayEvent.WaitOne();
            }
            else
            {
                Thread.Sleep(0);
            }
        }

        public void OnCompleted()
        {
            PlayEvent.Set();
            Playing = false;
        }

        ~WaveBuffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Header.lpData != IntPtr.Zero)
            {
                WaveNative.waveOutUnprepareHeader(WaveOut, ref Header, Marshal.SizeOf(Header));
                HeaderHandle.Free();
                Header.lpData = IntPtr.Zero;
            }

            PlayEvent.Close();

            if (HeaderDataHandle.IsAllocated)
            {
                HeaderDataHandle.Free();
            }

            GC.SuppressFinalize(this);
        }
    }
}
