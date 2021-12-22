using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace OP_Engine.Sounds.WaveOut
{
    public delegate void BufferFillEventHandler(Stream stream, IntPtr data, int size);

    public class WavePlayer : IDisposable
    {
        public IntPtr WaveOut;
        public WaveBuffer Buffers; // linked list
        public WaveBuffer CurrentBuffer;
        public Thread Thread;
        public BufferFillEventHandler FillProc;
        public bool Finished;
        public readonly byte zero;

        public WaveNative.WaveDelegate BufferProc = new WaveNative.WaveDelegate(WaveBuffer.WaveOutProc);

        public static int DeviceCount { get { return WaveNative.waveOutGetNumDevs(); } }

        public WavePlayer(int device, WaveFormat format, int bufferSize, int bufferCount, BufferFillEventHandler fillProc)
        {
            if (format.BitsPerSample == 8)
            {
                zero = 128;
            }
            else
            {
                zero = 0;
            }
             
            FillProc = fillProc;
            WaveNative.waveOutOpen(out WaveOut, device, format, BufferProc, 0, WaveNative.CALLBACK_FUNCTION);
            AllocateBuffers(bufferSize, bufferCount);
            Thread = new Thread(new ThreadStart(ThreadProc));
            Thread.Start();
        }

        private void ThreadProc()
        {
            while (!Finished)
            {
                Advance();

                if (FillProc != null && 
                    !Finished)
                {
                    FillProc(CurrentBuffer.Stream, CurrentBuffer.Data, CurrentBuffer.Size);
                }
                else
                {
                    byte v = zero;

                    byte[] b = new byte[CurrentBuffer.Size];
                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i] = v;
                    }

                    Marshal.Copy(b, 0, CurrentBuffer.Data, b.Length);
                }

                CurrentBuffer.Play();
            }

            WaitForAllBuffers();
        }

        private void AllocateBuffers(int bufferSize, int bufferCount)
        {
            FreeBuffers();

            if (bufferCount > 0)
            {
                Buffers = new WaveBuffer(WaveOut, bufferSize);
                WaveBuffer Prev = Buffers;

                try
                {
                    for (int i = 1; i < bufferCount; i++)
                    {
                        WaveBuffer Buf = new WaveBuffer(WaveOut, bufferSize);
                        Prev.NextBuffer = Buf;
                        Prev = Buf;
                    }
                }
                finally
                {
                    Prev.NextBuffer = Buffers;
                }
            }
        }

        private void FreeBuffers()
        {
            CurrentBuffer = null;

            if (Buffers != null)
            {
                WaveBuffer First = Buffers;
                Buffers = null;

                WaveBuffer Current = First;

                do
                {
                    WaveBuffer Next = Current.NextBuffer;
                    Current.Dispose();
                    Current = Next;
                } while (Current != First);
            }
        }

        private void Advance()
        {
            if (CurrentBuffer == null)
            {
                CurrentBuffer = Buffers;
            }
            else
            {
                CurrentBuffer = CurrentBuffer.NextBuffer;
            }

            CurrentBuffer.WaitFor();
        }

        private void WaitForAllBuffers()
        {
            WaveBuffer Buf = Buffers;
            while (Buf.NextBuffer != Buffers)
            {
                Buf.WaitFor();
                Buf = Buf.NextBuffer;
            }
        }

        ~WavePlayer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Thread != null)
            {
                try
                {
                    Finished = true;
                    if (WaveOut != IntPtr.Zero)
                    {
                        WaveNative.waveOutReset(WaveOut);
                    }

                    Thread.Join();
                    FillProc = null;
                    FreeBuffers();

                    if (WaveOut != IntPtr.Zero)
                    {
                        WaveNative.waveOutClose(WaveOut);
                    }
                }
                finally
                {
                    Thread = null;
                    WaveOut = IntPtr.Zero;
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
