using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdSelfShadowChunk : VmdChunkAbstract
	{
		public VmdSelfShadowChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new VmdSelfShadowData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new VmdSelfShadowData( aByteArray );
			}
		}
	}
}
