using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis.Header
{
	public class Identification
	{
		public UInt32 version;
		public Byte audioChannels;
		public UInt32 audioSampleRate;
		public Int32 bitrateMaximum;
		public Int32 bitrateNominal;
		public Int32 bitrateMinimum;
		public int blockSize0;
		public int blockSize1;
		public Byte framingFlag;

		public Identification( ByteArray aByteArray )
		{
			Read( aByteArray );
		}

		private void Read( ByteArray aByteArray )
		{
			Logger.LogWarning( "ReadVorbisHeader" );

			string lId = aByteArray.ReadString( 6 );

			if( lId != "vorbis" )
			{
				Logger.LogError( "The File Is Not a Vorbis File:" + lId );

				//throw new Exception();
			}

			version = aByteArray.ReadUInt32();
			audioChannels = aByteArray.ReadByte();
			audioSampleRate = aByteArray.ReadUInt32();
			bitrateMaximum = aByteArray.ReadInt32();
			bitrateNominal = aByteArray.ReadInt32();
			bitrateMinimum = aByteArray.ReadInt32();

			Byte I = aByteArray.ReadBitsAsByte( 4 );
			Byte J = aByteArray.ReadBitsAsByte( 4 );

			blockSize0 = ( int )Math.Pow( 2, I );
			blockSize1 = ( int )Math.Pow( 2, J );
			framingFlag = aByteArray.ReadByte();

			Logger.LogDebug( "Vorbis Version:" + version.ToString() );
			Logger.LogDebug( "Audio Channels:" + audioChannels.ToString() );
			Logger.LogDebug( "Audio SampleRate:" + audioSampleRate.ToString() );
			Logger.LogDebug( "Bitrate Maximum:" + bitrateMaximum.ToString() );
			Logger.LogDebug( "Bitrate Nominal:" + bitrateNominal.ToString() );
			Logger.LogDebug( "Bitrate Minimum:" + bitrateMinimum.ToString() );
			Logger.LogDebug( "Block Size 0:" + blockSize0.ToString() );
			Logger.LogDebug( "Block Size 1:" + blockSize1.ToString() );
			Logger.LogDebug( "Framing Flag:" + framingFlag.ToString() );
		}
	}
}
