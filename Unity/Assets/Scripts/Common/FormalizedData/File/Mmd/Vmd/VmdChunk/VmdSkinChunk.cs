using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdSkinChunk : VmdChunkAbstract
	{
		public VmdSkinChunk( ByteArray aByteArray )
		{
			count = aByteArray.ReadUInt32();

			dataArray = new VmdSkinData[count];

			for( int i = 0; i < count; i++ )
			{
				dataArray[i] = new VmdSkinData( aByteArray );
			}
		}
	}
}
