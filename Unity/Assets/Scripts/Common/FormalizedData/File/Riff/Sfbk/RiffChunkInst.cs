using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkInst : RiffChunk
	{
		public const string ID = "inst";

		public readonly InstData[] instDataArray;

		public RiffChunkInst( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			instDataArray = new InstData[size / 22];

			for( int i = 0; i * 22 < size; i++ )
			{
				instDataArray[i] = new InstData( aByteArray, informationList );
			}
		}
	}

	public class InstData
	{
		public readonly string name;
		public readonly UInt16 bagNdx;

		public InstData( ByteArray aByteArray, List<string> aInformationList )
		{
			name = aByteArray.ReadString( 20 );
			bagNdx = aByteArray.ReadUInt16();

			aInformationList.Add( "name:" + name );
			aInformationList.Add( "Bag Ndx:" + bagNdx );
		}
	}
}
