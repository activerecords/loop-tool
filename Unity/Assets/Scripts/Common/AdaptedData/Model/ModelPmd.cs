using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.FilePool;
using Curan.Common.FormalizedData.File.Mmd.Pmd;

/*
namespace Curan.Common.AdaptedData.Model
{
	public class ModelPmd
	{
		public readonly PmdVertex[] pmdVertexArray;
		public readonly PmdFaceVertex[] pmdFaceVertexArray;
		public readonly Dictionary<int, List<PmdFaceVertex>> pmdFaceVertexListDictionary;
		public readonly PmdMaterial[] pmdMaterialArray;
		public readonly PmdBone[] pmdBoneArray;
		public readonly PmdIk[] pmdIkArray;

		public ModelPmd( FileInfo aFileInfoPmd )
		{
			FileStream lFileStream = new FileStream( aFileInfoPmd.FullName, FileMode.Open, FileAccess.Read );
			PmdFile lPmdFile = new PmdFile( lFileStream );

			pmdVertexArray = new PmdVertex[lPmdFile.chunkVertex.count];
			pmdFaceVertexArray = new PmdFaceVertex[lPmdFile.chunkFaceVertex.count];
			pmdMaterialArray = new PmdMaterial[lPmdFile.chunkMaterial.count];
			pmdBoneArray = new PmdBone[lPmdFile.chunkBone.count];
			pmdIkArray = new PmdIk[lPmdFile.chunkIk.count];
			pmdFaceVertexListDictionary = new Dictionary<int, List<PmdFaceVertex>>();

			for( int i = 0; i < lPmdFile.chunkVertex.count; i++ )
			{
				pmdVertexArray[i] = new PmdVertex( ( PmdVertexData )lPmdFile.chunkVertex.dataArray[i] );
			}

			for( int i = 0; i < lPmdFile.chunkFaceVertex.count; i++ )
			{
				pmdFaceVertexArray[i] = new PmdFaceVertex( ( PmdFaceVertexData )lPmdFile.chunkFaceVertex.dataArray[i] );
			}

			for( int i = 0; i < lPmdFile.chunkMaterial.count; i++ )
			{
				pmdMaterialArray[i] = new PmdMaterial( ( PmdMaterialData )lPmdFile.chunkMaterial.dataArray[i], aFileInfoPmd.DirectoryName );
			}

			for( int i = 0; i < lPmdFile.chunkBone.count; i++ )
			{
				pmdBoneArray[i] = new PmdBone( ( PmdBoneData )lPmdFile.chunkBone.dataArray[i] );
			}

			for( int i = 0; i < lPmdFile.chunkIk.count; i++ )
			{
				pmdIkArray[i] = new PmdIk( ( PmdIkData )lPmdFile.chunkIk.dataArray[i] );
			}

			int lIndexBase = 0;

			for( int i = 0; i < pmdMaterialArray.Length; i++ )
			{
				if( pmdFaceVertexListDictionary.ContainsKey( i ) == false )
				{
					pmdFaceVertexListDictionary.Add( i, new List<PmdFaceVertex>() );
				}

				for( int j = 0; j < ( ( PmdMaterialData )lPmdFile.chunkMaterial.dataArray[i] ).faceVertCount; j++ )
				{
					pmdFaceVertexListDictionary[i].Add( pmdFaceVertexArray[lIndexBase + j] );
				}

				lIndexBase += ( int )( ( PmdMaterialData )lPmdFile.chunkMaterial.dataArray[i] ).faceVertCount;
			}

			for( int i = 0; i < pmdIkArray.Length; i++ )
			{
				pmdIkArray[i].Set( pmdBoneArray );
			}

			for( int i = 0; i < pmdBoneArray.Length; i++ )
			{
				if( pmdBoneArray[i].parentBoneIndex != 0xFFFF )
				{
					pmdBoneArray[i].SetParentBone( pmdBoneArray[pmdBoneArray[i].parentBoneIndex] );
				}
			}
		}

		public void Update()
		{
			for( int i = 0; i < pmdIkArray.Length; i++ )
			{
				pmdIkArray[i].Update();
			}
		}

		public void DrawPerticle()
		{
			/*
			GL.PushMatrix();

			for( int i = 0; i < pmdMaterialArray.Length; i++ )
			{
				pmdMaterialArray[i].material.SetPass( 0 );

				GL.Begin( GL.TRIANGLES );

				for( int j = 0; j < pmdFaceVertexListDictionary[i].Count; j++ )
				{
					pmdVertexArray[( int )pmdFaceVertexListDictionary[i][j].faceVertexIndex].Draw( pmdBoneArray );
				}

				GL.End();
			}

			GL.PopMatrix();

		}
	}
}
*/