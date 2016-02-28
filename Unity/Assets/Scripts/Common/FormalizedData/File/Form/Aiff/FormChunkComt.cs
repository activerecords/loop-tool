using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Form.Aiff
{
	public class FormChunkComt : FormChunk
	{
		public const string ID = "COMT";

		public FormChunkComt( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			aByteArray.AddPosition( ( int )size );
		}
	}
}
