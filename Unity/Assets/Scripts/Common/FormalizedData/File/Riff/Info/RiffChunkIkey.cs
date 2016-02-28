using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIkey : RiffChunk
	{
		public const string ID = "IKEY";

		public readonly string keywords;

		public RiffChunkIkey( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			keywords = aByteArray.ReadString( ( int )size );

			informationList.Add( "Keywords:" + keywords );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( keywords );
		}
	}
}
