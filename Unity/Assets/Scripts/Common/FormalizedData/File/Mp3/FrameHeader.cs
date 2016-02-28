using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public class FrameHeader
	{
		private UInt16 syncword;
		private Byte id;
		private Byte layer;
		private Byte protectionBit;
		private Byte bitrateIndex;
		private Byte samplingFrequency;
		private Byte paddlingBit;
		private Byte privateBit;
		private Byte mode;
		private Byte modeExtention;
		private Byte copyright;
		private Byte original;
		private Byte emphasis;

		public FrameHeader( BitArray bitArray )
		{
			syncword = bitArray.ReadBits16( 12 );
			id = bitArray.ReadBits8( 1 );
			layer = bitArray.ReadBits8( 2 );
			protectionBit = bitArray.ReadBits8( 1 );

			bitrateIndex = bitArray.ReadBits8( 4 );
			samplingFrequency = bitArray.ReadBits8( 2 );
			paddlingBit = bitArray.ReadBits8( 1 );
			privateBit = bitArray.ReadBits8( 1 );

			mode = bitArray.ReadBits8( 2 );
			modeExtention = bitArray.ReadBits8( 2 );
			copyright = bitArray.ReadBits8( 1 );
			original = bitArray.ReadBits8( 1 );
			emphasis = bitArray.ReadBits8( 2 );

			Logger.LogNormal( "Syncword:" + syncword );
			Logger.LogNormal( "Id:" + id );
			Logger.LogNormal( "Layer:" + layer );
			Logger.LogNormal( "ProtectionBit:" + protectionBit );
			Logger.LogNormal( "BitrateIndex:" + bitrateIndex );
			Logger.LogNormal( "SamplingFrequency:" + samplingFrequency );
			Logger.LogNormal( "PaddlingBit:" + paddlingBit );
			Logger.LogNormal( "PrivateBit:" + privateBit );
			Logger.LogNormal( "Mode:" + mode );
			Logger.LogNormal( "ModeExtention:" + modeExtention );
			Logger.LogNormal( "Copyright:" + copyright );
			Logger.LogNormal( "Original:" + original );
			Logger.LogNormal( "Emphasis:" + emphasis );

			int frameSize = 144 * 32000 / 44100 + 1 * ( int )paddlingBit;

			Logger.LogNormal( "FrameSize:" + frameSize );
		}

		public int GetChannels()
		{
			if( mode != 3 )
			{
				return 2;
			}
			else
			{
				return 1;
			}
		}
	}
}
