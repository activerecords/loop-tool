using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;

namespace Monoamp.Common.Component.Sound.Midi
{
	public abstract class MidiEventExecutorBase
	{
		protected MidiEventBase midiEvent;

		protected MidiEventExecutorBase( MidiEventBase aMidiEvent )
		{
			midiEvent = aMidiEvent;
		}

		public abstract void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm );

		public MidiEventBase GetMidiEvent()
		{
			return midiEvent;
		}

		public int GetDelta()
		{
			return midiEvent.GetDelta();
		}

		public int GetState()
		{
			return midiEvent.GetState();
		}

		public byte GetChannel()
		{
			return midiEvent.GetChannel();
		}
	}
}
