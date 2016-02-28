using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkListLart : RiffChunkList
	{
		public const string TYPE = "lart";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public readonly RiffChunkArt1 art1Body;
		public readonly RiffChunkArt2 art2Body;

		static RiffChunkListLart()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffChunkArt1.ID, typeof( RiffChunkArt1 ) );
			chunkTypeDictionary.Add( RiffChunkArt2.ID, typeof( RiffChunkArt2 ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
		}

		public RiffChunkListLart( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			art1Body = ( RiffChunkArt1 )GetChunk( RiffChunkArt1.ID );
			art2Body = ( RiffChunkArt2 )GetChunk( RiffChunkArt2.ID );
		}
	}
}
