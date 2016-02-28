using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdLightChunk : VmdChunkAbstract
	{
		public VmdLightChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new VmdLightData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new VmdLightData( aByteArray );
			}
		}
	}
}
