using System;
using System.Collections.Generic;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdFaceVertexChunk : PmdChunkAbstract
	{
		public PmdFaceVertexChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new PmdFaceVertexData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new PmdFaceVertexData( aByteArray );
			}
		}
	}
}
