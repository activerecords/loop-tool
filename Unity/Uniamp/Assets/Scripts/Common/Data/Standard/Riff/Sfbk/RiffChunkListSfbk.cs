using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkListSfbk : RiffChunkList
	{
		public const string TYPE = "sfbk";

		public readonly List<RiffChunkList> sdtaListList;
		public readonly List<RiffChunkList> pdtaListList;
		public readonly RiffChunkListSdta sdtaBodyList;
		
		public readonly string name;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> chunkTypeDictionary = new Dictionary<string, Type>();
					chunkTypeDictionary.Add( RiffInfoIbag.ID, typeof( RiffInfoIbag ) );
					chunkTypeDictionary.Add( RiffInfoIgen.ID, typeof( RiffInfoIgen ) );
					chunkTypeDictionary.Add( RiffInfoImod.ID, typeof( RiffInfoImod ) );
					chunkTypeDictionary.Add( RiffInfoInst.ID, typeof( RiffInfoInst ) );
					chunkTypeDictionary.Add( RiffChunkPbag.ID, typeof( RiffChunkPbag ) );
					chunkTypeDictionary.Add( RiffChunkPgen.ID, typeof( RiffChunkPgen ) );
					chunkTypeDictionary.Add( RiffChunkPhdr.ID, typeof( RiffChunkPhdr ) );
					chunkTypeDictionary.Add( RiffChunkPmod.ID, typeof( RiffChunkPmod ) );
					chunkTypeDictionary.Add( RiffChunkShdr.ID, typeof( RiffChunkShdr ) );
					chunkTypeDictionary.Add( RiffChunkSmpl.ID, typeof( RiffChunkSmpl ) );

			//bodyTypeDictionary = new Dictionary<string, Type>();
			//chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			//bodyTypeDictionary.Add( RiffChunkListInfo.TYPE, typeof( RiffChunkListInfo ) );
			//bodyTypeDictionary.Add( RiffChunkListSdta.TYPE, typeof( RiffChunkListSdta ) );
					//bodyTypeDictionary.Add( RiffChunkListPdta.TYPE, typeof( RiffChunkListPdta ) );
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
				}
				
				return chunkTypeDictionaryDictionary;
			}
		}
		
		public RiffChunkListSfbk( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{
			
		}
		
		public RiffChunkListSfbk( FileStream aStream )
			: this( new ByteArrayLittle( aStream ) )
		{
			
		}
		
		public RiffChunkListSfbk( AByteArray aByteArray )
			: base( aByteArray.ReadString( 4 ), aByteArray.ReadUInt32(), aByteArray, null )
		{
			name = aByteArray.GetName();
		}

		public RiffChunkListSfbk( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;
			name = aByteArray.GetName();

			sdtaListList = GetChunkListList( "LIST", RiffChunkListSdta.TYPE );
			pdtaListList = GetChunkListList( "LIST", RiffChunkListPdta.TYPE );
			sdtaBodyList = ( RiffChunkListSdta )GetChunkList( "LIST", RiffChunkListSdta.TYPE );
		}
	}
}
