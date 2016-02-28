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
	public class ComponentWaveformSmall
	{
		private sbyte[] waveform;
		private MeshFilter meshFilter;
		private MeshRenderer meshRenderer;

		public ComponentWaveformSmall( IPlayer aPlayer, sbyte[] aWaveform )
		{
			waveform = aWaveform;

			Mesh lMesh = new Mesh();

			Vector3[] vertices = new Vector3[1281 * 2];
			int[] lIndices = new int[1281 * 2];

			for( int i = 0; i < lIndices.Length; i++ )
			{
				lIndices[i] = i;
			}

			lMesh.vertices = vertices;
			lMesh.SetIndices( lIndices, MeshTopology.Lines, 0 );
			lMesh.RecalculateBounds();

			GameObject gameObjectWaveformAbstract = GameObject.Find( "WaveformSmall" );
			meshFilter= gameObjectWaveformAbstract.GetComponent<MeshFilter>();
			meshFilter.sharedMesh = lMesh;
			meshFilter.sharedMesh.name = "WaveformSmall";
			meshRenderer = gameObjectWaveformAbstract.GetComponent<MeshRenderer>();
			meshRenderer.material.color = new Color( 0.0f, 0.0f, 1.0f, 0.5f );

			Change( aPlayer.Loop );
		}
		
		public void Set( IPlayer aPlayer, sbyte[] aWaveform )
		{
			waveform = aWaveform;
			Change( aPlayer.Loop );
		}

		public void Change( LoopInformation aLoopInformation )
		{
			Vector3[] lVertices = meshFilter.mesh.vertices;

			int diff = ( int )aLoopInformation.length.sample;
			
			for( int i = 0; i < Screen.width; i++ )
			{
				sbyte lMax = 0;
				sbyte lMin = 0;
				
				sbyte lMaxRight = 0;
				sbyte lMinRight = 0;
				
				for( int j = ( int )( waveform.Length / Screen.width * i ); j < waveform.Length / Screen.width * ( i + 1 ); j += 20 )
				{
					int lIndexRight = j - ( int )aLoopInformation.length.sample;

					if( j >= 0 && j < waveform.Length )
					{
						sbyte lValue = waveform[j];
						
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
				double lY = Screen.height / 2.0d - 1.0d - 130.0d;

				lVertices[i * 2 + 0] = new Vector3( ( float )lX, ( float )( lY + ( float )lMax / 16.0f ), 0.0f );
				lVertices[i * 2 + 1] = new Vector3( ( float )lX, ( float )( lY + ( float )lMin / 16.0f ), 0.0f );
			}
			
			meshFilter.mesh.vertices = lVertices;
			meshFilter.mesh.RecalculateBounds();
		}
	}
}
