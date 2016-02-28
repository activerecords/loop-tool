using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdIkChunk : PmdChunkAbstract
	{
		public PmdIkChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt16();

			dataArray = new PmdIkData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new PmdIkData( aByteArray );
			}
		}
	}
}
