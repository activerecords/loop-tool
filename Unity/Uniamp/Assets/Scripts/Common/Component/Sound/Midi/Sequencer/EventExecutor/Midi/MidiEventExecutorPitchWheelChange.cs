using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;

namespace Monoamp.Common.Component.Sound.Midi
{
	public class MidiEventExecutorPitchWheelChange : MidiEventExecutorBase
	{
		public MidiEventExecutorPitchWheelChange( MidiEventPitchWheelChange aPitchWheelChange )
			: base( aPitchWheelChange )
		{

		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			MidiGenerator lMidiStatus = aMidiSynthesizer.GetMidiGeneratorArray()[midiEvent.GetChannel()];

			lMidiStatus.midiPitch.SetPitch( midiEvent.GetData1(), midiEvent.GetData2() );
		}
	}
}
