using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.Struct;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Music
{
	public class CNsfHeader
	{
		private Byte[] dataArray;

		private byte[] nesm = new byte[5];		// ?????q
		private Byte version;					// ?o?[?W????					0x05
		private Byte musicNum;					// ????							0x06
		private Byte startMusicNumber;			// ??????????					0x07
		private UInt16 loadAddress;				// ???[?h?J?n?A?h???X			0x0809
		private UInt16 initAddress;				// ???????A?h???X				0x0A0B
		private UInt16 startAddress;			// ?J?n?A?h???X					0x0C0D
		private byte[] title = new byte[32];	// ?^?C?g??						0x0E
		private byte[] artist = new byte[32];	// ?A?[?e?B?X?g??				0x2E
		private byte[] maker = new byte[32];	// ????????????					0x4E
		private UInt16 frequency;				// ?????A?h???X?????o??????		0x6E6F
		private Byte bank0;						// ?????o???N?w??(8000?`8FFF)	0x70
		private Byte bank1;						// ?????o???N?w??(9000?`9FFF)	0x71
		private Byte bank2;						// ?????o???N?w??(A000?`AFFF)	0x72
		private Byte bank3;						// ?????o???N?w??(B000?`BFFF)	0x73
		private Byte bank4;						// ?????o???N?w??(C000?`CFFF)	0x74
		private Byte bank5;						// ?????o???N?w??(D000?`DFFF)	0x75
		private Byte bank6;						// ?????o???N?w??(E000?`EFFF)	0x76
		private Byte bank7;						// ?????o???N?w??(F000?`FFFF)	0x77
		private UInt16 frequencyPal;			// ?????A?h???X?????o??????		0x7879
		private Byte mode;						// NTSC/PAL???[?h?w??			0x7A
		private Byte expectFlag;				// ?g???t???O					0x7B

		public CNsfHeader( Byte[] aDataArray )
		{
			dataArray = new Byte[0x80];
			MemoryTool.memcpy( dataArray, 0x00, aDataArray, 0x00, 0x80 );

			// nsf?w?b?_??????????
			ReadHeader();

			// nsf?w?b?_???\??
			PrintHeader();
		}

		public Byte GetByte( Byte addr )
		{
			return dataArray[addr];
		}

		public UInt16 GetWord( Byte addr )
		{
			return ( UInt16 )( ( UInt16 )dataArray[addr + 1] << 8 | ( UInt16 )dataArray[addr] );
		}

		public Byte GetMusicNum()
		{
			return musicNum;
		}

		public Byte GetStartMusicNumber()
		{
			return startMusicNumber;
		}

		public UInt16 GetLoadAddress()
		{
			return loadAddress;
		}

		public UInt16 GetInitAddress()
		{
			return initAddress;
		}

		public UInt16 GetStartAddress()
		{
			return startAddress;
		}

		public void ReadHeader()
		{
			MemoryTool.memcpy( nesm, 0x00, dataArray, 0x00, sizeof( Byte ) * 5 );

			version = GetByte( 0x05 );
			musicNum = GetByte( 0x06 );
			startMusicNumber = GetByte( 0x07 );
			loadAddress = GetWord( 0x08 );
			initAddress = GetWord( 0x0A );
			startAddress = GetWord( 0x0C );
			MemoryTool.memcpy( title, 0x00, dataArray, 0x0E, sizeof( Byte ) * 32 );
			MemoryTool.memcpy( artist, 0x00, dataArray, 0x2E, sizeof( Byte ) * 32 );
			MemoryTool.memcpy( maker, 0x00, dataArray, 0x4E, sizeof( Byte ) * 32 );
			frequency = GetWord( 0x6E );
			bank0 = GetByte( 0x70 );
			bank1 = GetByte( 0x71 );
			bank2 = GetByte( 0x72 );
			bank3 = GetByte( 0x73 );
			bank4 = GetByte( 0x74 );
			bank5 = GetByte( 0x75 );
			bank6 = GetByte( 0x76 );
			bank7 = GetByte( 0x77 );
			frequencyPal = GetWord( 0x78 );
			mode = GetByte( 0x7A );
			expectFlag = GetByte( 0x7B );

			if( nesm[0] != 'N' || nesm[1] != 'E' || nesm[2] != 'S' || nesm[3] != 'M' )
			{
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}
		}

		public void PrintHeader()
		{
			Logger.LogNormal( "?o?[?W????:%d\n" + version );
			Logger.LogNormal( "????:%d\n" + musicNum );
			Logger.LogNormal( "??????????:%d\n" + startMusicNumber );
			Logger.LogNormal( "???[?h?J?n?A?h???X:%04x\n" + loadAddress );
			Logger.LogNormal( "???????A?h???X:%04x\n" + initAddress );
			Logger.LogNormal( "?J?n?A?h???X:%04x\n" + startAddress );
			Logger.LogNormal( "?^?C?g??:%s\n" + title );
			Logger.LogNormal( "?A?[?e?B?X?g??:%s\n" + artist );
			Logger.LogNormal( "????????????:%s\n" + maker );
			Logger.LogNormal( "?????A?h???X?????o??????:%04x\n" + frequency );
			Logger.LogNormal( "?????o???N?w??(8000?`8FFF):%02x\n" + bank0 );
			Logger.LogNormal( "?????o???N?w??(9000?`9FFF):%02x\n" + bank1 );
			Logger.LogNormal( "?????o???N?w??(A000?`AFFF):%02x\n" + bank2 );
			Logger.LogNormal( "?????o???N?w??(B000?`BFFF):%02x\n" + bank3 );
			Logger.LogNormal( "?????o???N?w??(C000?`CFFF):%02x\n" + bank4 );
			Logger.LogNormal( "?????o???N?w??(D000?`DFFF):%02x\n" + bank5 );
			Logger.LogNormal( "?????o???N?w??(E000?`EFFF):%02x\n" + bank6 );
			Logger.LogNormal( "?????o???N?w??(F000?`FFFF):%02x\n" + bank7 );
			Logger.LogNormal( "?????A?h???X?????o??????(PAL):%04x\n" + frequencyPal );
			Logger.LogNormal( "NTSC/PAL???[?h?w??:%d\n" + mode );
			Logger.LogNormal( "?g???t???O:%d\n" + expectFlag );
			Logger.LogNormal( "\n" );
			
			if( bank0 != 0x00 || bank1 != 0x00 || bank2 != 0x00 || bank3 != 0x00 ||
				bank4 != 0x00 || bank5 != 0x00 || bank6 != 0x00 || bank7 != 0x00 )
			{
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}
		}
	}

	public class MusicNsf : IMusic
	{
		private Byte[] dataArray;
		private int dataSize;

		private CNsfHeader header;
		private char musicNumber;
		
		public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicNsf( string aPathFile )
		{

		}

		public MusicNsf( Stream aStream )
		{
			Byte[] lDataArray = new Byte[aStream.Length];

			aStream.Read( lDataArray, 0, lDataArray.Length );

			dataSize = lDataArray.Length - 0x80;

			if( dataSize >= 0x8000 )
			{
				Logger.LogError( "?T?|?[?g???????????t?@?C???????B" );
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}

			header = new CNsfHeader( lDataArray );

			dataArray = new Byte[0x10000];
			MemoryTool.memcpy( dataArray, 0x00, lDataArray, 0x80, dataSize );

			musicNumber = ( char )( header.GetStartMusicNumber() - 1 );

			Logger.LogNormal( "%d????????\n" + ( int )( musicNumber + 1 ) );
		}

		public void IncrementMusicNumber()
		{
			musicNumber++;

			if( musicNumber >= header.GetMusicNum() )
			{
				musicNumber = ( char )0;
			}

			Logger.LogNormal( "\n" );
			Logger.LogNormal( "?????X\n" );
			Logger.LogNormal( "%d????????\n" + musicNumber + 1 );
		}

		public void DecrementMusicNumber()
		{
			musicNumber--;

			if( musicNumber < 0 )
			{
				musicNumber = ( char )( header.GetMusicNum() - 1 );
			}

			Logger.LogNormal( "\n" );
			Logger.LogNormal( "?????X\n" );
			Logger.LogNormal( "%d????????\n" + musicNumber + 1 );
		}

		public Byte[] GetDataPoint()
		{
			return dataArray;
		}

		public int GetDataSize()
		{
			return dataSize;
		}

		public CNsfHeader GetHeader()
		{
			return header;
		}

		public char GetMusicNumber()
		{
			return musicNumber;
		}
	}
}
