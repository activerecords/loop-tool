using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffChunkListLart : RiffChunkList
	{
		public const string TYPE = "lart";
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				return chunkTypeDictionaryDictionary;
			}
		}
		public readonly RiffDls_Art1 art1Body;
		public readonly RiffDls_Art2 art2Body;

		/*
		static RiffChunkListLart()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffDls_Art1.ID, typeof( RiffDls_Art1 ) );
			chunkTypeDictionary.Add( RiffDls_Art2.ID, typeof( RiffDls_Art2 ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
		}*/

		public RiffChunkListLart( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			art1Body = ( RiffDls_Art1 )GetChunk( RiffDls_Art1.ID );
			art2Body = ( RiffDls_Art2 )GetChunk( RiffDls_Art2.ID );
		}
	}
}
