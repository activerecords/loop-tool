using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Bmp
{
	public class BmpData
	{
		public readonly byte[] dataArray;

		public BmpData( ByteArray aByteArray, BmpHeader aBmpHeader )
		{
			dataArray = aByteArray.ReadBytes( ( int )( aBmpHeader.bitCount / 8 * aBmpHeader.width * aBmpHeader.height ) );
		}

		public byte[] GetDataArray()
		{
			return dataArray;
		}
	}
}
