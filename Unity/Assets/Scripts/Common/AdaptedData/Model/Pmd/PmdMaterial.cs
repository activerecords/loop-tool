using System;
using System.IO;

using Curan.Common.FilePool;
using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Pmd;
using UnityEngine;

/*
namespace Curan.Common.AdaptedData.Model
{
	public class PmdMaterial
	{
		public readonly OpenTK.Vector3 diffuseColor;
		public readonly Material material;
		public readonly Texture2D texture;
		public readonly string texturePath;

		public PmdMaterial( PmdMaterialData aPmdMaterialData, string aPathDirectory )
		{
			diffuseColor = aPmdMaterialData.diffuseColor;

			//			material = new Material( Shader.Find( "Unlit/Texture" ) );
			//			material.hideFlags = HideFlags.HideAndDontSave;
			//			material.shader.hideFlags = HideFlags.HideAndDontSave;
			
			if( aPmdMaterialData.textureFileName != "" )
			{
				string lSetTexture = aPmdMaterialData.textureFileName.Split( '\0' )[0];
				string lNameTexture = lSetTexture.Split( '*' )[0];

				if( lNameTexture != "" )
				{
					texturePath = Path.Combine( aPathDirectory, lNameTexture );
					//					texture = ( Texture2D )PoolTexture.GetTexture( Path.Combine( aPathDirectory, lNameTexture ) );
				}
				else
				{
					//Logger.LogWarning( aPmdMaterialData.textureFileName );
					texture = CreateDotTexture2d( TextureFormat.RGBA32, new Color( diffuseColor.X, diffuseColor.Y, diffuseColor.Z ) );
				}
			}

			//			material.mainTexture = texture;
		}

		private Texture2D CreateDotTexture2d( TextureFormat aTextureFormat, Color aColor )
		{
			Texture2D lTexture = new Texture2D( 1, 1, aTextureFormat, false );

			lTexture.SetPixel( 0, 0, aColor );
			lTexture.Apply();

			return lTexture;
		}
	}
}
*/