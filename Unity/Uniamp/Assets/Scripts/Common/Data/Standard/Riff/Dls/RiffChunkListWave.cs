using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListWave : RiffDls_List
	{
		public const string TYPE = "wave";

		/*
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}
		*/

		public readonly RiffDls_Dlid dlidChunk;
		public readonly RiffDls_Fmt_ fmt_Chunk;
		public readonly RiffDls_Data dataChunk;
		public readonly RiffDls_Wsmp wsmpChunk;

		/*
		static RiffChunkListWave()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffDls_Dlid.ID, typeof( RiffDls_Dlid ) );
			chunkTypeDictionary.Add( RiffDls_Fmt_.ID, typeof( RiffDls_Fmt_ ) );
			chunkTypeDictionary.Add( RiffDls_Data.ID, typeof( RiffDls_Data ) );
			chunkTypeDictionary.Add( RiffDls_Wsmp.ID, typeof( RiffDls_Wsmp ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListLrgn.TYPE, typeof( RiffChunkListLrgn ) );
			bodyTypeDictionary.Add( RiffDls_List.TYPE, typeof( RiffDls_List ) );
		}*/

		public RiffChunkListWave( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			dlidChunk = ( RiffDls_Dlid )GetChunk( RiffDls_Dlid.ID );
			fmt_Chunk = ( RiffDls_Fmt_ )GetChunk( RiffDls_Fmt_.ID );
			dataChunk = ( RiffDls_Data )GetChunk( RiffDls_Data.ID );
			wsmpChunk = ( RiffDls_Wsmp )GetChunk( RiffDls_Wsmp.ID );
		}
	}
}
