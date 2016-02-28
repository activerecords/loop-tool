using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdSelfShadowData : VmdDataAbstract
	{
		public UInt32 flameNo;
		public byte mode;
		public float distance;

		private VmdSelfShadowData()
		{

		}

		public VmdSelfShadowData( ByteArray aByteArray )
		{
			flameNo = aByteArray.ReadUInt32();
			mode = aByteArray.ReadByte();
			distance = aByteArray.ReadSingle();
		}
	}
}
