using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListRgn_ : RiffChunkList
	{
		public const string TYPE = "rgn ";
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}

		public readonly RiffDls_Rgnh rgnhBody;
		public readonly RiffDls_Wsmp wsmpBody;
		public readonly RiffDls_Wlnk wlnkBody;
		public readonly List<RiffChunkList> rgn_ListList;

		/*
		static RiffChunkListRgn_()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffDls_Rgnh.ID, typeof( RiffDls_Rgnh ) );
			chunkTypeDictionary.Add( RiffDls_Wsmp.ID, typeof( RiffDls_Wsmp ) );
			chunkTypeDictionary.Add( RiffDls_Wlnk.ID, typeof( RiffDls_Wlnk ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListRgn_.TYPE, typeof( RiffChunkListRgn_ ) );
			bodyTypeDictionary.Add( RiffChunkListLart.TYPE, typeof( RiffChunkListLart ) );
		}*/

		public RiffChunkListRgn_( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			rgnhBody = ( RiffDls_Rgnh )GetChunk( RiffDls_Rgnh.ID );
			wsmpBody = ( RiffDls_Wsmp )GetChunk( RiffDls_Wsmp.ID );
			wlnkBody = ( RiffDls_Wlnk )GetChunk( RiffDls_Wlnk.ID );

			rgn_ListList = GetChunkListList( "LIST", RiffChunkListRgn_.TYPE );
		}
	}
}
