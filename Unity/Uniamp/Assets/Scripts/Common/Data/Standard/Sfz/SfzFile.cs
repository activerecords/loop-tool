using System;
using System.IO;
using System.Collections.Generic;

namespace Monoamp.Common.Data.Standard.Sfz
{
	public class SfzFile
	{
		public readonly List<SfzRegion> sfzRegionList;

		public SfzFile( string aPath )
			: this( new FileStream( aPath, FileMode.Open, FileAccess.Read ) )
		{

		}

		public SfzFile( FileStream aFileStream )
		{
			sfzRegionList = new List<SfzRegion>();

			using( StreamReader lStreamReader = new StreamReader( aFileStream ) )
			{
				SfzRegion sfzGroup = null;
				SfzRegion sfzDataRegion = null;

				string line;

				while( ( line = lStreamReader.ReadLine() ) != null )
				{
					if( line == "<group>" )
					{
						sfzGroup = new SfzRegion( lStreamReader, Path.GetDirectoryName( aFileStream.Name ) );
					}
					else if( line == "<region>" || line == "<global>" )
					{
						if( sfzGroup != null )
						{
							sfzDataRegion = ( SfzRegion )sfzGroup.Clone();
							sfzDataRegion.Read( lStreamReader );
						}
						else
						{
							sfzDataRegion = new SfzRegion( lStreamReader, aFileStream.Name );
						}

						sfzRegionList.Add( sfzDataRegion );
					}
					else if( line == "" || line.IndexOf( "//" ) == 0 )
					{
						// コメントまたは区切り行.
					}
					else
					{
						// 未定義またはフォーマットエラー.
					}
				}
			}
		}
	}
}
