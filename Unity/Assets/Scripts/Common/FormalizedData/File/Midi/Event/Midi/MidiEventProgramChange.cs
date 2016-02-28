using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventProgramChange : MidiEventBase
	{
		public MidiEventProgramChange( int aDelta, byte aState, ByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), 0 )
		{

		}

		public MidiEventProgramChange( MidiEventProgramChange aProgramChangeEvent )
			: base( aProgramChangeEvent )
		{

		}
	}
}
