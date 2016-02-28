using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class MappingHeader
	{
		public Byte[] magnitude;
		public Byte[] angle;
		public Byte[] mux;
		public Byte[] subMapFloor;
		public Byte subMaps;
		public Byte subMapsAdd1;
		public Byte[] subMapResidue;
		public Byte couplingSteps;
		public int couplingStepsAdd1;

		public MappingHeader( ByteArray aByteArray )
		{
			UInt16 lType = aByteArray.ReadBitsAsUInt16( 16 );

			Logger.LogWarning( "Mapping Type:" + lType );

			if( lType == 0x0000 )
			{
				DecodeHeader( aByteArray );
			}
			else
			{
				Logger.LogWarning( "■The Stream Is Undecodable." );
			}
		}

		private void DecodeHeader( ByteArray aByteArray )
		{
			Byte lFlag = aByteArray.ReadBitsAsByte( 1 );

			subMaps = 0x00;

			Logger.LogError( "Flag1:" + lFlag );

			if( lFlag == 0x01 )
			{
				subMaps = aByteArray.ReadBitsAsByte( 4 );
			}

			subMapsAdd1 = ( Byte )( subMaps + 1 );

			Logger.LogError( "Mapping Sub Maps Add 1:" + subMapsAdd1 );

			lFlag = aByteArray.ReadBitsAsByte( 1 );

			Logger.LogError( "Flag2:" + lFlag );

			if( lFlag == 0x01 )
			{
				couplingSteps = aByteArray.ReadBitsAsByte( 8 );
				couplingStepsAdd1 = couplingSteps + 1;

				magnitude = new Byte[couplingStepsAdd1];
				angle = new Byte[couplingStepsAdd1];

				for( int i = 0; i < couplingStepsAdd1; i++ )
				{
					magnitude[i] = aByteArray.ReadBitsAsByte( ilog( /*aOggHeaderIdentification.audioChannels - 1*/1 ) );
					angle[i] = aByteArray.ReadBitsAsByte( ilog( /*aOggHeaderIdentification.audioChannels - 1*/1 ) );
				}
			}
			else
			{
				couplingSteps = 0;
				couplingStepsAdd1 = 1;
			}

			Byte lReservedField = aByteArray.ReadBitsAsByte( 2 );

			if( lReservedField != 0x00 )
			{
				Logger.LogWarning( "■The Stream Is Undecodable." );
				return;
			}

			mux = new Byte[/*aOggHeaderIdentification.audioChannel*/2];

			if( subMapsAdd1 > 1 )
			{
				Logger.LogError( "■subMapsAdd1 > 1." );

				for( int i = 0; i < /*aOggHeaderIdentification.audioChannel*/2; i++ )
				{
					mux[i] = aByteArray.ReadBitsAsByte( 4 );

					if( mux[i] >= subMapsAdd1 )
					{
						Logger.LogError( "■this in an error condition rendering the stream undecodable." );
					}
				}
			}

			subMapFloor = new Byte[subMapsAdd1];
			subMapResidue = new Byte[subMapsAdd1];

			for( int i = 0; i < subMapsAdd1; i++ )
			{
				aByteArray.ReadBitsAsByte( 8 );
				subMapFloor[i] = aByteArray.ReadBitsAsByte( 8 );

				//C. verify the 
				//oor number is not greater than the highest number 
				//oor
				//congured for the bitstream. If it is, the bitstream is undecodable

				subMapResidue[i] = aByteArray.ReadBitsAsByte( 8 );
				Logger.LogError( "■subMapResidue:" + subMapResidue[i] );

				//E. verify the residue number is not greater than the highest number residue
				//congured for the bitstream. If it is, the bitstream is undecodable
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

	public class VorbisMapping
	{
		public int count;
		public MappingHeader[] header;

		public VorbisMapping( ByteArray aByteArray )
		{
			Logger.LogWarning( "■VorbisMapping" );
			
			count = aByteArray.ReadBitsAsByte( 6 ) + 1;

			Logger.LogWarning( "Mapping Count:" + count );

			header = new MappingHeader[count];

			for( int i = 0; i < count; i++ )
			{
				header[i] = new MappingHeader( aByteArray );
			}
		}
	}
}
