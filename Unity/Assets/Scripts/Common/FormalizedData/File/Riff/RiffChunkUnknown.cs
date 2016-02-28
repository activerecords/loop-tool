using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Riff
{
	public class RiffChunkUnknown : RiffChunk
	{
		public readonly Byte[] dataArray;

		public RiffChunkUnknown( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			Logger.LogWarning( "Unknown Type" );

			dataArray = aByteArray.ReadBytes( ( int )size );
		}
	}
}
