using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdSkinData : VmdDataAbstract
	{
		public string skinName;
		public UInt32 flameNo;
		public float weight;

		private VmdSkinData()
		{

		}

		public VmdSkinData( ByteArray aByteArray )
		{
			skinName = aByteArray.ReadShiftJisString( 15 );
			flameNo = aByteArray.ReadUInt32();
			weight = aByteArray.ReadSingle();
		}
	}
}
