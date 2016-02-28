using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorProgramChange : MidiEventExecutorBase
	{
		public MidiEventExecutorProgramChange( MidiEventProgramChange aProgramChangeEvent )
			: base( aProgramChangeEvent )
		{

		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			MidiGenerator lMidiStatus = aMidiSynthesizer.GetMidiGeneratorArray()[midiEvent.GetChannel()];

			lMidiStatus.Instrument = midiEvent.GetData1();
		}
	}
}
