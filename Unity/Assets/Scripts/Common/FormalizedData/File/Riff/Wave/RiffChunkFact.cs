using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkFact : RiffChunk
	{
		public const string ID = "fact";

		public readonly UInt32 sampleLength;

		public RiffChunkFact( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			sampleLength = aByteArray.ReadUInt32();

			informationList.Add( "Sample Length:" + sampleLength );
		}
	}
}
