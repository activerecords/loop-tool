using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Monoamp.Common.Utility;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Unity.View
{
	public class ComponentGui
	{
		private MeshFilter meshFilter;
		private MeshRenderer meshRenderer;
		private int index;

		public ComponentGui()
		{
			Mesh lMesh = new Mesh();

			Vector3[] vertices = new Vector3[1281];
			int[] lIndices = new int[1281];
			Vector2[] lUv = new Vector2[1281];

			for( int i = 0; i < lIndices.Length; i++ )
			{
				lIndices[i] = i;
			}

			lMesh.vertices = vertices;

			lMesh.triangles = new int[]
			{
				2, 1, 0,
				1, 2, 3,
				4, 3, 2,
				3, 4, 5,
				6, 5, 4,
				5, 6, 7,

				8, 7, 6,
				7, 8, 9,

				10, 9, 8,
				9, 10, 11,
				12, 11, 10,
				11, 12, 13,
				14, 13, 12,
				13, 14, 15,

				16, 15, 14,
				15, 16, 17,

				18, 17, 16,
				17, 18, 19,
				20, 19, 18,
				19, 20, 21,
				22, 21, 20,
				21, 22, 23,
				
				24, 23, 22,
				23, 24, 25,

				26, 25, 24,
				25, 26, 27,
				28, 27, 26,
				27, 28, 29,
				30, 29, 28,
				29, 30, 31,

				32, 31, 30,
				31, 32, 33,

				34, 33, 32,
				33, 34, 35,
				36, 35, 34,
				35, 36, 37,
				38, 37, 36,
				37, 38, 39,
				
				40, 39, 38,
				39, 40, 41,

				42, 41, 40,
				41, 42, 43,
				44, 43, 42,
				43, 44, 45,
				46, 45, 44,
				45, 46, 47,
				
				48, 47, 46,
				47, 48, 49,

				50, 49, 48,
				49, 50, 51,
				52, 51, 50,
				51, 52, 53,
				54, 53, 52,
				53, 54, 55,
				
				56, 55, 54,
				55, 56, 57,

				58, 57, 56,
				57, 58, 59,
				60, 59, 58,
				59, 60, 61,
				62, 61, 60,
				61, 62, 63,
				
				64, 63, 62,
				63, 64, 65,

				66, 65, 64,
				65, 66, 67,
				68, 67, 66,
				67, 68, 69,
				70, 69, 68,
				69, 70, 71,
				
				72, 71, 70,
				71, 72, 73,

				74, 73, 72,
				73, 74, 75,
				76, 75, 74,
				75, 76, 77,
				78, 77, 76,
				77, 78, 79,
				
				80, 79, 78,
				79, 80, 81,

				82, 81, 80,
				81, 82, 83,
				84, 83, 82,
				83, 84, 85,
				86, 85, 84,
				85, 86, 87,
				
				88, 87, 86,
				87, 88, 89,

				90, 89, 88,
				89, 90, 91,
				92, 91, 90,
				91, 92, 93,
				94, 93, 92,
				93, 94, 95,
			};

			lMesh.uv = lUv;
			//lMesh.SetIndices( lIndices, MeshTopology.Triangles, 0 );
			lMesh.RecalculateBounds();

			GameObject gameObject = GameObject.Find( "Gui" );

			meshFilter = gameObject.GetComponent<MeshFilter>();
			meshFilter.sharedMesh = lMesh;
			meshFilter.sharedMesh.name = "Gui";
			
			meshRenderer = gameObject.GetComponent<MeshRenderer>();
			meshRenderer.material.color = new Color( 0.0f, 0.0f, 1.0f, 1.0f );

			index = 0;
		}

		public void DrawUiTexture( Rect aRect, GUIStyle aStyle )
		{
			RectOffset lBorder = aStyle.border;
			Texture2D lTexture = aStyle.normal.background;
			
			float x1 = aRect.x;
			float x2 = aRect.x + lBorder.left;
			float x3 = aRect.x + aRect.width - lBorder.right;
			float x4 = aRect.x + aRect.width;
			
			float y4 = aRect.y;
			float y3 = aRect.y + lBorder.top;
			float y2 = aRect.y + aRect.height - lBorder.bottom;
			float y1 = aRect.y + aRect.height;
			
			Vector3 vertex11 = ScreenToWorldPoint( new Vector3( x1, y1, 0.1f ) );
			Vector3 vertex21 = ScreenToWorldPoint( new Vector3( x2, y1, 0.1f ) );
			Vector3 vertex31 = ScreenToWorldPoint( new Vector3( x3, y1, 0.1f ) );
			Vector3 vertex41 = ScreenToWorldPoint( new Vector3( x4, y1, 0.1f ) );
			
			Vector3 vertex12 = ScreenToWorldPoint( new Vector3( x1, y2, 0.1f ) );
			Vector3 vertex22 = ScreenToWorldPoint( new Vector3( x2, y2, 0.1f ) );
			Vector3 vertex32 = ScreenToWorldPoint( new Vector3( x3, y2, 0.1f ) );
			Vector3 vertex42 = ScreenToWorldPoint( new Vector3( x4, y2, 0.1f ) );
			
			Vector3 vertex13 = ScreenToWorldPoint( new Vector3( x1, y3, 0.1f ) );
			Vector3 vertex23 = ScreenToWorldPoint( new Vector3( x2, y3, 0.1f ) );
			Vector3 vertex33 = ScreenToWorldPoint( new Vector3( x3, y3, 0.1f ) );
			Vector3 vertex43 = ScreenToWorldPoint( new Vector3( x4, y3, 0.1f ) );
			
			Vector3 vertex14 = ScreenToWorldPoint( new Vector3( x1, y4, 0.1f ) );
			Vector3 vertex24 = ScreenToWorldPoint( new Vector3( x2, y4, 0.1f ) );
			Vector3 vertex34 = ScreenToWorldPoint( new Vector3( x3, y4, 0.1f ) );
			Vector3 vertex44 = ScreenToWorldPoint( new Vector3( x4, y4, 0.1f ) );
			
			float u1 = 0.0f;
			float u2 = ( float )lBorder.left / ( float )lTexture.width;
			float u3 = 1.0f - ( float )lBorder.right / ( float )lTexture.width;
			float u4 = 1.0f;
			
			float v1 = 0.0f;
			float v2 = ( float )lBorder.top / ( float )lTexture.height;
			float v3 = 1.0f - ( float )lBorder.bottom / ( float )lTexture.height;
			float v4 = 1.0f;
			
			Vector3 texcoord11 = new Vector3( u1, v1, 0.1f );
			Vector3 texcoord21 = new Vector3( u2, v1, 0.1f );
			Vector3 texcoord31 = new Vector3( u3, v1, 0.1f );
			Vector3 texcoord41 = new Vector3( u4, v1, 0.1f );
			
			Vector3 texcoord12 = new Vector3( u1, v2, 0.1f );
			Vector3 texcoord22 = new Vector3( u2, v2, 0.1f );
			Vector3 texcoord32 = new Vector3( u3, v2, 0.1f );
			Vector3 texcoord42 = new Vector3( u4, v2, 0.1f );
			
			Vector3 texcoord13 = new Vector3( u1, v3, 0.1f );
			Vector3 texcoord23 = new Vector3( u2, v3, 0.1f );
			Vector3 texcoord33 = new Vector3( u3, v3, 0.1f );
			Vector3 texcoord43 = new Vector3( u4, v3, 0.1f );
			
			Vector3 texcoord14 = new Vector3( u1, v4, 0.1f );
			Vector3 texcoord24 = new Vector3( u2, v4, 0.1f );
			Vector3 texcoord34 = new Vector3( u3, v4, 0.1f );
			Vector3 texcoord44 = new Vector3( u4, v4, 0.1f );

			GL.TexCoord( texcoord11 );
			GL.Vertex( vertex11 );
			GL.TexCoord( texcoord12 );
			GL.Vertex( vertex12 );
			GL.TexCoord( texcoord21 );
			GL.Vertex( vertex21 );
			GL.TexCoord( texcoord22 );
			GL.Vertex( vertex22 );
			GL.TexCoord( texcoord31 );
			GL.Vertex( vertex31 );
			GL.TexCoord( texcoord32 );
			GL.Vertex( vertex32 );
			GL.TexCoord( texcoord41 );
			GL.Vertex( vertex41 );
			GL.TexCoord( texcoord42 );
			GL.Vertex( vertex42 );

			GL.TexCoord( texcoord12 );
			GL.Vertex( vertex12 );
			GL.TexCoord( texcoord13 );
			GL.Vertex( vertex13 );
			GL.TexCoord( texcoord22 );
			GL.Vertex( vertex22 );
			GL.TexCoord( texcoord23 );
			GL.Vertex( vertex23 );
			GL.TexCoord( texcoord32 );
			GL.Vertex( vertex32 );
			GL.TexCoord( texcoord33 );
			GL.Vertex( vertex33 );
			GL.TexCoord( texcoord42 );
			GL.Vertex( vertex42 );
			GL.TexCoord( texcoord43 );
			GL.Vertex( vertex43 );

			GL.TexCoord( texcoord13 );
			GL.Vertex( vertex13 );
			GL.TexCoord( texcoord14 );
			GL.Vertex( vertex14 );
			GL.TexCoord( texcoord23 );
			GL.Vertex( vertex23 );
			GL.TexCoord( texcoord24 );
			GL.Vertex( vertex24 );
			GL.TexCoord( texcoord33 );
			GL.Vertex( vertex33 );
			GL.TexCoord( texcoord34 );
			GL.Vertex( vertex34 );
			GL.TexCoord( texcoord43 );
			GL.Vertex( vertex43 );
			GL.TexCoord( texcoord44 );
			GL.Vertex( vertex44 );
		}
		
		public void DrawSeekBar( Rect aRect, GUIStyle aStyle, float aPositionLoopStart, float aPositionLoopEnd, float aPositionCurrent )
		{
			index  = 0;
			Vector2[] lUv = meshFilter.mesh.uv;
			Vector3[] lVertices = meshFilter.mesh.vertices;

			if( aPositionCurrent < aPositionLoopStart )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aPositionCurrent );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionCurrent, aPositionLoopStart );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.active, aPositionLoopStart, aPositionLoopEnd );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionLoopEnd, 1.0f );
			}
			else if( aPositionCurrent == aPositionLoopStart )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aPositionCurrent );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.active, aPositionCurrent, aPositionLoopEnd );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionLoopEnd, 1.0f );
			}
			else if( aPositionCurrent < aPositionLoopEnd )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aPositionLoopStart );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onActive, aPositionLoopStart, aPositionCurrent );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.active, aPositionCurrent, aPositionLoopEnd );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionLoopEnd, 1.0f );
			}
			else if( aPositionCurrent == aPositionLoopEnd )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aPositionLoopStart );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onActive, aPositionLoopStart, aPositionCurrent );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionLoopEnd, 1.0f );
			}
			else
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aPositionLoopStart );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onActive, aPositionLoopStart, aPositionLoopEnd );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, aPositionLoopEnd, aPositionCurrent );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aPositionCurrent, 1.0f );
			}

			meshFilter.mesh.uv = lUv;
			meshFilter.mesh.vertices = lVertices;
			meshFilter.mesh.RecalculateBounds();
		}
		
		public void DrawVolumeBar( Rect aRect, GUIStyle aStyle, float aVolume )
		{
			Vector2[] lUv = meshFilter.mesh.uv;
			Vector3[] lVertices = meshFilter.mesh.vertices;

			if( aVolume <= 0 )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, 0.0f, 1.0f );
			}
			else if( aVolume < 1.0f )
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, aVolume );
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.normal, aVolume, 1.0f );
			}
			else
			{
				DrawSeekBarPartition( aRect, lUv, lVertices, aStyle, aStyle.onNormal, 0.0f, 1.0f );
			}
			
			meshFilter.mesh.uv = lUv;
			meshFilter.mesh.vertices = lVertices;
			meshFilter.mesh.RecalculateBounds();
		}
		
		public void DrawSeekBarPartition( Rect aRect, Vector2[] aUv, Vector3[] aVertices, GUIStyle aStyle, GUIStyleState aStyleState, float aPositionStart, float aPositionEnd )
		{
			RectOffset lBorder = aStyle.border;
			Texture2D lTexture = aStyleState.background;
			
			float x1 = aRect.x;
			float x2 = aRect.x + lBorder.left;
			float x3 = aRect.x + aRect.width - lBorder.right;
			float x4 = aRect.x + aRect.width;
			
			float xS = aRect.x + aRect.width * aPositionStart;
			float xE = aRect.x + aRect.width * aPositionEnd;
			
			float y1 = aRect.y;
			float y2 = aRect.y + lBorder.top;
			float y3 = aRect.y + aRect.height - lBorder.bottom;
			float y4 = aRect.y + aRect.height;
			
			Vector3 vertexS1 = ScreenToWorldPoint( new Vector3( xS, y1, 0.1f ) );
			Vector3 vertex21 = ScreenToWorldPoint( new Vector3( x2, y1, 0.1f ) );
			Vector3 vertex31 = ScreenToWorldPoint( new Vector3( x3, y1, 0.1f ) );
			Vector3 vertexE1 = ScreenToWorldPoint( new Vector3( xE, y1, 0.1f ) );
			
			Vector3 vertexS2 = ScreenToWorldPoint( new Vector3( xS, y2, 0.1f ) );
			Vector3 vertex22 = ScreenToWorldPoint( new Vector3( x2, y2, 0.1f ) );
			Vector3 vertex32 = ScreenToWorldPoint( new Vector3( x3, y2, 0.1f ) );
			Vector3 vertexE2 = ScreenToWorldPoint( new Vector3( xE, y2, 0.1f ) );
			
			Vector3 vertexS3 = ScreenToWorldPoint( new Vector3( xS, y3, 0.1f ) );
			Vector3 vertex23 = ScreenToWorldPoint( new Vector3( x2, y3, 0.1f ) );
			Vector3 vertex33 = ScreenToWorldPoint( new Vector3( x3, y3, 0.1f ) );
			Vector3 vertexE3 = ScreenToWorldPoint( new Vector3( xE, y3, 0.1f ) );
			
			Vector3 vertexS4 = ScreenToWorldPoint( new Vector3( xS, y4, 0.1f ) );
			Vector3 vertex24 = ScreenToWorldPoint( new Vector3( x2, y4, 0.1f ) );
			Vector3 vertex34 = ScreenToWorldPoint( new Vector3( x3, y4, 0.1f ) );
			Vector3 vertexE4 = ScreenToWorldPoint( new Vector3( xE, y4, 0.1f ) );
			
			float u1 = 0.0f;
			float u2 = ( float )lBorder.left / ( float )lTexture.width;
			float u3 = 1.0f - ( float )lBorder.right / ( float )lTexture.width;
			float u4 = 1.0f;
			
			float uS = 0.0f;
			float uE = 1.0f;
			
			if( xS <= x1 )
			{
				uS = u1;
			}
			else if( xS < x2 )
			{
				// Rate.
				uS = ( ( float )lBorder.left - ( x2 - xS ) ) / ( float )lTexture.width;
			}
			else if( xS <= x3 )
			{
				uS = u2;
			}
			else
			{
				uS = 1.0f - ( ( float )lBorder.right - ( xS - x3 ) ) / ( float )lTexture.width;
			}
			
			if( xE >= x4 )
			{
				uE = u4;
			}
			else if( xE > x3 )
			{
				// Rate.
				uE = 1.0f - ( ( float )lBorder.right - ( xE - x3 ) ) / ( float )lTexture.width;
			}
			else if( xE >= x2 )
			{
				uE = u3;
			}
			else
			{
				uE = ( ( float )lBorder.left - ( x2 - xE ) ) / ( float )lTexture.width;
			}
			
			float v4 = 0.0f;
			float v3 = ( float )lBorder.top / ( float )lTexture.height;
			float v2 = 1.0f - ( float )lBorder.bottom / ( float )lTexture.height;
			float v1 = 1.0f;
			
			Vector3 texcoordS1 = new Vector3( uS, v1, 0.1f );
			Vector3 texcoord21 = new Vector3( u2, v1, 0.1f );
			Vector3 texcoord31 = new Vector3( u3, v1, 0.1f );
			Vector3 texcoordE1 = new Vector3( uE, v1, 0.1f );
			
			Vector3 texcoordS2 = new Vector3( uS, v2, 0.1f );
			Vector3 texcoord22 = new Vector3( u2, v2, 0.1f );
			Vector3 texcoord32 = new Vector3( u3, v2, 0.1f );
			Vector3 texcoordE2 = new Vector3( uE, v2, 0.1f );
			
			Vector3 texcoordS3 = new Vector3( uS, v3, 0.1f );
			Vector3 texcoord23 = new Vector3( u2, v3, 0.1f );
			Vector3 texcoord33 = new Vector3( u3, v3, 0.1f );
			Vector3 texcoordE3 = new Vector3( uE, v3, 0.1f );
			
			Vector3 texcoordS4 = new Vector3( uS, v4, 0.1f );
			Vector3 texcoord24 = new Vector3( u2, v4, 0.1f );
			Vector3 texcoord34 = new Vector3( u3, v4, 0.1f );
			Vector3 texcoordE4 = new Vector3( uE, v4, 0.1f );

			aUv[index] = texcoordS1;
			aVertices[index++] = vertexS1;
			aUv[index] = texcoordS2;
			aVertices[index++] = vertexS2;
			
			if( x2 > xS && x2 < xE )
			{
				aUv[index] = texcoord21;
				aVertices[index++] = vertex21;
				aUv[index] = texcoord22;
				aVertices[index++] = vertex22;
			}
			
			if( x3 > xS && x3 < xE )
			{
				aUv[index] = texcoord31;
				aVertices[index++] = vertex31;
				aUv[index] = texcoord32;
				aVertices[index++] = vertex32;
			}
			
			aUv[index] = texcoordE1;
			aVertices[index++] = vertexE1;
			aUv[index] = texcoordE2;
			aVertices[index++] = vertexE2;

			aUv[index] = texcoordS2;
			aVertices[index++] = vertexS2;
			aUv[index] = texcoordS3;
			aVertices[index++] = vertexS3;
			
			if( x2 > xS && x2 < xE )
			{
				aUv[index] = texcoord22;
				aVertices[index++] = vertex22;
				aUv[index] = texcoord23;
				aVertices[index++] = vertex23;
			}
			
			if( x3 > xS && x3 < xE )
			{
				aUv[index] = texcoord32;
				aVertices[index++] = vertex32;
				aUv[index] = texcoord33;
				aVertices[index++] = vertex33;
			}
			
			aUv[index] = texcoordE2;
			aVertices[index++] = vertexE2;
			aUv[index] =texcoordE3;
			aVertices[index++] = vertexE3;

			aUv[index] = texcoordS3;
			aVertices[index++] = vertexS3;
			aUv[index] = texcoordS4;
			aVertices[index++] = vertexS4;
			
			if( x2 > xS && x2 < xE )
			{
				aUv[index] = texcoord23;
				aVertices[index++] = vertex23;
				aUv[index] = texcoord24;
				aVertices[index++] = vertex24;
			}
			
			if( x3 > xS && x3 < xE )
			{
				aUv[index] = texcoord33;
				aVertices[index++] = vertex33;
				aUv[index] = texcoord34;
				aVertices[index++] = vertex34;
			}
			
			aUv[index] = texcoordE3;
			aVertices[index++] = vertexE3;
			aUv[index] = texcoordE4;
			aVertices[index++] = vertexE4;
		}

		private static Vector3 ScreenToWorldPoint( Vector3 aVector )
		{
			return new Vector3( -Screen.width / 2.0f + aVector.x, Screen.height / 2.0f - 1.0f - aVector.y, aVector.z );
		}
	}
}
