using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventNoteOn : MidiEventBase
	{
		public MidiEventNoteOn( int aDelta, byte aState, ByteArray aByteArray )
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
