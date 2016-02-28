using System;

using Curan.Common.AdaptedData.Music;
using Curan.Common.AdaptedData;

namespace Curan.Common.Calculator
{
	public static class MeanInterpolation
	{
		public static float Calculate( MusicPcm aMusic, int aChannel, double aSampleCurrent )
		{
			float a = aMusic.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aMusic.GetSample( aChannel, ( int )aSampleCurrent + 1 );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}

		public static float Calculate( MusicPcm aMusic, int aChannel, double aSampleCurrent, int aSampleLoopStart )
		{
			float a = aMusic.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aMusic.GetSample( aChannel, aSampleLoopStart );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}

		public static float Calculate( WaveformBase aWaveformBase, double aSampleCurrent, int aChannel )
		{
			float a = aWaveformBase.data.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aWaveformBase.data.GetSample( aChannel, ( int )aSampleCurrent + 1 );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}

		public static float Calculate( WaveformBase aWaveformBase, double aSampleCurrent, int aSampleLoopStart, int aChannel )
		{
			float a = aWaveformBase.data.GetSample( aChannel, ( int )aSampleCurrent );
			float b = aWaveformBase.data.GetSample( aChannel, aSampleLoopStart );
			double positionDifference = aSampleCurrent - ( int )aSampleCurrent;

			return ( float )( a + ( b - a ) * positionDifference );
		}
	}
}
