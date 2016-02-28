using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventNoteOff : MidiEventBase
	{
		public MidiEventNoteOff( int aDelta, byte aState, ByteArray aByteArray )
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
