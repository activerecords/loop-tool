using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Adx
{
	public class AdxData
	{
		private byte[] dataArray;
		private float[][] sampleArray;

		public AdxData( ByteArray aByteArray, AdxHeader aAdxHeader )
		{
			sampleArray = new float[aAdxHeader.GetChannelLength()][];
			
			for( int i = 0; i < aAdxHeader.GetChannelLength(); i++ )
			{
				sampleArray[i] = new float[aAdxHeader.GetSampleLength()];
			}

			int s00 = 0;
			int s10 = 0;
			int s20 = 0;
			int d0 = 0;
			
			int s01 = 0;
			int s11 = 0;
			int s21 = 0;
			int d1 = 0;

			for( int I = 0; ( I + 1 ) * 32 < aAdxHeader.GetSampleLength(); I++ )
			{
				int scale = aByteArray.ReadInt16();

				for( int i=0; i < 16 ; i++ )
				{
					int lBase = I * 32;
					int lIn = aByteArray.ReadByte();

					d0 = lIn >> 4;

					if( ( d0 & 8 ) != 0 )
					{
						d0 -= 16;
					}

					s00 = ( 0x4000 * d0 * scale + 0x7298 * s10 - 0x3350 * s20 ) >> 14;

					if( s00 > 32767 )
					{
						s00 = 32767;
					}
					else if(s00 < -32768 )
					{
						s00 = -32768;
					}

					sampleArray[0][lBase + i * 2 + 0] = ( float )s00 / ( float )0x8000;

					s20 = s10;
					s10 = s00;

					d0 = lIn & 15;

					if( ( d0 & 8 ) != 0 )
					{
						d0 -= 16;
					}

					s00 = ( 0x4000 * d0 * scale + 0x7298 * s10 - 0x3350 * s20 ) >> 14;

					if( s00 > 32767 )
					{
						s00 = 32767;
					}
					else if( s00 < -32768 )
					{
						s00 = -32768;
					}

					sampleArray[0][lBase + i * 2 + 1] = ( float )s00 / ( float )0x8000;

					s20 = s10;
					s10 = s00;
				}

				scale = aByteArray.ReadInt16();

				for( int i = 0; i < 16; i++ )
				{
					int lBase = I * 32;
					int lIn = aByteArray.ReadByte();

					d1 = lIn >> 4;

					if( ( d1 & 8 ) != 0 )
					{
						d1 -= 16;
					}

					s01 = ( 0x4000 * d1 * scale + 0x7298 * s11 - 0x3350 * s21 ) >> 14;

					if( s01 > 32767 )
					{
						s01 = 32767;
					}
					else if( s01 < -32768 )
					{
						s01 = -32768;
					}

					sampleArray[1][lBase + i * 2 + 0] = ( float )s01 / ( float )0x8000;

					s21 = s11;
					s11 = s01;

					d1 = lIn & 15;

					if( ( d1 & 8 ) != 0 )
					{
						d1 -= 16;
					}

					s01 = ( 0x4000 * d1 * scale + 0x7298 * s11 - 0x3350 * s21 ) >> 14;

					if( s01 > 32767 )
					{
						s01 = 32767;
					}
					else if( s01 < -32768 )
					{
						s01 = -32768;
					}

					sampleArray[1][lBase + i * 2 + 1] = ( float )s01 / ( float )0x8000;

					s21 = s11;
					s11 = s01;
				}
			}
		}

		public byte[] GetDataArray()
		{
			return dataArray;
		}

		public float[][] GetSampleArray()
		{
			return sampleArray;
		}
	}
}
