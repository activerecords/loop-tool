using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorKeyPressure : MidiEventExecutorBase
	{
		public MidiEventExecutorKeyPressure( MidiEventKeyPressure aKeyPressure )
			: base( aKeyPressure )
		{

		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{

		}
	}
}
