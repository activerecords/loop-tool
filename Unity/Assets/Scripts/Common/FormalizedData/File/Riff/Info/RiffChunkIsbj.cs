using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIsbj : RiffChunk
	{
		public const string ID = "ISBJ";

		public readonly string subject;

		public RiffChunkIsbj( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			subject = aByteArray.ReadString( ( int )size );

			informationList.Add( "Subject:" + subject );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( subject );
		}
	}
}
