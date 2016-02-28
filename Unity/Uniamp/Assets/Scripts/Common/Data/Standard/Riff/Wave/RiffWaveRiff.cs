using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.system.io;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Riff.Wave
{
	public class RiffWaveRiff : RiffChunkList
	{
		public const string ID = "RIFF";

		public readonly string name;
		
		private Dictionary<string, Dictionary<string, Type>> chunkTypeDictionaryDictionary;
		public override Dictionary<string, Dictionary<string, Type>> ChunkTypeDictionaryDictionary
		{
			get
			{
				if( chunkTypeDictionaryDictionary == null )
				{
					chunkTypeDictionaryDictionary = new Dictionary<string, Dictionary<string,Type>>();

					Dictionary<string, Type> lChunkTypeDictionaryWave = new Dictionary<string, Type>();
					lChunkTypeDictionaryWave.Add( RiffWaveList.ID, typeof( RiffWaveList ) );
					lChunkTypeDictionaryWave.Add( RiffWaveBext.ID, typeof( RiffWaveBext ) );
					lChunkTypeDictionaryWave.Add( RiffWaveCue_.ID, typeof( RiffWaveCue_ ) );
					lChunkTypeDictionaryWave.Add( RiffWaveData.ID, typeof( RiffWaveData ) );
					lChunkTypeDictionaryWave.Add( RiffWaveDisp.ID, typeof( RiffWaveDisp ) );
					lChunkTypeDictionaryWave.Add( RiffWaveFact.ID, typeof( RiffWaveFact ) );
					lChunkTypeDictionaryWave.Add( RiffWaveFile.ID, typeof( RiffWaveFile ) );
					lChunkTypeDictionaryWave.Add( RiffWaveFmt_.ID, typeof( RiffWaveFmt_ ) );
					lChunkTypeDictionaryWave.Add( RiffWaveInst.ID, typeof( RiffWaveInst ) );
					lChunkTypeDictionaryWave.Add( RiffWaveLabl.ID, typeof( RiffWaveLabl ) );
					lChunkTypeDictionaryWave.Add( RiffWaveLgwv.ID, typeof( RiffWaveLgwv ) );
					lChunkTypeDictionaryWave.Add( RiffWaveLtxt.ID, typeof( RiffWaveLtxt ) );
					lChunkTypeDictionaryWave.Add( RiffWaveNote.ID, typeof( RiffWaveNote ) );
					lChunkTypeDictionaryWave.Add( RiffWavePlst.ID, typeof( RiffWavePlst ) );
					lChunkTypeDictionaryWave.Add( RiffWaveSmpl.ID, typeof( RiffWaveSmpl ) );

					chunkTypeDictionaryDictionary.Add( "WAVE", lChunkTypeDictionaryWave );
				}
				
				return chunkTypeDictionaryDictionary;
			}
		}
		
		public RiffWaveRiff( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{
			
		}

		public RiffWaveRiff( FileStream aStream )
			: this( new ByteArrayLittle( aStream ) )
		{

		}

		public RiffWaveRiff( AByteArray aByteArray )
			: base( aByteArray.ReadString( 4 ), aByteArray.ReadUInt32(), aByteArray, null )
		{
			name = aByteArray.GetName();
		}
		
		public RiffWaveRiff( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			
		}

		public void WriteByteArray( AByteArray aByteArray )
		{
			if( name != null && name != "" )
			{
				using ( FileStream u = new FileStream( name, FileMode.Open, FileAccess.Read ) )
				{
					AByteArray lByteArray = new ByteArrayLittle( u );

					WriteByteArray( lByteArray, aByteArray );
				}
			}
		}
	}
}
