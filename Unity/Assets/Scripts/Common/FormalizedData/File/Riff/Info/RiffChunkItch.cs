using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkItch : RiffChunk
	{
		public const string ID = "ITCH";

		public readonly string technician;

		public RiffChunkItch( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			technician = aByteArray.ReadString( ( int )size );

			informationList.Add( "Technician:" + technician );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( technician );
		}
	}
}
