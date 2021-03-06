﻿using System;

using Curan.Utility;

namespace Curan.Common.Emulator
{
	public class CVram
	{
		Byte[] data = new Byte[0x10000];

		public void Init()
		{
			MemoryTool.memset( data, 0x00, 0x00, sizeof( Byte ) * 0x10000 );
		}

		public CVram( NesRom rom )
		{
			MemoryTool.memcpy( data, 0x00, rom.GetDataArray(), 0x8000, sizeof( Byte ) * 0x2000 );
		}

		// ?A?h???X?????f?[?^???????????B
		public Byte GetDataByte( UInt16 addr )
		{
			return data[addr];
		}

		// ?A?h???X????16?r?b?g?f?[?^???????????B
		public UInt16 GetDataWord( UInt16 addr )
		{
			return ( UInt16 )( ( ( UInt16 )data[addr + 1] << 8 ) | data[addr] );
		}

		// ?w???????A?h???X???f?[?^???????????B
		public void SetDataByte( UInt16 addr, Byte b )
		{
			data[addr] = b;
		}
	}
}
