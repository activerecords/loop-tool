using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

using Curan.Common.AdaptedData;
using Curan.Common.system.io;
using Curan.Common.FileLoader.Waveform;
using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Wave;
using Curan.Common.FilePool;
using Curan.Common.Struct;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.LoopTool
{
	public static class LoopSearchExecutor
	{
		public static bool IsCutLast{ get; set; }

		static LoopSearchExecutor()
		{
			IsCutLast = false;
		}

		public static void Execute( FileInfo aFileInput, string aFilePathOutput, List<double> aProgressList, int aIndex )
		{
			RiffFile lRiffFile = ( RiffFile )PoolCollection.poolWav.Get( aFileInput.FullName );
			RiffChunkListWave lRiffChunkListWave = ( RiffChunkListWave )lRiffFile.riffChunkList;

			WaveformBase waveform = LoaderWaveform.Load( aFileInput.FullName );

			SByte[] lSampleArray = new SByte[waveform.format.samples];

			for( int i = 0; i < waveform.format.samples; i++ )
			{
				lSampleArray[i] = ( SByte )( waveform.data.GetSampleData( 0, i ) >> 8 );
			}

			List<LoopInformation> lLoopList = null;

			try
			{
				lLoopList = LoopSearchTool.Execute( lSampleArray, aProgressList, aIndex );
			}
			catch( Exception aExpection )
			{
				UnityEngine.Debug.Log( aExpection.ToString() + ":LoopTool Exception" );
			}

			//for( int i = 0; i < lLoopList.Count; i++ )
			if ( lLoopList.Count >= 1 )
			{
				//lRiffChunkListWave.AddCuePoint( ( int )lLoopList[i].start.sample, ( int )lLoopList[i].end.sample );
				//lRiffChunkListWave.AddSampleLoop( ( int )lLoopList[i].start.sample, ( int )lLoopList[i].end.sample );
				lRiffChunkListWave.AddCuePoint( ( int )lLoopList[0].start.sample, ( int )lLoopList[0].end.sample );
				lRiffChunkListWave.AddSampleLoop( ( int )lLoopList[0].start.sample, ( int )lLoopList[0].end.sample );
			}

			Byte[] lDataArrayRead = null;
			
			using ( FileStream u = new FileStream( lRiffFile.name, FileMode.Open, FileAccess.Read ) )
			{
				ByteArray l = new ByteArrayLittle( u );
				
				int bytePosition = ( int )lRiffChunkListWave.dataChunk.position;

				l.SetPosition( bytePosition );
				
				lDataArrayRead = l.ReadBytes( lRiffChunkListWave.dataChunk.size );
			}

			Byte[] lDataArrayWrite = lDataArrayRead;

			if( IsCutLast == true )
			{
				lDataArrayWrite = new Byte[( ( int )lLoopList[0].end.sample + 1 ) * 4];

				for( int i = 0; i < ( lLoopList[0].end.sample + 1 ) * 4; i++ )
				{
					lDataArrayWrite[i] = lDataArrayRead[i];
				}
			}
			
			for( int i = 0; i < 64; i++ )
			{
				Logger.LogDebug( i.ToString() + ":" + lDataArrayWrite[i] );
			}

			lRiffChunkListWave.SetDataArray( lDataArrayWrite );

			MemoryStream lMemoryStreamWrite = new MemoryStream( ( int )lRiffChunkListWave.size + 8 );
			ByteArrayLittle lByteArray = new ByteArrayLittle( lMemoryStreamWrite );

			//lByteArrayRead.Open();
			lRiffFile.WriteByteArray( null, lByteArray );
			//lByteArrayRead.Close();

			using( FileStream u = new FileStream( aFilePathOutput, FileMode.Create, FileAccess.Write ) )
			{
				u.Write( lMemoryStreamWrite.GetBuffer(), 0, ( int )lMemoryStreamWrite.Length );
			}
		}
	}
}
