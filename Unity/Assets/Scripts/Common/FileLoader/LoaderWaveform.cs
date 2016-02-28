using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.AdaptedData;
using Curan.Common.FilePool;
using Curan.Common.FormalizedData.File.Form;
using Curan.Common.FormalizedData.File.Riff;

namespace Curan.Common.FileLoader.Waveform
{
	public class LoaderWaveform : LoaderBase
	{
		private static Dictionary<string, Constructor> constructorDictionary;

		static LoaderWaveform()
		{
			constructorDictionary = new Dictionary<string, Constructor>();

			constructorDictionary.Add( ".aif", ( string aPathFile ) => { return new WaveformAiff( ( FormFile )PoolCollection.poolAif.Get( aPathFile ) ); } );
			constructorDictionary.Add( ".wav", ( string aPathFile ) => { return new WaveformWave( ( RiffFile )PoolCollection.poolWav.Get( aPathFile ) ); } );
		}

		public static WaveformBase Load( string aPathFile )
		{
			return ( WaveformBase )Load( constructorDictionary, aPathFile );
		}
	}
}
