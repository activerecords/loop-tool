using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiEventSystemExclusive : MidiEventBase
	{
		protected int length;
		protected byte[] dataArray;

		public MidiEventSystemExclusive( int aDelta, byte aState, AByteArray aByteArray )
			: base( aDelta, aState, 0, 0 )
		{
			length = MtrkChunk.GetVariableLengthByte( aByteArray );

			dataArray = new byte[length];

			for( int i = 0; i < length; i++ )
			{
				dataArray[i] = aByteArray.ReadByte();
			}
		}

		public MidiEventSystemExclusive( MidiEventSystemExclusive aSysExEvent )
			: base( aSysExEvent )
		{
			length = aSysExEvent.length;
			dataArray = aSysExEvent.dataArray;
		}

		public int GetLength()
		{
			return length;
		}

		public byte[] GetDataArray()
		{
			return dataArray;
		}
	}
}
