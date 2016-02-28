using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventPitchWheelChange : MidiEventBase
	{
		public MidiEventPitchWheelChange( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), aByteArray.ReadByte() )
		{

		}

		public MidiEventPitchWheelChange( MidiEventPitchWheelChange aPitchWheelChangeEvent )
			: base( aPitchWheelChangeEvent )
		{

		}
	}
}
