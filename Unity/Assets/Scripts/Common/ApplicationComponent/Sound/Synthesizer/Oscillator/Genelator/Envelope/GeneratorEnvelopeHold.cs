using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class GeneratorEnvelopeHold : AGeneratorEnvelope
    {
		public GeneratorEnvelopeHold()
		{

		}

		public override void Generate( double[] aWaveform, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			GetEnvelope( aWaveform, aAddSamples, ref aGeneratorEnvelope );
			double lEnvelope = 1.0f;

			aWaveform[0] *= lEnvelope;
			aWaveform[1] *= lEnvelope;

			aGeneratorEnvelope.timeElapsedHold += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( aGeneratorEnvelope.timeElapsedHold >= aGeneratorEnvelope.soundfont.ampeg.ampegHold )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeDecay.GetNextOscillator( ref aGeneratorEnvelope );
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
			return false;
		}
    }
}
