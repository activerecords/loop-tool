using System;
using System.IO;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ahx
{
	public class AhxHeader
	{
		//private Byte[] AHXHED = { 0x80, 0x00, 0x00, 0x20 };	//AHX Header with the ADX version chopped off, so older AHX files decode.
		private Byte[] AHXHED = { 0x80, 0x00, 0x00, 0x3C };	//AHX Header with the ADX version chopped off, so older AHX files decode.
		private Byte[] AHXCHK = { ( Byte )'(', ( Byte )'c', ( Byte )')', ( Byte )'C', ( Byte )'R', ( Byte )'I' };	//�m�F�pAHX�w�b�_�̒��`
		private Byte[] AHXFOT = { ( Byte )'A', ( Byte )'H', ( Byte )'X', ( Byte )'E', ( Byte )'(', ( Byte )'c', ( Byte )')', ( Byte )'C', ( Byte )'R', ( Byte )'I'};	//AHX�t�b�^�̒��`
		//private const Byte OPT_d = 0x01;							//�f�B���N�g���쐬�I�v�V����

		private float[][] sampleArray;

		//int getopt(int ac, char **av, char *opts);
		//extern int opterr, optind, optopt;
		//extern char *optarg;

		private int[] bit_alloc_table = { 4,4,4,4,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 };
		private int[] offset_table0 = { 0 };
		private int[] offset_table1 = { 0 };
		private int[] offset_table2 = { 0, 1,  3, 4 };
		private int[] offset_table3 = { 0, 1,  3, 4, 5, 6,  7, 8 };
		private int[] offset_table4 = { 0, 1,  2, 3, 4, 5,  6, 7,  8,  9, 10, 11, 12, 13, 14 };
		private int[][] offset_table;// = { offset_table0, offset_table1, offset_table2, offset_table3, offset_table4 };

		struct quantclass
		{
			public int nlevels;
			public int bits;

			public quantclass( int aNlevels, int aBits )
			{
				nlevels = aNlevels;
				bits = aBits;
			}
		}

		private quantclass[] qc_table = 
		{
			new quantclass( 3, -5 ),
			new quantclass( 5, -7 ),
			new quantclass( 7, 3 ),
			new quantclass( 9, -10 ),
			new quantclass( 15, 4 ),
			new quantclass( 31, 5 ),
			new quantclass( 63, 6 ),
			new quantclass( 127, 7 ),
			new quantclass( 255, 8 ),
			new quantclass( 511, 9 ),
			new quantclass( 1023, 10 ),
			new quantclass( 2047, 11 ),
			new quantclass( 4095, 12 ),
			new quantclass( 8191, 13 ),
			new quantclass( 16383, 14 ),
			new quantclass( 32767, 15 ),
			new quantclass( 65535, 16 )
		};
		
		private int[] intwinbase =
		{
			0,    -1,    -1,    -1,    -1,    -1,    -1,    -2,    -2,    -2,    -2,    -3,    -3,    -4,    -4,    -5,
			-5,    -6,    -7,    -7,    -8,    -9,   -10,   -11,   -13,   -14,   -16,   -17,   -19,   -21,   -24,   -26,
			-29,   -31,   -35,   -38,   -41,   -45,   -49,   -53,   -58,   -63,   -68,   -73,   -79,   -85,   -91,   -97,
			-104,  -111,  -117,  -125,  -132,  -139,  -147,  -154,  -161,  -169,  -176,  -183,  -190,  -196,  -202,  -208,
			-213,  -218,  -222,  -225,  -227,  -228,  -228,  -227,  -224,  -221,  -215,  -208,  -200,  -189,  -177,  -163,
			-146,  -127,  -106,   -83,   -57,   -29,     2,    36,    72,   111,   153,   197,   244,   294,   347,   401,
			459,   519,   581,   645,   711,   779,   848,   919,   991,  1064,  1137,  1210,  1283,  1356,  1428,  1498,
			1567,  1634,  1698,  1759,  1817,  1870,  1919,  1962,  2001,  2032,  2057,  2075,  2085,  2087,  2080,  2063,
			2037,  2000,  1952,  1893,  1822,  1739,  1644,  1535,  1414,  1280,  1131,   970,   794,   605,   402,   185,
			-45,  -288,  -545,  -814, -1095, -1388, -1692, -2006, -2330, -2663, -3004, -3351, -3705, -4063, -4425, -4788,
			-5153, -5517, -5879, -6237, -6589, -6935, -7271, -7597, -7910, -8209, -8491, -8755, -8998, -9219, -9416, -9585,
			-9727, -9838, -9916, -9959, -9966, -9935, -9863, -9750, -9592, -9389, -9139, -8840, -8492, -8092, -7640, -7134,
			-6574, -5959, -5288, -4561, -3776, -2935, -2037, -1082,   -70,   998,  2122,  3300,  4533,  5818,  7154,  8540,
			9975, 11455, 12980, 14548, 16155, 17799, 19478, 21189, 22929, 24694, 26482, 28289, 30112, 31947, 33791, 35640,
			37489, 39336, 41176, 43006, 44821, 46617, 48390, 50137, 51853, 53534, 55178, 56778, 58333, 59838, 61289, 62684,
			64019, 65290, 66494, 67629, 68692, 69679, 70590, 71420, 72169, 72835, 73415, 73908, 74313, 74630, 74856, 74992,
			75038
		};

		private double[][] costable = new double[5][];
		private double[] decwin = new double[512+32];

		private void dct( double[] src, double[] dst0, double[] dst1 )
		{
			double[][] tmp = new double[2][];
			tmp[0] = new double[32];
			tmp[1] = new double[32];

			for( int i = 0; i < 32; i++ )
			{
				if( ( i & 16 ) != 0 )
				{
					tmp[0][i] = ( - src[i] + src[31 ^ i]) * costable[0][~i & 15];
				}
				else
				{
					tmp[0][i] = ( + src[i] + src[31 ^ i]) ;
				}
			}
			
			for( int i = 0; i < 32; i++ )
			{
				if( ( i & 8 ) != 0 )
				{
					tmp[1][i] = ( - tmp[0][i] + tmp[0][15 ^ i]) * costable[1][~i & 7] * ( ( i & 16 ) != 0 ? -1.0d : 1.0d );
				}
				else
				{
					tmp[1][i] = ( + tmp[0][i] + tmp[0][15 ^ i]);
				}
			}

			for( int i = 0; i < 32; i++ )
			{
				if( ( i & 4 ) != 0 )
				{
					tmp[0][i] = ( - tmp[1][i] + tmp[1][7 ^ i]) * costable[2][~i & 3] * ( ( i & 8 ) != 0 ? -1.0d : 1.0d);
				}
				else
				{
					tmp[0][i] = ( + tmp[1][i] + tmp[1][7 ^ i]);
				}
			}
			
			for( int i=0; i < 32; i++)
			{
				if( ( i & 2 ) != 0 )
				{
					tmp[1][i] = ( - tmp[0][i] + tmp[0][3 ^ i]) * costable[3][~i & 1] * ( ( i & 4 ) != 0 ? -1.0d : 1.0d);
				}
				else
				{
					tmp[1][i] = ( + tmp[0][i] + tmp[0][3 ^ i]);
				}
			}

			for( int i = 0; i < 32; i++ )
			{
				if( ( i & 1 ) != 0 )
				{
					tmp[0][i] = ( - tmp[1][i] + tmp[1][1 ^ i]) * costable[4][0] * ( ( i & 2 ) != 0 ? -1.0d : 1.0d);
				}
				else
				{
					tmp[0][i] = ( + tmp[1][i] + tmp[1][1 ^ i]);
				}
			}

			for( int i = 0; i < 32; i += 4 )
			{
				tmp[0][i + 2] += tmp[0][i + 3]; //modify : from ExtractData
			}
  
			for( int i = 0; i < 32; i += 8 )
			{
				tmp[0][i + 4] += tmp[0][i + 6];
				tmp[0][i + 6] += tmp[0][i + 5];
				tmp[0][i + 5] += tmp[0][i + 7];
			} //modify : from ExtractData

			for( int i = 0; i < 32; i += 16 )
			{
				tmp[0][i +  8] += tmp[0][i + 12];
				tmp[0][i + 12] += tmp[0][i + 10];
				tmp[0][i + 10] += tmp[0][i + 14];
				tmp[0][i + 14] += tmp[0][i +  9];
				tmp[0][i +  9] += tmp[0][i + 13];
				tmp[0][i + 13] += tmp[0][i + 11];
				tmp[0][i + 11] += tmp[0][i + 15];
			} //modify : from ExtractData

			dst0[16] = tmp[0][0];
			dst0[15] = tmp[0][16+0]  + tmp[0][16+8];
			dst0[14] = tmp[0][8];
			dst0[13] = tmp[0][16+8]  + tmp[0][16+4];
			dst0[12] = tmp[0][4];
			dst0[11] = tmp[0][16+4]  + tmp[0][16+12];
			dst0[10] = tmp[0][12];
			dst0[ 9] = tmp[0][16+12] + tmp[0][16+2];
			dst0[ 8] = tmp[0][2];
			dst0[ 7] = tmp[0][16+2]  + tmp[0][16+10];
			dst0[ 6] = tmp[0][10];
			dst0[ 5] = tmp[0][16+10] + tmp[0][16+6];
			dst0[ 4] = tmp[0][6];
			dst0[ 3] = tmp[0][16+6]  + tmp[0][16+14];
			dst0[ 2] = tmp[0][14];
			dst0[ 1] = tmp[0][16+14] + tmp[0][16+1];
			dst0[ 0] = tmp[0][1];

			dst1[ 0] = tmp[0][1];
			dst1[ 1] = tmp[0][16+1]  + tmp[0][16+9];
			dst1[ 2] = tmp[0][9];
			dst1[ 3] = tmp[0][16+9]  + tmp[0][16+5];
			dst1[ 4] = tmp[0][5];
			dst1[ 5] = tmp[0][16+5]  + tmp[0][16+13];
			dst1[ 6] = tmp[0][13];
			dst1[ 7] = tmp[0][16+13] + tmp[0][16+3];
			dst1[ 8] = tmp[0][3];
			dst1[ 9] = tmp[0][16+3]  + tmp[0][16+11];
			dst1[10] = tmp[0][11];
			dst1[11] = tmp[0][16+11] + tmp[0][16+7];
			dst1[12] = tmp[0][7];
			dst1[13] = tmp[0][16+7]  + tmp[0][16+15];
			dst1[14] = tmp[0][15];
			dst1[15] = tmp[0][16+15];
		}

		private int decode_ahx( ByteArray aByteArray, Byte[] src, Int16[] dst, int srclen )
		{
			//UnityEngine.Debug.LogError( "decode_ahx" );

			int bit_rest = 0;
			int bit_data;
			int lIndexSrc = 0;
			int phase=0;
			int[] bit_alloc = new int[32];
			int[] scfsi = new int[32];
			int[][] scalefactor = new int[32][];//[3]={0}; // modify for bcc32

			for( int i = 0; i < 32; i++ )
			{
				scalefactor[i] = new int[3];
			}

			int lIndexDst = 0;
			double[][] sbsamples = new double[36][];//[32]={0}
			double[] powtable = new double[64];
			double[][][] dctbuf = new double[2][][];//[16][17]={0};
			int frame=0;

			for( int i = 0; i < 64; i++ )
			{
				powtable[i] = Math.Pow( 2.0d, ( 3.0d - i ) / 3.0d );
			}

			for( int i = 0; i < 36; i++ )
			{
				sbsamples[i] = new double[32];

				for( int sb = 0; sb < 32; sb++ )
				{
					sbsamples[i][sb] = 0.0d;
				}
			}

			for( int i = 0; i < 2; i++ )
			{
				dctbuf[i] = new double[16][];

				for( int j = 0; j < 16; j++ )
				{
					dctbuf[i][j] = new double[17];
				}
			}

			costable[0] = new double[16];
			costable[1] = new double[8];
			costable[2] = new double[4];
			costable[3] = new double[2];
			costable[4] = new double[1];

			for( int i = 0; i < 16; i++ )
			{
				costable[0][i] = 0.5d / Math.Cos( Math.PI * ( ( i << 1 ) + 1 ) / 64.0d ); //modify : from ExtractData
			}

			for( int i = 0; i < 8; i++ )
			{
				costable[1][i] = 0.5d / Math.Cos( Math.PI * ( ( i << 1 ) + 1 ) / 32.0d ); //modify : from ExtractData
			}

			for( int i = 0; i < 4; i++ )
			{
				costable[2][i] = 0.5d / Math.Cos( Math.PI * ( ( i << 1 ) + 1 ) / 16.0d ); //modify : from ExtractData
			}

			for( int i = 0; i < 2; i++ )
			{
				costable[3][i] = 0.5d / Math.Cos( Math.PI * ( ( i << 1 ) + 1 ) / 8.0d ); //modify : from ExtractData
			}

			for( int i = 0; i < 1; i++ )
			{
				costable[4][i] = 0.5d / Math.Cos( Math.PI * ( ( i << 1 ) + 1 ) / 4.0d ); //modify : from ExtractData
			}

			for( int i = 0, j = 0; i < 256; i++, j += 32 )
			{
				if( j < 512 + 16 )
				{
					decwin[j] = decwin[j + 16] = intwinbase[i] / 65536.0 * 32768.0 * ( ( i & 64 ) != 0 ? 1.0d : -1.0d );
				}

				if( ( i & 31 ) == 31 )
				{
					j -= 1023;
				}
			}

			for( int i = 0, j = 8; i < 256; i++, j += 32 )
			{
				if( j < 512 + 16 )
				{
					decwin[j] = decwin[j + 16] = intwinbase[256 - i] / 65536.0 * 32768.0 * ( ( i & 64 ) != 0 ? 1.0d : -1.0d );
				}

				if( ( i & 31 ) == 31 )
				{
					j -= 1023;
				}
			}

			//src[lIndexSrc] += ( Byte )( src[lIndexSrc + 2] * 256 + src[lIndexSrc + 3] + 4 );	// To Be Fixed.

			UInt16 read12 = aByteArray.ReadBitsAsUInt16( 12 );
			//UnityEngine.Debug.LogError( "read12:" + read12.ToString( "X4" ) );

			while( aByteArray.Position < srclen && read12 == 0x0FFF )//;getbits( &src, &bit_data, &bit_rest, 12 ) == 0xfff )
			{
				//UnityEngine.Debug.LogError( "while( aByteArray.Position < srclen && aByteArray.ReadBitsAsUInt32( 12 ) == 0x0FFF )" );

				frame++;
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// LSF
				aByteArray.ReadBitsAsByte( 2 );//getbits(&src, &bit_data, &bit_rest, 2);	// layer
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// CRC
				aByteArray.ReadBitsAsByte( 4 );//getbits(&src, &bit_data, &bit_rest, 4);	// bitrate
				aByteArray.ReadBitsAsByte( 2 );//getbits(&src, &bit_data, &bit_rest, 2);	// freq
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// padding
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// gap
				aByteArray.ReadBitsAsByte( 2 );//getbits(&src, &bit_data, &bit_rest, 2);	// mode
				aByteArray.ReadBitsAsByte( 2 );//getbits(&src, &bit_data, &bit_rest, 2);	// mode_ext
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// protect
				aByteArray.ReadBitsAsByte( 1 );//getbits(&src, &bit_data, &bit_rest, 1);	// copy
				aByteArray.ReadBitsAsByte( 2 );//getbits(&src, &bit_data, &bit_rest, 2);	// emphasis

				for( int sb = 0; sb < 30; sb++ )
				{
					bit_alloc[sb] = ( int )aByteArray.ReadBitsAsUInt32( bit_alloc_table[sb] );//getbits( &src, &bit_data, &bit_rest, bit_alloc_table[sb] );
				}

				for( int sb = 0; sb < 30; sb++ )
				{
					if( bit_alloc[sb] != 0 )
					{
						scfsi[sb] = ( int )aByteArray.ReadBitsAsByte( 2 );//getbits( &src, &bit_data, &bit_rest, 2 );
					}
				}

				for( int sb = 0; sb < 30; sb++ )
				{
					if( bit_alloc[sb] != 0 )
					{
						scalefactor[sb][0] = ( int )aByteArray.ReadBitsAsByte( 6 );//getbits(&src, &bit_data, &bit_rest, 6);

						switch( scfsi[sb] )
						{
						case 0:
							scalefactor[sb][1] = ( int )aByteArray.ReadBitsAsByte( 6 );//getbits(&src, &bit_data, &bit_rest, 6);
							scalefactor[sb][2] = ( int )aByteArray.ReadBitsAsByte( 6 );//getbits(&src, &bit_data, &bit_rest, 6);
							break;

						case 1:
							scalefactor[sb][1] = scalefactor[sb][0];
							scalefactor[sb][2] = ( int )aByteArray.ReadBitsAsByte( 6 );//getbits(&src, &bit_data, &bit_rest, 6);
							break;

						case 2:
							scalefactor[sb][1] = scalefactor[sb][0];
							scalefactor[sb][2] = scalefactor[sb][0];
							break;

						case 3:
							scalefactor[sb][1] = ( int )aByteArray.ReadBitsAsByte( 6 );//getbits(&src, &bit_data, &bit_rest, 6);
							scalefactor[sb][2] = scalefactor[sb][1];
							break;

						default:
							break;
						}
					}
				}

				for( int gr = 0; gr < 12; gr++ )
				{
					for( int sb = 0; sb < 30; sb++ )
					{
						if( bit_alloc[sb] != 0 )
						{
							int index = offset_table[bit_alloc_table[sb]][bit_alloc[sb] - 1];
							int q;

							if( qc_table[index].bits < 0 )
							{
								int t = ( int )aByteArray.ReadBitsAsUInt32( -qc_table[index].bits );//getbits(&src, &bit_data, &bit_rest, -qc_table[index].bits);
								q = ( t % qc_table[index].nlevels ) * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 0][sb] = ( double )q / ( double )qc_table[index].nlevels;
								t /= qc_table[index].nlevels;
								q = ( t % qc_table[index].nlevels ) * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 1][sb] = ( double )q / ( double )qc_table[index].nlevels;
								t /= qc_table[index].nlevels;
								q = t * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 2][sb] = ( double )q / ( double )qc_table[index].nlevels;
							}
							else
							{
								q = ( int )aByteArray.ReadBitsAsUInt32( qc_table[index].bits )/*getbits( &src, &bit_data, &bit_rest, qc_table[index].bits )*/ * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 0][sb] = ( double )q / ( double )qc_table[index].nlevels;
								q = ( int )aByteArray.ReadBitsAsUInt32( qc_table[index].bits )/*getbits( &src, &bit_data, &bit_rest, qc_table[index].bits )*/ * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 1][sb] = ( double )q / ( double )qc_table[index].nlevels;
								q = ( int )aByteArray.ReadBitsAsUInt32( qc_table[index].bits )/*getbits( &src, &bit_data, &bit_rest, qc_table[index].bits )*/ * 2 - qc_table[index].nlevels + 1;
								sbsamples[gr * 3 + 2][sb] = ( double )q / ( double )qc_table[index].nlevels;
							}
						}
						else
						{
							sbsamples[gr * 3 + 0][sb] = 0;
							sbsamples[gr * 3 + 1][sb] = 0;
							sbsamples[gr * 3 + 2][sb] = 0;
						}

						sbsamples[gr * 3 + 0][sb] *= powtable[scalefactor[sb][gr >> 2]]; //modify : from ExtractData
						sbsamples[gr * 3 + 1][sb] *= powtable[scalefactor[sb][gr >> 2]]; //modify : from ExtractData
						sbsamples[gr * 3 + 2][sb] *= powtable[scalefactor[sb][gr >> 2]]; //modify : from ExtractData
					}
				}

				// synth
				for( int gr = 0; gr < 36; gr++ )
				{
					double sum = 0;

					if( ( phase & 1 ) != 0 )
					{
						dct( sbsamples[gr], dctbuf[0][phase + 1 & 15], dctbuf[1][phase] );
					}
					else
					{
						dct( sbsamples[gr], dctbuf[1][phase], dctbuf[0][phase + 1] );
					}

					//win = decwin + 16 - ( phase | 1 );
					int lBaseIndex = 16 - ( phase | 1 );

					for( int i = 0; i < 16; i++, lBaseIndex += 32 )
					{
						sum = 0;
						sum += decwin[lBaseIndex + 0] * dctbuf[phase & 1][0][i];
						sum -= decwin[lBaseIndex + 1] * dctbuf[phase & 1][1][i];
						sum += decwin[lBaseIndex + 2] * dctbuf[phase & 1][2][i];
						sum -= decwin[lBaseIndex + 3] * dctbuf[phase & 1][3][i];
						sum += decwin[lBaseIndex + 4] * dctbuf[phase & 1][4][i];
						sum -= decwin[lBaseIndex + 5] * dctbuf[phase & 1][5][i];
						sum += decwin[lBaseIndex + 6] * dctbuf[phase & 1][6][i];
						sum -= decwin[lBaseIndex + 7] * dctbuf[phase & 1][7][i];
						sum += decwin[lBaseIndex + 8] * dctbuf[phase & 1][8][i];
						sum -= decwin[lBaseIndex + 9] * dctbuf[phase & 1][9][i];
						sum += decwin[lBaseIndex + 10] * dctbuf[phase & 1][10][i];
						sum -= decwin[lBaseIndex + 11] * dctbuf[phase & 1][11][i];
						sum += decwin[lBaseIndex + 12] * dctbuf[phase & 1][12][i];
						sum -= decwin[lBaseIndex + 13] * dctbuf[phase & 1][13][i];
						sum += decwin[lBaseIndex + 14] * dctbuf[phase & 1][14][i];
						sum -= decwin[lBaseIndex + 15] * dctbuf[phase & 1][15][i];

						if( sum >= 32767 )
						{
							sum = 32767;
						}
						else if( sum <= -32767 )
						{
							sum = -32767;
						}

						dst[lIndexDst] = ( Int16 )sum;
						lIndexDst++;
					}

					sum = 0;
					sum += decwin[lBaseIndex + 0] * dctbuf[phase & 1][0][16];
					sum += decwin[lBaseIndex + 2] * dctbuf[phase & 1][2][16];
					sum += decwin[lBaseIndex + 4] * dctbuf[phase & 1][4][16];
					sum += decwin[lBaseIndex + 6] * dctbuf[phase & 1][6][16];
					sum += decwin[lBaseIndex + 8] * dctbuf[phase & 1][8][16];
					sum += decwin[lBaseIndex + 10] * dctbuf[phase & 1][10][16];
					sum += decwin[lBaseIndex + 12] * dctbuf[phase & 1][12][16];
					sum += decwin[lBaseIndex + 14] * dctbuf[phase & 1][14][16];

					if( sum >= 32767 )
					{
						sum = 32767;
					}
					else if( sum <= -32767 )
					{
						sum = -32767;
					}

					dst[lIndexDst] = ( Int16 )sum;
					lIndexDst++;

					//win += -16 + (phase|1)*2;
					lBaseIndex += -16 + ( phase | 1 ) * 2;

					for( int i = 15; i >= 1; i--, lBaseIndex -= 32 )
					{
						sum = 0;

						sum -= decwin[lBaseIndex - 1] * dctbuf[phase & 1][0][i];
						sum -= decwin[lBaseIndex - 2] * dctbuf[phase & 1][1][i];
						sum -= decwin[lBaseIndex - 3] * dctbuf[phase & 1][2][i];
						sum -= decwin[lBaseIndex - 4] * dctbuf[phase & 1][3][i];
						sum -= decwin[lBaseIndex - 5] * dctbuf[phase & 1][4][i];
						sum -= decwin[lBaseIndex - 6] * dctbuf[phase & 1][5][i];
						sum -= decwin[lBaseIndex - 7] * dctbuf[phase & 1][6][i];
						sum -= decwin[lBaseIndex - 8] * dctbuf[phase & 1][7][i];
						sum -= decwin[lBaseIndex - 9] * dctbuf[phase & 1][8][i];
						sum -= decwin[lBaseIndex - 10] * dctbuf[phase & 1][9][i];
						sum -= decwin[lBaseIndex - 11] * dctbuf[phase & 1][10][i];
						sum -= decwin[lBaseIndex - 12] * dctbuf[phase & 1][11][i];
						sum -= decwin[lBaseIndex - 13] * dctbuf[phase & 1][12][i];
						sum -= decwin[lBaseIndex - 14] * dctbuf[phase & 1][13][i];
						sum -= decwin[lBaseIndex - 15] * dctbuf[phase & 1][14][i];
						sum -= decwin[lBaseIndex - 16] * dctbuf[phase & 1][15][i];

						if( sum >= 32767 )
						{
							sum = 32767;
						}
						else if( sum <= -32767 )
						{
							sum = -32767;
						}

						dst[lIndexDst] = ( Int16 )sum;
						lIndexDst++;
					}

					phase = phase-1 & 15;
				}

				// skip padding bits
				if( aByteArray.GetBitPositionInByte() != 0 )
				{
					aByteArray.ReadBitsAsUInt32( 8 - aByteArray.GetBitPositionInByte() );//getbits( &src, &bit_data, &bit_rest, bit_rest & 7 );
				}

				//UnityEngine.Debug.LogError( "Addr:" + ( aByteArray.Position + 0x40 ).ToString( "X8" ) );
				read12 = aByteArray.ReadBitsAsUInt16( 12 );
				//UnityEngine.Debug.LogError( "read12:" + read12.ToString( "X4" ) );
			}

			return lIndexDst;//( ( char* )dst_p - ( char* )dst ); //modify : from ExtractData
		}

		public AhxHeader( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayBig( aStream );

			offset_table = new int[5][];
			offset_table[0] = offset_table0;
			offset_table[1] = offset_table1;
			offset_table[2] = offset_table2;
			offset_table[3] = offset_table3;
			offset_table[4] = offset_table4;
			//fseek( fp, 0, SEEK_END );
			int src_buf_len = lByteArray.Length;
			//int src_buf_rem = src_buf_len;
			Byte[] src_buf_set = new Byte[src_buf_len];
			//fseek( fp, 0, SEEK_SET );
			//fread( src_buf_set, 1, src_buf_len, fp );
			//src_buf = src_buf_set;
			//fclose( fp );

			int ahx_count = 0;

			//while( aByteArray.Position < aByteArray.GetLength() )
			//{
				int lIndexAhxHed;
				int lIndexAhxFot;

				UInt16 head = lByteArray.ReadUInt16();
				UInt16 offset = lByteArray.ReadUInt16();

				//UnityEngine.Debug.LogError( "head:" + head );
				//UnityEngine.Debug.LogError( "offset:" + offset.ToString( "X4" ) );
				lIndexAhxHed = 0;
				lIndexAhxFot = lByteArray.Find( AHXFOT );

				//UnityEngine.Debug.LogError( "lIndexAhxHed:" + lIndexAhxHed );
				//UnityEngine.Debug.LogError( "lIndexAhxFot:" + lIndexAhxFot );

				//ahx_hed = lByteArray.ReadString( AHXHED.Length - 1 );//memmem( src_buf, src_buf_rem, AHXHED, sizeof( AHXHED ) - 1 );
				//ahx_fot = lByteArray.ReadString( AHXFOT.Length - 1 );//memmem( src_buf, src_buf_rem, AHXFOT, sizeof( AHXFOT ) - 1 );

				lByteArray.AddPosition( offset - 6 );

				if( head != 0x8000 || lIndexAhxFot == -1 )
				{
					Logger.LogError( "head != 0x8000 || lIndexAhxFot == -1" );
					lByteArray.SetPosition( lByteArray.Length );//src_buf_rem = 0;
				}
				else if( lByteArray.Find( AHXCHK, 0 ) == -1 )//strncmp( ahx_hed + 30, AHXCHK, sizeof( AHXCHK ) - 1 ) != 0 )
				{
					Logger.LogError( "lByteArrayChk.Find( AHXCHK, 0 ) == -1" );
					lByteArray.AddPosition( 1 );//src_buf++;
					//src_buf_rem--;
				}
				else
				{
					lByteArray.AddPosition( 6 );
					//UnityEngine.Debug.LogError( "Adde:" + lByteArray.Position.ToString( "X8" ) );
					int lLengthAhxBuf = lIndexAhxFot + 12 - lByteArray.Position;//ahx_buf_len = ahx_fot + 12 - ahx_hed;
					Byte[] ahx_buf = lByteArray.ReadBytes( ( UInt32 )lLengthAhxBuf );//ahx_buf = malloc( ahx_buf_len );
					MemoryStream lMemoryStream = new MemoryStream( ahx_buf );
					ByteArray lByteArrayBuf = new ByteArrayBig( lMemoryStream );

					ahx_count++;

					//int lLengthAhxBuf = lIndexAhxFot + 12 - lIndexAhxHed;//ahx_buf_len = ahx_fot + 12 - ahx_hed;
					//Byte[] ahx_buf = lByteArray.ReadBytes( lLengthAhxBuf );//ahx_buf = malloc( ahx_buf_len );
					//memcpy( ahx_buf, ahx_hed, ahx_buf_len );
					lByteArray.SetPosition( 0 );
					UInt32 uint1 = lByteArray.ReadUInt32();
					UInt32 uint2 = lByteArray.ReadUInt32();
					UInt32 uint3 = lByteArray.ReadUInt32();
					UInt32 uint4 = lByteArray.ReadUInt32();
					UInt32 uint5 = lByteArray.ReadUInt32();
					UInt32 uint6 = lByteArray.ReadUInt32();
					UInt32 uint7 = lByteArray.ReadUInt32();
					UInt32 uint8 = lByteArray.ReadUInt32();

					//UnityEngine.Debug.LogError( "1:" + uint1 );
					//UnityEngine.Debug.LogError( "2:" + uint2 );
					//UnityEngine.Debug.LogError( "3:" + uint3 );
					//UnityEngine.Debug.LogError( "4:" + uint4 );
					//UnityEngine.Debug.LogError( "5:" + uint5 );
					//UnityEngine.Debug.LogError( "6:" + uint6 );
					//UnityEngine.Debug.LogError( "7:" + uint7 );
					//UnityEngine.Debug.LogError( "8:" + uint8 );

					int wav_buf_len = ( int )( uint4 * 2 );//wav_buf_len = getlongb( ahx_buf + 12 ) * 2;
					Int16[] wav_buf = new Int16[wav_buf_len + 1152 * 16];//wav_buf = malloc( wav_buf_len + 1152 * 16 ); //modify : from ExtractData; //+1152*2); margen = layer-2 frame size
					wav_buf_len = decode_ahx( lByteArrayBuf, ahx_buf, wav_buf, lLengthAhxBuf ); //modify : from ExtractData

					sampleArray = new float[2][];
					sampleArray[0] = new float[wav_buf_len + 1152 * 16];
					sampleArray[1] = new float[wav_buf_len + 1152 * 16];

					for( int i = 0; i < wav_buf_len + 1152 * 16; i++ )
					{
						sampleArray[0][i] = ( float )wav_buf[i] / 0x8000;
						sampleArray[1][i] = ( float )wav_buf[i] / 0x8000;
					}
				}
			//}
		}

		public Byte GetChannelLength()
		{
			return 2;
		}

		public UInt32 GetSampleRate()
		{
			return 22050;
		}

		public int GetSampleLength()
		{
			return sampleArray[0].Length;
		}

		public UInt32 GetSampleLoopStart()
		{
			return 0;
		}

		public int GetSampleLoopEnd()
		{
			return sampleArray[0].Length - 1;
		}

		public float[][] GetSampleArray()
		{
			return sampleArray;
		}
	}
}
