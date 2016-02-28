using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class VorbisHeader
	{
		private const Byte IDENTIFICATION = 0x01;
		private const Byte COMMENT = 0x03;
		private const Byte SETUP = 0x05;

		public Identification identification;
		public Comment comment;
		public Setup setup;

		public VorbisHeader()
		{

		}

		public void Read( ByteArray aByteArray, Byte aType )
		{
			Logger.LogDebug( "Header Type:0x" + aType.ToString( "X2" ) );

			switch( aType )
			{
			case IDENTIFICATION:
				identification = new Identification( aByteArray );
				break;

			case COMMENT:
				comment = new Comment( aByteArray );
				break;

			case SETUP:
				setup = new Setup( aByteArray );
				break;

			default:
				Logger.LogError( "The Header ID Is Not Defined:" + aType.ToString( "X2" ) );
				break;
			}
		}

		public int GetSampleLoopStart()
		{
			return comment.GetSampleLoopStart();
		}

		public int GetSampleLoopEnd()
		{
			return comment.GetSampleLoopEnd();
		}
	}
}
