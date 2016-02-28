using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Xm
{
	public class SampleChunk
	{
		private UInt32 sampleLength;		// ?	4	(dword)	Sample length
		private UInt32 sampleLoopStart;		// +4	4	(dword)	Sample loop start
		private UInt32 sampleLoopLength;	// +8	4	(dword)	Sample loop length

		private Byte volume;				// +12	1	(byte)	Volume
		private Byte finetune;				// +13	1	(byte)	Finetune (signed byte -128..+127)
		private Byte type;					// +14	1	(byte)	Type: Bit	0-1:
											//									0 = No loop,
											//									1 = Forward loop,
											//									2 = Ping-pong loop;
											//								4: 16-bit sampledata
		private Byte panning;				// +15	1	(byte)	Panning (0-255)
		private SByte relativeNoteNumber;	// +16	1	(byte)	Relative note number (signed byte)
		private Byte sampleReserved;		// +17	1	(byte)	Reserved
		private string sampleName;			// +18	22	(char)	Sample name
		private SByte[] sampleData;			// ?	?			Sample data (signed): The samples are stored as delta values.
		private Int16[] sampleData16;

		private float[] waveData;

		public SampleChunk( ByteArray aByteArray )
		{
			sampleLength = aByteArray.ReadUInt32();
			sampleLoopStart = aByteArray.ReadUInt32();
			sampleLoopLength = aByteArray.ReadUInt32();

			volume = aByteArray.ReadByte();
			finetune = aByteArray.ReadByte();
			type = aByteArray.ReadByte();
			panning = aByteArray.ReadByte();
			relativeNoteNumber = aByteArray.ReadSByte();
			sampleReserved = aByteArray.ReadByte();
			sampleName = aByteArray.ReadString( 22 );
		}

		public void ReadSampleData( ByteArray aByteArray )
		{
			Int32 data = 0;

			if( ( type & 0x10 ) == 0x00 )
			{
				sampleData = new SByte[sampleLength];
				waveData = new float[sampleLength];

				for( int i = 0; i < sampleLength; i++ )
				{
					sampleData[i] = aByteArray.ReadSByte();
					data += ( Int32 )sampleData[i];
					waveData[i] = ( float )data / 0xFF;
					//Debug.Log( i.ToString() + ":" + waveData[i].ToString() );
				}
			}
			else
			{
				sampleData16 = new Int16[sampleLength / 2];
				waveData = new float[sampleLength / 2];

				for( int i = 0; i < sampleLength / 2; i++ )
				{
					sampleData16[i] = aByteArray.ReadInt16();
					data += ( Int32 )sampleData16[i];
					waveData[i] = ( float )data / 0xFFFF;
					//Debug.Log( i.ToString() + ":" + waveData[i].ToString() );
				}
			}
		}

		public UInt32 GetSampleLength()
		{
			return sampleLength;			// ?	4	(dword)	Sample length
		}

		public UInt32 GetSampleLoopStart()
		{
			return sampleLoopStart;		// +4	4	(dword)	Sample loop start
		}

		public UInt32 GetSampleLoopLength()
		{
			return sampleLoopLength;		// +8	4	(dword)	Sample loop length
		}

		public Byte GetVolume()
		{
			return volume;					// +12	1	(byte)	Volume
		}

		public Byte GetFinetune()
		{
			return finetune;				// +13	1	(byte)	Finetune (signed byte -128..+127)
		}

		public Byte GetLoopType()
		{
			return type;					// +14	1	(byte)	Type: Bit	0-1:
		}
		//									0 = No loop,
		//									1 = Forward loop,
		//									2 = Ping-pong loop;
		//								4: 16-bit sampledata

		public Byte GetPanning()
		{
			return panning;				// +15	1	(byte)	Panning (0-255)
		}

		public SByte GetRelativeNoteNumber()
		{
			return relativeNoteNumber;	// +16	1	(byte)	Relative note number (signed byte)
		}

		public Byte GetSampleReserved()
		{
			return sampleReserved;			// +17	1	(byte)	Reserved
		}

		public string GetSampleName()
		{
			return sampleName;			// +18	22	(char)	Sample name
		}

		public float[] GetWaveData()
		{
			return waveData;
		}

		public void Display()
		{
			Logger.LogNormal( "Sample length:" + GetSampleLength().ToString( "x08" ) );
			Logger.LogNormal( "Sample loop start:" + GetSampleLoopStart().ToString( "x08" ) );
			Logger.LogNormal( "Sample loop length:" + GetSampleLoopLength().ToString( "x08" ) );

			Logger.LogNormal( "Volume:" + GetVolume().ToString( "x02" ) );
			Logger.LogNormal( "Finetune:" + GetFinetune().ToString( "x02" ) );
			Logger.LogNormal( "Type:" + GetLoopType().ToString( "x02" ) );
			Logger.LogNormal( "Panning:" + GetPanning().ToString( "x02" ) );
			Logger.LogNormal( "Relative note number:" + GetRelativeNoteNumber().ToString( "x02" ) );
			Logger.LogNormal( "Reserved:" + GetSampleReserved().ToString( "x02" ) );

			Logger.LogNormal( "Sample name:" + GetSampleName() );
		}
	}
}
