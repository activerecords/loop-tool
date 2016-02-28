using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkPtbl : RiffChunk
	{
		public const string ID = "ptbl";

		public readonly UInt32 lsize;
		public readonly UInt32 cues;
		public readonly PoolCue[] poolCues;

		public RiffChunkPtbl( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
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

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( size );
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

		public PoolCue( ByteArray aByteArray, List<string> aInformationList )
		{
			offset = aByteArray.ReadUInt32();

			aInformationList.Add( "Offset:" + offset );
		}

		public void WriteByteArray( ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( offset );
		}
	}
}
