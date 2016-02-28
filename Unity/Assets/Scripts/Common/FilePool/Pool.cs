using System;
using System.Collections.Generic;
using System.IO;

namespace Curan.Common.FilePool
{
	public class Pool
	{
		public delegate object Constructor( FileStream aFileStream );

		private Constructor constructor;
		private object objectLock;
		private Dictionary<string, object> dictionary;

		public Pool( Constructor aConstructor )
		{
			constructor = aConstructor;
			objectLock = new object();
			dictionary = new Dictionary<string, object>();
		}

		public object Get( string aPathFile )
		{
			lock( objectLock )
			{
				if( dictionary.ContainsKey( aPathFile ) == false )
				{
					try
					{
						using( FileStream u = new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
						{
							object l = constructor( u );

							dictionary.Add( aPathFile, l );
						}
					}
					catch( Exception aExpection )
					{
						UnityEngine.Debug.LogError( "IOException:" + aPathFile );
					}
				}
			}

			if( dictionary.ContainsKey( aPathFile ) == true )
			{
				return dictionary[aPathFile];
			}
			else
			{
				return null;
			}
		}
	}
}
