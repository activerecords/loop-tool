using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.system.io;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Riff.Wave
{
	public class RiffWaveList : RiffChunkList
	{
		public const string ID = "LIST";
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					Dictionary<string, Type> lChunkTypeDictionaryAdtl = new Dictionary<string, Type>();
					lChunkTypeDictionaryAdtl.Add( RiffWaveLabl.ID, typeof( RiffWaveLabl ) );
					lChunkTypeDictionaryAdtl.Add( RiffWaveNote.ID, typeof( RiffWaveNote ) );
					lChunkTypeDictionaryAdtl.Add( RiffWaveLtxt.ID, typeof( RiffWaveLtxt ) );
					lChunkTypeDictionaryAdtl.Add( RiffWaveFile.ID, typeof( RiffWaveFile ) );

					Dictionary<string, Type>  lChunkTypeDictionaryInfo = new Dictionary<string, Type>();
					lChunkTypeDictionaryInfo.Add( RiffInfoIart.ID, typeof( RiffInfoIart ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcms.ID, typeof( RiffInfoIcms ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcmt.ID, typeof( RiffInfoIcmt ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcop.ID, typeof( RiffInfoIcop ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIcrd.ID, typeof( RiffInfoIcrd ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIeng.ID, typeof( RiffInfoIeng ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIgnr.ID, typeof( RiffInfoIgnr ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIkey.ID, typeof( RiffInfoIkey ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoInam.ID, typeof( RiffInfoInam ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIprd.ID, typeof( RiffInfoIprd ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsbj.ID, typeof( RiffInfoIsbj ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsft.ID, typeof( RiffInfoIsft ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoIsrc.ID, typeof( RiffInfoIsrc ) );
					lChunkTypeDictionaryInfo.Add( RiffInfoItch.ID, typeof( RiffInfoItch ) );
					
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();
					chunkTypeDictionaryDictionary.Add( "adtl", lChunkTypeDictionaryAdtl );
					chunkTypeDictionaryDictionary.Add( "INFO", lChunkTypeDictionaryInfo );
				}

				return chunkTypeDictionaryDictionary;
			}
		}
		
		public RiffWaveList( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{

		}
	}
}
