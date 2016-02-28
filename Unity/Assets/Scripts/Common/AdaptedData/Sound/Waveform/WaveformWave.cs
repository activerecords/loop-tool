using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Wave;

namespace Curan.Common.AdaptedData
{
	public class WaveformWave : WaveformBase
	{
		public WaveformWave( RiffFile aRiffFile )
			: base()
		{
			RiffChunkListWave lWaveList = ( RiffChunkListWave )aRiffFile.riffChunkList;

			int lPosition = ( int )lWaveList.dataChunk.position;
			int lLength = ( int )lWaveList.dataChunk.size;

			int lChannels = lWaveList.fmt_Chunk.channels;
			int lSampleRate = ( int )lWaveList.fmt_Chunk.samplesPerSec;
			int lSampleBits = lWaveList.fmt_Chunk.bitsPerSample;
			int lSamples = lLength / ( lSampleBits / 8 ) / lChannels;

			format = new FormatWaweform( lChannels, lSamples, lSampleRate, lSampleBits );
			data = new WaveformData( format, null, aRiffFile.name, lPosition );
		}
	}
}
