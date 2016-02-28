using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIeng : RiffChunk
	{
		public const string ID = "IENG";

		public readonly string engineer;

		public RiffChunkIeng( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			engineer = aByteArray.ReadString( ( int )size );

			informationList.Add( "Engineer:" + engineer );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( engineer );
		}
	}
}
