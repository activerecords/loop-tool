using System;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdVertexData : PmdDataAbstract
	{
		public Vector3 position;		// x, y, z // 座標
		public Vector3 normalVector;	// nx, ny, nz // 法線ベクトル
		public Vector2 uv;				// u, v // UV座標 // MMDは頂点UV
		public UInt16 boneNum1;			// ボーン番号1 // モデル変形(頂点移動)時に影響
		public UInt16 boneNum2;			// ボーン番号2 // モデル変形(頂点移動)時に影響
		public byte boneWeight;			// ボーン1に与える影響度 // min:0 max:100 // ボーン2への影響度は、(100 - bone_weight)
		public byte edgeFlag;			// 0:通常、1:エッジ無効 // エッジ(輪郭)が有効の場合

		public PmdVertexData( ByteArray lByteArray )
		{
			position = new Vector3( lByteArray.ReadSingle(), lByteArray.ReadSingle(), lByteArray.ReadSingle() );
			normalVector = new Vector3( lByteArray.ReadSingle(), lByteArray.ReadSingle(), lByteArray.ReadSingle() );
			uv = new Vector2( lByteArray.ReadSingle(), 1.0f - lByteArray.ReadSingle() );
			boneNum1 = lByteArray.ReadUInt16();
			boneNum2 = lByteArray.ReadUInt16();
			boneWeight = lByteArray.ReadByte();
			edgeFlag = lByteArray.ReadByte();
		}
	}
}
