using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkListPdta : RiffChunkList
	{
		public const string TYPE = "pdta";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public RiffChunkPhdr phdrBody;
		public RiffChunkPbag pbagBody;
		public RiffChunkPmod pmodBody;
		public RiffChunkPgen pgenBody;
		public RiffInfoInst instBody;
		public RiffInfoIbag ibagBody;
		public RiffInfoImod imodBody;
		public RiffInfoIgen igenBody;
		public RiffChunkShdr shdrBody;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> chunkTypeDictionary = new Dictionary<string, Type>();
					chunkTypeDictionary.Add( RiffChunkPhdr.ID, typeof( RiffChunkPhdr ) );
					chunkTypeDictionary.Add( RiffChunkPbag.ID, typeof( RiffChunkPbag ) );
					chunkTypeDictionary.Add( RiffChunkPmod.ID, typeof( RiffChunkPmod ) );
					chunkTypeDictionary.Add( RiffChunkPgen.ID, typeof( RiffChunkPgen ) );
					chunkTypeDictionary.Add( RiffInfoInst.ID, typeof( RiffInfoInst ) );
					chunkTypeDictionary.Add( RiffInfoIbag.ID, typeof( RiffInfoIbag ) );
					chunkTypeDictionary.Add( RiffInfoImod.ID, typeof( RiffInfoImod ) );
					chunkTypeDictionary.Add( RiffInfoIgen.ID, typeof( RiffInfoIgen ) );
					chunkTypeDictionary.Add( RiffChunkShdr.ID, typeof( RiffChunkShdr ) );
					
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
				}
		
		return chunkTypeDictionaryDictionary;
	}
}

		public RiffChunkListPdta( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			phdrBody = ( RiffChunkPhdr )GetChunk( RiffChunkPhdr.ID );
			pbagBody = ( RiffChunkPbag )GetChunk( RiffChunkPbag.ID );
			pmodBody = ( RiffChunkPmod )GetChunk( RiffChunkPmod.ID );
			pgenBody = ( RiffChunkPgen )GetChunk( RiffChunkPgen.ID );
			instBody = ( RiffInfoInst )GetChunk( RiffInfoInst.ID );
			ibagBody = ( RiffInfoIbag )GetChunk( RiffInfoIbag.ID );
			imodBody = ( RiffInfoImod )GetChunk( RiffInfoImod.ID );
			igenBody = ( RiffInfoIgen )GetChunk( RiffInfoIgen.ID );
			shdrBody = ( RiffChunkShdr )GetChunk( RiffChunkShdr.ID );
		}
	}
}
