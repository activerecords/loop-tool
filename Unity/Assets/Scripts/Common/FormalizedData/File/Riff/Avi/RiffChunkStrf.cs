using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkStrf : RiffChunk
	{
		public const string ID = "strf";

		public readonly StreamFormat streamFormat;

		public RiffChunkStrf( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			if( size == 30 )
			{
				streamFormat = new Mpeglayer3waveformat( aByteArray, informationList );
			}

			if( size == 40 )
			{
				streamFormat = new Bitmapinfo( aByteArray, informationList );
			}
		}
	}

	public interface StreamFormat
	{

	}

	public class Waveformatex
	{
		public readonly UInt16 formatTag;
		public readonly UInt16 channels;
		public readonly UInt32 samplesPerSec;
		public readonly UInt32 avgBytesPerSec;
		public readonly UInt16 blockAlign;
		public readonly UInt16 bitsPerSample;
		public readonly UInt16 size;

		public Waveformatex( ByteArray aByteArray, List<string> aInformationList )
		{
			formatTag = aByteArray.ReadUInt16();
			channels = aByteArray.ReadUInt16();
			samplesPerSec = aByteArray.ReadUInt32();
			avgBytesPerSec = aByteArray.ReadUInt32();
			blockAlign = aByteArray.ReadUInt16();
			bitsPerSample = aByteArray.ReadUInt16();
			size = aByteArray.ReadUInt16();

			aInformationList.Add( "Format Tag:" + formatTag );
			aInformationList.Add( "Channels:" + channels );
			aInformationList.Add( "Samples Per Sec:" + samplesPerSec );
			aInformationList.Add( "Avg Bytes Per Sec:" + avgBytesPerSec );
			aInformationList.Add( "Block Align:" + blockAlign );
			aInformationList.Add( "Bits Per Sample:" + bitsPerSample );
			aInformationList.Add( "Size:" + size );
		}
	}

	public class Mpeglayer3waveformat : StreamFormat
	{
		public readonly Waveformatex waveformatex;
		public readonly UInt16 id;
		public readonly UInt32 flags;
		public readonly UInt16 blockSize;
		public readonly UInt16 framesPerBlock;
		public readonly UInt16 codecDelay;

		public Mpeglayer3waveformat( ByteArray aByteArray, List<string> aInformationList )
		{
			waveformatex = new Waveformatex( aByteArray, aInformationList );

			id = aByteArray.ReadUInt16();
			flags = aByteArray.ReadUInt32();
			blockSize = aByteArray.ReadUInt16();
			framesPerBlock = aByteArray.ReadUInt16();
			codecDelay = aByteArray.ReadUInt16();

			aInformationList.Add( "ID:" + id );
			aInformationList.Add( "Flags:" + flags );
			aInformationList.Add( "Block Size:" + blockSize );
			aInformationList.Add( "Frames Per Block:" + framesPerBlock );
			aInformationList.Add( "Codec Delay:" + codecDelay );
		}
	}

	public class Bitmapinfoheader
	{
		public readonly UInt32 size;
		public readonly Int32 width;
		public readonly Int32 height;
		public readonly UInt16 planes;
		public readonly UInt16 bitCount;
		public readonly UInt32 compression;
		public readonly UInt32 sizeImage;
		public readonly Int32 xPelsPerMeter;
		public readonly Int32 yPelsPerMeter;
		public readonly UInt32 clrUsed;
		public readonly UInt32 clrImportant;

		public Bitmapinfoheader( ByteArray aByteArray, List<string> aInformationList )
		{
			size = aByteArray.ReadUInt32();
			width = aByteArray.ReadInt32();
			height = aByteArray.ReadInt32();
			planes = aByteArray.ReadUInt16();
			bitCount = aByteArray.ReadUInt16();
			compression = aByteArray.ReadUInt32();
			sizeImage = aByteArray.ReadUInt32();
			xPelsPerMeter = aByteArray.ReadInt32();
			yPelsPerMeter = aByteArray.ReadInt32();
			clrUsed = aByteArray.ReadUInt32();
			clrImportant = aByteArray.ReadUInt32();

			aInformationList.Add( "Size:" + size );
			aInformationList.Add( "Width:" + width );
			aInformationList.Add( "Height:" + height );
			aInformationList.Add( "Planes:" + planes );
			aInformationList.Add( "Bit Count:" + bitCount );
			aInformationList.Add( "Compression:" + compression );
			aInformationList.Add( "Size Image:" + sizeImage );
			aInformationList.Add( "X Pels Per Meter:" + xPelsPerMeter );
			aInformationList.Add( "Y Pels Per Meter:" + yPelsPerMeter );
			aInformationList.Add( "Clr Used:" + clrUsed );
			aInformationList.Add( "Clr Important:" + clrImportant );
		}
	}

	public class Rgbquad
	{
		public readonly Byte rgbBlue;
		public readonly Byte rgbGreen;
		public readonly Byte rgbRed;
		public readonly Byte rgbReserved;

		public Rgbquad( ByteArray aByteArray, List<string> aInformationList )
		{
			rgbBlue = aByteArray.ReadByte();
			rgbGreen = aByteArray.ReadByte();
			rgbRed = aByteArray.ReadByte();
			rgbReserved = aByteArray.ReadByte();

			aInformationList.Add( "Rgb Blue:" + rgbBlue );
			aInformationList.Add( "Rgb Green:" + rgbGreen );
			aInformationList.Add( "Rgb Red:" + rgbRed );
			aInformationList.Add( "Rgb Reserved:" + rgbReserved );
		}
	}

	public class Bitmapinfo : StreamFormat
	{
		public readonly Bitmapinfoheader bitmapinfoheader;
		public readonly Rgbquad[] rgbquad;

		public Bitmapinfo( ByteArray aByteArray, List<string> aInformationList )
		{
			bitmapinfoheader = new Bitmapinfoheader( aByteArray, aInformationList );

			if( bitmapinfoheader.bitCount < 8 ) {
				int lLength = ( int )bitmapinfoheader.clrUsed;

				if( lLength == 0 )
				{
					lLength = 1 << bitmapinfoheader.bitCount;
				}

				rgbquad = new Rgbquad[lLength];

				for( int i = 0; i < lLength; i++ ) {
					rgbquad[i] = new Rgbquad( aByteArray, aInformationList );
				}
			}
		}
	}
}
