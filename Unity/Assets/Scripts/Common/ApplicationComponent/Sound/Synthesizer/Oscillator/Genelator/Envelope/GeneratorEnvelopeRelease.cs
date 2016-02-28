using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class GeneratorEnvelopeRelease : AGeneratorEnvelope
    {
		public GeneratorEnvelopeRelease()
		{

		}

		public override void Generate( double[] aWaveform, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			GetEnvelope( aWaveform, aAddSamples, ref aGeneratorEnvelope );
			double lEnvelope = aGeneratorEnvelope.soundfont.ampeg.ampegSustain - aGeneratorEnvelope.soundfont.ampeg.ampegSustain * aGeneratorEnvelope.timeElapsedRelease / aGeneratorEnvelope.soundfont.ampeg.ampegRelease;

			aWaveform[0] *= lEnvelope;
			aWaveform[1] *= lEnvelope;

			aGeneratorEnvelope.timeElapsedRelease += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( aGeneratorEnvelope.timeElapsedRelease >= aGeneratorEnvelope.soundfont.ampeg.ampegRelease )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeEnd.GetNextOscillator( ref aGeneratorEnvelope );
			}
			else
			{
				return this;
			}
		}

		public override bool GetFlagNoteOn()
		{
			return false;
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
