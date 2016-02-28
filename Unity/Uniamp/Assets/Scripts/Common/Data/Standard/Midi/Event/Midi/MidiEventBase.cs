using System;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventBase
	{
		protected int delta;
		protected byte state;
		protected byte data1;
		protected byte data2;

		public MidiEventBase( int aDelta, byte aState, byte aData1, byte aData2 )
		{
			delta = aDelta;
			state = aState;
			data1 = aData1;
			data2 = aData2;
		}

        public MidiEventBase( MidiEventBase aMidiEvent )
        {
            delta = aMidiEvent.GetDelta();
            state = ( byte )( aMidiEvent.GetState() | aMidiEvent.GetChannel() );
            data1 = aMidiEvent.GetData1();
            data2 = aMidiEvent.GetData2();
        }

		public int GetDelta()
		{
			return delta;
		}

		public byte GetState()
		{
			return ( byte )( state & 0xF0 );
		}

		public byte GetChannel()
		{
			return ( byte )( state & 0x0F );
		}

		public byte GetData1()
		{
			return data1;
		}

		public byte GetData2()
		{
			return data2;
		}
	}
}
