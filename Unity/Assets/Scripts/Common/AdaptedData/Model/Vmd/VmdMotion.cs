using UnityEngine;

using System;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Vmd;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Model
{
	public class VmdMotion
	{
		public string boneName;
		public Vector3 location;
		public Quaternion rotation;
		public PmdBone bone;

		public VmdMotion( VmdMotionData aVmdMotionData, PmdBone[] PmdBoneArray )
		{
			boneName = aVmdMotionData.boneName;
			location = aVmdMotionData.location;
			rotation = aVmdMotionData.rotation;

			for( int j = 0; j < PmdBoneArray.Length; j++ )
			{
				if( boneName == PmdBoneArray[j].boneName )
				{
					bone = PmdBoneArray[j];

					break;
				}
			}
		}
	}
}
