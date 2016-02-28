﻿using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkArt2 : RiffChunk
	{
		public const string ID = "art2";

		public readonly UInt32 lsize;
		public readonly UInt32 collectionBlocks;
		public readonly CollectionBlock[] collectionBlock;

		public readonly int tuning;
		public readonly int count = 0;

		public RiffChunkArt2( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			tuning = 0;
			count = 0;

			// To Be Fixed.
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

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( lsize );
			aByteArray.WriteUInt32( collectionBlocks );

			for( int i = 0; i < collectionBlocks; i++ )
			{
				collectionBlock[i].WriteByteArray( aByteArray );
			}
		}
	}
}
