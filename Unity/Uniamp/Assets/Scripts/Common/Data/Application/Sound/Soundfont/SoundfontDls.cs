using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Dls;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundfontDls : SoundfontBase
	{
		public Ampeg ampeg{ get; private set; }
		public Soundinfo soundinfo{ get; private set; }
		public WaveformReaderPcm waveform{ get; private set; }

		public SoundfontDls( RiffChunkListRgn_ rgn_List, List<WaveformReaderPcm> waveformList, int tuning )
		{
			RiffDls_Rgnh rgnhChunk = rgn_List.rgnhBody;
			RiffDls_Wsmp wsmpChunk = rgn_List.wsmpBody;
			RiffDls_Wlnk wlnkChunk = rgn_List.wlnkBody;

			byte lokey = ( byte )rgnhChunk.rangeKey.low;
			byte hikey = ( byte )rgnhChunk.rangeKey.high;
			bool loopMode = wsmpChunk.GetLoopType() != 0 ? true : false;
			int loopStart = ( int )wsmpChunk.GetLoopStart();
			int loopEnd = ( int )( wsmpChunk.GetLoopStart() + wsmpChunk.GetLoopLength() );
			int tune = wsmpChunk.fineTune;
			int pitchKeyCenter = wsmpChunk.unityNote;
			float volume = 0.0f;

			soundinfo = new Soundinfo( lokey, hikey, loopMode, loopStart, loopEnd, 0, 0x7FFFFFFF, tune, pitchKeyCenter, 0, 0, volume );
			ampeg = new Ampeg( 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 1.0d, 0.25d );
			waveform = waveformList[( int )wlnkChunk.tableIndex];
		}

		public SoundfontDls( RiffChunkListRgn2 rgn2List, List<WaveformReaderPcm> waveformList, int tuning )
		{
			RiffDls_Rgnh rgnhChunk = rgn2List.rgnhBody;
			RiffDls_Wsmp wsmpChunk = rgn2List.wsmpBody;
			RiffDls_Wlnk wlnkChunk = rgn2List.wlnkBody;

			byte lokey = ( byte )rgnhChunk.rangeKey.low;
			byte hikey = ( byte )rgnhChunk.rangeKey.high;
			bool loopMode = wsmpChunk.GetLoopType() != 0 ? true : false;
			int loopStart = ( int )wsmpChunk.GetLoopStart();
			int loopEnd = ( int )( wsmpChunk.GetLoopStart() + wsmpChunk.GetLoopLength() );
			int tune = wsmpChunk.fineTune;
			int pitchKeyCenter = wsmpChunk.unityNote;
			float volume = 0.0f;

			soundinfo = new Soundinfo( lokey, hikey, loopMode, loopStart, loopEnd, 0, 0x7FFFFFFF, tune, pitchKeyCenter, 0, 0, volume );
			ampeg = new Ampeg( 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 1.0d, 0.25d );
			waveform = waveformList[( int )wlnkChunk.tableIndex];
		}
	}
}
