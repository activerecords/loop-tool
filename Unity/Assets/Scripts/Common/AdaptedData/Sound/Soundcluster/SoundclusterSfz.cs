using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Blst;
using Curan.Common.FormalizedData.File.Bnk;
using Curan.Common.FormalizedData.File.Sfz;
using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public class SoundclusterSfz : SoundclusterBase
	{
		public SoundclusterSfz( BlstFile aBlstFile )
			: base()
		{
			try
			{
				for( int i = 0; i < aBlstFile.pathBankList.Count; i++ )
				{
					UnityEngine.Debug.Log( aBlstFile.pathBankList[i] );

					FileStream lFileStream = new FileStream( aBlstFile.pathBankList[i], FileMode.Open, FileAccess.Read );
					BnkFile lBnkFile = new BnkFile( lFileStream );
					BankSfz lSfzBank = new BankSfz( lBnkFile, aBlstFile.pathWaveform );

					if( bankDictionary.ContainsKey( i ) == false )
					{
						bankDictionary.Add( i, lSfzBank );
					}

					if( i == 1 )
					{
						if( bankDictionary.ContainsKey( 0x7F00 ) == false )
						{
							bankDictionary.Add( 0x7F00, lSfzBank );
						}
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.LogError( "Expection:" + aExpection.ToString() );
			}
		}
	}
}
