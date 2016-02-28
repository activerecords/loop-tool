using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIprd : RiffChunk
	{
		public const string ID = "IPRD";

		public readonly string product;

		public RiffChunkIprd( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			product = aByteArray.ReadString( ( int )size );

			informationList.Add( "Product:" + product );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( product );
		}
	}
}
