using System;
using System.IO;

namespace OP_Engine.Sounds.WaveOut
{
	public class WaveStream : Stream, IDisposable
	{
		private Stream Stream;
		private long DataPos;
		private long WaveLength;

		public WaveFormat Format;

		public WaveStream(Stream S)
		{
			Stream = S;
			ReadHeader();
		}

		public WaveStream(string fileName) : this(new FileStream(fileName, FileMode.Open))
		{

		}

		private string ReadChunk(BinaryReader reader)
		{
			byte[] ch = new byte[4];
			reader.Read(ch, 0, ch.Length);
			return System.Text.Encoding.ASCII.GetString(ch);
		}

		private void ReadHeader()
		{
			BinaryReader Reader = new BinaryReader(Stream);
			Reader.ReadInt32();

			int len = Reader.ReadInt32();

			Format = new WaveFormat(44100, 16, 2);
			Format.FormatTag = Reader.ReadInt16();
			Format.Channels = Reader.ReadInt16();
			Format.SamplesPerSec = Reader.ReadInt32();
			Format.AvgBytesPerSec = Reader.ReadInt32();
			Format.BlockAlign = Reader.ReadInt16();
			Format.BitsPerSample = Reader.ReadInt16();

			len -= 16;
			while (len > 0)
			{
				Reader.ReadByte();
				len--;
			}

			while (Stream.Position < Stream.Length && ReadChunk(Reader) != "data");

			WaveLength = Reader.ReadInt32();
			DataPos = Stream.Position;

			Position = 0;
		}

		~WaveStream()
		{
			Dispose();
		}

		public new void Dispose()
		{
			if (Stream != null)
            {
				Stream.Close();
			}
				
			GC.SuppressFinalize(this);
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override long Length
		{
			get { return WaveLength; }
		}

		public override long Position
		{
			get { return Stream.Position - DataPos; }
			set { Seek(value, SeekOrigin.Begin); }
		}

		public override void Close()
		{
			Dispose();
		}

		public override void Flush()
		{

		}

		public override void SetLength(long len)
		{
			throw new InvalidOperationException();
		}

		public override long Seek(long pos, SeekOrigin o)
		{
			switch (o)
			{
				case SeekOrigin.Begin:
					Stream.Position = pos + DataPos;
					break;
				case SeekOrigin.Current:
					Stream.Seek(pos, SeekOrigin.Current);
					break;
				case SeekOrigin.End:
					Stream.Position = DataPos + WaveLength - pos;
					break;
			}
			return Position;
		}

		public override int Read(byte[] buf, int ofs, int count)
		{
			int toread = (int)Math.Min(count, WaveLength - Position);
			return Stream.Read(buf, ofs, toread);
		}
		public override void Write(byte[] buf, int ofs, int count)
		{
			throw new InvalidOperationException();
		}
	}
}
