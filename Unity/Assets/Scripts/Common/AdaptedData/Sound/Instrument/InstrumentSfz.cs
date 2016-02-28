using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Sfz;

namespace Curan.Common.AdaptedData
{
	public class InstrumentSfz : InstrumentBase
	{
		public InstrumentSfz( SfzFile aSfzFile, Dictionary<string, string> aPathWaveformDictionary )
			: base()
		{
			List<SoundfontBase> lSoundfontList = new List<SoundfontBase>();

			for( int i = 0; i < aSfzFile.sfzRegionList.Count; i++ )
			{
				SfzRegion lSfzData = aSfzFile.sfzRegionList[i];

				lSoundfontList.Add( new SoundfontSfz( lSfzData, aPathWaveformDictionary) );
			}

			AddSoundfont( lSoundfontList );
		}
	}
}
