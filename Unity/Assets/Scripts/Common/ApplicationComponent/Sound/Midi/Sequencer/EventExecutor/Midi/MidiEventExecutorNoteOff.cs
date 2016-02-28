using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorNoteOff : MidiEventExecutorBase
	{
		private MidiEventNoteOff eventNoteOff;

		public MidiEventExecutorNoteOff( MidiEventNoteOff aNoteOff )
			: base( aNoteOff )
		{
			eventNoteOff = aNoteOff;
		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			aMidiSynthesizer.NoteOff( GetChannel(), GetNote() );
		}

		public byte GetNote()
		{
			return eventNoteOff.GetNote();
		}
	}
}
