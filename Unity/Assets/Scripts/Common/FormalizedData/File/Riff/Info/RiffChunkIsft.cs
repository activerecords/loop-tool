using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIsft : RiffChunk
	{
		public const string ID = "ISFT";

		public readonly string software;

		public RiffChunkIsft( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			software = aByteArray.ReadString( ( int )size );

			informationList.Add( "Software:" + software );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( software );
		}
	}
}
