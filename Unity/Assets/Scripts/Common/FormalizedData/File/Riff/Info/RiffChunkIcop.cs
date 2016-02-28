using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIcop : RiffChunk
	{
		public const string ID = "ICOP";

		public readonly string corporation;

		public RiffChunkIcop( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			corporation = aByteArray.ReadString( ( int )size );

			informationList.Add( "Corporation:" + corporation );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( corporation );
		}
	}
}
