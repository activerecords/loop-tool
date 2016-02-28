using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class VorbisPacket
	{
		public OggPageHeader oggPageHeader;
		public VorbisSegment[] vorbisSegmentArray;

		public List<int> sizeSegmentList;

		public Byte[] nextSegmentArray;

		public VorbisPacket( ByteArray aByteArray, VorbisHeader aVorbisHeader, Byte[] aFirstSegmentArray )
		{
			oggPageHeader = new OggPageHeader( aByteArray );

			sizeSegmentList = oggPageHeader.GetSegmentSizeList();
			vorbisSegmentArray = new VorbisSegment[sizeSegmentList.Count];

			for( int i = 0; i < sizeSegmentList.Count; i++ )
			{
				Byte[] lPacket = aByteArray.ReadBytes( sizeSegmentList[i] );
				Byte[] lPacketAll = new Byte[sizeSegmentList[i] + aFirstSegmentArray.Length];

				for( int j = 0; j < aFirstSegmentArray.Length; j++ )
				{
					lPacketAll[j] = aFirstSegmentArray[j];
				}

				for( int j = 0; j < sizeSegmentList[i]; j++ )
				{
					lPacketAll[aFirstSegmentArray.Length + j] = lPacket[j];
				}

				MemoryStream lMemoryStream = new MemoryStream( lPacketAll );
				ByteArrayLittle lByteArray = new ByteArrayLittle( lMemoryStream );

				vorbisSegmentArray[i] = new VorbisSegment( lByteArray, aVorbisHeader );

				aFirstSegmentArray = new Byte[0];
			}

			nextSegmentArray = aByteArray.ReadBytes( oggPageHeader.GetNextSegmentSize() );
		}
	}
}
