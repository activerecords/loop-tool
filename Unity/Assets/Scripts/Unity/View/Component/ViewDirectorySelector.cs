using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View
{
	public class ViewDirectorySelector : IView
	{
		private ViewDirectoryTree viewDirectoryTree;

		private Vector2 positionScrollDirectory;
		private Vector2 positionScrollRecent;

		private DirectoryInfo directoryInfo;

		public delegate void CloseWindow( DirectoryInfo aDirectoryInfo );
		
		private CloseWindow closeWindow;
        private bool isPullDown;

		public Rect Rect{ get; set; }

		public ViewDirectorySelector( CloseWindow aCloseWindow, ViewDirectoryTree aViewDirectoryTree, DirectoryInfo aDirectoryInfo )
		{
			closeWindow = aCloseWindow;
			viewDirectoryTree = aViewDirectoryTree;
			directoryInfo = aDirectoryInfo;
            isPullDown = false;
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

        public void OnGUI()
		{
			GUILayout.BeginVertical();
			{
				DisplayPath();
				DisplayDirectoryTree();
				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );
				DisplayRecentDirectories();
				DisplayButton();
			}
			GUILayout.EndVertical();
		}
		
		private void DisplayPath()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.TextArea( viewDirectoryTree.DirectoryInfoSelected.FullName, GuiStyleSet.StyleFolder.barAddress );
				GUILayout.Button( new GUIContent( "", "StyleGeneral.ButtonPullDown" ), GuiStyleSet.StyleGeneral.buttonPullDown );
			}
			GUILayout.EndHorizontal();
		}

		private void DisplayDirectoryTree()
		{
			positionScrollDirectory = GUILayout.BeginScrollView( positionScrollDirectory, GuiStyleSet.StyleScrollbar.horizontalbar, GuiStyleSet.StyleScrollbar.verticalbar, GUILayout.Height( Rect.height / 2.0f ) );
			{
				viewDirectoryTree.OnGUI();
			}
			GUILayout.EndScrollView();
		}
        
        private void DisplayRecentDirectories()
        {
			positionScrollRecent = GUILayout.BeginScrollView( positionScrollRecent, GuiStyleSet.StyleScrollbar.horizontalbar, GuiStyleSet.StyleScrollbar.verticalbar );
            {
				GUILayout.Label( "Open Recent", GuiStyleSet.StyleGeneral.label );
				GUILayout.Toggle( false, "Recent 1", GuiStyleSet.StyleList.toggleLine );
				GUILayout.Toggle( false, "Recent 2", GuiStyleSet.StyleList.toggleLine );
				GUILayout.Toggle( false, "Recent 3", GuiStyleSet.StyleList.toggleLine );
				GUILayout.Toggle( false, "Recent 4", GuiStyleSet.StyleList.toggleLine );
				GUILayout.Toggle( false, "Recent 5", GuiStyleSet.StyleList.toggleLine );
            }
            GUILayout.EndScrollView();
		}

		private void DisplayButton()
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				
				if( GUILayout.Button( new GUIContent( "OK", "StyleGeneral.Button" ), GuiStyleSet.StyleGeneral.button ) == true )
				{
					directoryInfo = viewDirectoryTree.DirectoryInfoSelected;
					closeWindow( directoryInfo );
					isPullDown = false;
				}

				if( GUILayout.Button( new GUIContent( "Cancel", "StyleGeneral.Button" ), GuiStyleSet.StyleGeneral.button ) == true )
				{
					closeWindow( directoryInfo );
					isPullDown = false;
				}
			}
			GUILayout.EndHorizontal();
		}
	}
}
