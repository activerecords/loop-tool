using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;

using Curan.Common.Struct;
using Curan.Utility;

namespace Unity.View
{
	public class ViewLoopDisplay : IView
	{
		private LoopInformation loop;
		private string[] captions;
		private int grid;

		private Vector2 scrollPosition;
		
		public Rect Rect{ get; set; }

		public ViewLoopDisplay( LoopInformation aLoop )
		{
			loop = aLoop;

			captions = new string[3] { "min:sec.msec", "msec.", "samples" };
			grid = 0;
		}
		
		public void Awake()
		{
			
		}
		
		public void Start()
		{
			
		}
		
		public void Update()
		{
			
		}
		
		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			
		}
		
		public void OnApplicationQuit()
		{
			
		}
		
		public void OnRenderObject()
		{
			
		}
		
		public void OnGUI( int aX, int aY, int aWidth, int aHeight )
		{
			Rect lRectWindow = new Rect( aX, aY, aWidth, aHeight );
			
			GUILayout.BeginArea( lRectWindow );
			{
				GUILayout.BeginVertical();
				{
					grid = GUILayout.SelectionGrid( grid, captions, 1, GuiStyleSet.StyleGeneral.toggleRadio );
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();
		}

		public void OnGUI()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical();
				{
					grid = GUILayout.SelectionGrid( grid, captions, 1, GuiStyleSet.StyleGeneral.toggleRadio );
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}
	}
}
