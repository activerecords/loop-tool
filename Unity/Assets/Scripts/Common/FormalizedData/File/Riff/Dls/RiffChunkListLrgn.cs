using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkListLrgn : RiffChunkList
	{
		public const string TYPE = "lrgn";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public readonly List<RiffChunkList> rgn_ListList;
		public readonly List<RiffChunkList> rgn2ListList;

		static RiffChunkListLrgn()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListRgn_.TYPE, typeof( RiffChunkListRgn_ ) );
			bodyTypeDictionary.Add( RiffChunkListRgn2.TYPE, typeof( RiffChunkListRgn2 ) );
		}

		public RiffChunkListLrgn( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			rgn_ListList = GetChunkListList( "LIST", RiffChunkListRgn_.TYPE );
			rgn2ListList = GetChunkListList( "LIST", RiffChunkListRgn2.TYPE );
		}
	}
}
