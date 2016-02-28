using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Blst
{
	public class BlstFile
	{
		public readonly List<string> pathBankList;

		public BlstFile( string aFilePath )
			: this( new FileStream( aFilePath, FileMode.Open, FileAccess.Read ) )
		{

		}

		public BlstFile( FileStream aFileStream )
		{
			Logger.Debug( aFileStream.Name );
			pathBankList = new List<string>();

			using( StreamReader u = new StreamReader( aFileStream ) )
			{
				for( string l = u.ReadLine(); l != null; l = u.ReadLine() )
				{
					if( l != "" && l.IndexOf( "//" ) != 0 )
					{
						if( l[0] == '/' )
						{
							pathBankList.Add( l );
						}
						else
						{
							pathBankList.Add( Path.GetDirectoryName( aFileStream.Name ) + "/" + l );
						}
					}
				}
			}
		}
	}
}
