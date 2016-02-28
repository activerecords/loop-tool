using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffInfoIbag : RiffChunk
	{
		public const string ID = "ibag";

		public readonly IbagData[] dataArray;

		public RiffInfoIbag( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			dataArray = new IbagData[Size / 4];

			for( int i = 0; i * 4 < Size; i++ )
			{
				dataArray[i] = new IbagData( aByteArray, informationList );
			}
		}
	}

	public class IbagData
	{
		public readonly UInt16 genNdx;
		public readonly UInt16 modNdx;

		public IbagData( AByteArray aByteArray, List<string> aInformationList )
		{
			genNdx = aByteArray.ReadUInt16();
			modNdx = aByteArray.ReadUInt16();

			aInformationList.Add( "Gen Ndx:" + genNdx );
			aInformationList.Add( "Mod Ndx:" + modNdx );
		}
	}
}
