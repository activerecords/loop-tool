using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkSmpl : RiffChunk
	{
		public const string ID = "smpl";

		public RiffChunkSmpl( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			aByteArray.AddPosition( ( int )size );

			informationList.Add( "Sample Data:" + size );
		}
	}
}
