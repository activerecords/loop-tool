using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkIbag : RiffChunk
	{
		public const string ID = "ibag";

		public readonly IbagData[] dataArray;

		public RiffChunkIbag( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			dataArray = new IbagData[size / 4];

			for( int i = 0; i * 4 < size; i++ )
			{
				dataArray[i] = new IbagData( aByteArray, informationList );
			}
		}
	}

	public class IbagData
	{
		public readonly UInt16 genNdx;
		public readonly UInt16 modNdx;

		public IbagData( ByteArray aByteArray, List<string> aInformationList )
		{
			genNdx = aByteArray.ReadUInt16();
			modNdx = aByteArray.ReadUInt16();

			aInformationList.Add( "Gen Ndx:" + genNdx );
			aInformationList.Add( "Mod Ndx:" + modNdx );
		}
	}
}
