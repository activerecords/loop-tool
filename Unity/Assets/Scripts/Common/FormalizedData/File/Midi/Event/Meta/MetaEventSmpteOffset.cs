using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MetaEventSmpteOffset : MetaEventBase
	{
		public MetaEventSmpteOffset( int aDelta, byte aType, ByteArray byteArray )
			: base( aDelta, aType )
		{
			int length = byteArray.ReadByte();

			byteArray.AddPosition( length );
		}
	}
}
