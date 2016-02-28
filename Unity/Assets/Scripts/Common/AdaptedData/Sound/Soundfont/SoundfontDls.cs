using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Dls;

namespace Curan.Common.AdaptedData
{
	public class SoundfontDls : SoundfontBase
	{
		public SoundfontDls( RiffChunkListRgn_ rgn_List, List<WaveformBase> waveformList, int tuning )
		{
			RiffChunkRgnh rgnhChunk = rgn_List.rgnhBody;
			RiffChunkWsmp wsmpChunk = rgn_List.wsmpBody;
			RiffChunkWlnk wlnkChunk = rgn_List.wlnkBody;

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

		public SoundfontDls( RiffChunkListRgn2 rgn2List, List<WaveformBase> waveformList, int tuning )
		{
			RiffChunkRgnh rgnhChunk = rgn2List.rgnhBody;
			RiffChunkWsmp wsmpChunk = rgn2List.wsmpBody;
			RiffChunkWlnk wlnkChunk = rgn2List.wlnkBody;

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
