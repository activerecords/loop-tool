using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventKeyPressure : MidiEventBase
	{
		public MidiEventKeyPressure( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, 0, 0 )
		{
			int length = aByteArray.ReadByte();

			aByteArray.AddPosition( length );
		}

		public MidiEventKeyPressure( MidiEventKeyPressure aKeyPressureEvent )
			: base( aKeyPressureEvent )
		{

		}
	}
}
