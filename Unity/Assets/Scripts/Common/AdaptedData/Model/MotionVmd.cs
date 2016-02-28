using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Mmd.Vmd;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Model
{
	/*
	public class MotionVmd
	{
		private VmdMotion[] vmdMotionArray;
		private Dictionary<int, List<VmdMotion>> motionListDictionary;

		private int frame;

		public MotionVmd( FileInfo aFileInfoVmd, ModelPmd aModelPmd )
		{
			FileStream lFileStream = new FileStream( aFileInfoVmd.FullName, FileMode.Open, FileAccess.Read );
			VmdFile lVmdFile = new VmdFile( lFileStream );

			vmdMotionArray = new VmdMotion[lVmdFile.chunkMotion.count];

			for( int i = 0; i < lVmdFile.chunkMotion.count; i++ )
			{
				vmdMotionArray[i] = new VmdMotion( ( VmdMotionData )lVmdFile.chunkMotion.dataArray[i], aModelPmd.pmdBoneArray );
			}

			motionListDictionary = new Dictionary<int, List<VmdMotion>>();

			for( int i = 0; i < lVmdFile.chunkMotion.count; i++ )
			{
				int lFlameNo = ( int )( ( VmdMotionData )lVmdFile.chunkMotion.dataArray[i] ).flameNo;

				if( motionListDictionary.ContainsKey( lFlameNo ) == false )
				{
					motionListDictionary.Add( lFlameNo, new List<VmdMotion>() );
				}

				motionListDictionary[lFlameNo].Add( vmdMotionArray[i] );
			}

			frame = 0;
		}

		public void Update( ModelPmd aModelPmd )
		{
			Logger.LogNormal( "Update:" + frame );

			if( frame < 8000 )
			{
				if( motionListDictionary.ContainsKey( frame ) == true )
				{
					for( int i = 0; i < motionListDictionary[frame].Count; i++ )
					{
						if( motionListDictionary[frame][i].bone != null )
						{
							motionListDictionary[frame][i].bone.SetTransform( motionListDictionary[frame][i].location, motionListDictionary[frame][i].rotation, Vector3.one );
						}
					}
				}
			}

			frame++;
		}
	}*/
}
