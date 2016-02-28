using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class FloorHeader
	{
		public UInt16 types;
		public Byte order0;
		public UInt16 rate0;
		public UInt16 barkMapSize0;
		public Byte amplitudeBits0;
		public Byte amplitudeOffset0;
		public Byte numberOfBooks0;
		public Byte numberOfBooks0Add1;
		public Byte[] bookList0;

		public Byte partitions1;
		public List<UInt16> xList1;
		public Byte values1;
		public Byte[] partitionClassList1;
		public Byte[] classDimensions1;
		public Byte[] classDimensions1Add1;
		public Byte[] classSubClasses1;
		public Byte[] classMasterbooks1;
		public Byte[][] subClassBooks1;
		public int[][] subClassBooks1Sub1;
		public Byte multiplier1;
		public Byte multiplier1Add1;

		private UInt16[] floorYList1;
		private Byte[] floor1_step2_flag;
		private int[] floor1_final_Y;

		UInt16 range;

		public FloorHeader( ByteArray aByteArray )
		{
			types = aByteArray.ReadBitsAsUInt16( 16 );

			Logger.LogDebug( "Vorbis Floor Types:" + types.ToString() );

			if( types == 0x0000 )
			{
				Logger.LogDebug( "■Floor 0." );
				DecodeHeader0( aByteArray );
			}
			else if( types == 0x0001 )
			{
				Logger.LogDebug( "■Floor 1." );
				DecodeHeader1( aByteArray );
			}
			else
			{
				Logger.LogError( "■Undefined Floor Type." );
			}
		}

		private void DecodeHeader0( ByteArray aByteArray )
		{
			order0 = aByteArray.ReadBitsAsByte( 8 );
			rate0 = aByteArray.ReadBitsAsUInt16( 16 );
			barkMapSize0 = aByteArray.ReadBitsAsUInt16( 16 );
			amplitudeBits0 = aByteArray.ReadBitsAsByte( 6 );
			amplitudeOffset0 = aByteArray.ReadBitsAsByte( 8 );
			numberOfBooks0 = aByteArray.ReadBitsAsByte( 4 );
			numberOfBooks0Add1 = ( Byte )( numberOfBooks0 + 1 );
			bookList0 = new Byte[numberOfBooks0Add1];

			for( int j = 0; j < numberOfBooks0Add1; j++ )
			{
				bookList0[j] = aByteArray.ReadBitsAsByte( 8 );
			}
		}

		private void DecodeHeader1( ByteArray aByteArray )
		{
			partitions1 = aByteArray.ReadBitsAsByte( 5 );
			int lMaximumClass = -1;

			partitionClassList1 = new Byte[partitions1];

			Logger.LogDebug( "Floor1 Partitions:" + partitions1.ToString() );

			for( int i = 0; i < partitions1; i++ )
			{
				partitionClassList1[i] = aByteArray.ReadBitsAsByte( 4 );

				Logger.LogDebug( "Floor1 Partition Class List:" + partitionClassList1[i].ToString() );

				if( partitionClassList1[i] > lMaximumClass )
				{
					lMaximumClass = partitionClassList1[i];
				}
			}

			lMaximumClass++;

			Logger.LogDebug( "Maximum Class:" + lMaximumClass.ToString() );

			classDimensions1 = new Byte[lMaximumClass];
			classDimensions1Add1 = new Byte[lMaximumClass];
			classSubClasses1 = new Byte[lMaximumClass];
			classMasterbooks1 = new Byte[lMaximumClass];
			subClassBooks1 = new Byte[lMaximumClass][];
			subClassBooks1Sub1 = new int[lMaximumClass][];

			for( int i = 0; i < lMaximumClass; i++ )
			{
				classDimensions1[i] = aByteArray.ReadBitsAsByte( 3 );
				classDimensions1Add1[i] = ( Byte )( classDimensions1[i] + 1 );
				classSubClasses1[i] = aByteArray.ReadBitsAsByte( 2 );

				subClassBooks1[i] = new Byte[( int )Math.Pow( 2, classSubClasses1[i] )];
				subClassBooks1Sub1[i] = new int[( int )Math.Pow( 2, classSubClasses1[i] )];

				if( classSubClasses1[i] != 0 )
				{
					classMasterbooks1[i] = aByteArray.ReadBitsAsByte( 8 );
				}

				for( int j = 0; j < ( int )Math.Pow( 2, classSubClasses1[i] ); j++ )
				{
					subClassBooks1[i][j] = aByteArray.ReadBitsAsByte( 8 );
					subClassBooks1Sub1[i][j] = subClassBooks1[i][j] - 1;
				}
			}

			multiplier1 = aByteArray.ReadBitsAsByte( 2 );
			multiplier1Add1 = ( Byte )( multiplier1 + 1 );
			Byte lRangeBits = aByteArray.ReadBitsAsByte( 4 );

			Logger.LogDebug( "multiplier1Add1:" + multiplier1Add1 );

			xList1 = new List<UInt16>();

			xList1.Add( 0 );
			xList1.Add( ( UInt16 )Math.Pow( 2, lRangeBits ) );
			values1 = 2;

			for( int i = 0; i < partitions1; i++ )
			{
				Byte lCurrentClassNumber = partitionClassList1[i];

				Logger.LogDebug( "Current Class Number:" + lCurrentClassNumber.ToString() );

				for( int j = 0; j < classDimensions1Add1[lCurrentClassNumber]; j++ )
				{
					xList1.Add( aByteArray.ReadBitsAsByte( lRangeBits ) );
					Logger.LogDebug( "xList1[" + values1 + ":" + xList1[values1] );
					values1++;
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

		public int DecodePacket( ByteArray aByteArray, CodebookHeader[] aCodebookHeaderArray )
		{
			if( types == 0x00 )
			{
				return DecodePacket0( aByteArray );
			}
			else if( types == 0x01 )
			{
				return DecodePacket1( aByteArray, aCodebookHeaderArray );
			}

			return 1;
		}

		private int DecodePacket0( ByteArray aByteArray )
		{
			Logger.LogError( "■DecodePacket0" );

			UInt32 lAmplitude = aByteArray.ReadBitsAsUInt32( amplitudeBits0 );
			
			Byte[] coefficients;

			if( lAmplitude > 0 )
			{
				coefficients = new Byte[0];

				UInt16 lBookNumber = aByteArray.ReadBitsAsUInt16( ilog( numberOfBooks0 ) );

				// Check.
				if( lBookNumber > 9999 )
				{
					Logger.LogError( "■The Packet Is Undecodable." );

					return 1;
				}

				int lLast = 0x00;
				int lTempVector = bookList0[lBookNumber];

				lTempVector += lLast;
				lLast = lTempVector;

				coefficients = new Byte[1];

				if( coefficients.Length < order0 )
				{
					// Check.
				}
			}

			return 0;
		}

		private int DecodePacket1( ByteArray aByteArray, CodebookHeader[] aCodebookHeaderArray )
		{
			Logger.LogWarning( "■DecodePacket1" );

			Byte lNonzero = aByteArray.ReadBitsAsByte( 1 );

			if( lNonzero == 0x01 )
			{
				Logger.LogWarning( "Nonzero" );

				UInt16[] vector = { 256, 128, 86, 64 };
				range = vector[multiplier1Add1 - 1];

				Logger.LogWarning( "Range" + range );
				//Logger.LogWarning( "iLog:" + ilog( ( UInt32 )( range - 1 ) ) );

				Logger.LogWarning( "Count:" + xList1.Count );
				floorYList1 = new UInt16[xList1.Count];

				floorYList1[0] = aByteArray.ReadBitsAsUInt16( ilog( ( UInt32 )( range - 1 ) ) );
				floorYList1[1] = aByteArray.ReadBitsAsUInt16( ilog( ( UInt32 )( range - 1 ) ) );

				//Logger.LogError( "xList1,floorYList1[0]:" + xList1[0] + "," + floorYList1[0] );
				//Logger.LogError( "xList1,floorYList1[1]:" + xList1[1] + "," + floorYList1[1] );

				int offset = 2;

				for( int i = 0; i < partitions1; i++ )
				{
					Byte lClass = partitionClassList1[i];
					int cdim = classDimensions1Add1[lClass];
					Byte cbits = classSubClasses1[lClass];
					int csub = ( int )Math.Pow( 2, cbits ) - 1;
					UInt32 cval = 0;

					if( cbits > 0 )
					{
						CodebookHeader lCodebookHeader = aCodebookHeaderArray[classMasterbooks1[lClass]];
						int lHuffman = lCodebookHeader.huffmanRoot.Read( aByteArray );
						//Logger.LogError( "Huffman1:" + lHuffman );
						cval = ( UInt32 )lHuffman;
					}

					for( int j = 0; j < cdim; j++ )
					{
						int book = subClassBooks1Sub1[lClass][cval & csub];

						cval >>= cbits;

						if( book >= 0 )
						{
							CodebookHeader lCodebookHeader = aCodebookHeaderArray[book];
							int lHuffman = lCodebookHeader.huffmanRoot.Read( aByteArray );
							//Logger.LogError( "Huffman2:" + lHuffman );
							floorYList1[j + offset] = ( UInt16 )lHuffman;
						}
						else
						{
							floorYList1[j + offset] = 0;
						}

						//Logger.LogError( "xList1,floorYList1[" + ( j + offset ) + "]:" + xList1[j + offset] + "," + floorYList1[j + offset] );
					}

					offset += cdim;
				}
			}
			else
			{
				Logger.LogError( "Zero" );

				return 1;
			}

			return 0;
		}

		public void ComputeCurve( double[] a, int n )
		{
			Logger.LogWarning( "■ComputeCurve" );

			if( types == 0x00 )
			{
				Logger.LogError( "■ComputeCurve0" );
			}
			else if( types == 0x01 )
			{
				SynthesisAmplitudeValue();
				SynthesisCurve( a, n );
			}
		}

		public void SynthesisAmplitudeValue()
		{
			Logger.LogWarning( "■SynthesisAmplitudeValue" );

			UInt16[] vector = { 256, 128, 86, 64 };
			range = vector[multiplier1Add1 - 1];

			floor1_step2_flag = new Byte[values1];
			floor1_final_Y = new int[values1];

			floor1_step2_flag[0] = 0x01;
			floor1_step2_flag[1] = 0x01;
			floor1_final_Y[0] = floorYList1[0];
			floor1_final_Y[1] = floorYList1[1];

			for( int i = 2; i < values1; i++ )
			{
				int low_neighbor_offset = lowNeighbour( xList1, ( UInt16 )i );
				int high_neighbor_offset = highNeighbour( xList1, ( UInt16 )i );

				int predicted = CalculateRenderPoint( xList1[low_neighbor_offset], floor1_final_Y[low_neighbor_offset], xList1[high_neighbor_offset], floor1_final_Y[high_neighbor_offset], xList1[i] );

				UInt16 val = floorYList1[i];

				int highroom = range - predicted;
				int lowroom = predicted;
				int room = 0;

				if( lowroom <= highroom )
				{
					room = lowroom * 2;
				}
				else // lLowRoom > lHighRoom
				{
					room = highroom * 2;
				}

				if( val == 0 )
				{
					floor1_step2_flag[i] = 0x00;
					floor1_final_Y[i] = predicted;
				}
				else // if( val != 0 )
				{
					floor1_step2_flag[low_neighbor_offset] = 0x01;
					floor1_step2_flag[high_neighbor_offset] = 0x01;
					floor1_step2_flag[i] = 0x01;

					if( val < room )
					{
						if( val % 2 == 0 )
						{
							floor1_final_Y[i] = predicted + ( int )( val / 2 );
						}
						else // if( val % 2 != 0 )
						{
							floor1_final_Y[i] = predicted - ( int )( ( val + 1 ) / 2 );
						}
					}
					else // if( val >= room )
					{
						if( lowroom < highroom )
						{
							floor1_final_Y[i] = val - lowroom + predicted;
						}
						else // if( lLowRoom >= lHighRoom )
						{
							floor1_final_Y[i] = predicted - val + highroom - 1;
						}
					}
				}

				//Logger.LogError( predicted + "," + floor1_final_Y[i] );
			}
		}

		public void SynthesisCurve( double[] a, int n )
		{
			Logger.LogWarning( "■SynthesisCurve Start" );

			/*
			int[] lXList1 = new int[xList1.Count];
			int[] lFloor1_final_Y = new int[floor1_final_Y.Length];
			Byte[] lFloor1_step2_flag = new Byte[floor1_step2_flag.Length];

			for( int i = 0; i < xList1.Count; i++ )
			{
				lXList1[i] = xList1[i];
			}

			for( int i = 0; i < floor1_final_Y.Length; i++ )
			{
				lFloor1_final_Y[i] = floor1_final_Y[i];
			}

			for( int i = 0; i < floor1_step2_flag.Length; i++ )
			{
				lFloor1_step2_flag[i] = floor1_step2_flag[i];
			}

			Sort( lXList1, lFloor1_final_Y, lFloor1_step2_flag );
			*/
			int[] floor1_idx = new int[values1];

			for( int i = 0; i < values1; i++)
			{
				floor1_idx[i] = i;
			}

			for( int i = values1 - 1; i > 0; i-- )
			{
				for( int j = i - 1; j >= 0; j-- )
				{
					if( xList1[floor1_idx[i]] < xList1[floor1_idx[j]] )
					{
						int w = floor1_idx[i];
						floor1_idx[i] = floor1_idx[j];
						floor1_idx[j] = w;
					}
				}
			}

			int hx = 0;
			int hy = 0;
			int lx = 0;
			int ly = floor1_final_Y[0] * multiplier1Add1;

			//Logger.LogError( "■i,x,a:" + 0 + "," + lXList1[0] + "," + lFloor1_final_Y[0] );

			for( int I = 1; I < values1; I++ )
			{
				//Logger.LogError( "■i,x,a:" + i + "," + lXList1[i] + "," + lFloor1_final_Y[i] );

				int i = floor1_idx[I];

				if( floor1_step2_flag[i] == 0x01 )
				{
					hy = floor1_final_Y[i] * multiplier1Add1;
					hx = xList1[i];

					RenderLine( lx, ly, hx, hy, a );
					lx = hx;
					ly = hy;
				}
				else
				{
					//floor[i] = new int[0];
				}
			}

			if( hx < n / 2 )
			{
				RenderLine( hx, hy, n / 2, hy, a );
			}

			if( hx > n / 2 )
			{
				Logger.LogError( "truncate vector [floor] to [n] elements" );
				//truncate vector [floor] to [n] elements
			}
		}

		public int lowNeighbour( List<UInt16> v, UInt16 x )
		{
			int max = -1;
			int n = 0;

			for( int i = 0; i < v.Count && i < x; i++ )
			{
				if( v[i] >= max && v[i] < v[x] )
				{
					max = v[i];
					n = i;
				}
			}

			return n;
		}

		public int highNeighbour( List<UInt16> v, UInt16 x )
		{
			int min = 0x7FFFFFFF;
			int n = 0;

			for( int i = 0; i < v.Count && i < x; i++ )
			{
				if( v[i] <= min && v[i] > v[x] )
				{
					min = v[i];
					n = i;
				}
			}

			return n;
		}

		private static void Sort( int[] x, int[] y, Byte[] b )
		{
			int off = 0;
			int len = x.Length;
			int lim = len + off;
			int itmp;
			Byte btmp;

			// Insertion sort on smallest arrays
			for( int i = off; i < lim; i++ )
			{
				for( int j = i; j > off && x[j - 1] > x[j]; j-- )
				{
					itmp = x[j];
					x[j] = x[j - 1];
					x[j - 1] = itmp;
					itmp = y[j];
					y[j] = y[j - 1];
					y[j - 1] = itmp;
					btmp = b[j];
					b[j] = b[j - 1];
					b[j - 1] = btmp;
					//swap(x, j, j-1);
					//swap(y, j, j-1);
					//swap(b, j, j-1);
				}
			}
		}

		private static void swap( int[] x, int a, int b )
		{
			int t = x[a];
			x[a] = x[b];
			x[b] = t;
		}

		private static void swap( bool[] x, int a, int b )
		{
			bool t = x[a];
			x[a] = x[b];
			x[b] = t;
		}

		private int CalculateRenderPoint( int x0, int y0, int x1, int y1, int X )
		{
			int Y = 0;

			int dy = y1 - y0;
			int dx = x1 - x0;
			int ady = Math.Abs( dy );
			int err = ady * ( X - x0 );
			int off = ( int )( err / dx );

			if( dy < 0 )
			{
				Y = y0 - off;
			}
			else
			{
				Y = y0 + off;
			}

			return Y;
		}

		private void RenderLine( int x0, int y0, int x1, int y1, double[] v )
		{
			//Logger.LogError( "RenderLine x0,x1:" + x0 + "," + x1 );

			int dy = y1 - y0;
			int dx = x1 - x0;
			int ady = Math.Abs( dy );
			int bas = dy / dx;
			int x = x0;
			int y = y0;
			int err = 0;

			int sy = 0;

			if( dy < 0 )
			{
				sy = bas - 1;
			}
			else
			{
				sy = bas + 1;
			}

			ady -= Math.Abs( bas ) * dx;

			v[x] = floor1_inverse_dB_table[y];
			//Logger.LogError( "x,y,dB:" + x + "," + y + "," + v[x] );

			for( x = x0 + 1; x < x1; x++ )
			{
				err += ady;

				if( err >= dx )
				{
					err -= dx;
					y += sy;
				}
				else
				{
					y += bas;
				}

				v[x] = floor1_inverse_dB_table[y];

				//Logger.LogError( "x,y,dB:" + x + "," + y + "," + v[x] );
			}
		}

		private double[] floor1_inverse_dB_table =
		{
			0.00000010649863, 0.00000011341951, 0.00000012079015, 0.00000012863978,
			0.00000013699951, 0.00000014590251, 0.00000015538408, 0.00000016548181,
			0.00000017623575, 0.00000018768855, 0.00000019988561, 0.00000021287530,
			0.00000022670913, 0.00000024144197, 0.00000025713223, 0.00000027384213,
			0.00000029163793, 0.00000031059021, 0.00000033077411, 0.00000035226968,
			0.00000037516214, 0.00000039954229, 0.00000042550680, 0.00000045315863,
			0.00000048260743, 0.00000051396998, 0.00000054737065, 0.00000058294187,
			0.00000062082472, 0.00000066116941, 0.00000070413592, 0.00000074989464,
			0.00000079862701, 0.00000085052630, 0.00000090579828, 0.00000096466216,
			0.0000010273513, 0.0000010941144, 0.0000011652161, 0.0000012409384,
			0.0000013215816, 0.0000014074654, 0.0000014989305, 0.0000015963394,
			0.0000017000785, 0.0000018105592, 0.0000019282195, 0.0000020535261,
			0.0000021869758, 0.0000023290978, 0.0000024804557, 0.0000026416497,
			0.0000028133190, 0.0000029961443, 0.0000031908506, 0.0000033982101,
			0.0000036190449, 0.0000038542308, 0.0000041047004, 0.0000043714470,
			0.0000046555282, 0.0000049580707, 0.0000052802740, 0.0000056234160,
			0.0000059888572, 0.0000063780469, 0.0000067925283, 0.0000072339451,
			0.0000077040476, 0.0000082047000, 0.0000087378876, 0.0000093057248,
			0.0000099104632, 0.000010554501, 0.000011240392, 0.000011970856,
			0.000012748789, 0.000013577278, 0.000014459606, 0.000015399272,
			0.000016400004, 0.000017465768, 0.000018600792, 0.000019809576,
			0.000021096914, 0.000022467911, 0.000023928002, 0.000025482978,
			0.000027139006, 0.000028902651, 0.000030780908, 0.000032781225,
			0.000034911534, 0.000037180282, 0.000039596466, 0.000042169667,
			0.000044910090, 0.000047828601, 0.000050936773, 0.000054246931,
			0.000057772202, 0.000061526565, 0.000065524908, 0.000069783085,
			0.000074317983, 0.000079147585, 0.000084291040, 0.000089768747,
			0.000095602426, 0.00010181521, 0.00010843174, 0.00011547824,
			0.00012298267, 0.00013097477, 0.00013948625, 0.00014855085,
			0.00015820453, 0.00016848555, 0.00017943469, 0.00019109536,
			0.00020351382, 0.00021673929, 0.00023082423, 0.00024582449,
			0.00026179955, 0.00027881276, 0.00029693158, 0.00031622787,
			0.00033677814, 0.00035866388, 0.00038197188, 0.00040679456,
			0.00043323036, 0.00046138411, 0.00049136745, 0.00052329927,
			0.00055730621, 0.00059352311, 0.00063209358, 0.00067317058,
			0.00071691700, 0.00076350630, 0.00081312324, 0.00086596457,
			0.00092223983, 0.00098217216, 0.0010459992,  0.0011139742,
			0.0011863665,  0.0012634633,  0.0013455702,  0.0014330129,
			0.0015261382,  0.0016253153,  0.0017309374,  0.0018434235,
			0.0019632195,  0.0020908006,  0.0022266726,  0.0023713743,
			0.0025254795,  0.0026895994,  0.0028643847,  0.0030505286,
			0.0032487691,  0.0034598925,  0.0036847358,  0.0039241906,
			0.0041792066,  0.0044507950,  0.0047400328,  0.0050480668,
			0.0053761186,  0.0057254891,  0.0060975636,  0.0064938176,
			0.0069158225,  0.0073652516,  0.0078438871,  0.0083536271,
			0.0088964928,  0.009474637,   0.010090352,   0.010746080,
			0.011444421,   0.012188144,   0.012980198,   0.013823725,
			0.014722068,   0.015678791,   0.016697687,   0.017782797,
			0.018938423,   0.020169149,   0.021479854,   0.022875735,
			0.024362330,   0.025945531,   0.027631618,   0.029427276,
			0.031339626,   0.033376252,   0.035545228,   0.037855157,
			0.040315199,   0.042935108,   0.045725273,   0.048696758,
			0.051861348,   0.055231591,   0.058820850,   0.062643361,
			0.066714279,   0.071049749,   0.075666962,   0.080584227,
			0.085821044,   0.091398179,   0.097337747,   0.10366330,
			0.11039993,    0.11757434,    0.12521498,    0.13335215,
			0.14201813,    0.15124727,    0.16107617,    0.17154380,
			0.18269168,    0.19456402,    0.20720788,    0.22067342,
			0.23501402,    0.25028656,    0.26655159,    0.28387361,
			0.30232132,    0.32196786,    0.34289114,    0.36517414,
			0.38890521,    0.41417847,    0.44109412,    0.46975890,
			0.50028648,    0.53279791,    0.56742212,    0.60429640,
			0.64356699,    0.68538959,    0.72993007,    0.77736504,
			0.82788260,    0.88168307,    0.9389798,     1.0
		};
	}

	public class VorbisFloor
	{
		public int count;
		public FloorHeader[] header;

		public VorbisFloor( ByteArray aByteArray )
		{
			count = aByteArray.ReadBitsAsByte( 6 ) + 1;
			
			Logger.LogWarning( "Vorbis Floor Count:" + count.ToString() );

			header = new FloorHeader[count];

			for( int i = 0; i < count; i++ )
			{
				header[i] = new FloorHeader( aByteArray );
			}
		}
	}
}
