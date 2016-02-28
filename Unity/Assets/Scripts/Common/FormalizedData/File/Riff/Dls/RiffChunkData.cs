using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkData : RiffChunk
	{
		public const string ID = "data";

		public readonly Byte[] sampleArray;

		public RiffChunkData( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			informationList.Add( "Sample Array:" + position );

			aByteArray.SetPosition( ( int )( position + size ) );
		}
	}
}
