using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Application.Sound;
using Monoamp.Common.Component.Sound.Utility;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
	public abstract class AGeneratorEnvelope
	{
		public void GetEnvelope( double[] aBufer, double aAddSamples, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( ( int )aGeneratorEnvelope.samplePosition + 1 < aGeneratorEnvelope.waveform.format.samples )
			{
				aBufer[0] = MeanInterpolation.Calculate( aGeneratorEnvelope.waveform, 0, aGeneratorEnvelope.samplePosition );
				aBufer[1] = MeanInterpolation.Calculate( aGeneratorEnvelope.waveform, 1, aGeneratorEnvelope.samplePosition );
			}
			// To Be Commented.
			// ループエンドまでは再生する。
			else if( ( int )aGeneratorEnvelope.samplePosition >= aGeneratorEnvelope.soundfont.soundinfo.loopEnd && aGeneratorEnvelope.soundfont.soundinfo.loopMode == true )
			{
				// To Be Commented.
				// なぜ"loopStart + 1"になるのか確認する.
				aBufer[0] = MeanInterpolation.Calculate( aGeneratorEnvelope.waveform, 0, aGeneratorEnvelope.samplePosition, aGeneratorEnvelope.soundfont.soundinfo.loopStart + 1 );
				aBufer[1] = MeanInterpolation.Calculate( aGeneratorEnvelope.waveform, 1, aGeneratorEnvelope.samplePosition, aGeneratorEnvelope.soundfont.soundinfo.loopStart + 1 );
			}

			AddSamplePoint( aAddSamples, ref aGeneratorEnvelope );
		}

		private void AddSamplePoint( double aAddSamples, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			aGeneratorEnvelope.samplePosition += aAddSamples * aGeneratorEnvelope.sampleSpeed;

			if( aGeneratorEnvelope.soundfont.soundinfo.loopMode == true && aGeneratorEnvelope.samplePosition > aGeneratorEnvelope.soundfont.soundinfo.loopEnd )
			{
				// To Be Commented.
				// 計算式の意味を説明する.
				aGeneratorEnvelope.samplePosition = aGeneratorEnvelope.soundfont.soundinfo.loopStart + ( aGeneratorEnvelope.samplePosition - aGeneratorEnvelope.soundfont.soundinfo.loopEnd );
			}
		}

		public abstract void Generate( double[] aBuffer, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope );
		public abstract AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope );
		public abstract bool GetFlagNoteOn();
		public abstract bool GetFlagEnd();
		public abstract bool GetFlagSustain();
	}

	public static class GeneratorEnvelopeSet
	{
		public static GeneratorEnvelopeDelay generatorEnvelopeDelay;
		public static GeneratorEnvelopeAttack generatorEnvelopeAttack;
		public static GeneratorEnvelopeHold generatorEnvelopeHold;
		public static GeneratorEnvelopeDecay generatorEnvelopeDecay;
		public static GeneratorEnvelopeSustain generatorEnvelopeSustain;
		public static GeneratorEnvelopeRelease generatorEnvelopeRelease;
		public static GeneratorEnvelopeEnd generatorEnvelopeEnd;

		static GeneratorEnvelopeSet()
		{
			generatorEnvelopeDelay = new GeneratorEnvelopeDelay();
			generatorEnvelopeAttack = new GeneratorEnvelopeAttack();
			generatorEnvelopeHold = new GeneratorEnvelopeHold();
			generatorEnvelopeDecay = new GeneratorEnvelopeDecay();
			generatorEnvelopeSustain = new GeneratorEnvelopeSustain();
			generatorEnvelopeRelease = new GeneratorEnvelopeRelease();
			generatorEnvelopeEnd = new GeneratorEnvelopeEnd();
		}
	}
}
