using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Form;
using Monoamp.Common.Data.Standard.Form.Aiff;
using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Wave;
using Monoamp.Common.Data.Standard.Riff.Dls;
using Monoamp.Common.Data.Standard.Riff.Sfbk;
using Monoamp.Common.system.io;
using Monoamp.Common.Struct;

namespace Monoamp.Common.Data.Application.Sound
{
	public class WaveformReaderPcm
	{
		public readonly WaweformFormat format;
		public readonly WaveformReader reader;
	
		public WaveformReaderPcm( FormAiffForm aFormFile, bool aIsOnMemory )
		{
			FormAiffSsnd lSsndChunk = ( FormAiffSsnd )aFormFile.GetChunk( FormAiffSsnd.ID );
			int lPosition = ( int )( lSsndChunk.position + lSsndChunk.offset + 8 );
			int lLength = lSsndChunk.dataSize;
			
			FormAiffComm lChunkComm = ( FormAiffComm )aFormFile.GetChunk( FormAiffComm.ID );
			int lChannels = lChunkComm.numberOfChannels;
			int lSampleRate = ( int )lChunkComm.sampleRate;
			int lSampleBits = lChunkComm.bitsPerSamples;
			int lSamples = lLength / ( lSampleBits / 8 ) / lChannels;
			
			format = new WaweformFormat( lChannels, lSamples, lSampleRate, lSampleBits );
			reader = new WaveformReader( format, aFormFile.name, lPosition, aIsOnMemory, AByteArray.Endian.Big );
		}

		public WaveformReaderPcm( RiffWaveRiff aRiffWaveRiff, bool aIsOnMemory )
		{
			RiffWaveData lRiffWaveData = ( RiffWaveData )aRiffWaveRiff.GetChunk( RiffWaveData.ID );
			int lPosition = ( int )lRiffWaveData.position;
			int lLength = ( int )lRiffWaveData.Size;
			
			RiffWaveFmt_ lRiffWaveFmt_ = ( RiffWaveFmt_ )aRiffWaveRiff.GetChunk( RiffWaveFmt_.ID );
			int lChannels = lRiffWaveFmt_.channels;
			int lSampleRate = ( int )lRiffWaveFmt_.samplesPerSec;
			int lSampleBits = lRiffWaveFmt_.bitsPerSample;
			int lSamples = lLength / ( lSampleBits / 8 ) / lChannels;
			
			format = new WaweformFormat( lChannels, lSamples, lSampleRate, lSampleBits );
			reader = new WaveformReader( format, aRiffWaveRiff.name, lPosition, aIsOnMemory, AByteArray.Endian.Little );
		}

		public WaveformReaderPcm( RiffChunkListWave aWaveList, string aName )
			: base()
		{
			RiffDls_Fmt_ fmt_Chunk = aWaveList.fmt_Chunk;
			RiffDls_Data dataChunk = aWaveList.dataChunk;
			
			int position = ( int )dataChunk.position;
			int length = ( int )dataChunk.Size;
			
			int channels = fmt_Chunk.channles;
			int sampleRate = ( int )fmt_Chunk.samplesPerSec;
			int sampleBits = ( int )fmt_Chunk.bitsPerSample;
			int samples = length / ( sampleBits / 8 ) / channels;
			
			format = new WaweformFormat( channels, samples, sampleRate, sampleBits );
			reader = new WaveformReader( format, aName, position, true, AByteArray.Endian.Little );
		}

		public WaveformReaderPcm( RiffChunkListSdta[] sdtaBodyList, ShdrData shdrData, int startAddrsOffset, int endAddrsOffset, string aName )
			: base()
		{
			string name = aName;
			int position = ( int )sdtaBodyList[0].smplBody.position + ( int )( shdrData.start + startAddrsOffset ) * 2;
			int length = ( int )( ( shdrData.end + endAddrsOffset ) - ( shdrData.start + startAddrsOffset ) ) * 2;
			
			int channels = 1;
			int sampleRate = ( int )shdrData.sampleRate;
			int sampleBits = 16;
			int samples = length / channels;
			
			format = new WaweformFormat( channels, samples, sampleRate, sampleBits );
			reader = new WaveformReader( format, name, position, true, AByteArray.Endian.Little );
		}
	}
}
