using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListIns_ : RiffChunkList
	{
		public const string ID = "LIST";

		public readonly RiffDls_Dlid dlidChunk;
		public readonly RiffDls_Insh inshChunk;
		public readonly List<RiffChunkList> lrgnListList;
		public readonly List<RiffChunkList> lartListList;
		public readonly List<RiffChunkList> infoListList;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}
		/*
		static RiffChunkListIns_()
		{
			Dictionary<string, Type> lChunkTypeDictionary = new Dictionary<string, Type>();
			lChunkTypeDictionary.Add( RiffDls_Dlid.ID, typeof( RiffDls_Dlid ) );
			lChunkTypeDictionary.Add( RiffDls_Insh.ID, typeof( RiffDls_Insh ) );
			
			ChunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
			ChunkTypeDictionaryDictionary.Add( "ins ", lChunkTypeDictionary );

			//bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			//bodyTypeDictionary.Add( RiffChunkListLrgn.TYPE, typeof( RiffChunkListLrgn ) );
			//bodyTypeDictionary.Add( RiffChunkListLart.TYPE, typeof( RiffChunkListLart ) );
			//bodyTypeDictionary.Add( RiffInfoList.TYPE, typeof( RiffInfoList ) );
		}
*/
		public RiffChunkListIns_( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			dlidChunk = ( RiffDls_Dlid )GetChunk( RiffDls_Dlid.ID );
			inshChunk = ( RiffDls_Insh )GetChunk( RiffDls_Insh.ID );
			lrgnListList = null;// GetChunkListList( "LIST", RiffChunkListLrgn.TYPE );
			lartListList = null;//GetChunkListList( "LIST", RiffChunkListLart.TYPE );
			infoListList = null;//GetChunkListList( "LIST", RiffInfoList.TYPE );
		}
	}
}
