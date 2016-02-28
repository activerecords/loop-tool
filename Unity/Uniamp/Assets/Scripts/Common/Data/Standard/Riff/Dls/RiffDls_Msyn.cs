using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Msyn : RiffChunk
	{
		public const string ID = "msyn";

		public readonly UInt32 msyn;

		public RiffDls_Msyn( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			msyn = aByteArray.ReadUInt32();

			informationList.Add( "Msyn:" + msyn );
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( msyn );
		}
	}
}
