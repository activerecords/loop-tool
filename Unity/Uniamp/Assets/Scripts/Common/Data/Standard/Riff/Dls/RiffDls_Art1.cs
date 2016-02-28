using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Art1 : RiffChunk
	{
		public const string ID = "art1";

		public readonly UInt32 lsize;
		public readonly UInt32 collectionBlocks;
		public readonly CollectionBlock[] collectionBlock;

		public readonly int tuning;
		public readonly int count;

		public RiffDls_Art1( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			tuning = 0;
			count = 0;

			lsize = aByteArray.ReadUInt32();
			collectionBlocks = aByteArray.ReadUInt32();

			informationList.Add( "Size:" + lsize );
			informationList.Add( "Collection Blocks:" + collectionBlocks );

			collectionBlock = new CollectionBlock[collectionBlocks];

			for( int i = 0; i < collectionBlocks; i++ )
			{
				collectionBlock[i] = new CollectionBlock( aByteArray, informationList );

				if( collectionBlock[i].destination == 3 && count < 1 )
				{
					tuning = collectionBlock[i].scale;

					count++;
				}
			}
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( lsize );
			aByteArray.WriteUInt32( collectionBlocks );

			for( int i = 0; i < collectionBlocks; i++ )
			{
				collectionBlock[i].WriteByteArray( aByteArray );
			}
		}
	}

	public class CollectionBlock
	{
		public readonly UInt16 score;
		public readonly UInt16 control;
		public readonly UInt16 destination;
		public readonly UInt16 transform;
		public readonly Int32 scale;

		public CollectionBlock( AByteArray aByteArray, List<string> aInformationList )
		{
			score = aByteArray.ReadUInt16();
			control = aByteArray.ReadUInt16();
			destination = aByteArray.ReadUInt16();
			transform = aByteArray.ReadUInt16();
			scale = aByteArray.ReadInt32();

			aInformationList.Add( "Score:" + score );
			aInformationList.Add( "Control:" + control );
			aInformationList.Add( "Destination:" + destination );
			aInformationList.Add( "Transform:" + transform );
			aInformationList.Add( "Scale:" + scale );
		}

		public void WriteByteArray( AByteArray aByteArray )
		{
			aByteArray.WriteUInt16( score );
			aByteArray.WriteUInt16( control );
			aByteArray.WriteUInt16( destination );
			aByteArray.WriteUInt16( transform );
			aByteArray.WriteUInt32( ( UInt32 )scale );
		}
	}
}
