using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Dls;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundclusterDls : ISoundcluster
	{
		public Dictionary<int, ABank> BankDictionary{ get; private set; }

		public SoundclusterDls( RiffDls_Riff aDls_Riff )
		{
			BankDictionary = new Dictionary<int, ABank>();

			RiffDls_Riff dls_Riff = aDls_Riff;

			List<WaveformReaderPcm> lWaveformList = new List<WaveformReaderPcm>();

			try
			{
				Logger.Error( dls_Riff.GetChunk( "LIST" ).GetType().ToString() );
				RiffChunkList lRiffChunkList = ( RiffChunkList )dls_Riff.GetChunk( "LIST" );
				List<RiffChunkList> wvplListList = lRiffChunkList.GetChunkListList( "LIST", RiffChunkListWvpl.TYPE );
				//RiffChunkListWvpl wvplList = ( RiffChunkListWvpl )dls_Riff.wvplListList;

				List<RiffChunkList> waveListList = wvplListList[0].GetChunkListList( "LIST", RiffChunkListWave.TYPE );

				for( int i = 0; i < waveListList.Count; i++ )
				{
					RiffChunkListWave lWaveList = ( RiffChunkListWave )waveListList[i];

					lWaveformList.Add( new WaveformReaderPcm( lWaveList, dls_Riff.name ) );
				}

				RiffChunkListLins linsList = ( RiffChunkListLins )dls_Riff.linsListList;

				Logger.Warning( "linsList.ins_ListList.Count:" + linsList.ins_ListList.Count );

				for( int i = 0; i < linsList.ins_ListList.Count; i++ )
				{
					RiffChunkListIns_ ins_List = ( RiffChunkListIns_ )linsList.ins_ListList[i];

					RiffDls_Insh inshChunk = ( RiffDls_Insh )ins_List.inshChunk;

					int bank = ( int )inshChunk.midiLocal.bank;

					if( ( uint )bank == 0x80000000 )
					{
						Logger.Warning( "Change Bank:" + bank.ToString( "X8" ) );

						bank = 0x7F00;
					}

					if( BankDictionary.ContainsKey( bank ) == false )
					{
						Logger.Warning( "Bank:" + bank.ToString( "X8" ) );
						BankDictionary.Add( bank, new BankDls() );
					}

					BankDls lDlsBank = ( BankDls )BankDictionary[bank];
					lDlsBank.AddInstrument( ins_List, lWaveformList );
				}
			}
			catch( Exception aExpection )
			{
				Logger.Error( "Expection at RIFF Read:" + aExpection.ToString() );
			}
		}
	}
}
