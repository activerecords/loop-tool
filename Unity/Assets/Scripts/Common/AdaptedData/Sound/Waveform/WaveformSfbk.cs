using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Riff.Sfbk;

namespace Curan.Common.AdaptedData
{
	public class WaveformSfbk : WaveformBase
	{
		public WaveformSfbk( RiffChunkListSdta[] sdtaBodyList, ShdrData shdrData, int startAddrsOffset, int endAddrsOffset, string aName )
			: base()
		{
			string name = aName;
			int position = ( int )sdtaBodyList[0].smplBody.position + ( int )( shdrData.start + startAddrsOffset ) * 2;
			int length = ( int )( ( shdrData.end + endAddrsOffset ) - ( shdrData.start + startAddrsOffset ) ) * 2;

			int channels = 1;
			int sampleRate = ( int )shdrData.sampleRate;
			int sampleBits = 16;
			int samples = length / channels;

			format = new FormatWaweform( channels, samples, sampleRate, sampleBits );
			data = new WaveformData( format, null, name, position );
		}
	}
}
