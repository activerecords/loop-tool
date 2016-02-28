using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorNoteOn : MidiEventExecutorBase
	{
		private int deltaLength;
		private MidiEventNoteOn eventNoteOn;

		public MidiEventExecutorNoteOn( MidiEventNoteOn aNoteOn, int aDeltaLength )
			: base( aNoteOn )
		{
			deltaLength = aDeltaLength;

			eventNoteOn = aNoteOn;
		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			double lSecondLength = ( double )GetDeltaLength() / ( double )aDivision * 60.0d / aBpm;

			aMidiSynthesizer.NoteOn( GetChannel(), GetNote(), GetVelocity(), lSecondLength );
		}

		public int GetDeltaLength()
		{
			return deltaLength;
		}

		public byte GetNote()
		{
			return eventNoteOn.GetNote();
		}

		public byte GetVelocity()
		{
			return eventNoteOn.GetVelocity();
		}
	}
}
