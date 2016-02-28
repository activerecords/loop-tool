using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MetaEventTempo : MetaEventBase
	{
		private int tempo;

		public MetaEventTempo( int aDelta, byte aType, ByteArray byteArray )
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
				Logger.LogException( new Exception() );
			}
		}

		public float GetTempo()
		{
			return ( float )tempo;
		}
	}
}
