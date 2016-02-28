using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Bnk;
using Curan.Common.FormalizedData.File.Sfz;
using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public class BankSfz : BankBase
	{
		public BankSfz( BnkFile aBnkFile, string aPathWaveform )
			: base()
		{
			Dictionary<string, string> lPathWaveformDictionary = GetWaveformPathDictionary( aPathWaveform );

			for( int i = 0; i < aBnkFile.pathSfzList.Count; i++ )
			{
				FileStream lFileStream = new FileStream( aBnkFile.pathSfzList[i], FileMode.Open, FileAccess.Read );
				SfzFile lSfzFile = new SfzFile( lFileStream );

				instrumentArray[i] = new InstrumentSfz( lSfzFile, lPathWaveformDictionary );
			}
		}

		private Dictionary<string, string> GetWaveformPathDictionary( string aPathWaveform )
		{
			string[] lPathWaveformArray = null;

			try
			{
				Logger.LogNormal( aPathWaveform );
				lPathWaveformArray = Directory.GetFiles( aPathWaveform, "*.wav", SearchOption.AllDirectories );
			}
			catch( Exception aExpection )
			{
				//Logger.LogWarning( "length:" + lPathWaveformArray.Length );
				Logger.LogError( "ex:" + aExpection.ToString() );
			}

			Dictionary<string, string> lPathWaveformDictionary = new Dictionary<string, string>();

			for( int j = 0; j < lPathWaveformArray.Length; j++ )
			{
				if( lPathWaveformDictionary.ContainsKey( Path.GetFileName( lPathWaveformArray[j] ) ) == false )
				{
					lPathWaveformDictionary.Add( Path.GetFileName( lPathWaveformArray[j] ), lPathWaveformArray[j] );
				}
				else
				{
					Logger.LogWarning( "This waveform is found lather than 1:" + lPathWaveformArray[j] );
				}
			}

			return lPathWaveformDictionary;
		}
	}
}
