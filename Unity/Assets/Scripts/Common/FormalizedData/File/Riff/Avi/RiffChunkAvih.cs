using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkAvih : RiffChunk
	{
		public const string ID = "avih";

		public readonly UInt32 microSecPerFrame;
		public readonly UInt32 maxBytesPerSec;
		public readonly UInt32 paddingGranularity;
		public readonly UInt32 flags;
		public readonly UInt32 totalFrames;
		public readonly UInt32 initialFrames;
		public readonly UInt32 streams;
		public readonly UInt32 suggestedBufferSize;
		public readonly UInt32 width;
		public readonly UInt32 height;
		public readonly Byte[] reserved;

		public RiffChunkAvih( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			microSecPerFrame = aByteArray.ReadUInt32();
			maxBytesPerSec = aByteArray.ReadUInt32();
			paddingGranularity = aByteArray.ReadUInt32();
			flags = aByteArray.ReadUInt32();
			totalFrames = aByteArray.ReadUInt32();
			initialFrames = aByteArray.ReadUInt32();
			streams = aByteArray.ReadUInt32();
			suggestedBufferSize = aByteArray.ReadUInt32();
			width = aByteArray.ReadUInt32();
			height = aByteArray.ReadUInt32();
			reserved = aByteArray.ReadBytes( 16 );

			informationList.Add( "Micro Sec Per Frame:" + microSecPerFrame );
			informationList.Add( "Max Bytes Per Sec:" + maxBytesPerSec );
			informationList.Add( "Padding Granularity:" + paddingGranularity );
			informationList.Add( "Flags:" + flags );
			informationList.Add( "Total Frames:" + totalFrames );
			informationList.Add( "Initial Frames:" + initialFrames );
			informationList.Add( "Streams:" + streams );
			informationList.Add( "Suggested Buffer Size:" + suggestedBufferSize );
			informationList.Add( "Width:" + width );
			informationList.Add( "Height:" + height );
			informationList.Add( "Reserved:" + reserved );
		}
	}
}
