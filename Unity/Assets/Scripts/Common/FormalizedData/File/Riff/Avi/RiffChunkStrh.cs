using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkStrh : RiffChunk
	{
		public const string ID = "strh";

		public readonly string fccType;
		public readonly string fccHandler;
		public readonly UInt32 flags;
		public readonly UInt16 priority;
		public readonly UInt16 language;
		public readonly UInt32 initialFrames;
		public readonly UInt32 scale;
		public readonly UInt32 rate;
		public readonly UInt32 start;
		public readonly UInt32 length;
		public readonly UInt32 suggestedBufferSize;
		public readonly UInt32 quality;
		public readonly UInt32 sampleSize;
		public readonly Int16 left;
		public readonly Int16 top;
		public readonly Int16 right;
		public readonly Int16 bottom;

		public RiffChunkStrh( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			fccType = aByteArray.ReadString( 4 );
			fccHandler = aByteArray.ReadString( 4 );
			flags = aByteArray.ReadUInt32();
			priority = aByteArray.ReadUInt16();
			language = aByteArray.ReadUInt16();
			initialFrames = aByteArray.ReadUInt32();
			scale = aByteArray.ReadUInt32();
			rate = aByteArray.ReadUInt32();
			start = aByteArray.ReadUInt32();
			length = aByteArray.ReadUInt32();
			suggestedBufferSize = aByteArray.ReadUInt32();
			quality = aByteArray.ReadUInt32();
			sampleSize = aByteArray.ReadUInt32();
			left = aByteArray.ReadInt16();
			top = aByteArray.ReadInt16();
			right = aByteArray.ReadInt16();
			bottom = aByteArray.ReadInt16();

			informationList.Add( "Fcc Type:" + fccType );
			informationList.Add( "Fcc Handler:" + fccHandler );
			informationList.Add( "Flags:" + flags );
			informationList.Add( "Priority:" + priority );
			informationList.Add( "Language:" + language );
			informationList.Add( "Initial Frames:" + initialFrames );
			informationList.Add( "Scale:" + scale );
			informationList.Add( "Rate:" + rate );
			informationList.Add( "Start:" + start );
			informationList.Add( "Length:" + length );
			informationList.Add( "Suggested Buffer Size:" + suggestedBufferSize );
			informationList.Add( "Quality:" + quality );
			informationList.Add( "Sample Size:" + sampleSize );
			informationList.Add( "Left:" + left );
			informationList.Add( "Top:" + top );
			informationList.Add( "Right:" + right );
			informationList.Add( "Bottom:" + bottom );
		}
	}
}
