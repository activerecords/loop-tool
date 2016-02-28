using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkListMovi : RiffChunkList
	{
		public const string TYPE = "movi";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public RiffChunkXxdb chunkXxdb;

		static RiffChunkListMovi()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffChunkXxdb.ID, typeof( RiffChunkXxdb ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
		}

		public RiffChunkListMovi( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			chunkXxdb = ( RiffChunkXxdb )GetChunk( RiffChunkXxdb.ID );
		}
	}
}
