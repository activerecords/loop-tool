using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListWvpl : RiffDls_List
	{
		public const string TYPE = "wvpl";

		/*
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}*/

		public readonly List<RiffChunkList> waveListList;

		/*
		static RiffChunkListWvpl()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListWave.TYPE, typeof( RiffChunkListWave ) );
		}*/

		public RiffChunkListWvpl( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			waveListList = GetChunkListList( "LIST", RiffChunkListWave.TYPE );
		}
	}
}
