using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdIkData : PmdDataAbstract
	{
		public UInt16 ikBoneIndex;			// IKボーン番号
		public UInt16 ikTargetBoneIndex;	// IKターゲットボーン番号 // IKボーンが最初に接続するボーン
		public byte ikChainLength;			// IKチェーンの長さ(子の数)
		public UInt16 iterations;			// 再帰演算回数 // IK値1
		public float controlWeight;			// IKの影響度 // IK値2
		public UInt16[] ikChildBoneIndex;	// IK影響下のボーン番号

		public PmdIkData( ByteArray aByteArray )
		{
			ikBoneIndex = aByteArray.ReadUInt16();
			ikTargetBoneIndex = aByteArray.ReadUInt16();
			ikChainLength = aByteArray.ReadByte();
			iterations = aByteArray.ReadUInt16();
			controlWeight = aByteArray.ReadSingle();

			ikChildBoneIndex = new UInt16[ikChainLength];

			for( int i = 0; i < ikChainLength; i++ )
			{
				ikChildBoneIndex[i] = aByteArray.ReadUInt16();
			}
		}
	}
}
