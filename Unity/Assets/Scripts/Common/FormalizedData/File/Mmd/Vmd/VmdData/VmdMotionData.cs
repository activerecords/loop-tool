using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdMotionData : VmdDataAbstract
	{
		public string boneName;
		public UInt32 flameNo;
		public Vector3 location;
		public Quaternion rotation;
		public byte[] interpolation;
		
		private VmdMotionData()
		{

		}

		public VmdMotionData( ByteArray aByteArray )
		{
			boneName = aByteArray.ReadShiftJisString( 15 ).Split( '\0' )[0];
			flameNo = aByteArray.ReadUInt32();
			location = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			rotation = new Quaternion( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			interpolation = aByteArray.ReadBytes( 64 );
		}
	}
}
