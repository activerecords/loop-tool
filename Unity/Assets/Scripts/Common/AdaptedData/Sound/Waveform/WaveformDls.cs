using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Riff.Dls;

namespace Curan.Common.AdaptedData
{
	public class WaveformDls : WaveformBase
	{
		public WaveformDls( RiffChunkListWave aWaveList, string aName )
			: base()
		{
			RiffChunkFmt_ fmt_Chunk = aWaveList.fmt_Chunk;
			RiffChunkData dataChunk = aWaveList.dataChunk;

			int position = ( int )dataChunk.position;
			int length = ( int )dataChunk.size;

			int channels = fmt_Chunk.channles;
			int sampleRate = ( int )fmt_Chunk.samplesPerSec;
			int sampleBits = ( int )fmt_Chunk.bitsPerSample;
			int samples = length / ( sampleBits / 8 ) / channels;

			format = new FormatWaweform( channels, samples, sampleRate, sampleBits );
			data = new WaveformData( format, null, aName, position );
		}
	}
}
