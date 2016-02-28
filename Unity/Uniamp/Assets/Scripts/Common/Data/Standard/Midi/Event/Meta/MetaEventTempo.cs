using System;

using Monoamp.Common.system.io;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MetaEventTempo : MetaEventBase
	{
		private int tempo;

		public MetaEventTempo( int aDelta, byte aType, AByteArray byteArray )
			: base( aDelta, aType )
		{
			int length = byteArray.ReadByte();

			if( length == 3 )
			{
				byte data1 = byteArray.ReadByte();
				byte data2 = byteArray.ReadByte();
				byte data3 = byteArray.ReadByte();

				tempo = ( ( int )data1 << 16 ) | ( ( int )data2 << 8 ) | data3;
			}
			else
			{
				byteArray.AddPosition( 3 );
				Logger.Exception( new Exception() );
			}
		}

		public float GetTempo()
		{
			return ( float )tempo;
		}
	}
}
