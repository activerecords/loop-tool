using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Blst;
using Monoamp.Common.Data.Standard.Bnk;
using Monoamp.Common.Data.Standard.Sfz;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundclusterSfz : ISoundcluster
	{
		public Dictionary<int, ABank> BankDictionary{ get; private set; }

		public SoundclusterSfz( BlstFile aBlstFile )
		{
			BankDictionary = new Dictionary<int, ABank>();

			try
			{
				for( int i = 0; i < aBlstFile.pathBankList.Count; i++ )
				{
					Logger.Debug( aBlstFile.pathBankList[i] );

					using( FileStream uFileStream = new FileStream( aBlstFile.pathBankList[i], FileMode.Open, FileAccess.Read ) )
					{
						BankSfz lSfzBank = new BankSfz( new BnkFile( uFileStream ) );

						if( BankDictionary.ContainsKey( i ) == false )
						{
							BankDictionary.Add( i, lSfzBank );
						}

						if( i == 1 )
						{
							if( BankDictionary.ContainsKey( 0x7F00 ) == false )
							{
								BankDictionary.Add( 0x7F00, lSfzBank );
							}
						}
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.Error( "Expection:" + aExpection.ToString() );
			}
		}
	}
}
