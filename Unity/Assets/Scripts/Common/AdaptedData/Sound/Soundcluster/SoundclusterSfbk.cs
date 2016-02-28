using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Sfbk;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public class SoundclusterSfbk : SoundclusterBase
	{
		public SoundclusterSfbk( RiffFile aRiffFile )
			: base()
		{
			RiffChunkListSfbk lSfbkList = ( RiffChunkListSfbk )aRiffFile.riffChunkList;
			
			try
			{
				List<RiffChunkList> lSdtaListList = lSfbkList.sdtaListList;

				RiffChunkListSdta[] sdtaBodyList = new RiffChunkListSdta[lSdtaListList.Count];

				for( int i = 0; i < lSdtaListList.Count; i++ )
				{
					sdtaBodyList[i] = ( RiffChunkListSdta )lSdtaListList[i];
				}

				List<RiffChunkList> pdtaListList = lSfbkList.pdtaListList;

				for( int i = 0; i < pdtaListList.Count; i++ )
				{
					RiffChunkListPdta pdtaList = ( RiffChunkListPdta )pdtaListList[i];

					Dictionary<string, Instrument> lInstrumentDictionary = InstrumentSfbk.CreateInstrumentList( pdtaList );
					List<InstrumentBase> instrumentList = CreateInstrumentList( pdtaList, sdtaBodyList, lInstrumentDictionary, aRiffFile.name );

					PhdrData[] phdrDataArray = pdtaList.phdrBody.phdrDataArray;

					for( int j = 0; j < phdrDataArray.Length - 1; j++ )
					{
						int bank = phdrDataArray[j].bank;
						int instrument = phdrDataArray[j].preset % 128;

						Logger.LogNormal( "Name:" + phdrDataArray[j].name + "" );
						Logger.LogNormal( "Bank/Preset:" + bank + "/" + instrument );

						if( bank == 128 )
						{
							Logger.LogWarning( "Change Bank:" + bank );
							bank = 0x7F00;
						}

						if( bankDictionary.ContainsKey( bank ) == false )
						{
							Logger.LogWarning( "Bank:" + bank );
							bankDictionary.Add( bank, new BankSfbk() );
						}

						bankDictionary[bank].AddInstrument( instrument, instrumentList[j] );
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.LogError( "Expection at RIFF Read:" + aExpection.ToString() );
			}
		}

		public List<InstrumentBase> CreateInstrumentList( RiffChunkListPdta pdtaList, RiffChunkListSdta[] sdtaBodyList, Dictionary<string, Instrument> instrumentDictionary, string aName )
		{
			List<InstrumentBase> lInstrumentList = new List<InstrumentBase>();

			RiffChunkInst instChunk = pdtaList.instBody;
			RiffChunkIbag ibagChunk = pdtaList.ibagBody;

			for( int i = 0; i < instChunk.instDataArray.Length; i++ )
			{
				IbagData ibagData0 = ibagChunk.dataArray[instChunk.instDataArray[i].bagNdx];

				IbagData ibagData1 = null;

				if( i < instChunk.instDataArray.Length - 1 )
				{
					ibagData1 = ibagChunk.dataArray[instChunk.instDataArray[i + 1].bagNdx];
				}

				lInstrumentList.Add( new InstrumentSfbk( pdtaList, sdtaBodyList, instChunk.instDataArray[i], ibagData0, ibagData1, aName ) );
			}

			return lInstrumentList;
		}
	}
}
