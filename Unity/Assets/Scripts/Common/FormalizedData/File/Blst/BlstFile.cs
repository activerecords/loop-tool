using System;
using System.Collections.Generic;
using System.IO;

using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Blst
{
	public class BlstFile
	{
		public readonly string pathWaveform;
		public readonly List<string> pathBankList;

		public BlstFile( string aPath )
		{

		}

		public BlstFile( FileStream aFileStream )
		{
			pathBankList = new List<string>();

			using( StreamReader u = new StreamReader( aFileStream ) )
			{
				string lLine = u.ReadLine();
				pathWaveform = lLine;

				for( int i = 0; i < 0x10000; i++ )
				{
					lLine = u.ReadLine();

					if( lLine != null && lLine != "" && lLine.IndexOf( "//" ) != 0 )
					{
						if( lLine[0] == '/' )
						{
							pathBankList.Add( lLine );
						}
						else
						{
							pathBankList.Add( Path.GetDirectoryName( aFileStream.Name ) + "/" + lLine );
						}
					}
				}
			}
		}
	}
}
