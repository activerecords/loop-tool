using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventProgramChange : MidiEventBase
	{
		public MidiEventProgramChange( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), 0 )
		{

		}

		public MidiEventProgramChange( MidiEventProgramChange aProgramChangeEvent )
			: base( aProgramChangeEvent )
		{

		}
	}
}
