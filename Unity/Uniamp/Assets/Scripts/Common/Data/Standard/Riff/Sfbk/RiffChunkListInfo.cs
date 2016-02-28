using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkListInfo : RiffChunkList
	{
		public const string TYPE = "INFO";

		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		public static readonly Dictionary<string,Type> bodyTypeDictionary;

		public RiffInfoIart iartBody;
		public RiffInfoIcms icmsBody;
		public RiffInfoIcmt icmtBody;
		public RiffInfoIcop icopBody;
		public RiffInfoIcrd icrdBody;
		public RiffInfoIeng iengBody;
		public RiffInfoIfil ifilBody;
		public RiffInfoIgnr ignrBody;
		public RiffInfoIkey ikeyBody;
		public RiffInfoInam inamBody;
		public RiffInfoIprd iprdBody;
		public RiffInfoIsbj isbjBody;
		public RiffInfoIsft isftBody;
		public RiffInfoIsng isngBody;
		public RiffInfoIsrc isrcBody;
		public RiffInfoItch itchBody;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> chunkTypeDictionary = new Dictionary<string, Type>();
					chunkTypeDictionary.Add( RiffInfoIart.ID, typeof( RiffInfoIart ) );
					chunkTypeDictionary.Add( RiffInfoIcms.ID, typeof( RiffInfoIcms ) );
					chunkTypeDictionary.Add( RiffInfoIcmt.ID, typeof( RiffInfoIcmt ) );
					chunkTypeDictionary.Add( RiffInfoIcop.ID, typeof( RiffInfoIcop ) );
					chunkTypeDictionary.Add( RiffInfoIcrd.ID, typeof( RiffInfoIcrd ) );
					chunkTypeDictionary.Add( RiffInfoIeng.ID, typeof( RiffInfoIeng ) );
					chunkTypeDictionary.Add( RiffInfoIfil.ID, typeof( RiffInfoIfil ) );
					chunkTypeDictionary.Add( RiffInfoIgnr.ID, typeof( RiffInfoIgnr ) );
					chunkTypeDictionary.Add( RiffInfoIkey.ID, typeof( RiffInfoIkey ) );
					chunkTypeDictionary.Add( RiffInfoInam.ID, typeof( RiffInfoInam ) );
					chunkTypeDictionary.Add( RiffInfoIprd.ID, typeof( RiffInfoIprd ) );
					chunkTypeDictionary.Add( RiffInfoIsbj.ID, typeof( RiffInfoIsbj ) );
					chunkTypeDictionary.Add( RiffInfoIsft.ID, typeof( RiffInfoIsft ) );
					chunkTypeDictionary.Add( RiffInfoIsng.ID, typeof( RiffInfoIsng ) );
					chunkTypeDictionary.Add( RiffInfoIsrc.ID, typeof( RiffInfoIsrc ) );
					chunkTypeDictionary.Add( RiffInfoItch.ID, typeof( RiffInfoItch ) );
					
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
				}

				return chunkTypeDictionaryDictionary;
			}
		}

		public RiffChunkListInfo( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			iartBody = ( RiffInfoIart )GetChunk( RiffInfoIart.ID );
			icmsBody = ( RiffInfoIcms )GetChunk( RiffInfoIcms.ID );
			icmtBody = ( RiffInfoIcmt )GetChunk( RiffInfoIcmt.ID );
			icopBody = ( RiffInfoIcop )GetChunk( RiffInfoIcop.ID );
			icrdBody = ( RiffInfoIcrd )GetChunk( RiffInfoIcrd.ID );
			iengBody = ( RiffInfoIeng )GetChunk( RiffInfoIeng.ID );
			ifilBody = ( RiffInfoIfil )GetChunk( RiffInfoIfil.ID );
			ignrBody = ( RiffInfoIgnr )GetChunk( RiffInfoIgnr.ID );
			ikeyBody = ( RiffInfoIkey )GetChunk( RiffInfoIkey.ID );
			inamBody = ( RiffInfoInam )GetChunk( RiffInfoInam.ID );
			iprdBody = ( RiffInfoIprd )GetChunk( RiffInfoIprd.ID );
			isbjBody = ( RiffInfoIsbj )GetChunk( RiffInfoIsbj.ID );
			isftBody = ( RiffInfoIsft )GetChunk( RiffInfoIsft.ID );
			isngBody = ( RiffInfoIsng )GetChunk( RiffInfoIsng.ID );
			isrcBody = ( RiffInfoIsrc )GetChunk( RiffInfoIsrc.ID );
			itchBody = ( RiffInfoItch )GetChunk( RiffInfoItch.ID );
		}
	}
}
