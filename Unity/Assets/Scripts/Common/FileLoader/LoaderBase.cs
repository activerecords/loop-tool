using System;
using System.Collections.Generic;
using System.IO;

using Curan.Utility;

namespace Curan.Common.FileLoader
{
	public class LoaderBase
	{
		protected delegate Object Constructor( string aFilePath );

		protected static Object Load( Dictionary<string, Constructor> aConstructorDictionary, string aPathFile )
		{
			Object lObject = null;

			string lExtension = Path.GetExtension( aPathFile ).ToLower();

			// 登録してある拡張子の場合はインスタンスを生成する.
			if( aConstructorDictionary.ContainsKey( lExtension ) )
			{
				lObject = aConstructorDictionary[lExtension]( aPathFile );
			}
			else
			{
				Logger.LogWarning( aPathFile + " is an Unknown File." );
			}

			return lObject;
		}
	}
}
