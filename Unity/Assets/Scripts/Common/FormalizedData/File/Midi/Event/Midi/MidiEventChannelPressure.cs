using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventChannelPressure : MidiEventBase
	{
		public MidiEventChannelPressure( int aDelta, byte aState, ByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), 0 )
		{

		}
		
		public MidiEventChannelPressure( int aDelta, byte aState, byte aData1, byte aData2 )
			: base( aDelta, aState, aData1, aData2 )
		{

		}
	}
}
