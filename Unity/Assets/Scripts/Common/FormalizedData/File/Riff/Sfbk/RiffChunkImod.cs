using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkImod : RiffChunk
	{
		public const string ID = "imod";

		public readonly ImodData[] imodDataArray;

		public RiffChunkImod( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			imodDataArray = new ImodData[size / 10];

			for( int i = 0; i * 10 < size; i++ )
			{
				imodDataArray[i] = new ImodData( aByteArray, informationList );
			}
		}
	}

	public class ImodData
	{
		public readonly SFModulator modSrcOper;
		public readonly SFGenerator modDestOper;
		public readonly UInt16 amount;
		public readonly SFModulator modAmtSrcOper;
		public readonly SFTransform modTransOper;

		public ImodData( ByteArray aByteArray, List<string> aInformationList )
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
