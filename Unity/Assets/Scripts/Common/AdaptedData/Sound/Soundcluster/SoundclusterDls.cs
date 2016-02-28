using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Dls;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public class SoundclusterDls : SoundclusterBase
	{
		public SoundclusterDls( RiffFile aRiffFile )
			: base()
		{
			RiffChunkListDls_ dls_Riff = ( RiffChunkListDls_ )aRiffFile.riffChunkList;

			List<WaveformBase> lWaveformList = new List<WaveformBase>();

			try
			{
				RiffChunkListWvpl wvplList = ( RiffChunkListWvpl )dls_Riff.wvplListList;

				for( int i = 0; i < wvplList.waveListList.Count; i++ )
				{
					RiffChunkListWave lWaveList = ( RiffChunkListWave )wvplList.waveListList[i];

					lWaveformList.Add( new WaveformDls( lWaveList, aRiffFile.name ) );
				}

				RiffChunkListLins linsList = ( RiffChunkListLins )dls_Riff.linsListList;

				Logger.LogWarning( "linsList.ins_ListList.Count:" + linsList.ins_ListList.Count );

				for( int i = 0; i < linsList.ins_ListList.Count; i++ )
				{
					RiffChunkListIns_ ins_List = ( RiffChunkListIns_ )linsList.ins_ListList[i];

					RiffChunkInsh inshChunk = ( RiffChunkInsh )ins_List.inshChunk;

					int bank = ( int )inshChunk.midiLocal.bank;

					if( ( uint )bank == 0x80000000 )
					{
						Logger.LogWarning( "Change Bank:" + bank.ToString( "X8" ) );

						bank = 0x7F00;
					}

					if( bankDictionary.ContainsKey( bank ) == false )
					{
						Logger.LogWarning( "Bank:" + bank.ToString( "X8" ) );
						bankDictionary.Add( bank, new BankDls() );
					}

					BankDls lDlsBank = ( BankDls )bankDictionary[bank];
					lDlsBank.AddInstrument( ins_List, lWaveformList );
				}
			}
			catch( Exception aExpection )
			{
				Logger.LogError( "Expection at RIFF Read:" + aExpection.ToString() );
			}
		}
	}
}
