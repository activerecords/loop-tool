using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiEventSystemExclusive : MidiEventBase
	{
		protected int length;
		protected byte[] dataArray;

		public MidiEventSystemExclusive( int aDelta, byte aState, ByteArray aByteArray )
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
