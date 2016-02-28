using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffInfoInst : RiffChunk
	{
		public const string ID = "inst";

		public readonly InstData[] instDataArray;

		public RiffInfoInst( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			instDataArray = new InstData[Size / 22];

			for( int i = 0; i * 22 < Size; i++ )
			{
				instDataArray[i] = new InstData( aByteArray, informationList );
			}
		}
	}

	public class InstData
	{
		public readonly string name;
		public readonly UInt16 bagNdx;

		public InstData( AByteArray aByteArray, List<string> aInformationList )
		{
			name = aByteArray.ReadString( 20 );
			bagNdx = aByteArray.ReadUInt16();

			aInformationList.Add( "name:" + name );
			aInformationList.Add( "Bag Ndx:" + bagNdx );
		}
	}
}
