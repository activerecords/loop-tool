using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Monoamp.Common.Component.Sound.Player;
using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Data.Application.Sound;
using Monoamp.Common.Data.Standard.Riff.Wave;
using Monoamp.Common.Utility;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Unity.View
{
	public class ComponentWaveform
	{
		private sbyte[] waveform;

		private MeshFilter meshFilterDetail;
		private MeshFilter meshFilterDetailLeft;
		private MeshFilter meshFilterDetailRight;
		
		private MeshRenderer meshRendererDetail;
		private MeshRenderer meshRendererDetailLeft;
		private MeshRenderer meshRendererDetailRight;

		private ObjectWaveform objectWaveformDetailLeft;
		private ObjectWaveform objectWaveformDetailRight;

		public ComponentWaveform( IPlayer aPlayer, sbyte[] aWaveform )
		{
			waveform = aWaveform;

			Mesh lMeshDetail = new Mesh();
			Mesh lMeshDetailLeft = new Mesh();
			Mesh lMeshDetailRight = new Mesh();

			Vector3[] vertices = new Vector3[1281 * 2];
			int[] lIndices = new int[1281 * 2];

			for( int i = 0; i < lIndices.Length; i++ )
			{
				lIndices[i] = i;
			}
			
			lMeshDetail.vertices = vertices;
			lMeshDetail.SetIndices( lIndices, MeshTopology.Lines, 0 );
			lMeshDetail.RecalculateBounds();
			
			lMeshDetailLeft.vertices = vertices;
			lMeshDetailLeft.SetIndices( lIndices, MeshTopology.Lines, 0 );
			lMeshDetailLeft.RecalculateBounds();

			lMeshDetailRight.vertices = vertices;
			lMeshDetailRight.SetIndices( lIndices, MeshTopology.Lines, 0 );
			lMeshDetailRight.RecalculateBounds();

			GameObject gameObjectWaveform = GameObject.Find( "Waveform" );
			GameObject gameObjectWaveformLeft = GameObject.Find( "WaveformLeft" );
			GameObject gameObjectWaveformRight = GameObject.Find( "WaveformRight" );

			meshFilterDetail = gameObjectWaveform.GetComponent<MeshFilter>();
			meshFilterDetailLeft = gameObjectWaveformLeft.GetComponent<MeshFilter>();
			meshFilterDetailRight = gameObjectWaveformRight.GetComponent<MeshFilter>();

			meshFilterDetail.sharedMesh = lMeshDetail;
			meshFilterDetailLeft.sharedMesh = lMeshDetailLeft;
			meshFilterDetailRight.sharedMesh = lMeshDetailRight;
			
			meshFilterDetail.sharedMesh.name = "Waveform";
			meshFilterDetailLeft.sharedMesh.name = "WaveformLeft";
			meshFilterDetailRight.sharedMesh.name = "WaveformRight";
			
			meshRendererDetail = gameObjectWaveform.GetComponent<MeshRenderer>();
			meshRendererDetailLeft = gameObjectWaveformLeft.GetComponent<MeshRenderer>();
			meshRendererDetailRight = gameObjectWaveformRight.GetComponent<MeshRenderer>();

			meshRendererDetail.material.color = new Color( 0.0f, 0.0f, 1.0f, 0.5f );
			meshRendererDetailLeft.material.color = new Color( 0.0f, 1.0f, 0.0f, 0.5f );
			meshRendererDetailRight.material.color = new Color( 1.0f, 0.0f, 0.0f, 0.5f );
			
			objectWaveformDetailLeft = new ObjectWaveform( gameObjectWaveformLeft.transform, meshFilterDetailLeft );
			objectWaveformDetailRight = new ObjectWaveform( gameObjectWaveformRight.transform, meshFilterDetailRight );
			
			ChangeScale( aPlayer.Loop, 1.0f, 0.0f );
		}
		
		public void Set( IPlayer aPlayer, sbyte[] aWaveform )
		{
			waveform = aWaveform;
			ChangeScale( aPlayer.Loop, 1.0f, 0.0f );
		}

		public void UpdateVertex( IPlayer aPlayer, float scale, float positionWaveform )
		{
			objectWaveformDetailLeft.SetLoop( aPlayer.Loop, ( int )aPlayer.GetLength().sample, scale, positionWaveform );
			objectWaveformDetailRight.SetLoop( aPlayer.Loop, ( int )aPlayer.GetLength().sample, scale, positionWaveform - aPlayer.Loop.length.sample / aPlayer.GetLength().sample );
		}
		
		public void ChangeScale( LoopInformation aLoopInformation, float scale, float positionWaveform )
		{
			Vector3[] lVertices = meshFilterDetail.mesh.vertices;
			Vector3[] lVerticesRight = meshFilterDetailRight.mesh.vertices;

			int diff = ( int )aLoopInformation.length.sample;// % ( waveform.Length / Screen.width );

			for( int i = 0; i < Screen.width; i++ )
			{
				sbyte lMax = 0;
				sbyte lMin = 0;
				
				sbyte lMaxRight = 0;
				sbyte lMinRight = 0;

				for( int j = ( int )( waveform.Length / Screen.width * i / scale ); j < waveform.Length / Screen.width * ( i + 1 ) / scale; j += ( int )Math.Ceiling( 20.0d / scale ) )
				{
					int lIndex = ( int )( waveform.Length * positionWaveform + j );
					int lIndexRight = ( int )( waveform.Length * positionWaveform + j - diff );
					
					if( lIndex >= 0 && lIndex < waveform.Length )
					{
						sbyte lValue = waveform[lIndex];

						if( lValue > lMax )
						{
							lMax = lValue;
						}

						if( lValue < lMin )
						{
							lMin = lValue;
						}
					}

					if( lIndexRight >= 0 && lIndexRight < waveform.Length )
					{
						sbyte lValue = waveform[lIndexRight];
						
						if( lValue > lMaxRight )
						{
							lMaxRight = lValue;
						}
						
						if( lValue < lMinRight )
						{
							lMinRight = lValue;
						}
					}
				}

				double lX = -Screen.width / 2.0d + i;
				double lY = Screen.height / 2.0d - 1.0d - GuiSettings.GuiSettingLoopEditor.seekbarTop - GuiStyleSet.StylePlayer.seekbar.fixedHeight / 2.0d;
				
				lVertices[i * 2 + 0] = new Vector3( ( float )lX, ( float )( lY + ( float )lMax / 4.0f ), 0.0f );
				lVertices[i * 2 + 1] = new Vector3( ( float )lX, ( float )( lY + ( float )lMin / 4.0f ), 0.0f );
				
				lVerticesRight[i * 2 + 0] = new Vector3( ( float )lX, ( float )( lY + ( float )lMaxRight / 4.0f ), 0.0f );
				lVerticesRight[i * 2 + 1] = new Vector3( ( float )lX, ( float )( lY + ( float )lMinRight / 4.0f ), 0.0f );
			}
			
			meshFilterDetail.mesh.vertices = lVertices;
			meshFilterDetail.mesh.RecalculateBounds();
			
			meshFilterDetailLeft.mesh.vertices = lVertices;
			meshFilterDetailLeft.mesh.RecalculateBounds();

			meshFilterDetailRight.mesh.vertices = lVerticesRight;
			meshFilterDetailRight.mesh.RecalculateBounds();
		}

		public void ChangeLoop( LoopInformation aLoopInformation, float scale, float positionWaveform )
		{
			Vector3[] lVerticesRight = meshFilterDetailRight.mesh.vertices;

			int diff = ( int )aLoopInformation.length.sample;// % ( waveform.Length / Screen.width );
			
			for( int i = 0; i < Screen.width; i++ )
			{
				sbyte lMaxRight = 0;
				sbyte lMinRight = 0;
				
				for( int j = ( int )( waveform.Length / Screen.width * i / scale ); j < waveform.Length / Screen.width * ( i + 1 ) / scale; j += ( int )Math.Ceiling( 20.0d / scale ) )
				{
					int lIndexRight = ( int )( waveform.Length * positionWaveform + j - diff );

					if( lIndexRight >= 0 && lIndexRight < waveform.Length )
					{
						sbyte lValue = waveform[lIndexRight];
						
						if( lValue > lMaxRight )
						{
							lMaxRight = lValue;
						}
						
						if( lValue < lMinRight )
						{
							lMinRight = lValue;
						}
					}
				}

				double lX = -Screen.width / 2.0d + i;
				double lY = Screen.height / 2.0d - 1.0d - GuiSettings.GuiSettingLoopEditor.seekbarTop - GuiStyleSet.StylePlayer.seekbar.fixedHeight / 2.0d;

				lVerticesRight[i * 2 + 0] = new Vector3( ( float )lX, ( float )( lY + ( float )lMaxRight / 4.0f ), 0.0f );
				lVerticesRight[i * 2 + 1] = new Vector3( ( float )lX, ( float )( lY + ( float )lMinRight / 4.0f ), 0.0f );
			}

			meshFilterDetailRight.mesh.vertices = lVerticesRight;
			meshFilterDetailRight.mesh.RecalculateBounds();
		}
	}
}
