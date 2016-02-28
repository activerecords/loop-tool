using System;
using System.IO;

using Curan.Common.AdaptedData;
using Curan.Common.AdaptedData.Graphic;
using UnityEngine;

namespace Curan.Common.FileLoader
{
    public class TextureLoader
    {
        public static Texture2D Load( string aName )
        {
            GraphicBase lTexture = null;
            
            string lFileExtension = Path.GetExtension( aName );
            
            switch( lFileExtension )
            {
                case ".bmp":
                    lTexture = new GraphicBmp( Application.streamingAssetsPath + aName );
                    break;
                    
                case ".tga":
                    lTexture = new GraphicTga( Application.streamingAssetsPath + aName );
                    break;
                    
                default:
                    WWW www = new WWW( "file://" + Path.Combine( Application.streamingAssetsPath, aName ) );
                    lTexture = new GraphicBase( www.texture );
                    break;
            }
            
            return lTexture.texture;
        }
    }
}
