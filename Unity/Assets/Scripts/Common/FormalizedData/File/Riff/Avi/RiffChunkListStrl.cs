using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkListStrl : RiffChunkList
	{
		public const string TYPE = "strl";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public RiffChunkStrf chunkStrf;
		public RiffChunkStrh chunkStrh;

		static RiffChunkListStrl()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffChunkStrh.ID, typeof( RiffChunkStrh ) );
			chunkTypeDictionary.Add( RiffChunkStrf.ID, typeof( RiffChunkStrf ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListInfo.TYPE, typeof( RiffChunkListInfo ) );
		}

		public RiffChunkListStrl( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			chunkStrf = ( RiffChunkStrf )GetChunk( RiffChunkStrf.ID );
			chunkStrh = ( RiffChunkStrh )GetChunk( RiffChunkStrh.ID );
		}
	}
}
