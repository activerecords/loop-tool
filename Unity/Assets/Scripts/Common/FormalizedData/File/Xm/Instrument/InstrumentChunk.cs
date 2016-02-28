using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Xm
{
	public class InstrumentChunk
	{
		private UInt32 instrumentSize;				// ?	4	(dword)	Instrument size
		private string instrumentName;				// +4	22	(char)	Instrument name
		private Byte instrumentType;				// +26	1	(byte)	Instrument type (always 0)
		private UInt16 numberOfSamplesInInstrument;	// +27	2	(word)	Number of samples in instrument

		private UInt32 sampleHeaderSize;			// +29	4	(dword)	Sample header size
		private Byte[] sampleNumberForAllNotes;		// +33	96	(byte)	Sample number for all notes
		private Byte[] pointsForVolumeEnvelope;		// +129	48	(word)	Points for volume envelope
		private UInt16[] pointsForVolumeEnvelopeX;	// +129	48	(word)	Points for volume envelopeX
		private UInt16[] pointsForVolumeEnvelopeY;	// +129	48	(word)	Points for volume envelopeY
		private Byte[] pointsForPanningEnvelope;	// +177	48	(word)	Points for panning envelope
		private UInt16[] pointsForPanningEnvelopeX;	// +177	48	(word)	Points for panning envelopeX
		private UInt16[] pointsForPanningEnvelopeY;	// +177	48	(word)	Points for panning envelopeY

		private Byte numberOfVolumePoints;			// +225	1	(byte)	Number of volume points
		private Byte numberOfPanningPoints;			// +226	1	(byte)	Number of panning points
		private Byte volumeSustainPoint;			// +227	1	(byte)	Volume sustain point
		private Byte volumeLoopStartPoint;			// +228	1	(byte)	Volume loop start point
		private Byte volumeLoopEndPoint;			// +229	1	(byte)	Volume loop end point
		private Byte panningSustainPoint;			// +230	1	(byte)	Panning sustain point
		private Byte panningLoopStartPoint;			// +231	1	(byte)	Panning loop start point
		private Byte panningLoopEndPoint;			// +232	1	(byte)	Panning loop end point
		private Byte volumeType;					// +233	1	(byte)	Volume  type: bit 0: On; 1: Sustain; 2: Loop
		private Byte panningType;					// +234	1	(byte)	Panning type: bit 0: On; 1: Sustain; 2: Loop
		private Byte vibratoType;					// +235	1	(byte)	Vibrato type
		private Byte vibratoSweep;					// +236	1	(byte)	Vibrato sweep
		private Byte vibratoDepth;					// +237	1	(byte)	Vibrato depth
		private Byte vibratoRate;					// +238	1	(byte)	Vibrato rate
		private UInt16 volumeFadeout;				// +239	2	(word)	Volume fadeout
		private Byte[] instrumentReserved;			// +241	11	(word)	Reserved

		private SampleChunk[] sampleChunkArray;

		public InstrumentChunk( ByteArray aByteArray )
		{
			instrumentSize = aByteArray.ReadUInt32();
			instrumentName = aByteArray.ReadString( 22 );
			instrumentType = aByteArray.ReadByte();
			numberOfSamplesInInstrument = aByteArray.ReadUInt16();

			Logger.LogError( instrumentName );

			if( numberOfSamplesInInstrument == 0 )
			{
				aByteArray.ReadBytes( ( int )instrumentSize - 29 );
			}
			else
			{
				sampleHeaderSize = aByteArray.ReadUInt32();
				sampleNumberForAllNotes = aByteArray.ReadBytes( 96 );
				//pointsForVolumeEnvelope = byteArrayLittle.ReadBytes( 48 );
				//pointsForPanningenvelope = byteArrayLittle.ReadBytes( 48 );

				pointsForVolumeEnvelopeX = new UInt16[12];
				pointsForVolumeEnvelopeY = new UInt16[12];

				for( int i = 0; i < 12; i++ )
				{
					pointsForVolumeEnvelopeX[i] = aByteArray.ReadUInt16();
					pointsForVolumeEnvelopeY[i] = aByteArray.ReadUInt16();

					Logger.LogError( "X:" + pointsForVolumeEnvelopeX[i] + ", Y:" + pointsForVolumeEnvelopeY[i] );
				}

				pointsForPanningEnvelopeX = new UInt16[12];
				pointsForPanningEnvelopeY = new UInt16[12];

				for( int i = 0; i < 12; i++ )
				{
					pointsForPanningEnvelopeX[i] = aByteArray.ReadUInt16();
					pointsForPanningEnvelopeY[i] = aByteArray.ReadUInt16();
				}

				//Debug.Log( "Points for volume envelope:" + System.Text.Encoding.ASCII.GetString( pointsForVolumeEnvelope ) );
				//Debug.Log( "Points for panning envelope:" + System.Text.Encoding.ASCII.GetString( pointsForPanningenvelope ) );

				numberOfVolumePoints = aByteArray.ReadByte();

				Logger.LogError( "Number of Volume Points:" + numberOfVolumePoints );

				numberOfPanningPoints = aByteArray.ReadByte();
				volumeSustainPoint = aByteArray.ReadByte();
				volumeLoopStartPoint = aByteArray.ReadByte();
				volumeLoopEndPoint = aByteArray.ReadByte();
				panningSustainPoint = aByteArray.ReadByte();
				panningLoopStartPoint = aByteArray.ReadByte();
				panningLoopEndPoint = aByteArray.ReadByte();
				volumeType = aByteArray.ReadByte();
				panningType = aByteArray.ReadByte();
				vibratoType = aByteArray.ReadByte();
				vibratoSweep = aByteArray.ReadByte();
				vibratoDepth = aByteArray.ReadByte();
				vibratoRate = aByteArray.ReadByte();
				volumeFadeout = aByteArray.ReadUInt16();
				instrumentReserved = aByteArray.ReadBytes( 22 );

				sampleChunkArray = new SampleChunk[numberOfSamplesInInstrument];

				for( int i = 0; i < numberOfSamplesInInstrument; i++ )
				{
					sampleChunkArray[i] = new SampleChunk( aByteArray );
				}

				for( int i = 0; i < numberOfSamplesInInstrument; i++ )
				{
					sampleChunkArray[i].ReadSampleData( aByteArray );
				}
			}
		}

		public UInt32 GetInstrumentSize()
		{
			return instrumentSize;				// ?	4	(dword) Instrument size
		}

		public string GetInstrumentName()
		{
			return instrumentName;				// +4	22	(char) Instrument name
		}

		public Byte GetInstrumentType()
		{
			return instrumentType;				// +26	1	(byte) Instrument type (always 0)
		}

		public UInt16 GetNumberOfSamplesInInstrument()
		{
			return numberOfSamplesInInstrument;	// +27	2	(word) Number of samples in instrument
		}

		public UInt32 GetSampleHeaderSize()
		{
			return sampleHeaderSize;			// +29	4	(dword)	Sample header size
		}

		public Byte[] GetSampleNumberForAllNotes()
		{
			return sampleNumberForAllNotes;		// +33	96	(byte)	Sample number for all notes
		}

		public Byte[] GetPointsForVolumeEnvelope()
		{
			return pointsForVolumeEnvelope;		// +129	48	(word)	Points for volume envelope
		}

		public UInt16[] GetPointsForVolumeEnvelopeX()
		{
			return pointsForVolumeEnvelopeX;	// +129	48	(word)	Points for volume envelopeX
		}

		public UInt16[] GetPointsForVolumeEnvelopeY()
		{
			return pointsForVolumeEnvelopeY;	// +129	48	(word)	Points for volume envelopeY
		}

		public Byte[] GetPointsForPanningEnvelope()
		{
			return pointsForPanningEnvelope;	// +177	48	(word)	Points for panning envelope
		}

		public UInt16[] GetPointsForPanningEnvelopeX()
		{
			return pointsForPanningEnvelopeX;	// +177	48	(word)	Points for panning envelopeX
		}

		public UInt16[] GetPointsForPanningEnvelopeY()
		{
			return pointsForPanningEnvelopeY;	// +177	48	(word)	Points for panning envelopeY
		}

		public Byte GetNumberOfVolumePoints()
		{
			return numberOfVolumePoints;		// +225	1	(byte)	Number of volume points
		}

		public Byte GetNumberOfPanningPoints()
		{
			return numberOfPanningPoints;		// +226	1	(byte)	Number of panning points
		}

		public Byte GetVolumeSustainPoint()
		{
			return volumeSustainPoint;			// +227	1	(byte)	Volume sustain point
		}

		public Byte GetVolumeLoopStartPoint()
		{
			return volumeLoopStartPoint;		// +228	1	(byte)	Volume loop start point
		}

		public Byte GetVolumeLoopEndPoint()
		{
			return volumeLoopEndPoint;			// +229	1	(byte)	Volume loop end point
		}

		public Byte GetPanningSustainPoint()
		{
			return panningSustainPoint;			// +230	1	(byte)	Panning sustain point
		}

		public Byte GetPanningLoopStartPoint()
		{
			return panningLoopStartPoint;		// +231	1	(byte)	Panning loop start point
		}

		public Byte GetPanningLoopEndPoint()
		{
			return panningLoopEndPoint;			// +232	1	(byte)	Panning loop end point
		}

		public Byte GetVolumeType()
		{
			return volumeType;					// +233	1	(byte)	Volume  type: bit 0: On; 1: Sustain; 2: Loop
		}

		public Byte GetPanningType()
		{
			return panningType;					// +234	1	(byte)	Panning type: bit 0: On; 1: Sustain; 2: Loop
		}

		public Byte GetVibratoType()
		{
			return vibratoType;					// +235	1	(byte)	Vibrato type
		}

		public Byte GetVibratoSweep()
		{
			return vibratoSweep;				// +236	1	(byte)	Vibrato sweep
		}

		public Byte GetVibratoDepth()
		{
			return vibratoDepth;				// +237	1	(byte)	Vibrato depth
		}

		public Byte GetVibratoRate()
		{
			return vibratoRate;					// +238	1	(byte)	Vibrato rate
		}

		public UInt16 GetVolumeFadeout()
		{
			return volumeFadeout;				// +239	2	(word)	Volume fadeout
		}

		public Byte[] GetInstrumentReserved()
		{
			return instrumentReserved;			// +241	11	(word)	Reserved
		}

		public SampleChunk[] GetSampleChunkArray()
		{
			return sampleChunkArray;
		}

		public void Display()
		{
			Logger.LogNormal( "Instrument size:" + GetInstrumentSize().ToString( "x08" ) );
			Logger.LogNormal( "Instrument name:" + GetInstrumentName() );
			Logger.LogNormal( "Instrument type:" + GetInstrumentType().ToString( "x02" ) );

			Logger.LogNormal( "Number of samples in instrument:" + GetNumberOfSamplesInInstrument().ToString( "x04" ) );

			if( GetNumberOfSamplesInInstrument() > 0 )
			{
				Logger.LogNormal( "Sample header size:" + GetSampleHeaderSize().ToString( "x08" ) );
				Logger.LogNormal( "Sample number for all notes:" + System.Text.Encoding.ASCII.GetString( GetSampleNumberForAllNotes() ) );

				for( int i = 0; i < 12; i++ )
				{
					Logger.LogNormal( "Points for volume envelope" + i.ToString() + ":" + GetPointsForVolumeEnvelopeX()[i] + ", " + GetPointsForVolumeEnvelopeY()[i] );
				}

				for( int i = 0; i < 12; i++ )
				{
					Logger.LogNormal( "Points for pan envelope" + i.ToString() + ":" + GetPointsForPanningEnvelopeX()[i] + ", " + GetPointsForPanningEnvelopeY()[i] );
				}

				Logger.LogNormal( "Number of volume points:" + GetNumberOfVolumePoints().ToString( "x02" ) );
				Logger.LogNormal( "Number of panning points:" + GetNumberOfPanningPoints().ToString( "x02" ) );
				Logger.LogNormal( "Volume sustain point:" + GetVolumeSustainPoint().ToString( "x02" ) );
				Logger.LogNormal( "Volume loop start point:" + GetVolumeLoopStartPoint().ToString( "x02" ) );
				Logger.LogNormal( "Volume loop end point:" + GetVolumeLoopEndPoint().ToString( "x02" ) );
				Logger.LogNormal( "Panning sustain point:" + GetPanningSustainPoint().ToString( "x02" ) );
				Logger.LogNormal( "Panning loop start point:" + GetPanningLoopStartPoint().ToString( "x02" ) );
				Logger.LogNormal( "Panning loop end point:" + GetPanningLoopEndPoint().ToString( "x02" ) );
				Logger.LogNormal( "Volume  type:" + GetVolumeType().ToString( "x02" ) );
				Logger.LogNormal( "Panning type:" + GetPanningType().ToString( "x02" ) );
				Logger.LogNormal( "Vibrato type:" + GetVibratoType().ToString( "x02" ) );
				Logger.LogNormal( "Vibrato sweep:" + GetVibratoSweep().ToString( "x02" ) );
				Logger.LogNormal( "Vibrato depth:" + GetVibratoDepth().ToString( "x02" ) );
				Logger.LogNormal( "Vibrato rate:" + GetVibratoRate().ToString( "x02" ) );
				Logger.LogNormal( "Volume fadeout:" + GetVolumeFadeout().ToString( "x04" ) );
				Logger.LogNormal( "Reserved:" + System.Text.Encoding.ASCII.GetString( GetInstrumentReserved() ) );

				for( int i = 0; i < GetNumberOfSamplesInInstrument(); i++ )
				{
					Logger.LogNormal( "■Sample Header" + i.ToString() + ":" + GetSampleChunkArray()[i] );
				}

				for( int i = 0; i < GetNumberOfSamplesInInstrument(); i++ )
				{
					Logger.LogNormal( "■Sample Header" + i.ToString() + ":" + GetSampleChunkArray()[i] );
				}
			}
		}
	}
}
