using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdMotionChunk : VmdChunkAbstract
	{
		public VmdMotionChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new VmdMotionData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new VmdMotionData( aByteArray );
			}
		}
	}
}
