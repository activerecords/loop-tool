using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIcms : RiffChunk
	{
		public const string ID = "ICMS";

		public readonly string commisioned;

		public RiffChunkIcms( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			commisioned = aByteArray.ReadString( ( int )size );

			informationList.Add( "Commisioned:" + commisioned );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( commisioned );
		}
	}
}
