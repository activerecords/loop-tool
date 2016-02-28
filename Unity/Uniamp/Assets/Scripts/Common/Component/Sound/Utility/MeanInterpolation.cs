using System;

using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Data.Application.Sound;

namespace Monoamp.Common.Component.Sound.Utility
{
	public static class MeanInterpolation
	{
		public static float Calculate( WaveformReaderPcm aWaveformPcm, int aChannel, double aSampleCurrent )
		{
			float a = aWaveformPcm.reader.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aWaveformPcm.reader.GetSample( aChannel, ( int )aSampleCurrent + 1 );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}

		public static float Calculate( WaveformReaderPcm aWaveformPcm, int aChannel, double aSampleCurrent, double aSampleLoopStart )
		{
			float a = aWaveformPcm.reader.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aWaveformPcm.reader.GetSample( aChannel, ( int )aSampleLoopStart );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}
	}
}
