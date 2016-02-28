using UnityEngine;
using UnityEditor;

using Unity.Data;
using Unity.GuiStyle;
using Unity.View.Scene;
using Unity.View;

using System;
using System.IO;
using System.Collections.Generic;

namespace Curan.UnityEditorView.Sound
{
	public class EditorLoopTool : EditorWindow
	{
		private LoopTool viewLoopTool;
		private GUIStyle[] customStyles;
		
		[MenuItem( "Window/Sound/Loop Tool" )]
		static void Open()
		{
			GetWindow<EditorLoopTool>( true, "Loop Tool" );
		}

		void Awake()
		{
			Debug.Log( "Editor Awake" );

			viewLoopTool = new LoopTool( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmInput" ), new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmOutput" ) );
			viewLoopTool.Awake();

			SceneDesktop lSceneDesktop = GameObject.Find( "Main Camera" ).GetComponent<SceneDesktop>();

			if( lSceneDesktop != null )
			{
				lSceneDesktop.delegateAudioFilter = viewLoopTool.OnAudioFilterRead;
			}

			customStyles = new GUIStyle[24];
			
			customStyles[0] = GuiStyleSet.StyleScrollbar.verticalbar;
			customStyles[1] = GuiStyleSet.StyleScrollbar.verticalbarThumb;
			customStyles[2] = GuiStyleSet.StyleScrollbar.verticalbarUpButton;
			customStyles[3] = GuiStyleSet.StyleScrollbar.verticalbarDownButton;
			
			customStyles[4] = GuiStyleSet.StyleScrollbar.horizontalbar;
			customStyles[5] = GuiStyleSet.StyleScrollbar.horizontalbarThumb;
			customStyles[6] = GuiStyleSet.StyleScrollbar.horizontalbarLeftButton;
			customStyles[7] = GuiStyleSet.StyleScrollbar.horizontalbarRightButton;
			
			customStyles[8] = GuiStyleSet.StylePlayer.seekbar;
			customStyles[9] = GuiStyleSet.StylePlayer.seekbarThumb;
			customStyles[10] = GuiStyleSet.StylePlayer.seekbarLeftButton;
			customStyles[11] = GuiStyleSet.StylePlayer.seekbarRightButton;
			
			customStyles[12] = GuiStyleSet.StyleProgressbar.progressbar;
			customStyles[13] = GuiStyleSet.StyleProgressbar.progressbarThumb;
			customStyles[14] = GuiStyleSet.StyleProgressbar.progressbarLeftButton;
			customStyles[15] = GuiStyleSet.StyleProgressbar.progressbarRightButton;
			
			customStyles[16] = GuiStyleSet.StyleTable.verticalbarHeader;
			customStyles[17] = GuiStyleSet.StyleTable.verticalbarHeaderThumb;
			customStyles[18] = GuiStyleSet.StyleTable.verticalbarHeaderUpButton;
			customStyles[19] = GuiStyleSet.StyleTable.verticalbarHeaderDownButton;
			
			customStyles[20] = GuiStyleSet.StyleTable.horizontalbarHeader;
			customStyles[21] = GuiStyleSet.StyleTable.horizontalbarHeaderThumb;
			customStyles[22] = GuiStyleSet.StyleTable.horizontalbarHeaderLeftButton;
			customStyles[23] = GuiStyleSet.StyleTable.horizontalbarHeaderRightButton;
		}

		void Update()
		{
			if( viewLoopTool == null )
			{
				Debug.Log( "Reset Editor" );
				viewLoopTool = new LoopTool( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmInput" ), new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmOutput" ) );
				viewLoopTool.Awake();
				
				SceneDesktop lSceneDesktop = GameObject.Find( "Main Camera" ).GetComponent<SceneDesktop>();
				
				if( lSceneDesktop != null )
				{
					lSceneDesktop.delegateAudioFilter = viewLoopTool.OnAudioFilterRead;
				}
			}

			viewLoopTool.Update();
		}
		
		void OnInspectorUpdate()
		{
			Repaint();
		}
		
		void OnApplicationQuit()
		{
			if( viewLoopTool != null )
			{
				viewLoopTool.OnApplicationQuit();
			}
		}
		
		void OnRenderObject()
		{
			if( viewLoopTool != null )
			{
				viewLoopTool.OnRenderObject();
			}
		}
		
		void OnGUI()
		{
			if( customStyles != null && GUI.skin.GetStyle( GuiStyleSet.StylePlayer.seekbar.name ).name == "" )
			{
				GUIStyle[] lGuiStyleslDefault = GUI.skin.customStyles;
				GUIStyle[] lGuiStylesNew = new GUIStyle[lGuiStyleslDefault.Length + customStyles.Length];

				Debug.Log( "Editor:" + lGuiStylesNew.Length );
				
				for( int i = 0; i < lGuiStyleslDefault.Length; i++ )
				{
					lGuiStylesNew[i] = lGuiStyleslDefault[i];
				}
				
				for( int i = 0; i < customStyles.Length; i++ )
				{
					lGuiStylesNew[lGuiStyleslDefault.Length + i] = customStyles[i];
				}

				GUI.skin.customStyles = lGuiStylesNew;
			}

			GUILayout.BeginVertical();
			{
				if( viewLoopTool != null )
				{
					viewLoopTool.OnGUI();
				}
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndVertical();
		}
	}
}
