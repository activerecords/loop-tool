using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventControlChange : MidiEventBase
	{
		private UInt16 data;

		public MidiEventControlChange( int aDelta, byte aState, ByteArray aByteArray )
			: base( aDelta, aState, aByteArray.ReadByte(), aByteArray.ReadByte() )
		{
			if( data1 == 0 )
			{
				data = ( UInt16 )( data2 << 8 );
			}
			else if( data1 == 32 )
			{
				data |= ( UInt16 )data2;
			}
		}

        public MidiEventControlChange( MidiEventControlChange aControlChangeEvent )
            : base( aControlChangeEvent )
		{
            data = aControlChangeEvent.GetData();
		}

		public UInt16 GetData()
		{
			return data;
		}
	}
}
