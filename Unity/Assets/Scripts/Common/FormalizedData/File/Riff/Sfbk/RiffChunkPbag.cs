using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkPbag : RiffChunk
	{
		public const string ID = "pbag";

		public readonly PbagData[] pbagDataArray;

		public RiffChunkPbag( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			pbagDataArray = new PbagData[size / 4];

			for( int i = 0; i * 4 < size; i++ )
			{
				pbagDataArray[i] = new PbagData( aByteArray, informationList );
			}
		}

		/*
		public void SetGens( int index, int gens )
		{
			pbagDataArray[index].SetGens( gens );
		}

		public void SetMods( int index, int mods )
		{
			pbagDataArray[index].SetMods( mods );
		}

		public int GetGens( int index )
		{
			return pbagDataArray[index].GetGens();
		}

		public int GetMods( int index )
		{
			return pbagDataArray[index].GetMods();
		}*/
	}

	public class PbagData
	{
		public readonly UInt16 genNdx;
		public readonly UInt16 modNdx;

		public PbagData( ByteArray aByteArray, List<string> aInformationList )
		{
			genNdx = aByteArray.ReadUInt16();
			modNdx = aByteArray.ReadUInt16();

			aInformationList.Add( "Gen Ndx:" + genNdx );
			aInformationList.Add( "Mod Ndx:" + modNdx );
		}
	}
}
