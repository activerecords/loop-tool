using UnityEngine;

using System;
using System.IO;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Bmp;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Graphic
{
	public class GraphicBmp : GraphicBase
	{
		public GraphicBmp( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public GraphicBmp( Stream aStream )
			: this( new BmpFile( aStream ) )
		{

		}

		public GraphicBmp( BmpFile aBmpFile )
            : base()
		{
			int lWidth = ( int )aBmpFile.bmpHeader.width;
			int lHeight = ( int )aBmpFile.bmpHeader.height;
			int lBitCount = aBmpFile.bmpHeader.bitCount;

			Byte[] lDataArray = aBmpFile.bmpData.dataArray;

			Color[] lColorArray = new Color[lWidth * lHeight];

			if( lBitCount == 24 )
			{
				for( int i = 0; i < lWidth * lHeight; i++ )
				{
					byte b = lDataArray[i * 3 + 0];
					byte g = lDataArray[i * 3 + 1];
					byte r = lDataArray[i * 3 + 2];

					lColorArray[i] = new Color( ( float )r / 0x100, ( float )g / 0x100, ( float )b / 0x100 );
				}
			}
			else if( lBitCount == 32 )
			{
				for( int i = 0; i < lWidth * lHeight; i++ )
				{
					byte b = lDataArray[i * 4 + 0];
					byte g = lDataArray[i * 4 + 1];
					byte r = lDataArray[i * 4 + 2];
					byte a = lDataArray[i * 4 + 3];

					lColorArray[i] = new Color( ( float )r / 0x100, ( float )g / 0x100, ( float )b / 0x100, ( float )a / 0x100 );
				}
			}
			else
			{
				Logger.LogError( "Bit Count:" + lBitCount + " is not supported" );
			}

			texture = new Texture2D( ( int )lWidth, ( int )lHeight, TextureFormat.RGBA32, false );
			texture.SetPixels( 0, 0, ( int )lWidth, ( int )lHeight, lColorArray );
			texture.Apply();
		}
	}
}
