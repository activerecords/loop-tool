using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Form.Aiff
{
	public class FormChunkChan : FormChunk
	{
		public const string ID = "CHAN";

		public FormChunkChan( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			aByteArray.AddPosition( ( int )size );
		}
	}
}
