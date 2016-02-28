using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkLgwv : RiffChunk
	{
		public const string ID = "LGWV";

		public readonly byte[] dataArray;

		public RiffChunkLgwv( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			dataArray = aByteArray.ReadBytes( ( int )size );
		}
	}
}
