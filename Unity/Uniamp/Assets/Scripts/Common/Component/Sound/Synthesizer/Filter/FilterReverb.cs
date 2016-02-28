using System;

namespace Monoamp.Common.Component.Application.Sound
{
	public struct FilterReverb
	{
		public static double GAIN_DEFAULT;
		public static int SAMPLES_DELAY_TIME_DEFAULT;

		private double gain;
		private int samplesDelayTime;

		private double[] bufferArray;

		private float reverbSendLevel;
		private float reverbDelayTime;

		public bool isOnReverb;

		static FilterReverb()
		{
			GAIN_DEFAULT = 0.6d;
			SAMPLES_DELAY_TIME_DEFAULT = 12000;
		}

		public void Init()
		{
			gain = GAIN_DEFAULT;
			samplesDelayTime = SAMPLES_DELAY_TIME_DEFAULT;

			reverbSendLevel = 0.0f;
			reverbDelayTime = 0.5f;

			isOnReverb = false;

			bufferArray = new double[samplesDelayTime];
		}

		public FilterReverb( double aGain, int aDelayTimeSamples )
		{
			gain = aGain;
			samplesDelayTime = aDelayTimeSamples;

			reverbSendLevel = 0.0f;
			reverbDelayTime = 0.5f;

			isOnReverb = false;

			bufferArray = new double[samplesDelayTime];
		}

		public void SetReverbSendLevel( int level )
		{
			float levelData = level;

			if( levelData > 127.0f )
			{
				levelData = 127.0f;
			}

			reverbSendLevel = levelData / 256.0f;
		}

		public float GetReverbSendLevel()
		{
			return reverbSendLevel;
		}

		public void Filter( ref double aWaveform, int aSampleRate )
		{
			int lDelayTimeSamples = ( int )( samplesDelayTime * reverbDelayTime );

			// リバーブ音を追加する.
			aWaveform += bufferArray[( int )( samplesDelayTime - lDelayTimeSamples )] * reverbSendLevel * gain;

			// 次のリバーブ計算用に、配列をコピーする.
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
		}
	}
}
