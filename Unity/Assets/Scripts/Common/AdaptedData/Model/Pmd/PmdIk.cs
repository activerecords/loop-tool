using System;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Pmd;

using UnityEngine;

namespace Curan.Common.AdaptedData.Model
{
	public class PmdIk
	{
		public UInt16 ikBoneIndex;			// IKボーン番号
		public UInt16 ikTargetBoneIndex;	// IKターゲットボーン番号 // IKボーンが最初に接続するボーン
		public byte ikChainLength;			// IKチェーンの長さ(子の数)
		private UInt16 iterations;			// 再帰演算回数 // IK値1
		private float controlWeight;		// IKの影響度 // IK値2
		public UInt16[] ikChildBoneIndex;	// IK影響下のボーン番号

		private PmdBone ikBone;				// IKボーン
		private PmdBone ikTargetBone;		// IKターゲットボーン // IKボーンが最初に接続するボーン
		private PmdBone[] ikChildBone;		// IK影響下のボーン

		public PmdIk( PmdIkData aPmdIkData )
		{
			ikBoneIndex = aPmdIkData.ikBoneIndex;
			ikTargetBoneIndex = aPmdIkData.ikTargetBoneIndex;
			ikChainLength = aPmdIkData.ikChainLength;
			iterations = aPmdIkData.iterations;
			controlWeight = aPmdIkData.controlWeight;
			ikChildBoneIndex = aPmdIkData.ikChildBoneIndex;
		}

		public void Set( PmdBone[] pmdBoneArray )
		{
			ikBone = pmdBoneArray[ikBoneIndex];
			ikTargetBone = pmdBoneArray[ikTargetBoneIndex];

			ikChildBone = new PmdBone[ikChainLength];

			for( int j = 0; j < ikChainLength; j++ )
			{
				ikChildBone[j] = pmdBoneArray[ikChildBoneIndex[j]];
			}
		}

		public void Update()
		{
			/*
			GameObject gameObject = GameObject.Find( ikBone.boneName );

			if( gameObject != null ){
				gameObject.transform.localPosition = ikBone.GetAbsoutePositoin();
			}

			for( int i = 0; i < iterations; i++ )
			{
				for( int j = 0; j < ikChainLength; j++ )
				{
					// ターゲットボーンまでのベクトル.
					Vector3 vectorTarget = Vector3.Normalize( ikTargetBone.GetAbsoutePositoin() - ikChildBone[j].GetAbsoutePositoin() );

					// IKボーンまでのベクトル.
					Vector3 vectorIk = Vector3.Normalize( ikBone.GetAbsoutePositoin() - ikChildBone[j].GetAbsoutePositoin() );

					Vector3 axis = Vector3.Cross( vectorTarget, vectorIk );
					axis.Normalize();
					float angle = Vector3.Angle( vectorTarget, vectorIk );
					//Quaternion.FromToRotation( ikChildBone[j].GetAbsoutePositoin(), ikTargetBone.GetAbsoutePositoin() );
					//float angle = ( float )Math.Acos( Vector3.Dot( vectorTarget, vectorIk ) ) * 180.0f / ( float )Math.PI;

					if( !float.IsNaN( angle ) && axis != Vector3.zero )
					{
						// 回転量制限.
						angle = Mathf.Clamp( angle, -controlWeight * 4 * 180.0f / ( float )Math.PI, controlWeight * 4 * 180.0f / ( float )Math.PI );

						if( ikChildBone[j].boneName.IndexOf( "ひざ" ) != -1 )
						{
							Quaternion rotation = Quaternion.AngleAxis( angle, axis );
							
							Vector3 eulerAngles = rotation.eulerAngles;
							eulerAngles.x += ikChildBone[j].rotation.eulerAngles.x;
							eulerAngles.y = 0.0f;
							eulerAngles.z = 0.0f;
							
							if( eulerAngles.x > 0.0f && eulerAngles.x < 180.0f )
							{
								eulerAngles.x = 0.0f;
								//Debug.Log( ikChildBone[j].eulerAngles );
							}
							rotation.eulerAngles = eulerAngles;

							ikChildBone[j].SetTransform( Vector3.zero, rotation, Vector3.one );
						}
						else
						{
							Quaternion rotation = Quaternion.AngleAxis( angle, axis );
							ikChildBone[j].MultipleTransform( Vector3.zero, rotation, Vector3.one );
						}
					}
				}
			}
			*/
		}
	}
}
