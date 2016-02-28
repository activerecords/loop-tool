using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIsng : RiffChunk
	{
		public const string ID = "isng";

		public readonly string unknown;

		public RiffChunkIsng( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			unknown = aByteArray.ReadString( ( int )size );

			informationList.Add( "Unknown:" + unknown );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteString( unknown );
		}
	}
}
