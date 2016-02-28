using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;

namespace Curan.Common.ApplicationComponent.Sound.Midi
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
