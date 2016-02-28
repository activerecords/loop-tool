using System;
using System.IO;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Xm
{
	public class XmFile
	{
		private string idText;				// 0	17	(char)	ID text: 'Extended module: '
		private string moduleName;			// 17	20	(char)	Module name, padded with spaces
		private Byte _1A;					// 37	1	(char)	$1a
		private string trackerName;			// 38	20	(char)	Tracker name
		private UInt16 versionNumber;		// 58	2	(word)	Version number, hi-byte major and low-byte minor
											//					The current format is version $0103
		private UInt32 headerSize;			// 60	4	(dword)	Header size
		private UInt16 songLength;			// +4	2	(word)	Song length (in patten order table)
		private UInt16 restartPosition;		// +6	2	(word)	Restart position
		private UInt16 numberOfChannels;	// +8	2	(word)	Number of channels (2,4,6,8,10,...,32)
		private UInt16 numberOfPatterns;	// +10	2	(word)	Number of patterns (max 256)
		private UInt16 numberOfInstruments;	// +12	2	(word)	Number of instruments (max 128)
		private UInt16 flags;				// +14	2	(word)	Flags: bit 0:
											//									0 = Amiga frequency table (see below);
											//									1 = Linear frequency table
		private UInt16 defaultTempo;		// +16	2	(word) Default tempo
		private UInt16 defaultBpm;			// +18	2	(word) Default BPM
		private Byte[] patternOrderTable;	// +20	256	(byte) Pattern order table

		private PatternChunk[] patternChunkArray;
		private InstrumentChunk[] instrumentChunkArray;

		public XmFile( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayLittle( aStream );

			idText = lByteArray.ReadString( 17 );
			moduleName = lByteArray.ReadString( 20 );
			_1A = lByteArray.ReadByte();
			trackerName = lByteArray.ReadString( 20 );
			versionNumber = lByteArray.ReadUInt16();

			headerSize = lByteArray.ReadUInt32();
			songLength = lByteArray.ReadUInt16();
			restartPosition = lByteArray.ReadUInt16();
			numberOfChannels = lByteArray.ReadUInt16();
			numberOfPatterns = lByteArray.ReadUInt16();
			numberOfInstruments = lByteArray.ReadUInt16();
			flags = lByteArray.ReadUInt16();
			defaultTempo = lByteArray.ReadUInt16();
			defaultBpm = lByteArray.ReadUInt16();
			patternOrderTable = lByteArray.ReadBytes( 256 );

			patternChunkArray = new PatternChunk[numberOfPatterns];
			instrumentChunkArray = new InstrumentChunk[numberOfInstruments];

			for( int i = 0; i < numberOfPatterns; i++ )
			{
				patternChunkArray[i] = new PatternChunk( lByteArray );
			}

			for( int i = 0; i < numberOfInstruments; i++ )
			{
				instrumentChunkArray[i] = new InstrumentChunk( lByteArray );
			}
		}

		public void Display()
		{
			Logger.LogNormal( "ID text:" + idText );
			Logger.LogNormal( "Module name:" + moduleName );
			Logger.LogNormal( "$1a:" + _1A.ToString( "x02" ) );
			Logger.LogNormal( "Tracker name:" + trackerName );
			Logger.LogNormal( "Version number:" + versionNumber.ToString( "x04" ) );

			Logger.LogNormal( "Header size:" + headerSize.ToString( "x08" ) );
			Logger.LogNormal( "Song length:" + songLength.ToString( "x04" ) );
			Logger.LogNormal( "Restart position:" + restartPosition.ToString( "x04" ) );
			Logger.LogNormal( "Number of channels:" + numberOfChannels.ToString( "x04" ) );
			Logger.LogNormal( "Number of patterns:" + numberOfPatterns.ToString( "x04" ) );
			Logger.LogNormal( "Number of instruments:" + numberOfInstruments.ToString( "x04" ) );
			Logger.LogNormal( "Flags:" + flags.ToString( "x04" ) );
			Logger.LogNormal( "Default tempo:" + defaultTempo.ToString( "x04" ) );
			Logger.LogNormal( "Default BPM:" + defaultBpm.ToString( "x04" ) );
			Logger.LogNormal( "Pattern order table:" + System.Text.Encoding.ASCII.GetString( patternOrderTable ) );
		}
		
		public string GetIdText()
		{
			return idText;
		}

		public string GetModuleName()
		{
			return moduleName;
		}

		public Byte Get_1A()
		{
			return _1A;
		}

		public string GetTrackerName()
		{
			return trackerName;
		}

		public UInt16 GetVersionNumber()
		{
			return versionNumber;
		}

		public UInt32 GetHeaderSize()
		{
			return headerSize;
		}

		public UInt16 GetSongLength()
		{
			return songLength;
		}

		public UInt16 GetRestartPosition()
		{
			return restartPosition;
		}

		public UInt16 GetNumberOfChannels()
		{
			return numberOfChannels;
		}

		public UInt16 GetNumberOfPatterns()
		{
			return numberOfPatterns;
		}

		public UInt16 GetNumberOfInstruments()
		{
			return numberOfInstruments;
		}

		public UInt16 GetFlags()
		{
			return flags;
		}

		public UInt16 GetDefaultTempo()
		{
			return defaultTempo;
		}

		public UInt16 GetDefaultBpm()
		{
			return defaultBpm;
		}

		public Byte[] GetPatternOrderTable()
		{
			return patternOrderTable;
		}

		public PatternChunk[] GetPatternChunkArray()
		{
			return patternChunkArray;
		}

		public InstrumentChunk[] GetInstrumentChunkArray()
		{
			return instrumentChunkArray;
		}
	}
}
