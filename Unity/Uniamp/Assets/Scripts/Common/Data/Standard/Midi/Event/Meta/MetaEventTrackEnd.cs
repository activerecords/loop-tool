using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MetaEventTrackEnd : MetaEventBase
	{
		public MetaEventTrackEnd( int aDelta, byte aType, AByteArray byteArray )
			: base( aDelta, aType )
		{
			byteArray.AddPosition( 1 );
		}
	}
}
