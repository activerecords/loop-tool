using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkColh : RiffChunk
	{
		public const string ID = "colh";

		public readonly UInt32 instruments;

		public RiffChunkColh( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			instruments = aByteArray.ReadUInt32();

			informationList.Add( "Instruments:" + instruments );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( instruments );
		}
	}
}
