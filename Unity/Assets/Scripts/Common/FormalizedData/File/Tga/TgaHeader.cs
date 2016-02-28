using System;
using System.IO;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Tga
{
	public class TgaHeader
	{
		public readonly byte idFieldLength;
		public readonly byte colorMapType;
		public readonly byte imageType;
		public readonly UInt16 colorMapIndex;
		public readonly UInt16 colorMapLength;
		public readonly byte colorMapSize;
		public readonly UInt16 imageOriginX;
		public readonly UInt16 imageOriginY;
		public readonly UInt16 imageWidth;
		public readonly UInt16 imageHeight;
		public readonly byte bitPerPixel;
		public readonly byte discripter;

		public TgaHeader( ByteArray aByteArray )
		{
			idFieldLength = aByteArray.ReadByte();
			colorMapType = aByteArray.ReadByte();
			imageType = aByteArray.ReadByte();
			colorMapIndex = aByteArray.ReadUInt16();
			colorMapLength = aByteArray.ReadUInt16();
			colorMapSize = aByteArray.ReadByte();
			imageOriginX = aByteArray.ReadUInt16();
			imageOriginY = aByteArray.ReadUInt16();
			imageWidth = aByteArray.ReadUInt16();
			imageHeight = aByteArray.ReadUInt16();
			bitPerPixel = aByteArray.ReadByte();
			discripter = aByteArray.ReadByte();

			Logger.LogWarning( "ID Field Length:" + idFieldLength );
			Logger.LogWarning( "Color Map Type" + colorMapType.ToString( "X2" ) );
			Logger.LogWarning( "Image Type:" + imageType.ToString( "X2" ) );
			Logger.LogWarning( "Color Map Index:" + colorMapIndex.ToString( "X2" ) );
			Logger.LogWarning( "Color Map Length:" + colorMapLength.ToString( "X4" ) );
			Logger.LogWarning( "Color Map Size:" + colorMapSize );
			Logger.LogWarning( "Image Origin X:" + imageOriginX );
			Logger.LogWarning( "Image Origin Y:" + imageOriginY );
			Logger.LogWarning( "Image Width:" + imageWidth );
			Logger.LogWarning( "Image Height:" + imageHeight );
			Logger.LogWarning( "Bit Per Pixel:" + bitPerPixel );
			Logger.LogWarning( "Discripter:" + discripter );
		}
	}
}
