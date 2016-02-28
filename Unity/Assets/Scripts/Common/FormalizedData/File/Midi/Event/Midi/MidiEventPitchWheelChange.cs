using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventPitchWheelChange : MidiEventBase
	{
		public MidiEventPitchWheelChange( int aDelta, byte aState, ByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), aByteArray.ReadByte() )
		{

		}

		public MidiEventPitchWheelChange( MidiEventPitchWheelChange aPitchWheelChangeEvent )
			: base( aPitchWheelChangeEvent )
		{

		}
	}
}
