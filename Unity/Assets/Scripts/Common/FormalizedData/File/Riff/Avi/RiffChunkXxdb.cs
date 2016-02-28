using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Avi
{
	public class RiffChunkXxdb : RiffChunk
	{
		public const string ID = "00db";

		public readonly byte[] dataArray;

		public RiffChunkXxdb( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			dataArray = aByteArray.ReadBytes( ( int )size );

			informationList.Add( "Data Array:" + dataArray );
		}
	}
}
