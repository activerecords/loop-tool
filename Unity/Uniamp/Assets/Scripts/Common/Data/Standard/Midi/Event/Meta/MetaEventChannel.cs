using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MetaEventChannel : MetaEventBase
	{
		public MetaEventChannel( int aDelta, byte aType, AByteArray byteArray )
			: base( aDelta, aType )
		{
			int length = byteArray.ReadByte();

			byteArray.AddPosition( length );
		}
	}
}
