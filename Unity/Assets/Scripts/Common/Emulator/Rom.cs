using System;

using Curan.Utility;

namespace Curan.Common.Emulator
{
	public class CNesHeader
	{
		private Byte[] dataArray;

		private Byte[] nes;		// ?????q
		private Byte prgBanks;	// PRG-ROM?y?[?W??
		private Byte chrBanks;	// CHR0ROM?y?[?W??
		private Byte controll1;	// ?R???g???[???o?C?g1
		private Byte controll2;	// ?R???g???[???o?C?g2
		private Byte zero1;		// 00
		private Byte zero2;		// 00
		private Byte mode;		// NTSC/PAL
		private Byte zero4;		// 00
		private Byte zero5;		// 00
		private Byte zero6;		// 00
		private Byte zero7;		// 00
		private Byte zero8;		// 00

		public CNesHeader( Byte[] aDataArray )
		{
			dataArray = new Byte[0x10];
			MemoryTool.memcpy( dataArray, 0x00, aDataArray, 0x00, 0x10 );

			// nes?w?b?_??????????
			ReadHeader();

			// nes?w?b?_???\??
			PrintHeader();
		}

		public void ReadHeader()
		{
			nes = new Byte[4];
			MemoryTool.memcpy( nes, 0x00, dataArray, 0x00, sizeof( Byte ) * 4 );

			prgBanks = GetByte( 0x04 );		// PRG-ROM?y?[?W??
			chrBanks = GetByte( 0x05 );		// CHR0ROM?y?[?W??
			controll1 = GetByte( 0x06 );	// ?R???g???[???o?C?g1
			controll2 = GetByte( 0x07 );	// ?R???g???[???o?C?g2
			zero1 = GetByte( 0x08 );		// 00
			zero2 = GetByte( 0x09 );		// 00
			mode = GetByte( 0x0A );			// NTCS/PAL
			zero4 = GetByte( 0x0B );		// 00
			zero5 = GetByte( 0x0C );		// 00
			zero6 = GetByte( 0x0D );		// 00
			zero7 = GetByte( 0x0E );		// 00
			zero8 = GetByte( 0x0F );		// 00

			if( nes[0] != 'N' || nes[1] != 'E' || nes[2] != 'S' )
			{
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}
		}

		public void PrintHeader()
		{
			// Temporary Fix.
			/*
			Logger.LogNormal( "PRG-ROM?y?[?W??:%02x\n" + prgBanks );		// PRG-ROM?y?[?W??
			Logger.LogNormal( "CHR0ROM?y?[?W??:%02x\n" + chrBanks );		// CHR0ROM?y?[?W??
			Logger.LogNormal( "?R???g???[???o?C?g1:%02x\n" + controll1 );	// ?R???g???[???o?C?g1
			Logger.LogNormal( "?R???g???[???o?C?g2:%02x\n" + controll2 );	// ?R???g???[???o?C?g2
			Logger.LogNormal( "NTSC/PAL:%02x\n" + mode );					// NTSC/PAL
			Logger.LogNormal( "\n" );
		
			if( prgBanks > 0x02 || chrBanks > 0x08 )
			{
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}
			*/
		}

		public Byte GetByte( Byte addr )
		{
			return dataArray[addr];
		}

		public UInt16 GetWord( Byte addr )
		{
			return ( UInt16 )( ( UInt16 )dataArray[addr + 1] << 8 | ( UInt16 )dataArray[addr] );
		}

		public Byte GetPrgBanks()
		{
			return prgBanks;
		}

		public Byte GetChrBanks()
		{
			return chrBanks;
		}

		public Byte GetControll1()
		{
			return controll1;
		}

		public Byte GetControll2()
		{
			return controll2;
		}
	}

	public class NesRom
	{
		private CNesHeader header;

		private Byte[] dataArray;
		private int dataSize;

		private NesRom()
		{

		}

		public NesRom( Byte[] aDataArray )
		{
			dataSize = aDataArray.Length - 0x10;

			if( dataSize >= 0x10000 )
			{
				// To Be Fixed.
				//MessageBox( NULL, TEXT( "?T?|?[?g???????????t?@?C???????B" ), TEXT( "?G???[" ), MB_OK );

				//exit( 1 );
			}

			header = new CNesHeader( aDataArray );

			dataArray = new Byte[0x10000];
			MemoryTool.memcpy( dataArray, 0x00, aDataArray, 0x10, dataSize );
		}

		public Byte[] GetDataArray()
		{
			return dataArray;
		}

		public int GetDataSize()
		{
			return dataSize;
		}

		public CNesHeader GetHeader()
		{
			return header;
		}
	}
}
