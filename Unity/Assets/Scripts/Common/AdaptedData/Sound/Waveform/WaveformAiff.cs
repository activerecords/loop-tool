using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Form;
using Curan.Common.FormalizedData.File.Form.Aiff;

namespace Curan.Common.AdaptedData
{
	public class WaveformAiff : WaveformBase
	{
		public WaveformAiff( FormFile aFormFile )
			: base()
		{
			FormChunkListAiff aAiffForm = ( FormChunkListAiff )aFormFile.formChunkList;

			FormChunkSsnd lSsndChunk = aAiffForm.chunkSsnd;
			int lPosition = ( int )lSsndChunk.position;
			int lLength = lSsndChunk.dataSize;

			FormChunkComm lChunkComm = aAiffForm.chunkComm;
			int lChannels = lChunkComm.numberOfChannels;
			int lSampleRate = ( int )lChunkComm.sampleRate;
			int lSampleBits = lChunkComm.bitsPerSamples;
			int lSamples = lLength / ( lSampleBits / 8 ) / lChannels;

			format = new FormatWaweform( lChannels, lSamples, lSampleRate, lSampleBits );
			data = new WaveformData( format, null, aFormFile.name, lPosition );
		}
	}
}
