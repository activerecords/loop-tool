using System;
using System.Collections.Generic;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdBoneData : PmdDataAbstract
	{
		public string boneName;					// ボーン名
		public UInt16 parentBoneIndex;			// 親ボーン番号(ない場合は0xFFFF)
		public UInt16 tailPositionBoneIndex;	// tail位置のボーン番号(チェーン末端の場合は0xFFFF) // 親：子は1：多なので、主に位置決め用
		public byte boneType;					// ボーンの種類
		public UInt16 ikParentBoneIndex;		// IKボーン番号(影響IKボーン。ない場合は0)
		public Vector3 boneHeadPosition;		// x, y, z // ボーンのヘッドの位置

		public PmdBoneData( ByteArray aByteArray )
		{
			boneName = aByteArray.ReadShiftJisString( 20 ).Split( '\0' )[0];
			parentBoneIndex = aByteArray.ReadUInt16();
			tailPositionBoneIndex = aByteArray.ReadUInt16();
			boneType = aByteArray.ReadByte();
			ikParentBoneIndex = aByteArray.ReadUInt16();
			boneHeadPosition = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );

			Console.WriteLine( boneName );
		}
	}
}
