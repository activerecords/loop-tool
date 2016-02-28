using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkDisp : RiffChunk
	{
		public const string ID = "DISP";

		public readonly UInt32 type;
		public readonly UInt32 data;

		public RiffChunkDisp( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = aByteArray.ReadUInt32();
			data = aByteArray.ReadUInt32();
		}
	}
}
