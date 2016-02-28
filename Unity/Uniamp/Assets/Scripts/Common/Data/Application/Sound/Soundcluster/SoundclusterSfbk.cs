using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Sfbk;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundclusterSfbk : ISoundcluster
	{
		public Dictionary<int, ABank> BankDictionary{ get; private set; }

		public SoundclusterSfbk( RiffChunkListSfbk aSfbkList )
		{
			BankDictionary = new Dictionary<int, ABank>();
			RiffChunkListSfbk lSfbkList = aSfbkList;
			
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
					List<InstrumentBase> instrumentList = CreateInstrumentList( pdtaList, sdtaBodyList, lInstrumentDictionary, lSfbkList.name );

					PhdrData[] phdrDataArray = pdtaList.phdrBody.phdrDataArray;

					for( int j = 0; j < phdrDataArray.Length - 1; j++ )
					{
						int bank = phdrDataArray[j].bank;
						int instrument = phdrDataArray[j].preset % 128;

						Logger.Normal( "Name:" + phdrDataArray[j].name + "" );
						Logger.Normal( "Bank/Preset:" + bank + "/" + instrument );

						if( bank == 128 )
						{
							Logger.Warning( "Change Bank:" + bank );
							bank = 0x7F00;
						}

						if( BankDictionary.ContainsKey( bank ) == false )
						{
							Logger.Warning( "Bank:" + bank );
							BankDictionary.Add( bank, new BankSfbk() );
						}

						BankDictionary[bank].AddInstrument( instrument, instrumentList[j] );
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.Error( "Expection at RIFF Read:" + aExpection.ToString() );
			}
		}

		public List<InstrumentBase> CreateInstrumentList( RiffChunkListPdta pdtaList, RiffChunkListSdta[] sdtaBodyList, Dictionary<string, Instrument> instrumentDictionary, string aName )
		{
			List<InstrumentBase> lInstrumentList = new List<InstrumentBase>();

			RiffInfoInst instChunk = pdtaList.instBody;
			RiffInfoIbag ibagChunk = pdtaList.ibagBody;

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
