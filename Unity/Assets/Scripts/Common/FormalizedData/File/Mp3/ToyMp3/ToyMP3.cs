using System;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mp3
{
	class ToyMP3
	{
		private static int FRAME_SEEK_LIMIT;

		static ToyMP3()
		{
			FRAME_SEEK_LIMIT = 40960;
		}
		
		private ByteArray byteArray;

		public ToyMP3( Stream aStream )
		{
			byteArray = new ByteArrayBig( aStream );
		}

		public bool SeekMP3Frame( ToyMP3Frame frame )
		{
			try
			{
			for( ; ; )
			{
				for( int i = 0; ; i++ )
				{
					if( byteArray.ReadBitsAsUInt16( 12 ) != 4095 )
					{
							if( i > FRAME_SEEK_LIMIT || byteArray.Position >= byteArray.Length )
						{
							return false;
						}

						// back 1 byte (in order to check byte by byte)
						byteArray.SubBitPosition( 4 );

						continue;
					}
					else
					{
						break;
					}
				}

				frame.Id =             ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.Layer =          ( int )byteArray.ReadBitsAsUInt16( 2 );
				frame.ProtectionBit =  ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.BitrateIndex =   ( int )byteArray.ReadBitsAsUInt16( 4 );
				frame.FrequencyIndex = ( int )byteArray.ReadBitsAsUInt16( 2 );
				frame.PaddingBit =     ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.PrivateBit =     ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.Mode =           ( int )byteArray.ReadBitsAsUInt16( 2 );
				frame.ModeExtention =  ( int )byteArray.ReadBitsAsUInt16( 2 );
				frame.Copyright =      ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.Original =       ( int )byteArray.ReadBitsAsUInt16( 1 );
				frame.Emphasis =       ( int )byteArray.ReadBitsAsUInt16( 2 );

				if( frame.IsValidHeader )
				{
					break;
				}
				else
				{
					byteArray.SubBitPosition( 24 );
				}
			}
			}
			catch( Exception e )
			{
				// Exist BitStream Error.
				UnityEngine.Debug.LogError( "Toy Error:" + e );
			}

			// CRC word
			if( frame.ProtectionBit == 0 )
			{
				frame.CRCCheck = byteArray.ReadBitsAsUInt16( 16 );
			}
			else
			{
				frame.CRCCheck = 0;
			}

			if( !DecodeSideTableInformation( frame ) )
			{
				return false;
			}

			// Format check
			if( frame.Id != 1 || frame.Layer != 1 || frame.BitrateIndex == 0 )
			{
				return false;
			}

			// MainData
			if( frame.MainDataSize < 0 )
			{
				return false;
			}
			
			if( ( byteArray.PositionBit & 7 ) != 0 )
			{
				throw new Exception( "GetByByteArray can be used only at byte border." );
			}

			frame.MainData = byteArray.ReadBytes( frame.MainDataSize );
			
			return true;
		}

		private bool DecodeSideTableInformation( ToyMP3Frame frame )
		{
			frame.MainDataBegin = byteArray.ReadBitsAsUInt16( 9 );

			// Skipping Private Bits
			if( frame.Channels == 1 )
			{
				byteArray.AddBitPosition( 5 );
			}
			else
			{
				byteArray.AddBitPosition( 3 );
			}

			// Get Scfsi
			for( int ch = 0; ch < frame.Channels; ch++ )
			{
				for( int i = 0; i < 4; i++ )
				{
					frame.SetScfsi( ch, i, byteArray.ReadBitsAsUInt16( 1 ) );
				}
			}

			// Get Granule Info
			for( int g = 0; g < 2; g++ )
			{
				for( int ch = 0; ch < frame.Channels; ch++ )
				{
					frame.granule[ch, g].Part23Length        = byteArray.ReadBitsAsUInt16( 12 );
					frame.granule[ch, g].BigValues           = byteArray.ReadBitsAsUInt16( 9 );
					frame.granule[ch, g].GlobalGain          = byteArray.ReadBitsAsUInt16( 8 );
					frame.granule[ch, g].ScalefacCompress    = byteArray.ReadBitsAsUInt16( 4 );
					frame.granule[ch, g].WindowSwitchingFlag = byteArray.ReadBitsAsUInt16( 1 );

					if( frame.granule[ch, g].WindowSwitchingFlag == 1 )
					{
						frame.granule[ch, g].BlockType = byteArray.ReadBitsAsUInt16( 2 );
						frame.granule[ch, g].MixedBlockFlag = byteArray.ReadBitsAsUInt16( 1 );

						if( frame.granule[ch, g].BlockType == 0 )
						{
							return false;
						}

						for( int w = 0; w < 2; w++ )
						{
							frame.granule[ch, g].SetTableSelect(w, byteArray.ReadBitsAsUInt16( 5 ) );
						}

						for( int w = 0; w < 3; w++ )
						{
							frame.granule[ch, g].SetSubblockGain(w, byteArray.ReadBitsAsUInt16( 3 ) );
						}

						if( ( frame.granule[ch, g].BlockType == 2 ) && ( frame.granule[ch, g].MixedBlockFlag == 0 ) )
						{
							frame.granule[ch, g].Region0Count= 8;
							frame.granule[ch, g].Region1Count= 36;
						}
						else
						{
							frame.granule[ch, g].Region0Count = 7;
							frame.granule[ch, g].Region1Count = 36;
						}
					}
					else
					{
						for( int w = 0; w < 3; w++ )
						{
							frame.granule[ch, g].SetTableSelect( w, byteArray.ReadBitsAsUInt16( 5 ) );
						}

						frame.granule[ch, g].Region0Count = byteArray.ReadBitsAsUInt16( 4 );
						frame.granule[ch, g].Region1Count = byteArray.ReadBitsAsUInt16( 3 );
						frame.granule[ch, g].MixedBlockFlag = 0;
						frame.granule[ch, g].BlockType = 0;
					}

					frame.granule[ch, g].PreFlag = byteArray.ReadBitsAsUInt16( 1 );
					frame.granule[ch, g].ScalefacScale = byteArray.ReadBitsAsUInt16( 1 );
					frame.granule[ch, g].Count1TableSelect = byteArray.ReadBitsAsUInt16( 1 );
				}
			}

			return true;
		}
	}
}
