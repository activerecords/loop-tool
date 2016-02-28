using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis.Header
{
	public class Comment
	{
		private int sampleLoopStart;
		private int sampleLoopEnd;

		public Comment( ByteArray aByteArray )
		{
			sampleLoopStart = 0;
			sampleLoopEnd = 0;

			Read( aByteArray );
		}

		private void Read( ByteArray aByteArray )
		{
			Logger.LogWarning( "ReadVorbisComment" );

			string lId = aByteArray.ReadString( 6 );

			if( lId != "vorbis" )
			{
				Logger.LogError( "The Packet Is Not a Vorbis Comment:" + lId );

				return;
			}

			UInt32 lVendorLength = aByteArray.ReadUInt32();
			string lVendorString = aByteArray.ReadString( ( int )lVendorLength );

			Logger.LogWarning( "Vendor String:" + lVendorString );

			UInt32 lUserCommentListLength = aByteArray.ReadUInt32();

			for( int i = 0; i < lUserCommentListLength; i++ )
			{
				UInt32 lCommentLength = aByteArray.ReadUInt32();
				string lCommentString = aByteArray.ReadString( ( int )lCommentLength );

				Logger.LogWarning( "Comment String:" + lCommentString );

				switch( lCommentString.Split( '=' )[0] )	
				{
				case "LOOPSTART":
					sampleLoopStart = Convert.ToInt32( lCommentString.Split( '=' )[1] );
					break;

				case "LOOPLENGTH":
					sampleLoopEnd = Convert.ToInt32( lCommentString.Split( '=' )[1] ) + sampleLoopStart - 1;
					break;

				default:
					break;
				}
			}

			Byte lFramingBit = aByteArray.ReadByte();

			Logger.LogWarning( "Framing Bit:" + lFramingBit.ToString() );
		}

		public int GetSampleLoopStart()
		{
			return sampleLoopStart;
		}

		public int GetSampleLoopEnd()
		{
			return sampleLoopEnd;
		}
	}
}
