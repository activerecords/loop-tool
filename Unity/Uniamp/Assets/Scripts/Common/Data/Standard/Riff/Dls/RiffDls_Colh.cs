using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Colh : RiffChunk
	{
		public const string ID = "colh";

		public readonly UInt32 instruments;

		public RiffDls_Colh( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			instruments = aByteArray.ReadUInt32();

			informationList.Add( "Instruments:" + instruments );
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( instruments );
		}
	}
}
