using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkLabl : RiffChunk
	{
		public const string ID = "labl";

		public readonly UInt32 name;
		public readonly string data;

		public RiffChunkLabl( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			name = aByteArray.ReadUInt32();
			data = aByteArray.ReadString( ( int )size - 4 );

			informationList.Add( "    Name:" + name );
			informationList.Add( "    Data:" + data );
		}
	}
}
