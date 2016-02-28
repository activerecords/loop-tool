using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Form
{
	public class FormChunkUnknown : FormChunk
	{
		public readonly Byte[] dataArray;

		public FormChunkUnknown( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			Logger.LogWarning( "Unknown Type" );

			if( position + size < aByteArray.Length )
			{
				dataArray = aByteArray.ReadBytes( ( int )size );
			}
		}
	}
}
