using System;

namespace Monoamp.Common.Component.Application.Sound
{
	public class GeneratorEnvelopeSustain : AGeneratorEnvelope
	{
		public GeneratorEnvelopeSustain()
		{

		}

		public override void Generate( double[] aWaveform, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			GetEnvelope( aWaveform, aAddSamples, ref aGeneratorEnvelope );
			double lEnvelope = aGeneratorEnvelope.soundfont.ampeg.ampegSustain;

			aWaveform[0] *= lEnvelope;
			aWaveform[1] *= lEnvelope;

			aGeneratorEnvelope.timeElapsedSustain += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;

			//elapsed += 1.0d;
			//elapsedSum += 1.0d;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			// リリース時間に適当に重みを掛け引く.
			if( aGeneratorEnvelope.secondsSustain != 0 && aGeneratorEnvelope.timeElapsedSustain >= aGeneratorEnvelope.secondsSustain - aGeneratorEnvelope.soundfont.ampeg.ampegRelease / 3.0d )
			//if( aGeneratorEnvelope.secondsSustain != 0 && timeElapsed >= aGeneratorEnvelope.secondsSustain )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeRelease.GetNextOscillator( ref aGeneratorEnvelope );
			}
			else
			{
				return this;
			}
		}

		public override bool GetFlagNoteOn()
		{
			return true;
		}

		public override bool GetFlagEnd()
		{
			return false;
		}

		public override bool GetFlagSustain()
		{
			return true;
		}
    }
}
