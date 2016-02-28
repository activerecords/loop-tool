using System;

namespace Monoamp.Common.Component.Application.Sound
{
	public class GeneratorEnvelopeDelay : AGeneratorEnvelope
    {
		public GeneratorEnvelopeDelay()
		{

		}

		public override void Generate( double[] aBuffer, double aAddSamples, double aSampleSpeed, ref GeneratorEnvelope aGeneratorEnvelope )
		{
			aBuffer[0] = 0;
			aBuffer[1] = 0;

			aGeneratorEnvelope.timeElapsedDelay += aSampleSpeed;
			aGeneratorEnvelope.timeElapsedSum += aSampleSpeed;
        }

		public override AGeneratorEnvelope GetNextOscillator( ref GeneratorEnvelope aGeneratorEnvelope )
		{
			if( aGeneratorEnvelope.timeElapsedDelay >= aGeneratorEnvelope.soundfont.ampeg.ampegDelay )
			{
				return GeneratorEnvelopeSet.generatorEnvelopeAttack.GetNextOscillator( ref aGeneratorEnvelope );
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
