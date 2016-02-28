using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffFile : RiffChunkList
	{
		public const string ID = "RIFF";

		public static readonly Dictionary<string,Type> bodyTypeDictionary;
		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		
		public readonly string name;
		public readonly RiffChunkList riffChunkList;

		static RiffFile()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();

			bodyTypeDictionary = new Dictionary<string, Type>();
			bodyTypeDictionary.Add( Avi.RiffChunkListAvi_.TYPE, typeof( Avi.RiffChunkListAvi_ ) );
			bodyTypeDictionary.Add( Dls.RiffChunkListDls_.TYPE, typeof( Dls.RiffChunkListDls_ ) );
			bodyTypeDictionary.Add( Sfbk.RiffChunkListSfbk.TYPE, typeof( Sfbk.RiffChunkListSfbk ) );
			bodyTypeDictionary.Add( Wave.RiffChunkListWave.TYPE, typeof( Wave.RiffChunkListWave ) );
		}
		
		public RiffFile( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{
			
		}

		public RiffFile( FileStream aStream )
			: this( new ByteArrayLittle( aStream ) )
		{

		}

		public RiffFile( ByteArray aByteArray )
			: base( chunkTypeDictionary, bodyTypeDictionary, ID, ( UInt32 )aByteArray.Length, aByteArray, null )
		{
			name = aByteArray.GetName();
			riffChunkList = ( RiffChunkList )GetChunk( "RIFF" );
		}
		
		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			if( name != null && name != "" )
			{
				using ( FileStream u = new FileStream( name, FileMode.Open, FileAccess.Read ) )
				{
					ByteArray lByteArray = new ByteArrayLittle( u );

					foreach( RiffChunk lChunk in chunkList )
					{
						lChunk.WriteByteArray( lByteArray, aByteArray );
					}
				}
			}
		}
	}
}
