using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class ModeHeader
	{
		public Byte blockFlag;
		public UInt16 windowType;
		public UInt16 transformType;
		public Byte mapping;

		public Byte configurations;

		public ModeHeader( ByteArray aByteArray )
		{
			blockFlag = aByteArray.ReadBitsAsByte( 1 );
			windowType = aByteArray.ReadBitsAsUInt16( 16 );
			transformType = aByteArray.ReadBitsAsUInt16( 16 );
			mapping = aByteArray.ReadBitsAsByte( 8 );
			configurations = blockFlag;
		}

		private int ilog( UInt32 aX )
		{
			int lReturnValue = 0;

			while( aX > 0 )
			{
				lReturnValue++;

				aX >>= 1;
			}

			return lReturnValue;
		}
	}

	public class VorbisMode
	{
		public int count;
		public ModeHeader[] header;

		public VorbisMode( ByteArray aByteArray )
		{
			count = aByteArray.ReadBitsAsByte( 6 ) + 1;

			Logger.LogError( "VorbisMode count:" + count );

			header = new ModeHeader[count];
			
			for( int i = 0; i < count; i++ )
			{
				header[i] = new ModeHeader( aByteArray );
			}

			Byte lFramingFlag = aByteArray.ReadBitsAsByte( 1 );

			if( lFramingFlag == 0 )
			{
				Logger.LogError( "Framing error. The Stream Is Undecodable." );
			}
		}

		private int ilog( UInt32 aX )
		{
			int lReturnValue = 0;

			while( aX > 0 )
			{
				lReturnValue++;

				aX >>= 1;
			}

			return lReturnValue;
		}
	}
}
