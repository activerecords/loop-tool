using UnityEngine;

using System;
using System.IO;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Tga;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Graphic
{
	public class GraphicTga : GraphicBase
	{
		public GraphicTga( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public GraphicTga( Stream aStream )
			: this( new TgaFile( aStream ) )
		{

		}

        public GraphicTga( TgaFile aTgaFile )
            : base()
		{
			int lWidth = aTgaFile.header.imageWidth;
			int lHeight = aTgaFile.header.imageHeight;
			Byte lImageType = aTgaFile.header.imageType;

			Byte[] lDataArray = aTgaFile.data.dataArray;

			Color[] lColorArray = new Color[lWidth * lHeight];

			if( lImageType == 0x02 )
			{
				texture = new Texture2D( ( int )lWidth, ( int )lHeight, TextureFormat.RGBA32, false );

				for( int i = 0; i < lWidth * lHeight; i++ )
				{
					Byte b = lDataArray[i * 4 + 0];
					Byte g = lDataArray[i * 4 + 1];
					Byte r = lDataArray[i * 4 + 2];
					Byte a = lDataArray[i * 4 + 3];

					lColorArray[i] = new Color( ( float )r / 0x100, ( float )g / 0x100, ( float )b / 0x100, ( float )a / 0x100 );
				}
			}
			else
			{
				Logger.LogError( "Bit Count:" + lImageType.ToString( "X2" ) + " is not supported" );
			}

			texture.SetPixels( 0, 0, ( int )lWidth, ( int )lHeight, lColorArray );
			texture.Apply();
		}
	}
}
