using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventNoteOn : MidiEventBase
	{
		public MidiEventNoteOn( int aDelta, byte aState, AByteArray aByteArray )
			:base( aDelta, aState, aByteArray.ReadByte(), aByteArray.ReadByte() )
		{

		}

        public MidiEventNoteOn( MidiEventNoteOn aNoteOnEvent )
            : base( aNoteOnEvent )
		{

		}

		public byte GetNote()
		{
			return data1;
		}

		public byte GetVelocity()
		{
			return data2;
		}
	}
}
