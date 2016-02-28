using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkMsyn : RiffChunk
	{
		public const string ID = "msyn";

		public readonly UInt32 msyn;

		public RiffChunkMsyn( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			msyn = aByteArray.ReadUInt32();

			informationList.Add( "Msyn:" + msyn );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( msyn );
		}
	}
}
