using System;

using Curan.Common.AdaptedData;
using Curan.Common.Calculator;
using Curan.Common.FilePool;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
    public struct GeneratorEnvelope
    {
		public SoundfontBase soundfont;
		public WaveformBase waveform;

		public double secondsSustain;
		public double samplePosition;

		public float volume;
		public double sampleSpeed;

		public AGeneratorEnvelope generatorEnvelopeCurrent;

		public double timeElapsedDelay;
		public double timeElapsedAttack;
		public double timeElapsedHold;
		public double timeElapsedDecay;
		public double timeElapsedSustain;
		public double timeElapsedRelease;
		public double timeElapsedEnd;
		public double timeElapsedSum;

		public GeneratorEnvelope( SoundfontBase aSoundfont, byte aVelocity, double aSecondsSustain = 0.0d )
		{
			soundfont = aSoundfont;

			waveform = aSoundfont.waveform;

			secondsSustain = aSecondsSustain;
			samplePosition = soundfont.soundinfo.offset;

			volume = ( float )Math.Pow( 10.0d, ( double )aSoundfont.soundinfo.volume / 20.0d ) * ( float )aVelocity / 8.0f;
			double lCenterFrequency = 440.0d * Math.Pow( 2.0d, ( ( double )aSoundfont.soundinfo.pitchKeyCenter - 69.0d ) / 12.0d );
			sampleSpeed = waveform.format.sampleRate / lCenterFrequency;

			generatorEnvelopeCurrent = GeneratorEnvelopeSet.generatorEnvelopeDelay;

			timeElapsedDelay = 0.0d;
			timeElapsedAttack = 0.0d;
			timeElapsedHold = 0.0d;
			timeElapsedDecay = 0.0d;
			timeElapsedSustain = 0.0d;
			timeElapsedRelease = 0.0d;
			timeElapsedEnd = 0.0d;
			timeElapsedSum = 0.0d;

			aSoundfont.waveform.data.GetSample( 0, 0 );
        }

		public void Set( SoundfontBase aSoundfont, byte aVelocity, double aSecondsSustain = 0.0d )
		{
			soundfont = aSoundfont;
			volume = ( float )Math.Pow( 10.0d, ( double )aSoundfont.soundinfo.volume / 20.0d ) * ( float )aVelocity / 8.0f;
		}

		public void Generate( double[] aBuffer, double aAddSamples, double aSampleSpeed )
		{
			generatorEnvelopeCurrent = generatorEnvelopeCurrent.GetNextOscillator( ref this );
			
			if( samplePosition >= waveform.format.samples || samplePosition > soundfont.soundinfo.end )
			{
				generatorEnvelopeCurrent = GeneratorEnvelopeSet.generatorEnvelopeEnd;

				if( soundfont.soundinfo.loopMode == true )
				{
					Logger.LogWarning( "End" );
				}
			}

			generatorEnvelopeCurrent.Generate( aBuffer, aAddSamples, aSampleSpeed, ref this );

			aBuffer[0] *= volume;
			aBuffer[1] *= volume;
		}
    }
}
