using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class TimeDomainTransformsHeader
	{
		public UInt16 zero;

		public TimeDomainTransformsHeader( ByteArray aByteArray )
		{
			zero = aByteArray.ReadBitsAsUInt16( 16 );

			if( zero != 0x0000 )
			{
				Logger.LogError( "Nonzero:" + zero.ToString( "X4" ) );
			}
		}
	}

	public class VorbisTimeDomainTransforms
	{
		public int count;
		public TimeDomainTransformsHeader[] header;

		public VorbisTimeDomainTransforms( ByteArray aByteArray )
		{
			count = aByteArray.ReadBitsAsByte( 6 ) + 1;

			Logger.LogWarning( "Vorbis Time Count:" + count.ToString() );

			header = new TimeDomainTransformsHeader[count];

			for( int i = 0; i < count; i++ )
			{
				header[i] = new TimeDomainTransformsHeader( aByteArray );
			}
		}
	}
}
