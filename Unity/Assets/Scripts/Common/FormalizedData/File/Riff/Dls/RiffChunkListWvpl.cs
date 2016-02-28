using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkListWvpl : RiffChunkList
	{
		public const string TYPE = "wvpl";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public readonly List<RiffChunkList> waveListList;

		static RiffChunkListWvpl()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListWave.TYPE, typeof( RiffChunkListWave ) );
		}

		public RiffChunkListWvpl( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			waveListList = GetChunkListList( "LIST", RiffChunkListWave.TYPE );
		}
	}
}
