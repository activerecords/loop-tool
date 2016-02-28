using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventNoteOff : MidiEventBase
	{
		public MidiEventNoteOff( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), aByteArray.ReadByte() )
		{

		}

		public MidiEventNoteOff( MidiEventBase aMidiEvent )
			: base( aMidiEvent )
		{

		}

		public MidiEventNoteOff( MidiEventNoteOff aNoteOffEvent )
			: base( aNoteOffEvent )
		{

		}

		public byte GetNote()
		{
			return data1;
		}
	}
}
