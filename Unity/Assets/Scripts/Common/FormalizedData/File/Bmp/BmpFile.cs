using System;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Bmp
{
	public class BmpFile
	{
		public readonly BmpHeader bmpHeader;
		public readonly BmpData bmpData;

		public BmpFile( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayLittle( aStream );

			bmpHeader = new BmpHeader( lByteArray );
			bmpData = new BmpData( lByteArray, bmpHeader );
		}
	}
}
