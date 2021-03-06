using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View
{
	public class ViewMenu : IView
	{
		private ViewMenuFile viewMenuFile;
		private ViewMenuConfig viewMenuConfig;
		
		public Rect Rect{ get; set; }

		public ViewMenu()
		{
            viewMenuFile = new ViewMenuFile();
            viewMenuConfig = new ViewMenuConfig();
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

		public void OnGUI()
		{
			GUILayout.BeginHorizontal( GuiStyleSet.StyleMenu.bar );
			{
				float lHeightItem = ( float )Math.Ceiling( GuiStyleSet.StyleMenu.item.CalcSize( new GUIContent( "" ) ).y );
				float lHeightPaddingV = GuiStyleSet.StyleMenu.window.padding.top + GuiStyleSet.StyleMenu.window.padding.bottom;
				float lHeightPaddingH = GuiStyleSet.StyleMenu.window.padding.left + GuiStyleSet.StyleMenu.window.padding.right;

				viewMenuFile.rectMenu = new Rect( GuiStyleSet.StyleMenu.button.margin.left, GuiStyleSet.StyleMenu.bar.fixedHeight, 100.0f + lHeightPaddingH, lHeightItem * 2 + lHeightPaddingV );

				if( GUILayout.Button( new GUIContent( "File", "StyleMenu.Button" ), GuiStyleSet.StyleMenu.button ) == true )
				{
					viewMenuFile.Awake();
				}

				float lWidthMenu = GuiStyleSet.StyleMenu.button.CalcSize( new GUIContent( "File" ) ).x + GuiStyleSet.StyleMenu.button.margin.left * 2 + GuiStyleSet.StyleMenu.button.margin.right;
				
				viewMenuConfig.rectMenu = new Rect( lWidthMenu, GuiStyleSet.StyleMenu.bar.fixedHeight, 100.0f + lHeightPaddingH, lHeightItem + lHeightPaddingV );

				if( GUILayout.Button( new GUIContent( "Config", "StyleMenu.Button" ), GuiStyleSet.StyleMenu.button ) == true )
				{
					viewMenuConfig.Awake();
				}

				GUILayout.Button( new GUIContent( "Help", "StyleMenu.Button" ), GuiStyleSet.StyleMenu.button );
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();

			viewMenuFile.OnGUI();
			viewMenuConfig.OnGUI();
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
	}
}
