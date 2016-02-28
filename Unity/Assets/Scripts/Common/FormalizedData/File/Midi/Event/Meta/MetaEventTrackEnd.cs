using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MetaEventTrackEnd : MetaEventBase
	{
		public MetaEventTrackEnd( int aDelta, byte aType, ByteArray byteArray )
			: base( aDelta, aType )
		{
			byteArray.AddPosition( 1 );
		}
	}
}
