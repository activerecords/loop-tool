using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIcmt : RiffChunk
	{
		public const string ID = "ICMT";

		public readonly string comment;

		public RiffChunkIcmt( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			comment = aByteArray.ReadString( ( int )size );

			informationList.Add( "Comment:" + comment );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( comment );
		}
	}
}
