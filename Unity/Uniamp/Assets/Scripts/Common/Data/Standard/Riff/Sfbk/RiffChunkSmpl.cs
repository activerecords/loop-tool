using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkSmpl : RiffChunk
	{
		public const string ID = "smpl";

		public RiffChunkSmpl( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			aByteArray.AddPosition( ( int )Size );

			informationList.Add( "Sample Data:" + Size );
		}
	}
}
