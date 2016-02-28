using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Fmt_ : RiffChunk
	{
		public const string ID = "fmt ";

		public readonly UInt16 tag;
		public readonly UInt16 channles;
		public readonly UInt32 samplesPerSec;
		public readonly UInt32 averageBytesPerSec;
		public readonly UInt16 blockAlign;
		public readonly UInt16 bitsPerSample;

		public RiffDls_Fmt_( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			tag = aByteArray.ReadUInt16();
			channles = aByteArray.ReadUInt16();
			samplesPerSec = aByteArray.ReadUInt32();
			averageBytesPerSec = aByteArray.ReadUInt32();
			blockAlign = aByteArray.ReadUInt16();
			bitsPerSample = aByteArray.ReadUInt16();

			informationList.Add( "Tag:" + tag );
			informationList.Add( "Channels:" + channles );
			informationList.Add( "Samples Per Sec:" + samplesPerSec );
			informationList.Add( "Average Bytes Per Sec:" + averageBytesPerSec );
			informationList.Add( "Block Align:" + blockAlign );
			informationList.Add( "Bits Per Sample:" + bitsPerSample );

			aByteArray.SetPosition( ( int )( position + Size ) );
		}
	}
}
