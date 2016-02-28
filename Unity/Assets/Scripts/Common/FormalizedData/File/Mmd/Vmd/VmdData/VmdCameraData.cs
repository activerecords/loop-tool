using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdCameraData : VmdDataAbstract
	{
		public UInt32 flameNo;
		public float length;
		public Vector3 location;
		public Quaternion rotation;
		public byte[] interpolation;
		public UInt32 viewingAngle;
		public byte perspective;

		private VmdCameraData()
		{

		}

		public VmdCameraData( ByteArray aByteArray )
		{
			flameNo = aByteArray.ReadUInt32();
			length = aByteArray.ReadSingle();
			location = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			rotation = new Quaternion( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			interpolation = aByteArray.ReadBytes( 24 );
			viewingAngle = aByteArray.ReadUInt32();
			perspective = aByteArray.ReadUByte();
		}
	}
}
