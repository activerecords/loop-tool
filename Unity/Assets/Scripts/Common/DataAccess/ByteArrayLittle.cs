﻿using System;
using System.IO;

namespace Curan.Common.system.io
{
	public class ByteArrayLittle : ByteArray
	{
		private Byte[] dataArray2;
		private Byte[] dataArray3;
		private Byte[] dataArray4;
		private Byte[] dataArray8;
		private Byte[] dataArray10;
		private Byte[] mask;

		public ByteArrayLittle( Stream aStream )
			: base( aStream )
		{
			dataArray2 = new Byte[2];
			dataArray3 = new Byte[3];
			dataArray4 = new Byte[4];
			dataArray8 = new Byte[8];
			dataArray10 = new Byte[10];
			mask = new Byte[]{ 0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF };
		}

		public override UInt16 ReadUInt16()
		{
			ReadBytes( dataArray2 );

			return BitConverter.ToUInt16( dataArray2, 0 );
		}

		public override UInt32 ReadUInt24()
		{
			ReadBytes( dataArray3 );

			dataArray4[0] = dataArray3[0];
			dataArray4[1] = dataArray3[1];
			dataArray4[2] = dataArray3[2];
			dataArray4[3] = 0x00;

			return BitConverter.ToUInt32( dataArray4, 0 );
		}

		public override UInt32 ReadUInt32()
		{
			ReadBytes( dataArray4 );

			return BitConverter.ToUInt32( dataArray4, 0 );
		}

		public override UInt64 ReadUInt64()
		{
			ReadBytes( dataArray8 );

			return BitConverter.ToUInt64( dataArray8, 0 );
		}

		public override Int16 ReadInt16()
		{
			ReadBytes( dataArray2 );

			return BitConverter.ToInt16( dataArray2, 0 );
		}

		public override Int32 ReadInt24()
		{
			ReadBytes( dataArray3 );
			
			dataArray4[0] = dataArray3[0];
			dataArray4[1] = dataArray3[1];
			dataArray4[2] = dataArray3[2];
			dataArray4[3] = 0x00;
			
			if( dataArray3[2] >= 0x80 )
			{
				dataArray4[3] = 0xFF;
			}

			/*
			byte[] readData = ReadBytes( 3 );
			byte[] tempData = 
			{
				readData[0], readData[1], readData[2], 0x00
			};

			if( readData[2] >= 0x80 )
			{
				tempData[3] = 0xFF;
			}*/

			return BitConverter.ToInt32( dataArray4, 0 );
		}

		public override Int32 ReadInt32()
		{
			ReadBytes( dataArray4 );

			return BitConverter.ToInt32( dataArray4, 0 );
		}

		public override Int64 ReadInt64()
		{
			ReadBytes( dataArray8 );

			return BitConverter.ToInt64( dataArray8, 0 );
		}

		public override Single ReadSingle()
		{
			ReadBytes( dataArray4 );

			return BitConverter.ToSingle( dataArray4, 0 );
		}

		public override Double ReadDouble()
		{
			ReadBytes( dataArray8 );

			return BitConverter.ToDouble( dataArray8, 0 );
		}

		public Byte ReadBit()
		{
			Byte lData = ( Byte )( ( stream.ReadByte() >> ( PositionBit & 7 ) ) & 0x01 );

			AddBitPosition( 1 );

			return lData;
		}
		
		public UInt32 ReadBits( int aLengthBit )
		{
			int lRead = 0;
			int lPositionBit = PositionBit;
			UInt32 lData = 0;

			while( lRead < aLengthBit )
			{
				int lBit = ( lPositionBit + lRead ) & 7;
				int lRestBit = 8 - lBit;
				int lRestReadBit = aLengthBit - lRead;
				int lReadNow = Math.Min( lRestReadBit, lRestBit );

				UInt32 lDataRead = ( UInt32 )( ( stream.ReadByte() >> lBit ) & mask[lReadNow] );
				lData |= ( UInt32 )( lDataRead << lRead );
				lRead += lReadNow;
			}
			
			AddBitPosition( aLengthBit );
			
			if( PositionBit <= LengthBit )
			{
				return lData;
			}
			else
			{
				return 0;
			}
		}

		public override Byte ReadBitsAsByte( int aLengthBit )
		{
			return ( Byte )ReadBits( aLengthBit );
		}

		public override UInt16 ReadBitsAsUInt16( int aLengthBit )
		{
			return ( UInt16 )ReadBits( aLengthBit );
		}

		public override UInt32 ReadBitsAsUInt32( int aLengthBit )
		{
			return ( UInt32 )ReadBits( aLengthBit );
		}

		/*
		public override Byte ReadBitsAsUInt16( int aLengthBit )
		{
			Byte lData = 0x00;

			for( int i = 0; i < aLengthBit; i++ )
			{
				lData |= ( Byte )( ReadBit() << i );
			}

			return lData;
		}

		public override UInt16 ReadBitsAsUInt16( int aLengthBit )
		{
			UInt16 lData = 0x00;

			for( int i = 0; i < aLengthBit; i++ )
			{
				lData |= ( UInt16 )( ReadBit() << i );
			}

			return lData;
		}

		public override UInt32 ReadBitsAsUInt32( int aLengthBit )
		{
			UInt32 lData = 0x00;

			for( int i = 0; i < aLengthBit; i++ )
			{
				lData |= ( UInt32 )( ReadBit() << i );
			}

			return lData;
		}
		*/
	}
}
