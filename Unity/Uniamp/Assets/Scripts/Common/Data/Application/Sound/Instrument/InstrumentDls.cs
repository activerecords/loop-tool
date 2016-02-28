using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Dls;

namespace Monoamp.Common.Data.Application.Sound
{
	public class InstrumentDls : InstrumentBase
	{
		public override SoundfontBase[] soundfontArray{ get; set; }

		public InstrumentDls( RiffChunkListLrgn aLrgnList, List<WaveformReaderPcm> aWaveformList, int aTuning )
			: base()
		{
			AddSoundfont( aLrgnList, aWaveformList, aTuning );
		}

		public void AddSoundfont( RiffChunkListLrgn aLrgnList, List<WaveformReaderPcm> aWaveformList, int aTuning )
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
