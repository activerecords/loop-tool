using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Xm
{
	public class PatternChunk
	{
		private UInt32 patternHeaderLength;		// ?	4	(dword)	Pattern header length
		private Byte packingType;				// +4	1	(byte)	Packing type (always 0)
		private UInt16 numberOfRowsInPattern;	// +5	2	(word)	Number of rows in pattern (1..256)
		private UInt16 packedPatterndataSize;	// +7	2	(word)	Packed patterndata size
		private Byte[] packedPatternData;		// ?	?			Packed pattern data

		private Byte[] note;
		private Byte[] instrument;

		public PatternChunk( ByteArray aByteArray )
		{
			patternHeaderLength = aByteArray.ReadUInt32();
			packingType = aByteArray.ReadByte();
			numberOfRowsInPattern = aByteArray.ReadUInt16();
			packedPatterndataSize = aByteArray.ReadUInt16();
			packedPatternData = aByteArray.ReadBytes( packedPatterndataSize );

			//Debug.Log( "Pattern header length:" + patternHeaderLength.ToString( "x08" ) );
			//Debug.Log( "Packing type:" + packingType.ToString( "x02" ) );
			//Debug.Log( "Number of rows in pattern:" + numberOfRowsInPattern.ToString( "x04" ) );
			//Debug.Log( "Packed patterndata size:" + packedPatterndataSize.ToString( "x04" ) );
			//Debug.Log( "Packed pattern data:" + System.Text.Encoding.ASCII.GetString( packedPatternData ) );

			int count = 0;
			note = new Byte[packedPatterndataSize];
			instrument = new Byte[packedPatterndataSize];

			for( int i = 0; i < packedPatterndataSize; i++ )
			{
				note[count] = 0x00;
				instrument[count] = 0x00;

				if( ( packedPatternData[i] & 0x80 ) == 0x80 )
				{
					Byte flag = packedPatternData[i];

					if( ( flag & 0x01 ) == 0x01 )
					{
						i++;

						note[count] = packedPatternData[i];
						Logger.LogError( "	Note:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x02 ) == 0x02 )
					{
						i++;

						instrument[count] = packedPatternData[i];
						Logger.LogError( "	Instrument:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x04 ) == 0x04 )
					{
						i++;

						Logger.LogError( "	Volume column byte:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x08 ) == 0x08 )
					{
						i++;

						Logger.LogError( "	Effect type:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x10 ) == 0x10 )
					{
						i++;

						Logger.LogError( "	Guess what!" );
					}
				}
				else
				{
					note[count] = packedPatternData[i];
					Logger.LogError( "	Note:" + packedPatternData[i].ToString( "d" ) );

					i++;
					instrument[count] = packedPatternData[i];
					Logger.LogError( "	Instrument:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogError( "	Volume column byte:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogError( "	Effect type:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogError( "	Effect parameter:" + packedPatternData[i].ToString( "d" ) );
				}

				count++;
			}
		}

		public UInt32 GetPatternHeaderLength()
		{
			return patternHeaderLength;
		}

		public Byte GetPackingType()
		{
			return packingType;
		}

		public UInt16 GetNumberOfRowsInPattern()
		{
			return numberOfRowsInPattern;
		}

		public UInt16 GetPackedPatterndataSize()
		{
			return packedPatterndataSize;
		}

		public Byte[] GetackedPatternData()
		{
			return packedPatternData;
		}

		public Byte[] GetNote()
		{
			return note;
		}

		public Byte[] GetInstrument()
		{
			return instrument;
		}

		public void Display()
		{
			int count = 0;

			Logger.LogNormal( "Pattern header length:" + patternHeaderLength.ToString( "x08" ) );
			Logger.LogNormal( "Packing type:" + packingType.ToString( "x02" ) );
			Logger.LogNormal( "Number of rows in pattern:" + numberOfRowsInPattern.ToString( "x04" ) );
			Logger.LogNormal( "Packed patterndata size:" + packedPatterndataSize.ToString( "x04" ) );
			Logger.LogNormal( "Packed pattern data:" + System.Text.Encoding.ASCII.GetString( packedPatternData ) );

			note = new Byte[packedPatterndataSize];
			instrument = new Byte[packedPatterndataSize];

			for( int i = 0; i < packedPatterndataSize; i++ )
			{
				note[count] = 0x00;
				instrument[count] = 0x00;

				if( ( packedPatternData[i] & 0x80 ) == 0x80 )
				{

					Byte flag = packedPatternData[i];

					//Debug.Log( "MSB use:" + packedPatternData[i].ToString( "x02" ) );

					if( ( flag & 0x01 ) == 0x01 )
					{
						i++;

						note[count] = packedPatternData[i];
						//Debug.Log( "	Note:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x02 ) == 0x02 )
					{
						i++;

						instrument[count] = packedPatternData[i];
						//Debug.Log( "	Instrument:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x04 ) == 0x04 )
					{
						i++;

						Logger.LogNormal( "	Volume column byte:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x08 ) == 0x08 )
					{
						i++;

						Logger.LogNormal( "	Effect type:" + packedPatternData[i].ToString( "d" ) );
					}

					if( ( flag & 0x10 ) == 0x10 )
					{
						Logger.LogNormal( "	Guess what!" );
					}
				}
				else
				{
					note[count] = packedPatternData[i];
					//Debug.Log( "	Note:" + packedPatternData[i].ToString( "d" ) );

					i++;
					instrument[count] = packedPatternData[i];
					//Debug.Log( "	Instrument:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogNormal( "	Volume column byte:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogNormal( "	Effect type:" + packedPatternData[i].ToString( "d" ) );

					i++;
					Logger.LogNormal( "	Effect parameter:" + packedPatternData[i].ToString( "d" ) );
				}

				count++;
			}
		}
	}
}
