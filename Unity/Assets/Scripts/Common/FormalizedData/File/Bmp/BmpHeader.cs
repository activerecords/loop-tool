using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Bmp
{
	public class BmpHeader
	{
		public readonly UInt16 type;
		public readonly UInt32 fileSize;
		public readonly UInt16 reserved1;
		public readonly UInt16 reserved2;
		public readonly UInt32 offset;

		public readonly UInt32 size;
		public readonly UInt32 width;
		public readonly UInt32 height;
		public readonly UInt16 planes;
		public readonly UInt16 bitCount;
		public readonly UInt32 compression;
		public readonly UInt32 sizeImage;
		public readonly UInt32 xPelsPerMeter;
		public readonly UInt32 yPelsPerMeter;
		public readonly UInt32 clrUsed;
		public readonly UInt32 clrImportant;

		public BmpHeader( ByteArray byteArray )
		{
			type = byteArray.ReadUInt16();
			fileSize = byteArray.ReadUInt32();
			reserved1 = byteArray.ReadUInt16();
			reserved2 = byteArray.ReadUInt16();
			offset = byteArray.ReadUInt32();

			size = byteArray.ReadUInt32();
			width = byteArray.ReadUInt32();
			height = byteArray.ReadUInt32();
			planes = byteArray.ReadUInt16();
			bitCount = byteArray.ReadUInt16();
			compression = byteArray.ReadUInt32();
			sizeImage = byteArray.ReadUInt32();
			xPelsPerMeter = byteArray.ReadUInt32();
			yPelsPerMeter = byteArray.ReadUInt32();
			clrUsed = byteArray.ReadUInt32();
			clrImportant = byteArray.ReadUInt32();

			Logger.LogWarning( "Type:" + type );
			Logger.LogWarning( "File Size" + fileSize.ToString( "X4" ) );
			Logger.LogWarning( "Reserved 1:" + reserved1.ToString( "X4" ) );
			Logger.LogWarning( "Reserved 2:" + reserved2.ToString( "X4" ) );
			Logger.LogWarning( "Offset:" + offset.ToString( "X4" ) );

			Logger.LogWarning( "Size:" + size );
			Logger.LogWarning( "Width:" + width );
			Logger.LogWarning( "Height:" + height );
			Logger.LogWarning( "Planes:" + planes );
			Logger.LogWarning( "Bit Count:" + bitCount );
			Logger.LogWarning( "Compression:" + compression );
			Logger.LogWarning( "Size Image:" + sizeImage );
			Logger.LogWarning( "X Pels Per Meter:" + xPelsPerMeter );
			Logger.LogWarning( "Y Pels Per Meter:" + yPelsPerMeter );
			Logger.LogWarning( "Clr Used:" + clrUsed );
			Logger.LogWarning( "Clr Important:" + clrImportant );
		}
	}
}
