using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdVertexChunk : PmdChunkAbstract
	{
		public PmdVertexChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new PmdVertexData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new PmdVertexData( aByteArray );
			}
		}
	}
}
