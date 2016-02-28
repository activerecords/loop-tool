using System;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Pmd;

using UnityEngine;

namespace Curan.Common.AdaptedData.Model
{
	public class PmdVertex
	{
		//public Vector3 position;		// x, y, z // 座標
		public Vector3 normalVector;	// nx, ny, nz // 法線ベクトル

		public Vector2 uv;				// u, v // UV座標 // MMDは頂点UV
		public UInt16 boneNum1;			// ボーン番号1 // モデル変形(頂点移動)時に影響
		public UInt16 boneNum2;			// ボーン番号2 // モデル変形(頂点移動)時に影響
		/*
		public byte boneWeight;			// ボーン1に与える影響度 // min:0 max:100 // ボーン2への影響度は、(100 - bone_weight)
		public byte edgeFlag;			// 0:通常、1:エッジ無効 // エッジ(輪郭)が有効の場合
		*/
		public float bone1Weight;
		public float bone2Weight;

		public Vector4 position4;
		public Vector4 calcPosition;

		public PmdVertex( PmdVertexData aPmdVertexData )
		{
			//position = new Vector3( lByteArray.ReadSingle(), lByteArray.ReadSingle(), lByteArray.ReadSingle() );

			normalVector = aPmdVertexData.normalVector;
			uv = aPmdVertexData.uv;
			boneNum1 = aPmdVertexData.boneNum1;
			boneNum2 = aPmdVertexData.boneNum2;

			/*
			boneWeight = lByteArray.ReadByte();
			edgeFlag = lByteArray.ReadByte();
			*/

			bone1Weight = aPmdVertexData.boneWeight / 100.0f;
			bone2Weight = ( 100.0f - aPmdVertexData.boneWeight ) / 100.0f;

			position4 = new Vector4( aPmdVertexData.position.x, aPmdVertexData.position.y, aPmdVertexData.position.z, 1.0f );
		}

		public void Draw( PmdBone[] pmdBoneArray )
		{
			//PmdBone lPmdBone1 = new PmdBone( pmdBoneArray[boneNum1] );
			//PmdBone lPmdBone2 = new PmdBone( pmdBoneArray[boneNum2] );

			Matrix4x4 transform1 = pmdBoneArray[boneNum1].GetTransform();
			Matrix4x4 transform2 = pmdBoneArray[boneNum2].GetTransform();
			//Matrix4x4 transform1 = lPmdBone1.GetTransform();
			//Matrix4x4 transform2 = lPmdBone2.GetTransform();

			Vector4 calcPosition = ( transform1 * position4 * bone1Weight ) + ( transform2 * position4 * bone2Weight );

			//			GL.TexCoord( uv );
			//			GL.Vertex( calcPosition );
		}
	}
}
