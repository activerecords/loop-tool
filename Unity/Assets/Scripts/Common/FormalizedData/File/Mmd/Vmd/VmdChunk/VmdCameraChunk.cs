using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdCameraChunk : VmdChunkAbstract
	{
		public VmdCameraChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new VmdCameraData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new VmdCameraData( aByteArray );
			}
		}
	}
}
