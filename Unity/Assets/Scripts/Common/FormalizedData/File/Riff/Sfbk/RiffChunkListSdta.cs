using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkListSdta : RiffChunkList
	{
		public const string TYPE = "sdta";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public readonly RiffChunkSmpl smplBody;

		static RiffChunkListSdta()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffChunkSmpl.ID, typeof( RiffChunkSmpl ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
		}

		public RiffChunkListSdta( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			smplBody = ( RiffChunkSmpl )GetChunk( RiffChunkSmpl.ID );
		}
	}
}
