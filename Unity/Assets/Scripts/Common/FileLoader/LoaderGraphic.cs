using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.AdaptedData.Graphic;

namespace Curan.Common.FileLoader.Graphic
{
	public class LoaderGraphic : LoaderBase
	{
		private static Dictionary<string, Constructor> constructorDictionary;

		static LoaderGraphic()
		{
			constructorDictionary = new Dictionary<string, Constructor>();

			constructorDictionary.Add( ".bmp", ( string a ) => { return new GraphicBmp( a ); } );
			constructorDictionary.Add( ".tga", ( string a ) => { return new GraphicTga( a ); } );
		}

		public static GraphicBase Load( string aPathFile )
		{
			return ( GraphicBase )Load( constructorDictionary, aPathFile );
		}
	}
}
