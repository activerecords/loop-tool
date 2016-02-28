using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_List : RiffChunkList
	{
		public const string ID = "LIST";

		public readonly RiffInfoIfil ifilBody;
		public readonly RiffInfoInam inamBody;
		public readonly RiffInfoIsng isngBody;
		public readonly RiffInfoIprd iprdBody;
		public readonly RiffInfoIeng iengBody;
		public readonly RiffInfoIsbj isbjBody;
		public readonly RiffInfoIsft isftBody;
		public readonly RiffInfoIcrd icrdBody;
		public readonly RiffInfoIcmt icmtBody;
		public readonly RiffInfoIcop icopBody;

		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> lChunkTypeDictionaryInfo = new Dictionary<string, Type>();
					lChunkTypeDictionaryInfo.Add( RiffInfoIfil.ID, typeof( RiffInfoIfil ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoInam.ID, typeof( RiffInfoInam ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsng.ID, typeof( RiffInfoIsng ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIprd.ID, typeof( RiffInfoIprd ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIeng.ID, typeof( RiffInfoIeng ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsbj.ID, typeof( RiffInfoIsbj ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsft.ID, typeof( RiffInfoIsft ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcrd.ID, typeof( RiffInfoIcrd ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcmt.ID, typeof( RiffInfoIcmt ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcop.ID, typeof( RiffInfoIcop ) );

					Dictionary<string, Type> lChunkTypeDictionaryIns_ = new Dictionary<string, Type>();
					lChunkTypeDictionaryIns_.Add( RiffDls_Dlid.ID, typeof( RiffDls_Dlid ) );
					lChunkTypeDictionaryIns_.Add( RiffDls_Insh.ID, typeof( RiffDls_Insh ) );
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryLar2 = new Dictionary<string, Type>();
					lChunkTypeDictionaryLar2.Add( RiffDls_Art1.ID, typeof( RiffDls_Art1 ) );
					lChunkTypeDictionaryLar2.Add( RiffDls_Art2.ID, typeof( RiffDls_Art2 ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryLart = new Dictionary<string, Type>();
					lChunkTypeDictionaryLart.Add( RiffDls_Art1.ID, typeof( RiffDls_Art1 ) );
					lChunkTypeDictionaryLart.Add( RiffDls_Art2.ID, typeof( RiffDls_Art2 ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryLins = new Dictionary<string, Type>();
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryLrgn = new Dictionary<string, Type>();
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryRgn_ = new Dictionary<string, Type>();
					lChunkTypeDictionaryRgn_.Add( RiffDls_Rgnh.ID, typeof( RiffDls_Rgnh ) );
					lChunkTypeDictionaryRgn_.Add( RiffDls_Wsmp.ID, typeof( RiffDls_Wsmp ) );
					lChunkTypeDictionaryRgn_.Add( RiffDls_Wlnk.ID, typeof( RiffDls_Wlnk ) );
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );

					Dictionary<string, Type> lChunkTypeDictionaryRgn2 = new Dictionary<string, Type>();
					lChunkTypeDictionaryRgn2.Add( RiffDls_Rgnh.ID, typeof( RiffDls_Rgnh ) );
					lChunkTypeDictionaryRgn2.Add( RiffDls_Wsmp.ID, typeof( RiffDls_Wsmp ) );
					lChunkTypeDictionaryRgn2.Add( RiffDls_Wlnk.ID, typeof( RiffDls_Wlnk ) );
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryWave = new Dictionary<string, Type>();
					lChunkTypeDictionaryWave.Add( RiffDls_Dlid.ID, typeof( RiffDls_Dlid ) );
					lChunkTypeDictionaryWave.Add( RiffDls_Fmt_.ID, typeof( RiffDls_Fmt_ ) );
					lChunkTypeDictionaryWave.Add( RiffDls_Data.ID, typeof( RiffDls_Data ) );
					lChunkTypeDictionaryWave.Add( RiffDls_Wsmp.ID, typeof( RiffDls_Wsmp ) );
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );
					
					Dictionary<string, Type> lChunkTypeDictionaryWvpl = new Dictionary<string, Type>();
					lChunkTypeDictionaryIns_.Add( RiffDls_List.ID, typeof( RiffDls_List ) );

					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
					chunkTypeDictionaryDictionary.Add( "INFO", lChunkTypeDictionaryInfo );
					chunkTypeDictionaryDictionary.Add( "ins ", lChunkTypeDictionaryIns_ );
					chunkTypeDictionaryDictionary.Add( "lar2", lChunkTypeDictionaryLar2 );
					chunkTypeDictionaryDictionary.Add( "lart", lChunkTypeDictionaryLart );
					chunkTypeDictionaryDictionary.Add( "lins", lChunkTypeDictionaryLins );
					chunkTypeDictionaryDictionary.Add( "lrgn", lChunkTypeDictionaryLrgn );
					chunkTypeDictionaryDictionary.Add( "rgn ", lChunkTypeDictionaryRgn_ );
					chunkTypeDictionaryDictionary.Add( "rgn2", lChunkTypeDictionaryRgn2 );
					chunkTypeDictionaryDictionary.Add( "wave", lChunkTypeDictionaryLins );
					chunkTypeDictionaryDictionary.Add( "lrgn", lChunkTypeDictionaryWave );
				}

				return chunkTypeDictionaryDictionary;
			}
		}

		public RiffDls_List( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			ifilBody = ( RiffInfoIfil )GetChunk( RiffInfoIfil.ID );
			inamBody = ( RiffInfoInam )GetChunk( RiffInfoInam.ID );
			isngBody = ( RiffInfoIsng )GetChunk( RiffInfoIsng.ID );
			iprdBody = ( RiffInfoIprd )GetChunk( RiffInfoIprd.ID );
			iengBody = ( RiffInfoIeng )GetChunk( RiffInfoIeng.ID );
			isbjBody = ( RiffInfoIsbj )GetChunk( RiffInfoIsbj.ID );
			isftBody = ( RiffInfoIsft )GetChunk( RiffInfoIsft.ID );
			icrdBody = ( RiffInfoIcrd )GetChunk( RiffInfoIcrd.ID );
			icmtBody = ( RiffInfoIcmt )GetChunk( RiffInfoIcmt.ID );
			icopBody = ( RiffInfoIcop )GetChunk( RiffInfoIcop.ID );
		}
	}
}
