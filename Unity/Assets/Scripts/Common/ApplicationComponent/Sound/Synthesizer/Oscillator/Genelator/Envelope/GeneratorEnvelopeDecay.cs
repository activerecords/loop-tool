using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class GeneratorEnvelopeDecay : AGeneratorEnvelope
    {
		public GeneratorEnvelopeDecay()
		{

		}

		public override void Generate( double[] aWaveform, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			GetEnvelope( aWaveform, aAddSamples, ref aGeneratorEnvelope );
			double lEnvelope = 1.0d - ( 1.0d - aGeneratorEnvelope.soundfont.ampeg.ampegSustain ) * aGeneratorEnvelope.timeElapsedDecay / aGeneratorEnvelope.soundfont.ampeg.ampegDecay;

			aWaveform[0] *= lEnvelope;
			aWaveform[1] *= lEnvelope;

			aGeneratorEnvelope.timeElapsedDecay += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( aGeneratorEnvelope.timeElapsedDecay >= aGeneratorEnvelope.soundfont.ampeg.ampegDecay )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeSustain.GetNextOscillator( ref aGeneratorEnvelope );
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
