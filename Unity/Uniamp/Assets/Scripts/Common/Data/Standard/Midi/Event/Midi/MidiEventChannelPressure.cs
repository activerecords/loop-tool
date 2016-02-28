using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventChannelPressure : MidiEventBase
	{
		public MidiEventChannelPressure( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), 0 )
		{

		}
		
		public MidiEventChannelPressure( int aDelta, byte aState, byte aData1, byte aData2 )
			: base( aDelta, aState, aData1, aData2 )
		{

		}
	}
}
