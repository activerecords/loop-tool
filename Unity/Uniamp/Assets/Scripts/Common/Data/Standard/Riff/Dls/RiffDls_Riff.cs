using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Riff : RiffChunkList
	{
		public const string ID = "RIFF";

		public readonly RiffDls_Colh colhChunkList;
		public readonly RiffDls_Msyn msynChunkList;
		public readonly RiffDls_Ptbl ptblChunkList;
		public readonly RiffDls_Vers versChunkList;
		public readonly RiffChunkListLins linsListList;
		public readonly RiffChunkListWvpl wvplListList;
		public readonly RiffDls_List infoListList;
		
		public readonly string name;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string,Type> lChunkTypeDictionaryDls_ = new Dictionary<string, Type>();
					lChunkTypeDictionaryDls_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					lChunkTypeDictionaryDls_.Add( RiffDls_Colh.ID, typeof( RiffDls_Colh ) );
					lChunkTypeDictionaryDls_.Add( RiffDls_Msyn.ID, typeof( RiffDls_Msyn ) );
					lChunkTypeDictionaryDls_.Add( RiffDls_Ptbl.ID, typeof( RiffDls_Ptbl ) );
					lChunkTypeDictionaryDls_.Add( RiffDls_Vers.ID, typeof( RiffDls_Vers ) );
					
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
					chunkTypeDictionaryDictionary.Add( "DLS ", lChunkTypeDictionaryDls_ );
				}

				return chunkTypeDictionaryDictionary;
			}
		}
		
		public RiffDls_Riff( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{
			
		}
		
		public RiffDls_Riff( FileStream aStream )
			: this( new ByteArrayLittle( aStream ) )
		{
			
		}
		
		public RiffDls_Riff( AByteArray aByteArray )
			: base( aByteArray.ReadString( 4 ), aByteArray.ReadUInt32(), aByteArray, null )
		{
			name = aByteArray.GetName();
		}

		public RiffDls_Riff( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			name = aByteArray.GetName();

			colhChunkList = ( RiffDls_Colh )GetChunk( RiffDls_Colh.ID );
			msynChunkList = ( RiffDls_Msyn )GetChunk( RiffDls_Msyn.ID );
			ptblChunkList = ( RiffDls_Ptbl )GetChunk( RiffDls_Ptbl.ID );
			versChunkList = ( RiffDls_Vers )GetChunk( RiffDls_Vers.ID );
			linsListList = null;// RiffChunkListLins )GetChunkList( "LIST", RiffChunkListLins.TYPE );
			wvplListList = null;//( RiffChunkListWvpl )GetChunkList( "LIST", RiffChunkListWvpl.TYPE );
			infoListList = null;//( RiffInfoList )GetChunkList( "LIST", RiffInfoList.TYPE );
		}
	}
}
