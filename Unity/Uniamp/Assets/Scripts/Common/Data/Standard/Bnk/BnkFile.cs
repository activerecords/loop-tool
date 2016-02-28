using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Bnk
{
	public class BnkFile
	{
		public readonly string[] pathSfzArray;

		public BnkFile( FileStream aStream )
		{
			pathSfzArray = new string[128];

			using( StreamReader u = new StreamReader( aStream ) )
			{
				for( int i = 0; i < 128; i++ )
				{
					string lLine = u.ReadLine();

					if( lLine != null && lLine != "" && lLine.IndexOf( "//" ) != 0 )
					{
						if( lLine[0] == '/' )
						{
							pathSfzArray[i] = lLine;
						}
						else
						{
							pathSfzArray[i] = Path.GetDirectoryName( aStream.Name ) + "/" + lLine;
						}

						Logger.Debug( pathSfzArray[i] );
					}
				}
			}
		}
	}
}
