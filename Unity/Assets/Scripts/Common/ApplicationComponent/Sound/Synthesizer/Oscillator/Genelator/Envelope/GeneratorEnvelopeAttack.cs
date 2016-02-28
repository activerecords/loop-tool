using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class GeneratorEnvelopeAttack : AGeneratorEnvelope
    {
		public GeneratorEnvelopeAttack()
		{

		}

		public override void Generate( double[] aBuffer, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			GetEnvelope( aBuffer, aAddSamples, ref aGeneratorEnvelope );
			double lEnvelope = aGeneratorEnvelope.soundfont.ampeg.ampegStart + ( 1.0f - aGeneratorEnvelope.soundfont.ampeg.ampegStart ) * aGeneratorEnvelope.timeElapsedAttack / aGeneratorEnvelope.soundfont.ampeg.ampegAttack;

			aBuffer[0] *= lEnvelope;
			aBuffer[1] *= lEnvelope;

			aGeneratorEnvelope.timeElapsedAttack += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( aGeneratorEnvelope.timeElapsedAttack >= aGeneratorEnvelope.soundfont.ampeg.ampegAttack )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeHold.GetNextOscillator( ref aGeneratorEnvelope );
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
