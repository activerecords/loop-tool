using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIart : RiffChunk
	{
		public const string ID = "IART";

		public readonly string artist;

		public RiffChunkIart( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			artist = aByteArray.ReadString( ( int )size );

			informationList.Add( "Artist:" + artist );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( artist );
		}
	}
}
