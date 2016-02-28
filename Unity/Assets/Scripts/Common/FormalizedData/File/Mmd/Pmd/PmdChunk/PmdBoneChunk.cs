using System;
using System.Collections.Generic;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdBoneChunk : PmdChunkAbstract
	{
		public PmdBoneChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt16();

			dataArray = new PmdBoneData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new PmdBoneData( aByteArray );
			}
		}
	}
}
