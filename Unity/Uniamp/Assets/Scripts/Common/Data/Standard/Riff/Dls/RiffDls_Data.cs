using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Data : RiffChunk
	{
		public const string ID = "data";

		public readonly Byte[] sampleArray;

		public RiffDls_Data( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			informationList.Add( "Sample Array:" + position );

			aByteArray.SetPosition( ( int )( position + Size ) );
		}
	}
}
