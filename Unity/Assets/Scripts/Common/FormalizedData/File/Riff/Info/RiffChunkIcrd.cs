using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIcrd : RiffChunk
	{
		public const string ID = "ICRD";

		public readonly string creationDate;

		public RiffChunkIcrd( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			creationDate = aByteArray.ReadString( ( int )size );

			informationList.Add( "Creation Date:" + creationDate );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( creationDate );
		}
	}
}
