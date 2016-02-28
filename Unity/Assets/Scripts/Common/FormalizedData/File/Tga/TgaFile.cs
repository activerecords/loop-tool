using System;
using System.IO;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Tga
{
	public class TgaFile
	{
		public readonly TgaHeader header;
		public readonly TgaData data;

		public TgaFile( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayLittle( aStream );

			header = new TgaHeader( lByteArray );
			data = new TgaData( lByteArray, header );
		}
	}
}
