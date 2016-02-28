using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public struct FilterChorus
	{
		public static double GAIN_DEFAULT;
		public static int SAMPLES_DELAY_TIME_DEFAULT;

		private double gain;
		private int samplesDelayTime;

		private double[] bufferArray;

		private float chorusSendLevel;
		private float chorusDelayTime;

		public bool isOnChorus;

		static FilterChorus()
		{
			GAIN_DEFAULT = 0.8d;
			SAMPLES_DELAY_TIME_DEFAULT = 2400;
		}

		public void Init()
		{
			gain = GAIN_DEFAULT;
			samplesDelayTime = SAMPLES_DELAY_TIME_DEFAULT;

			chorusSendLevel = 0.0f;
			chorusDelayTime = 0.5f;

			isOnChorus = false;

			bufferArray = new double[samplesDelayTime];
		}

		public FilterChorus( float aGain, int aDelayTimeSamples )
		{
			gain = aGain;
			samplesDelayTime = aDelayTimeSamples;

			chorusSendLevel = 0.0f;
			chorusDelayTime = 0.5f;

			isOnChorus = false;

			bufferArray = new double[samplesDelayTime];
		}

		public void SetChorusSendLevel( int level )
		{
			float levelData = level;

			if( levelData > 127.0f )
			{
				levelData = 127.0f;
			}

			chorusSendLevel = levelData / 127.0f;
		}

		public float GetChorusSendLevel()
		{
			return chorusSendLevel;
		}

		public void Filter( ref double aWaveform, int aSampleRate )
		{
			// 次のコーラス計算用に、配列をコピーする.
			for( int j = 0; j < samplesDelayTime; j++ )
			{
				if( j < samplesDelayTime - 1 )
				{
					bufferArray[j] = bufferArray[j + 1];
				}
				else
				{
					bufferArray[j] = aWaveform;
				}
			}

			int lDelayTimeSamples = ( int )( samplesDelayTime * chorusDelayTime );

			// コーラス音を追加する.
			aWaveform += bufferArray[( int )( samplesDelayTime - lDelayTimeSamples )] * chorusSendLevel * gain;
		}
	}
}
