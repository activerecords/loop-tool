using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdLightData : VmdDataAbstract
	{
		public UInt32 flameNo;
		public Vector3 rgb;
		public Vector3 location;

		private VmdLightData()
		{

		}

		public VmdLightData( ByteArray aByteArray )
		{
			flameNo = aByteArray.ReadUInt32();
			rgb = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			location = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
		}
	}
}
