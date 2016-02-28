using System;
using System.IO;
using System.Collections.Generic;

namespace Curan.Common.FormalizedData.File.Bnk
{
	public class BnkFile
	{
		public readonly List<string> pathSfzList;

		public BnkFile( FileStream aStream )
		{
			pathSfzList = new List<string>();

			using( StreamReader u = new StreamReader( aStream ) )
			{
				for( int i = 0; i < 128; i++ )
				{
					string lLine = u.ReadLine();

					if( lLine != null && lLine != "" && lLine.IndexOf( "//" ) != 0 )
					{
						if( lLine[0] == '/' )
						{
							pathSfzList.Add( lLine );
						}
						else
						{
							pathSfzList.Add( Path.GetDirectoryName( aStream.Name ) + "/" + lLine );
						}
					}
				}
			}
		}
	}
}
