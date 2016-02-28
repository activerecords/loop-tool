using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Form;
using Monoamp.Common.Data.Standard.Form.Aiff;
using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Wave;
using Monoamp.Common.system.io;
using Monoamp.Common.Struct;

namespace Monoamp.Common.Data.Application.Sound
{
	public class WaveformPcm
	{
		public readonly WaweformFormat format;
		public readonly WaveformData data;
	
		public WaveformPcm( FormAiffForm aFormFile )
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

			using ( FileStream u = new FileStream( aFormFile.name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
			{
				AByteArray lByteArray = new ByteArrayLittle( u );
				data = new WaveformData( format, lByteArray, lPosition );
			}
		}

		public WaveformPcm( RiffWaveRiff aRiffFile )
		{
			RiffWaveData lRiffWaveData = ( RiffWaveData )aRiffFile.GetChunk( RiffWaveData.ID );
			int lPosition = ( int )lRiffWaveData.position;
			int lLength = ( int )lRiffWaveData.Size;
			
			RiffWaveFmt_ lRiffWaveFmt_ = ( RiffWaveFmt_ )aRiffFile.GetChunk( RiffWaveFmt_.ID );
			int lChannels = lRiffWaveFmt_.channels;
			int lSampleRate = ( int )lRiffWaveFmt_.samplesPerSec;
			int lSampleBits = lRiffWaveFmt_.bitsPerSample;
			int lSamples = lLength / ( lSampleBits / 8 ) / lChannels;
			
			format = new WaweformFormat( lChannels, lSamples, lSampleRate, lSampleBits );

			using ( FileStream u = new FileStream( aRiffFile.name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
			{
				AByteArray lByteArray = new ByteArrayLittle( u );
				data = new WaveformData( format, lByteArray, lPosition );
			}
		}
	}
}
