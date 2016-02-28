using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventKeyPressure : MidiEventBase
	{
		public MidiEventKeyPressure( int aDelta, byte aState, ByteArray aByteArray )
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
