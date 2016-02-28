using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkIfil : RiffChunk
	{
		public const string ID = "ifil";

		public readonly string unknown;

		public RiffChunkIfil( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
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
