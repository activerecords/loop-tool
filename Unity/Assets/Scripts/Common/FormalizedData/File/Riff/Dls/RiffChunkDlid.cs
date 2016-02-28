using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkDlid : RiffChunk
	{
		public const string ID = "dlid";

		public readonly UInt32 data1;
		public readonly UInt16 data2;
		public readonly UInt16 data3;
		public readonly Byte[] data4;

		public RiffChunkDlid( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			data1 = aByteArray.ReadUInt32();
			data2 = aByteArray.ReadUInt16();
			data3 = aByteArray.ReadUInt16();
			data4 = aByteArray.ReadBytes( 8 );

			informationList.Add( "Data1:" + data1 );
			informationList.Add( "Data2:" + data2 );
			informationList.Add( "Data3:" + data3 );
			informationList.Add( "Data4:" + data4 );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( data1 );
			aByteArray.WriteUInt16( data2 );
			aByteArray.WriteUInt16( data3 );
			aByteArray.WriteBytes( data4 );
		}
	}
}
