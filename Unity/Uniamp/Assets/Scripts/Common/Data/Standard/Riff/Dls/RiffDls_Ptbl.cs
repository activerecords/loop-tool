using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Ptbl : RiffChunk
	{
		public const string ID = "ptbl";

		public readonly UInt32 lsize;
		public readonly UInt32 cues;
		public readonly PoolCue[] poolCues;

		public RiffDls_Ptbl( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			lsize = aByteArray.ReadUInt32();
			cues = aByteArray.ReadUInt32();

			informationList.Add( "size:" + lsize );
			informationList.Add( "Cues:" + cues );

			poolCues = new PoolCue[cues];

			for( int i = 0; i < cues; i++ )
			{
				poolCues[i] = new PoolCue( aByteArray, informationList );
			}
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( Size );
			aByteArray.WriteUInt32( cues );

			for( int i = 0; i < cues; i++ )
			{
				poolCues[i].WriteByteArray( aByteArray );
			}
		}
	}

	public class PoolCue
	{
		public readonly UInt32 offset;

		public PoolCue( AByteArray aByteArray, List<string> aInformationList )
		{
			offset = aByteArray.ReadUInt32();

			aInformationList.Add( "Offset:" + offset );
		}

		public void WriteByteArray( AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( offset );
		}
	}
}
