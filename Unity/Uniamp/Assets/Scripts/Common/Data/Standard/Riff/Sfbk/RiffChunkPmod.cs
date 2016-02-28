using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkPmod : RiffChunk
	{
		public const string ID = "pmod";

		public readonly PmodData[] pmodDataArray;

		public RiffChunkPmod( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			pmodDataArray = new PmodData[Size / 10];

			for( int i = 0; i * 10 < Size; i++ )
			{
				pmodDataArray[i] = new PmodData( aByteArray, informationList );
			}
		}
	}

	public class PmodData
	{
		public readonly SFModulator modSrcOper;
		public readonly SFGenerator modDestOper;
		public readonly UInt16 amount;
		public readonly SFModulator modAmtSrcOper;
		public readonly SFTransform modTransOper;

		public PmodData( AByteArray aByteArray, List<string> aInformationList )
		{
			modSrcOper = ( SFModulator )aByteArray.ReadUInt16();
			modDestOper = ( SFGenerator )aByteArray.ReadUInt16();
			amount = aByteArray.ReadUInt16();
			//UInt16 amount = aByteArray.ReadUInt16();
			modAmtSrcOper = ( SFModulator )aByteArray.ReadUInt16();
			modTransOper = ( SFTransform )aByteArray.ReadUInt16();

			aInformationList.Add( "Mod Src Oper:" + modSrcOper );
			aInformationList.Add( "Mod Dest Oper:" + modDestOper );
			aInformationList.Add( "Amount:" + amount );
			aInformationList.Add( "Mod Amt Src Oper:" + modAmtSrcOper );
			aInformationList.Add( "Mod Trans Oper:" + modTransOper );
		}
	}
}
