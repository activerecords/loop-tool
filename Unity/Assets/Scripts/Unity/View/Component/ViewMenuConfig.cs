using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View
{
	public class ViewMenuConfig : IView
	{
		public Rect rectMenu;
		private bool isShowMenu;
		private DialogConfigLoopTool windowConfig;
		
		public Rect Rect{ get; set; }

		public ViewMenuConfig()
		{
			rectMenu = new Rect( 100.0f, GuiStyleSet.StyleMenu.bar.fixedHeight, 100.0f, 200.0f );
			isShowMenu = false;
		}
		
		public void Awake()
		{
			isShowMenu = true;
		}
		
		public void Start()
		{
			
		}
		
		public void Update()
		{
			
		}
		
		public void OnGUI()
		{
			if( windowConfig != null )
			{
				windowConfig.OnGUI();
			}
			
			if( isShowMenu == true )
			{
				GUI.Window( 1, rectMenu, SelectItemWindow, "", GuiStyleSet.StyleMenu.window );
			}
			
			if( Input.GetMouseButtonDown( 0 ) == true )
			{
				float lY = Screen.height - 1 - Input.mousePosition.y;
				
				if( Input.mousePosition.x < rectMenu.x || Input.mousePosition.x >= rectMenu.x + rectMenu.width ||
				   lY < rectMenu.y || lY >= rectMenu.y + rectMenu.height )
				{
					isShowMenu = false;
				}
			}
		}
		
		public void SelectItemWindow( int windowID )
		{
			GUILayout.BeginVertical();
			{
				if( GUILayout.Button( new GUIContent( "Config", "StyleMenu.Item" ), GuiStyleSet.StyleMenu.item ) == true )
				{
					windowConfig = new DialogConfigLoopTool( ChangeConfig );
					isShowMenu = false;
				}
			}
			GUILayout.EndVertical();
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
		
		private void ChangeConfig( int[] aConfig )
        {
            Debug.Log( "Save" );
			windowConfig = null;
		}
	}
}
