using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;

namespace Monoamp.Common.Component.Sound.Midi
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
