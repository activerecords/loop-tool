using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIgnr : RiffChunk
	{
		public const string ID = "IGNR";

		public readonly string genre;

		public RiffChunkIgnr( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			genre = aByteArray.ReadString( ( int )size );

			informationList.Add( "Genre:" + genre );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( genre );
		}
	}
}
