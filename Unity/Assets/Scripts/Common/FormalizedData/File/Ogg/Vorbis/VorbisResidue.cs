using System;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class ResidueHeader
	{
		public UInt16 types;
		public UInt32 begin;
		public UInt32 end;
		public UInt32 partitionSize;
		public UInt32 partitionSizeAdd1;
		public Byte classifications;
		public Byte classificationsAdd1;
		public Byte classbook;

		public UInt16[] cascade;
		public UInt16[][] books;

		public ResidueHeader( ByteArray aByteArray )
		{
			types = aByteArray.ReadBitsAsUInt16( 16 );

			if( types == 0x0000 )
			{
				Logger.LogWarning( "■Residue Type 0." );
			}
			else if( types == 0x0001 )
			{
				Logger.LogWarning( "■Residue Type 1." );
			}
			else if( types == 0x0002 )
			{
				Logger.LogWarning( "■Residue Type 2." );
			}
			else
			{
				Logger.LogError( "■Undefined Residue Type." );

				return;
			}

			begin = aByteArray.ReadBitsAsUInt32( 24 );
			end = aByteArray.ReadBitsAsUInt32( 24 );
			partitionSize = aByteArray.ReadBitsAsUInt32( 24 );
			partitionSizeAdd1 = ( UInt32 )( partitionSize + 1 );
			classifications = aByteArray.ReadBitsAsByte( 6 );
			classificationsAdd1 = ( Byte )( classifications + 1 );
			classbook = aByteArray.ReadBitsAsByte( 8 );

			cascade = new UInt16[classificationsAdd1];
			books = new UInt16[classificationsAdd1][];

			for( int i = 0; i < classificationsAdd1; i++ )
			{
				Byte lHighBits = 0;
				Byte lLowBits = aByteArray.ReadBitsAsByte( 3 );
				Byte lBitFlag = aByteArray.ReadBitsAsByte( 1 );

				if( lBitFlag == 0x01 )
				{
					lHighBits = aByteArray.ReadBitsAsByte( 5 );
				}

				cascade[i] = ( UInt16 )( ( lHighBits << 3 ) | lLowBits );
			}

			for( int i = 0; i < classificationsAdd1; i++ )
			{
				books[i] = new UInt16[8];

				for( int j = 0; j < 8; j++ )
				{
					if( ( ( cascade[i] >> j ) & 0x01 ) == 0x01 )
					{
						books[i][j] = aByteArray.ReadBitsAsByte( 8 );
					}
					else
					{
						books[i][j] = 0xFFFF;//unused
					}
				}
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

		public void DecodePacket( ByteArray aByteArray, int aAudioChannels, CodebookHeader[] aCodebookHeaderArray, int n, int aCh, Byte[] do_not_decode_flag, double[][] aResidueArray )
		{
			//Logger.LogError( "■DecodePacket:" + classbook );

			CodebookHeader lCodebookHeader = aCodebookHeaderArray[classbook];

			int actual_size = n / 2;

			if( types == 0x0002 )
			{
				actual_size *= aCh;
				aCh = 1;
			}

			// Maximum.
			/*
			int limit_residue_begin = actual_size;

			if( begin > actual_size )
			{
				limit_residue_begin = ( int )begin;
			}

			int limit_residue_end = actual_size;

			if( end > actual_size )
			{
				limit_residue_end = ( int )end;
			}
			*/
			
			// Miminam.
			int limit_residue_begin = ( int )begin;

			if( actual_size < begin )
			{
				limit_residue_begin = actual_size;
			}

			int limit_residue_end = ( int )end;

			if( actual_size < end )
			{
				limit_residue_end = actual_size;
			}

			Logger.LogDebug( "■actual_size,.begin,end:" + actual_size + "," + begin + "," + end );
			
			int classwords_per_codeword = lCodebookHeader.dimensions;
			int n_to_read = limit_residue_end - limit_residue_begin;
			int partitions_to_read = ( int )( n_to_read / partitionSizeAdd1 );

			//Logger.LogError( "classwords_per_codeword:" + classwords_per_codeword );
			//Logger.LogError( "partitionSizeAdd1:" + partitionSizeAdd1 );
			//Logger.LogError( "partitions_to_read:" + partitions_to_read );

			int[][] lClassifications = new int[aCh][];

			for( int j = 0; j < aCh; j++ )
			{
				lClassifications[j] = new int[partitions_to_read + classwords_per_codeword];
			}

			if( n_to_read <= 0 )
			{
				Logger.LogError( "■there is no residue to decode." );
				return;
			}

			for( int pass = 0; pass < 8; pass++ )
			{
				int partition_count = 0;

				while( partition_count < partitions_to_read )
				{
					if( pass == 0 )
					{
						for( int j = 0; j < aCh; j++ )
						{
							if( do_not_decode_flag[j] == 0x00 )
							{
								int temp = lCodebookHeader.huffmanRoot.Read( aByteArray );

								//Logger.LogWarning( "temp:" + temp );

								for( int i = classwords_per_codeword - 1; i >= 0; i-- )
								{
									lClassifications[j][i + partition_count] = temp % classificationsAdd1;
									temp /= classificationsAdd1;
									//Logger.LogError( "classifications[" + j + "][" + ( i + partition_count ) + "]:" + classifications[j][i + partition_count] );
								}
							}
						}
					}

					for( int i = 0; i < classwords_per_codeword && partition_count < partitions_to_read; i++ )
					{
						for( int j = 0; j < aCh; j++ )
						{
							if( do_not_decode_flag[j] == 0x00 )
							{
								//Logger.LogError( "classifications[" + j + "][" + ( partition_count ) + "]:" + classifications[j][partition_count] );
								int vqclass = lClassifications[j][partition_count];
								UInt16 vqbook = books[vqclass][pass];

								if( vqbook != 0xFFFF/*'unused'*/ )
								{
									switch( types )
									{
									case 0:
										Logger.LogDebug( "■case 0" );
										//DecodePacket0( aOggHeaderIdentification, aOggHeaderSetup, aResidueNumber );
										break;
									case 1:
										Logger.LogDebug( "■case 1" );
										//DecodePacket1( aOggHeaderIdentification, aOggHeaderSetup, aResidueNumber );
										break;

									case 2:
										int offset = ( int )( limit_residue_begin + partition_count * partitionSizeAdd1 );

										//Logger.LogError( "pass,partition_count,i:" + pass + "," + partition_count + "," + i );
										//Logger.LogError( "■ch:" + aCh );
										CodebookHeader llCodebookHeader = aCodebookHeaderArray[vqbook];
										DecodePacket2( llCodebookHeader, aAudioChannels, aResidueArray, aByteArray, offset, aCh, do_not_decode_flag );
										//llCodebookHeader.ReadVvAdd( aOggHeaderIdentification.a, aByteArray, offset, ( int )partitionSizeAdd1 );

										//DecodePacket2( aOggHeaderIdentification, aOggHeaderSetup, aResidueNumber );
										break;

									default:
										Logger.LogDebug( "■default" );
										return;
									}
								}
								else
								{
									//Logger.LogError( "vqbook == 0xFFFF,pass,partition_count,i:" + pass + "," + partition_count + "," + i );
								}
							}
						}

						partition_count++;
					}
				}
			}
		}

		private void DecodePacket0( Identification aOggHeaderIdentification, Setup aOggHeaderSetup )
		{
			CodebookHeader lCodebookHeader = aOggHeaderSetup.codebook.headerArray[classbook];

			// Check 61.
			int n = ( int )partitionSizeAdd1;
			//[v] is the residue vector
			//[offset] is the beginning read ofset in [v]

			int step = n / lCodebookHeader.dimensions;

			for( int i = 0; i < step; i++ )
			{
				//vector[entry_temp] = 0;//read vector from packet using current codebook in VQ context

				for( int j = 0; j < lCodebookHeader.dimensions; j++ )
				{
					//vector [v] element ([offset]+[i]+[j]*[step]) = vector [v] element ([offset]+[i]+[j]*[step]) + vector [entry\_temp] element [j]
				}
			}
		}

		public void DecodePacket1( CodebookHeader aCodebookHeader, double[] a, ByteArray aByteArray, int offset )
		{
			int n = ( int )partitionSizeAdd1;

			int i = 0;

			while( i < n )
			{
				//Logger.LogError( "aCodebookHeader.ReadVv( aByteArray ):" + i );

				double[] entry_temp = aCodebookHeader.ReadVv( aByteArray );

				for( int j = 0; j < aCodebookHeader.dimensions; j++ )
				{
					a[offset + i] += entry_temp[j];

					i++;
				}
			}
		}

		public void DecodePacket2( CodebookHeader aCodebookHeader, int aAudioChannels, double[][] a, ByteArray aByteArray, int offset, int aCh, Byte[] do_not_decode_flag )
		{
			for( int i = 0; i < aCh; i++ )
			{
				if( do_not_decode_flag[i] == 1 )
				{
					Logger.LogError( "do_not_decode_flag:" + i );
				}
			}

			double[] lValueArray = new double[aAudioChannels * a[0].Length];

			DecodePacket1( aCodebookHeader, lValueArray, aByteArray, offset );

			for( int i = 0; i < a[0].Length; i++ )
			{
				for( int j = 0; j < aAudioChannels; j++ )
				{
					a[j][i] += lValueArray[i * aAudioChannels + j];
				}
			}
		}
	}

	public class VorbisResidue
	{
		public readonly int count;
		public readonly ResidueHeader[] header;

		public VorbisResidue( ByteArray aByteArray )
		{
			count = ( Byte )( aByteArray.ReadBitsAsByte( 6 ) + 1 );

			Logger.LogWarning( "Vorbis Residue Count:" + count.ToString() );

			header = new ResidueHeader[count];

			for( int i = 0; i < count; i++ )
			{
				header[i] = new ResidueHeader( aByteArray );
			}
		}
	}
}
