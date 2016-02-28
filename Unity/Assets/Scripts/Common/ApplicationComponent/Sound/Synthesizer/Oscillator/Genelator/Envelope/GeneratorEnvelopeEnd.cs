using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class GeneratorEnvelopeEnd : AGeneratorEnvelope
    {
		public GeneratorEnvelopeEnd()
		{

		}

		public override void Generate( double[] aBuffer, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			aBuffer[0] = 0;
			aBuffer[1] = 0;
		}

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			return this;
		}

		public override bool GetFlagNoteOn()
		{
			return false;
		}

		public override bool GetFlagEnd()
		{
			return true;
		}

		public override bool GetFlagSustain()
		{
			return false;
		}
    }
}
