using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class HuffmanNode
	{
		private HuffmanNode parent;
		protected HuffmanNode o0;
		protected HuffmanNode o1;
		private int depth;
		protected int value;
		private bool isFull;
		private bool isSetValue;

		public HuffmanNode()
		{
			depth = 0;
			isFull = false;
			isSetValue = false;
		}

		protected HuffmanNode( HuffmanNode aParent )
		{
			isFull = false;
			isSetValue = false;
			parent = aParent;

			if( parent != null )
			{
				depth = parent.GetDepth() + 1;
			}
		}

		protected HuffmanNode( HuffmanNode aParent, int aValue )
		{
			parent = aParent;

			if( parent != null )
			{
				depth = parent.GetDepth() + 1;
			}

			value = aValue;

			isFull = true;
			isSetValue = true;
		}

		public int Read( ByteArray aByteArray )
		{
			HuffmanNode iter = this;

			while( iter.isSetValue == false )
			{
				if( aByteArray.PositionBit + 1 >= aByteArray.Length * 8 )
				{
					//Logger.LogError( "                                ■Read Error" );

					return 0;
				}

				if( aByteArray.ReadBitsAsByte( 1 ) == 0x01 )
				{
					iter = iter.o1;
				}
				else
				{
					iter = iter.o0;
				}

				if( iter == null )
				{
					Logger.LogWarning( "■Read Error" );

					return 0;
				}
			}

			return iter.value;
		}

		protected int GetValue()
		{
			return value;
		}

		private HuffmanNode GetParent()
		{
			return parent;
		}

		protected int GetDepth()
		{
			return depth;
		}

		private bool GetFullFlag()
		{
			if( isFull == true )
			{
				return true;
			}

			if( o0 != null && o0.GetFullFlag() == true && o1 != null && o1.GetFullFlag() == true )
			{
				isFull = true;

				return true;
			}

			return false;
		}

		public bool SetNewValue( int aDepth, int aValue )
		{
			if( GetFullFlag() == true )
			{
				return false;
			}

			if( aDepth == 1 )
			{
				if( o0 == null )
				{
					o0 = new HuffmanNode( this, aValue );

					return true;
				}
				
				if( o1 == null )
				{
					o1 = new HuffmanNode( this, aValue );

					return true;
				}
			}
			else
			{
				if( o0 == null )
				{
					o0 = new HuffmanNode( this );
				}

				if( o0.SetNewValue( aDepth - 1, aValue ) == true )
				{
					return true;
				}

				if( o1 == null )
				{
					o1 = new HuffmanNode( this );
				}

				if( o1.SetNewValue( aDepth - 1, aValue ) == true )
				{
					return true;
				}
			}

			return false;
		}
	}

	public class CodebookHeader
	{
		public int[] codewordLengths;
		public UInt16 dimensions;
		public UInt32 entries;

		Byte lookupType;
		UInt32 lookupValues;
		float minimumValue;
		float deltaValue;
		Byte valueBits;
		Byte valueBitsAdd1;
		Byte sequenceP;
		UInt16[] multiplicands;

		public double[][] value_vector;
		public HuffmanNode huffmanRoot;

		public CodebookHeader( ByteArray aByteArray )
		{
			UInt32 lBcv = aByteArray.ReadBitsAsUInt32( 24 );

			Logger.LogWarning( "■BCV:0x" + lBcv.ToString( "X6" ) );

			if( lBcv != 0x564342 )
			{
				Logger.LogWarning( "■CodebookHeader Error" );

				return;
			}

			dimensions = aByteArray.ReadBitsAsUInt16( 16 );
			entries = aByteArray.ReadBitsAsUInt32( 24 );
			Byte lOrdered = aByteArray.ReadBitsAsByte( 1 );

			Logger.LogNormal( "Codebook Dimensions:0x" + dimensions.ToString( "X4" ) );
			Logger.LogNormal( "Codebook Entries:0x" + entries.ToString( "X6" ) );
			Logger.LogNormal( "Ordered:0x" + lOrdered.ToString( "X2" ) );

			codewordLengths = new int[entries];

			if( lOrdered == 0x00 )
			{
				Logger.LogWarning( "Ordered is Unset." );

				Byte lSparse = aByteArray.ReadBitsAsByte( 1 );

				Logger.LogWarning( "Sparse:0x" + lSparse.ToString( "X1" ) );

				if( lSparse == 0x01 )
				{
					for( int j = 0; j < entries; j++ )
					{
						UInt32 lFlag = aByteArray.ReadBitsAsByte( 1 );

						//Logger.LogWarning( "Flag:" + lFlag.ToString() );

						if( lFlag == 0x01 )
						{
							int lLength = aByteArray.ReadBitsAsByte( 5 ) + 1;

							//Logger.LogWarning( aByteArray.Position.ToString( "X8" ) + "::Length:" + lLength.ToString() );

							codewordLengths[j] = lLength;
						}
						else
						{
							codewordLengths[j] = -1;

							//Logger.LogError( "this entry is unused. mark it as such." );
						}
					}
				}
				else
				{
					for( int j = 0; j < entries; j++ )
					{
						int lLength = aByteArray.ReadBitsAsByte( 5 ) + 1;

						//Logger.LogWarning( aByteArray.Position.ToString( "X8" ) + "::Length:" + lLength.ToString() );

						codewordLengths[j] = lLength;
					}
				}
			}
			else
			{
				Logger.LogError( "Ordered is Set." );

				int lCurrentEntry = 0;
				int lCurrentLength = aByteArray.ReadBitsAsByte( 5 ) + 1;

				while( lCurrentEntry < entries )
				{
					int lNumber = aByteArray.ReadBitsAsByte( ilog( ( UInt32 )( entries - lCurrentEntry ) ) );

					// Check 19.
					codewordLengths[lCurrentLength] = lCurrentEntry + lNumber - 1;

					lCurrentEntry = lNumber + lCurrentEntry;
					lCurrentLength++;

					if( lCurrentEntry > entries )
					{
						Logger.LogError( "ERROR CONDITION" );

						return;
					}
				}
			}

			CreateHuffmanTree();

			lookupType = aByteArray.ReadBitsAsByte( 4 );

			Logger.LogDebug( "Codebook Lookup Type:0x" + lookupType.ToString( "X1" ) );

			if( lookupType == 0x00 )
			{
				Logger.LogError( "■No Lookup." );
			}
			else if( lookupType == 0x01 || lookupType == 0x02 )
			{
				Logger.LogDebug( "■Defined Lookup." );

				lookupValues = 0;

				if( lookupType == 0x01 )
				{
					lookupValues = GetLookup1Values();
				}
				else
				{
					lookupValues = entries * dimensions;
				}

				minimumValue = unpack( aByteArray.ReadBitsAsUInt32( 32 ) );
				deltaValue = unpack( aByteArray.ReadBitsAsUInt32( 32 ) );
				valueBits = aByteArray.ReadBitsAsByte( 4 );//and add 1
				valueBitsAdd1 = ( Byte )( valueBits + 1 );
				sequenceP = aByteArray.ReadBitsAsByte( 1 );

				Logger.LogDebug( "lookupValues:" + lookupValues );
				Logger.LogDebug( "Codebook Minimum Value:" + minimumValue );
				Logger.LogDebug( "Codebook Delta Value:" + deltaValue );
				Logger.LogDebug( "Codebook Value Bits:0x" + valueBits.ToString( "X1" ) );
				Logger.LogDebug( "Codebook Sequence P:0x" + sequenceP.ToString( "X1" ) );

				multiplicands = new UInt16[lookupValues];

				for( int i = 0; i < lookupValues; i++ )
				{
					multiplicands[i] = aByteArray.ReadBitsAsUInt16( valueBitsAdd1 );

					//Logger.LogWarning( "lPacket:0x" + lPacket.ToString( "X4" ) );
				}

				DecodeValues();
			}
			else
			{
				Logger.LogWarning( "■Undefined Lookup." );
			}
		}

		public void DecodeValues()
		{
			if( lookupType == 0x01 )
			{
				DecodeValues1();
			}
			else
			{
				Logger.LogError( "■Lookup 2." );
				DecodeValues2();
			}
		}

		private void DecodeValues1()
		{
			value_vector = new double[entries][];

			Logger.LogDebug( "entries:" + entries );

			for( int i = 0; i < entries; i++ )
			{
				double last = 0;
				int index_divisor = 1;
				value_vector[i] = new double[dimensions];

				for( int j = 0; j < dimensions; j++ )
				{
					int multiplicand_offset = ( int )( ( i / index_divisor ) % lookupValues );

					value_vector[i][j] = ( double )multiplicands[multiplicand_offset] * deltaValue + minimumValue + last;

					//Logger.LogError( "value_vector[" + i + "][" + j + "]:" +value_vector[i][j] );

					if( sequenceP == 0x01 )
					{
						last = value_vector[i][j];
					}

					index_divisor = ( int )( index_divisor * lookupValues );
				}
			}
		}

		private void DecodeValues2()
		{
			value_vector = new double[entries][];

			for( int i = 0; i < entries; i++ )
			{
				double last = 0;
				value_vector[i] = new double[dimensions];

				int multiplicand_offset = i * ( int )dimensions;

				for( int j = 0; j < dimensions; j++ )
				{
					value_vector[i][j] = multiplicands[multiplicand_offset] * deltaValue + minimumValue + last;

					if( sequenceP == 0x01 )
					{
						last = value_vector[i][j];
					}

					multiplicand_offset++;
				}
			}
		}

		private UInt32 GetLookup1Values()
		{
			UInt32 res = ( UInt32 )Math.Pow( Math.E, Math.Log( entries ) / dimensions );

			if( intPow( res + 1, dimensions ) <= entries )
			{
				return res + 1;
			}
			else
			{
				return res;
			}
		}

		private UInt32 intPow( UInt32 aBase, UInt32 e )
		{
			UInt32 res = 1;

			for( ; e > 0; e--, res *= aBase )
			{
				;
			}

			return res;
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

		private float unpack( UInt32 aX )
		{
			float lMantissa = ( float )( aX & 0x1FFFFF );
			UInt32 lSign = aX & 0x80000000;
			int lExponent = ( int )( aX & 0x7FE00000 ) >> 21;

			if( lSign != 0 )
			{
				lMantissa *= -1;
			}

			return lMantissa * ( float )Math.Pow( 2.0d, ( double )lExponent - 788.0d );
		}

		private bool CreateHuffmanTree()
		{
			Logger.LogDebug( "■CreateHuffmanTree:" + codewordLengths.Length );

			huffmanRoot = new HuffmanNode();

			for( int i = 0; i < codewordLengths.Length; i++ )
			{
				if( codewordLengths[i] > 0 )
				{
					if( huffmanRoot.SetNewValue( codewordLengths[i], i ) == false )
					{
						Logger.LogError( "■huffmanRoot.SetNewValue Error" );

						return false;
					}
				}
				else
				{
					//Logger.LogError( "■EntryLengths Zero" );
				}
			}

			return true;
		}

		public void ReadVvAdd( double[][] a, ByteArray aByteArray, int offset, int length )
		{
			int chptr = 0;
			int ch = 2;

			if( ch == 0 )
			{
				return;
			}

			int lim = ( offset + length ) / ch;

			for( int i = offset / ch; i < lim; )
			{
				double[] ve = value_vector[huffmanRoot.Read( aByteArray )];

				for( int j = 0; j < dimensions; j++ )
				{
					a[chptr++][i] += ve[j];

					//Logger.LogWarning( a[chptr - 1][i] );

					if( chptr == ch )
					{
						chptr = 0;
						i++;
					}
				}
			}
		}

		public double[] ReadVv( ByteArray aByteArray )
		{
			if( lookupType != 0x01 )
			{
				Logger.LogNormal( "Codebook Lookup Type:0x" + lookupType.ToString( "X1" ) );
			}

			return value_vector[huffmanRoot.Read( aByteArray )];
		}
	}

	public class VorbisCodebook
	{
		public int count;
		public CodebookHeader[] headerArray;

		public VorbisCodebook( ByteArray aByteArray )
		{
			count = aByteArray.ReadByte() + 1;

			Logger.LogWarning( "Vorbis Codebook Count:" + count );

			headerArray = new CodebookHeader[count];

			for( int i = 0; i < count; i++ )
			{
				headerArray[i] = new CodebookHeader( aByteArray );
			}
		}
	}
}
