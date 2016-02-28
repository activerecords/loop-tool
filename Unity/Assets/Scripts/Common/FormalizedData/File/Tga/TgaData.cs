using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Tga
{
	public class TgaData
	{
		public readonly byte[] dataArray;

		public TgaData( ByteArray aByteArray, TgaHeader aHeader )
		{
			if( aHeader.imageType == 0x02 )
			{
				dataArray = aByteArray.ReadBytes( aHeader.imageWidth * aHeader.imageHeight * 4 );
			}
		}
	}
}
