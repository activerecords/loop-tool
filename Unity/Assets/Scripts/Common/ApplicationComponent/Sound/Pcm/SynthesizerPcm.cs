using System;

using Curan.Common.AdaptedData.Music;
using Curan.Common.Calculator;
using Curan.Common.Struct;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Pcm
{
	public class SynthesizerPcm
	{
		private MusicPcm music;
		private SoundTime timePosition;
		private SoundTime timeElapsed;

		public int loopNumber1;
		public int loopNumber2;

		public SynthesizerPcm( MusicPcm aMusicPcm )
		{
			music = aMusicPcm;
			timePosition = new SoundTime( 44100, 0 );
			timeElapsed = new SoundTime( 44100, 0 );
			loopNumber1 = 0;
			loopNumber2 = 0;
		}

		public void Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			LoopInformation lLoop = music.Loop[loopNumber1][loopNumber2];

			if( lLoop.start.sample != 0 && lLoop.end.sample != 0 && ( int )timePosition.sample > lLoop.end.sample )
			{
				Logger.LogDebug( "Start:" + lLoop.start.sample + ", End:" + lLoop.end.sample );

				double diff = timePosition.sample % 1.0d;
				timePosition.sample = ( int )lLoop.start.sample + diff;
			}

			if( ( int )timePosition.sample + 1 < music.SampleLength )
			{
				for( int i = 0; i < aChannels; i++ )
				{
					aSoundBuffer[i] = MeanInterpolation.Calculate( music, i, timePosition.sample );
				}
			}
			else if( ( int )timePosition.sample < music.SampleLength )
			{
				for( int i = 0; i < aChannels; i++ )
				{
					aSoundBuffer[i] = MeanInterpolation.Calculate( music, i, timePosition.sample, ( int )lLoop.start.sample );
				}
			}
			else
			{
				timePosition.sample = 0.0d;
			}

			if( timePosition.sample == 1 )
			{
				Logger.LogDebug( "Start:" + aSoundBuffer[0] );
			}

			timePosition.sample += ( double )music.SampleRate / ( double )aSampleRate;
			timeElapsed.sample += ( double )music.SampleRate / ( double )aSampleRate;
		}

		public void SetPosition( double aPosition )
		{
			timePosition.sample = ( double )music.SampleLength * aPosition;
		}

		public double GetPosition()
		{
			return timePosition.sample / ( double )music.SampleLength;
		}

		public SoundTime GetTimePosition()
		{
			return timePosition;
		}

		public SoundTime GetTimeElapsed()
		{
			return timeElapsed;
		}

		public SoundTime GetSecondLength()
		{
			return new SoundTime( music.SampleRate, music.SampleLength );
		}

		public LoopInformation GetLoopPoint()
		{
			return music.Loop[loopNumber1][loopNumber2];
		}

		public int GetLoopCount()
		{
			return music.Loop[loopNumber1].Count;
		}

		public int GetLoopNumberX()
		{
			return loopNumber1;
		}

		public int GetLoopNumberY()
		{
			return loopNumber2;
		}

		public void SetNextLoop()
		{
			loopNumber1++;
			loopNumber1 %= music.Loop.Count;

			loopNumber2 = 0;
		}

		public void SetPreviousLoop()
		{
			loopNumber1 += music.Loop.Count;
			loopNumber1--;
			loopNumber1 %= music.Loop.Count;

			loopNumber2 = 0;
		}

		public void SetUpLoop()
		{
			loopNumber2++;
			loopNumber2 %= music.Loop[loopNumber1].Count;
		}

		public void SetDownLoop()
		{
			loopNumber2 += music.Loop[loopNumber1].Count;
			loopNumber2--;
			loopNumber2 %= music.Loop[loopNumber1].Count;
		}
	}
}
