using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Pmd;

using UnityEngine;

namespace Curan.Common.AdaptedData.Model
{
	public class PmdBone
	{
		public string boneName;					// ボーン名
		public UInt16 parentBoneIndex;			// 親ボーン番号(ない場合は0xFFFF)
		/*
		public UInt16 tailPositionBoneIndex;	// tail位置のボーン番号(チェーン末端の場合は0xFFFF) // 親：子は1：多なので、主に位置決め用
		public byte boneType;					// ボーンの種類
		public UInt16 ikParentBoneIndex;		// IKボーン番号(影響IKボーン。ない場合は0)
		public Vector3 boneHeadPosition;		// x, y, z // ボーンのヘッドの位置
		*/
		public Matrix4x4 matrix;
		public Matrix4x4 absoluteMatrix;
		public Matrix4x4 offset;
		public Matrix4x4 parentOffset;
		public PmdBone parentBone;
		public Matrix4x4 localTransform;
		public Matrix4x4 transformMatrix;
		public Matrix4x4 transform;

		public Quaternion rotation;
		private List<PmdBone> childBoneList;

		public PmdBone( PmdBoneData aPmdBoneData )
		{
			boneName = aPmdBoneData.boneName;
			parentBoneIndex = aPmdBoneData.parentBoneIndex;
			/*
			tailPositionBoneIndex = aByteArray.ReadUInt16();
			boneType = aByteArray.ReadByte();
			ikParentBoneIndex = aByteArray.ReadUInt16();
			boneHeadPosition = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			*/
			//Debug.Log( "\"" + boneName + "\"" );

			//			matrix = Matrix4x4.TRS( aPmdBoneData.boneHeadPosition, Quaternion.identity, Vector3.one );
			absoluteMatrix = matrix;
			//			offset = matrix.inverse;
			parentOffset = Matrix4x4.identity;
			localTransform = Matrix4x4.identity;
			transform = Matrix4x4.identity;
			transformMatrix = Matrix4x4.identity;
			
			rotation = Quaternion.identity;
			childBoneList = new List<PmdBone>();
		}

		public void SetParentBone( PmdBone parent )
		{
			parentBone = parent;
			parentOffset = parentBone.offset;
			matrix *= parentOffset;

			parentBone.AddChildBone( this );
		}

		public void AddChildBone( PmdBone child )
		{
			childBoneList.Add( child );
		}

		public void SetTransform( Vector3 aLocation, Quaternion aRotation, Vector3 aScale )
		{
			rotation = aRotation;

			//			localTransform.SetTRS( aLocation, aRotation, aScale );

			SetTransform();
		}

		public void MultipleTransform( Vector3 aLocation, Quaternion aRotation, Vector3 aScale )
		{
			rotation *= aRotation;

			//			localTransform *= Matrix4x4.TRS( aLocation, aRotation, aScale );

			SetTransform();
		}

		public void SetTransform()
		{
			if( parentBone != null )
			{
				transformMatrix = parentBone.transformMatrix * matrix * localTransform;
			}
			else
			{
				transformMatrix = matrix * localTransform;
			}

			transform = transformMatrix * offset;
			
			for( int i = 0; i < childBoneList.Count; i++ )
			{
				childBoneList[i].SetTransform();
			}
		}

		public Matrix4x4 GetTransform()
		{
			return transform;
		}

		public Vector3 GetAbsoutePositoin()
		{
			Vector4 tempPosition = new Vector4( 0.0f, 0.0f, 0.0f, 1.0f );

			return transformMatrix * tempPosition;
		}
	}
}
