using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListLrgn : RiffChunkList
	{
		public const string TYPE = "lrgn";
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}

		public readonly List<RiffChunkList> rgn_ListList;
		public readonly List<RiffChunkList> rgn2ListList;

		/*
		static RiffChunkListLrgn()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListRgn_.TYPE, typeof( RiffChunkListRgn_ ) );
			bodyTypeDictionary.Add( RiffChunkListRgn2.TYPE, typeof( RiffChunkListRgn2 ) );
		}*/

		public RiffChunkListLrgn( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			rgn_ListList = GetChunkListList( "LIST", RiffChunkListRgn_.TYPE );
			rgn2ListList = GetChunkListList( "LIST", RiffChunkListRgn2.TYPE );
		}
	}
}
