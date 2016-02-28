using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkListSdta : RiffChunkList
	{
		public const string TYPE = "sdta";

		public readonly RiffChunkSmpl smplBody;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> chunkTypeDictionary = new Dictionary<string, Type>();
					chunkTypeDictionary.Add( RiffChunkSmpl.ID, typeof( RiffChunkSmpl ) );
					
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
				}
				
				return chunkTypeDictionaryDictionary;
			}
		}

		public RiffChunkListSdta( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			smplBody = ( RiffChunkSmpl )GetChunk( RiffChunkSmpl.ID );
		}
	}
}
