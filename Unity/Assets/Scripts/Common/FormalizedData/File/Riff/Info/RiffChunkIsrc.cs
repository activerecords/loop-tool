using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIsrc : RiffChunk
	{
		public const string ID = "ISRC";

		public readonly string source;

		public RiffChunkIsrc( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			source = aByteArray.ReadString( ( int )size );

			informationList.Add( "Source:" + source );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( source );
		}
	}
}
