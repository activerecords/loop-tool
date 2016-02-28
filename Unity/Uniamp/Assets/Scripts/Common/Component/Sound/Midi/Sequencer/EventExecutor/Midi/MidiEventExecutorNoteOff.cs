using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;

namespace Monoamp.Common.Component.Sound.Midi
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
