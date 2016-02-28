using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Dls;

namespace Curan.Common.AdaptedData
{
	public class InstrumentDls : InstrumentBase
	{
		public InstrumentDls( RiffChunkListLrgn aLrgnList, List<WaveformBase> aWaveformList, int aTuning )
			: base()
		{
			AddSoundfont( aLrgnList, aWaveformList, aTuning );
		}

		public void AddSoundfont( RiffChunkListLrgn aLrgnList, List<WaveformBase> aWaveformList, int aTuning )
		{
			for( int l = 0; l < aLrgnList.rgn_ListList.Count; l++ ) {
				RiffChunkListRgn_ rgn_List = ( RiffChunkListRgn_ )aLrgnList.rgn_ListList[l];

				AddSoundfont( new SoundfontDls( rgn_List, aWaveformList, aTuning ) );
			}

			for( int l = 0; l < aLrgnList.rgn2ListList.Count; l++ )
			{
				RiffChunkListRgn2 rgn2List = ( RiffChunkListRgn2 )aLrgnList.rgn2ListList[l];

				AddSoundfont( new SoundfontDls( rgn2List, aWaveformList, aTuning ) );
			}
		}
	}
}
