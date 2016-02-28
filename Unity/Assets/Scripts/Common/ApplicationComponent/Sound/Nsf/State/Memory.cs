using System;

using Curan.Common.AdaptedData.Music;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public class NesMemory
	{
		private const Byte READ_FLAG = 0x01;
		private const Byte WRITE_FLAG = 0x02;

		private Byte[] data = new Byte[0x10000];
		private Byte[] flag = new Byte[0x10000];
		private UInt16 address;

		public void Init()
		{
			MemoryTool.memset( data, 0x00, 0x00, sizeof( Byte ) * 0x8000 );
			MemoryTool.memset( flag, 0x00, 0x00, sizeof( Byte ) * 0x10000 );

			data[0x4010] = 0x10;
			data[0x4015] = 0x0F;
		}

		public NesMemory( MusicNsf nsf )
		{
			Logger.LogNormal( nsf.GetHeader().GetLoadAddress() );
			Logger.LogNormal( nsf.GetDataSize() );
			MemoryTool.memcpy( data, nsf.GetHeader().GetLoadAddress(), nsf.GetDataPoint(), 0x00, sizeof( Byte ) * ( UInt16 )nsf.GetDataSize() );
		}

		// ?v???O?????J?E???^???l???Z?b?g?????B
		// ?I?y?????h???A?h???X???A?????A?h???X???????v?Z???????B
		public void SetAddressInstruction( UInt16 addr )
		{
			address = addr;
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

		// ?t???O???????????B
		public Byte GetFlag( UInt16 addr )
		{
			return flag[addr];
		}

		// ?????????t???O???????B
		public bool IsRead( UInt16 addr )
		{
			if( flag[addr] == READ_FLAG )
			{
				flag[addr] = 0x00;
				return true;
			}
			else
			{
				return false;
			}
		}

		// ?????????t???O???????????B
		public bool IsWrite( UInt16 addr )
		{
			if( flag[addr] == WRITE_FLAG )
			{
				flag[addr] = 0x00;
				return true;
			}
			else
			{
				return false;
			}
		}

		// ?????R?[?h???????????B
		public Byte GetOpecode()
		{
			return data[address];
		}

		// ------------------------------------------------------------------------
		// ?A?h???X????
		// ------------------------------------------------------------------------
		public UInt16 GetAddressImmediate()
		{
			return ( UInt16 )( address + 1 );
		}

		public UInt16 GetAddressZeropage()
		{
			return ( UInt16 )data[address + 1];
		}

		public UInt16 GetAddressZeropageIndexX( Byte x )
		{
			return ( UInt16 )( GetAddressZeropage() + x );
		}

		public UInt16 GetAddressZeropageIndexY( Byte y )
		{
			return ( UInt16 )( GetAddressZeropage() + y );
		}

		public UInt16 GetAddressAbsolute()
		{
			return ( UInt16 )( ( ( UInt16 )data[address + 2] << 8 ) | data[address + 1] );
		}

		public UInt16 GetAddressAbsoluteIndexX( Byte x )
		{
			return ( UInt16 )( GetAddressAbsolute() + x );
		}

		public UInt16 GetAddressAbsoluteIndexY( Byte y )
		{
			return ( UInt16 )( GetAddressAbsolute() + y );
		}

		// ?????????K?v
		public UInt16 GetAddressIndirect()
		{
			if( ( GetAddressAbsolute() & 0xFF ) == 0xFF )
			{
				return ( UInt16 )( ( ( UInt16 )data[GetAddressAbsolute() - 0xFF] << 8 ) | data[GetAddressAbsolute()] );
			}
			else
			{
				return ( UInt16 )( ( ( UInt16 )data[GetAddressAbsolute() + 1] << 8 ) | data[GetAddressAbsolute()] );
			}
		}

		public UInt16 GetAddressIndirectIndexX( Byte x )
		{
			return ( UInt16 )( ( ( UInt16 )data[( data[address + 1] + x + 1 ) & 0xFF] << 8 ) | data[data[address + 1] + x] );
		}

		public UInt16 GetAddressIndirectIndexY( Byte y )
		{
			return ( UInt16 )( ( ( UInt16 )data[( data[address + 1] + 1 ) & 0xFF] << 8 ) | data[data[address + 1]] + y );
		}

		public UInt16 GetAddressRerative()
		{
			return ( UInt16 )( address + ( SByte )data[address + 1] );
		}

		public UInt16 GetAddressStack( Byte s )
		{
			return ( UInt16 )( ( UInt16 )0x0100 + s );
		}

		// ------------------------------------------------------------------------
		// ?I?y?????h????
		// ------------------------------------------------------------------------
		public Byte GetDataImmediate()
		{
			return data[GetAddressImmediate()];
		}

		public Byte GetDataZeropage()
		{
			return data[GetAddressZeropage()];
		}

		public Byte GetDataZeropageIndexX( Byte x )
		{
			return data[GetAddressZeropageIndexX( x )];
		}

		public Byte GetDataZeropageIndexY( Byte y )
		{
			return data[GetAddressZeropageIndexY( y )];
		}

		public Byte GetDataAbsolute()
		{
			return data[GetAddressAbsolute()];
		}

		public Byte GetDataAbsoluteIndexX( Byte x )
		{
			return data[GetAddressAbsoluteIndexX( x )];
		}

		public Byte GetDataAbsoluteIndexY( Byte y )
		{
			return data[GetAddressAbsoluteIndexY( y )];
		}

		public Byte GetDataIndirect()
		{
			return data[GetAddressIndirect()];
		}

		public Byte GetDataIndirectIndexX( Byte x )
		{
			return data[GetAddressIndirectIndexX( x )];
		}

		public Byte GetDataIndirectIndexY( Byte y )
		{
			return data[GetAddressIndirectIndexY( y )];
		}

		public Byte GetDataStack( Byte s )
		{
			return data[GetAddressStack( s )];
		}

		// ------------------------------------------------------------------------
		// ?f?[?^????????
		// ------------------------------------------------------------------------
		public void SetDataZeropage( Byte b )
		{
			data[GetAddressZeropage()] = b;
		}

		public void SetDataZeropageIndexX( Byte x, Byte b )
		{
			data[GetAddressZeropageIndexX( x )] = b;
		}

		public void SetDataZeropageIndexY( Byte y, Byte b )
		{
			data[GetAddressZeropageIndexY( y )] = b;
		}

		public void SetDataAbsolute( Byte b )
		{
			data[GetAddressAbsolute()] = b;
		}

		public void SetDataAbsoluteIndexX( Byte x, Byte b )
		{
			data[GetAddressAbsoluteIndexX( x )] = b;
		}

		public void SetDataAbsoluteIndexY( Byte y, Byte b )
		{
			data[GetAddressAbsoluteIndexY( y )] = b;
		}

		public void SetDataIndirect( Byte b )
		{
			data[GetAddressIndirect()] = b;
		}

		public void SetDataIndirectIndexX( Byte x, Byte b )
		{
			data[GetAddressIndirectIndexX( x )] = b;
		}

		public void SetDataIndirectIndexY( Byte y, Byte b )
		{
			data[GetAddressIndirectIndexY( y )] = b;
		}

		public void SetDataStack( Byte s, Byte b )
		{
			data[GetAddressStack( s )] = b;
		}

		// ------------------------------------------------------------------------
		// ?f?[?^???[?h
		// ------------------------------------------------------------------------
		public void LoadDataImmediate( ref Byte b )
		{
			flag[address + 1] = READ_FLAG;
			b = GetDataImmediate();
		}

		public void LoadDataZeropage( ref Byte b )
		{
			flag[GetAddressZeropage()] = READ_FLAG;
			b = GetDataZeropage();
		}

		public void LoadDataZeropageIndexX( Byte x, ref Byte b )
		{
			flag[GetAddressZeropageIndexX( x )] = READ_FLAG;
			b = GetDataZeropageIndexX( x );
		}

		public void LoadDataZeropageIndexY( Byte y, ref Byte b )
		{
			flag[GetAddressZeropageIndexY( y )] = READ_FLAG;
			b = GetDataZeropageIndexY( y );
		}

		public void LoadDataAbsolute( ref Byte b )
		{
			flag[GetAddressAbsolute()] = READ_FLAG;
			b = GetDataAbsolute();
		}

		public void LoadDataAbsoluteIndexX( Byte x, ref Byte b )
		{
			flag[GetAddressAbsoluteIndexX( x )] = READ_FLAG;
			b = GetDataAbsoluteIndexX( x );
		}

		public void LoadDataAbsoluteIndexY( Byte y, ref Byte b )
		{
			flag[GetAddressAbsoluteIndexY( y )] = READ_FLAG;
			b = GetDataAbsoluteIndexY( y );
		}

		public void LoadDataIndirect( ref Byte b )
		{
			flag[GetAddressIndirect()] = READ_FLAG;
			b = GetDataIndirect();
		}

		public void LoadDataIndirectIndexX( Byte x, ref Byte b )
		{
			flag[GetAddressIndirectIndexX( x )] = READ_FLAG;
			b = GetDataIndirectIndexX( x );
		}

		public void LoadDataIndirectIndexY( Byte y, ref Byte b )
		{
			flag[GetAddressIndirectIndexY( y )] = READ_FLAG;
			b = GetDataIndirectIndexY( y );
		}

		public void LoadDataStack( Byte s, ref Byte b )
		{
			flag[GetAddressStack( s )] = READ_FLAG;
			b = GetDataStack( s );
		}

		// ------------------------------------------------------------------------
		// ?f?[?^?X?g?A
		// ------------------------------------------------------------------------
		public void StoreDataZeropage( ref Byte b )
		{
			flag[GetAddressZeropage()] = WRITE_FLAG;
			data[GetAddressZeropage()] = b;
		}

		public void StoreDataZeropageIndexX( ref Byte x, ref Byte b )
		{
			flag[GetAddressZeropageIndexX( x )] = WRITE_FLAG;
			data[GetAddressZeropageIndexX( x )] = b;
		}

		public void StoreDataZeropageIndexY( ref Byte y, ref Byte b )
		{
			flag[GetAddressZeropageIndexY( y )] = WRITE_FLAG;
			data[GetAddressZeropageIndexY( y )] = b;
		}

		public void StoreDataAbsolute( ref Byte b )
		{
			flag[GetAddressAbsolute()] = WRITE_FLAG;
			data[GetAddressAbsolute()] = b;
		}

		public void StoreDataAbsoluteIndexX( ref Byte x, ref Byte b )
		{
			flag[GetAddressAbsoluteIndexX( x )] = WRITE_FLAG;
			data[GetAddressAbsoluteIndexX( x )] = b;
		}

		public void StoreDataAbsoluteIndexY( ref Byte y, ref Byte b )
		{
			flag[GetAddressAbsoluteIndexY( y )] = WRITE_FLAG;
			data[GetAddressAbsoluteIndexY( y )] = b;
		}

		public void StoreDataIndirect( ref Byte b )
		{
			flag[GetAddressIndirect()] = WRITE_FLAG;
			data[GetAddressIndirect()] = b;
		}

		public void StoreDataIndirectIndexX( ref Byte x, ref Byte b )
		{
			flag[GetAddressIndirectIndexX( x )] = WRITE_FLAG;
			data[GetAddressIndirectIndexX( x )] = b;
		}

		public void StoreDataIndirectIndexY( ref Byte y, ref Byte b )
		{
			flag[GetAddressIndirectIndexY( y )] = WRITE_FLAG;
			data[GetAddressIndirectIndexY( y )] = b;
		}

		public void StoreDataStack( ref Byte s, ref Byte b )
		{
			flag[GetAddressStack( s )] = WRITE_FLAG;
			data[GetAddressStack( s )] = b;
		}

		// ------------------------------------------------------------------------
		// ?f?[?^????????
		// ------------------------------------------------------------------------
		public Byte ReadDataImmediate()
		{
			flag[address + 1] = READ_FLAG;
			return GetDataImmediate();
		}

		public Byte ReadDataZeropage()
		{
			flag[GetAddressZeropage()] = READ_FLAG;
			return GetDataZeropage();
		}

		public Byte ReadDataZeropageIndexX( Byte x )
		{
			flag[GetAddressZeropageIndexX( x )] = READ_FLAG;
			return GetDataZeropageIndexX( x );
		}

		public Byte ReadDataZeropageIndexY( Byte y )
		{
			flag[GetAddressZeropageIndexY( y )] = READ_FLAG;
			return GetDataZeropageIndexY( y );
		}

		public Byte ReadDataAbsolute()
		{
			flag[GetAddressAbsolute()] = READ_FLAG;
			return GetDataAbsolute();
		}

		public Byte ReadDataAbsoluteIndexX( Byte x )
		{
			flag[GetAddressAbsoluteIndexX( x )] = READ_FLAG;
			return GetDataAbsoluteIndexX( x );
		}

		public Byte ReadDataAbsoluteIndexY( Byte y )
		{
			flag[GetAddressAbsoluteIndexY( y )] = READ_FLAG;
			return GetDataAbsoluteIndexY( y );
		}

		public Byte ReadDataIndirect()
		{
			flag[GetAddressIndirect()] = READ_FLAG;
			return GetDataIndirect();
		}

		public Byte ReadDataIndirectIndexX( Byte x )
		{
			flag[GetAddressIndirectIndexX( x )] = READ_FLAG;
			return GetDataIndirectIndexX( x );
		}

		public Byte ReadDataIndirectIndexY( Byte y )
		{
			flag[GetAddressIndirectIndexY( y )] = READ_FLAG;
			return GetDataIndirectIndexY( y );
		}

		public Byte ReadDataStack( Byte s )
		{
			flag[GetAddressStack( s )] = READ_FLAG;
			return GetDataStack( s );
		}

		// ------------------------------------------------------------------------
		// ?f?[?^????????
		// ------------------------------------------------------------------------
		public void WriteDataZeropage( Byte b )
		{
			flag[GetAddressZeropage()] = WRITE_FLAG;
			data[GetAddressZeropage()] = b;
		}

		public void WriteDataZeropageIndexX( Byte x, Byte b )
		{
			flag[GetAddressZeropageIndexX( x )] = WRITE_FLAG;
			data[GetAddressZeropageIndexX( x )] = b;
		}

		public void WriteDataZeropageIndexY( Byte y, Byte b )
		{
			flag[GetAddressZeropageIndexY( y )] = WRITE_FLAG;
			data[GetAddressZeropageIndexY( y )] = b;
		}

		public void WriteDataAbsolute( Byte b )
		{
			flag[GetAddressAbsolute()] = WRITE_FLAG;
			data[GetAddressAbsolute()] = b;
		}

		public void WriteDataAbsoluteIndexX( Byte x, Byte b )
		{
			flag[GetAddressAbsoluteIndexX( x )] = WRITE_FLAG;
			data[GetAddressAbsoluteIndexX( x )] = b;
		}

		public void WriteDataAbsoluteIndexY( Byte y, Byte b )
		{
			flag[GetAddressAbsoluteIndexY( y )] = WRITE_FLAG;
			data[GetAddressAbsoluteIndexY( y )] = b;
		}

		public void WriteDataIndirect( Byte b )
		{
			flag[GetAddressIndirect()] = WRITE_FLAG;
			data[GetAddressIndirect()] = b;
		}

		public void WriteDataIndirectIndexX( Byte x, Byte b )
		{
			flag[GetAddressIndirectIndexX( x )] = WRITE_FLAG;
			data[GetAddressIndirectIndexX( x )] = b;
		}

		public void WriteDataIndirectIndexY( Byte y, Byte b )
		{
			flag[GetAddressIndirectIndexY( y )] = WRITE_FLAG;
			data[GetAddressIndirectIndexY( y )] = b;
		}

		public void WriteDataStack( Byte s, Byte b )
		{
			flag[GetAddressStack( s )] = WRITE_FLAG;
			data[GetAddressStack( s )] = b;
		}

		// ------------------------------------------------------------------------
		// ?f?[?^?C???N???????g
		// ------------------------------------------------------------------------
		public void IncDataZeropage()
		{
			data[GetAddressZeropage()]++;
		}

		public void IncDataZeropageIndexX( ref Byte x )
		{
			data[GetAddressZeropageIndexX( x )]++;
		}

		public void IncDataZeropageIndexY( ref Byte y )
		{
			data[GetAddressZeropageIndexY( y )]++;
		}

		public void IncDataAbsolute()
		{
			data[GetAddressAbsolute()]++;
		}

		public void IncDataAbsoluteIndexX( ref Byte x )
		{
			data[GetAddressAbsoluteIndexX( x )]++;
		}

		public void IncDataAbsoluteIndexY( ref Byte y )
		{
			data[GetAddressAbsoluteIndexY( y )]++;
		}

		public void IncDataIndirect()
		{
			data[GetAddressIndirect()]++;
		}

		public void IncDataIndirectIndexX( ref Byte x )
		{
			data[GetAddressIndirectIndexX( x )]++;
		}

		public void IncDataIndirectIndexY( ref Byte y )
		{
			data[GetAddressIndirectIndexY( y )]++;
		}

		// ------------------------------------------------------------------------
		// ?f?[?^?f?N???????g
		// ------------------------------------------------------------------------
		public void DecDataZeropage()
		{
			data[GetAddressZeropage()]--;
		}

		public void DecDataZeropageIndexX( ref Byte x )
		{
			data[GetAddressZeropageIndexX( x )]--;
		}

		public void DecDataZeropageIndexY( ref Byte y )
		{
			data[GetAddressZeropageIndexY( y )]--;
		}

		public void DecDataAbsolute()
		{
			data[GetAddressAbsolute()]--;
		}

		public void DecDataAbsoluteIndexX( ref Byte x )
		{
			data[GetAddressAbsoluteIndexX( x )]--;
		}

		public void DecDataAbsoluteIndexY( ref Byte y )
		{
			data[GetAddressAbsoluteIndexY( y )]--;
		}

		public void DecDataIndirect()
		{
			data[GetAddressIndirect()]--;
		}

		public void DecDataIndirectIndexX( ref Byte x )
		{
			data[GetAddressIndirectIndexX( x )]--;
		}

		public void DecDataIndirectIndexY( ref Byte y )
		{
			data[GetAddressIndirectIndexY( y )]--;
		}
	}
}
