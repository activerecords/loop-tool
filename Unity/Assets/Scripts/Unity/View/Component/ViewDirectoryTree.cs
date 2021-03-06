using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Curan.Utility;

namespace Unity.View
{
	public class ViewDirectoryTree : IView
	{
		private DirectoryInfo directoryInfoSelf;
		//private DirectoryInfo directoryInfoSelected;

		private readonly ViewDirectoryTree root;
		private readonly List<ViewDirectoryTree> childList;

		private bool isDisplayChildren;
		private bool haveChildren;

		public DirectoryInfo DirectoryInfoSelected{ get; private set; }
		public Rect Rect{ get; set; }

		// ルート用のコンストラクタ.
		public ViewDirectoryTree( DirectoryInfo aDirectoryInfo, DirectoryInfo aDirectoryInfoCurrent )
		{
			isDisplayChildren = false;
			haveChildren = false;
			directoryInfoSelf = aDirectoryInfo;
			DirectoryInfoSelected = aDirectoryInfoCurrent;
			root = this;
			childList = new List<ViewDirectoryTree>();

			LoadChildren();
		}

		public ViewDirectoryTree( ViewDirectoryTree aRoot, DirectoryInfo aDirectoryInfo )
		{
			isDisplayChildren = false;
			haveChildren = false;
			directoryInfoSelf = aDirectoryInfo;
			root = aRoot;
			childList = new List<ViewDirectoryTree>();

			LoadChildren();
		}

		private void LoadChildren()
		{
			if( root.DirectoryInfoSelected.FullName.IndexOf( directoryInfoSelf.FullName ) == 0 )
			{
				isDisplayChildren = true;
			}

			try
			{
				DirectoryInfo[] lDirectoryInfoArray = directoryInfoSelf.GetDirectories( "*", SearchOption.TopDirectoryOnly );

				if( lDirectoryInfoArray.Length > 0 )
				{
					haveChildren = true;
				}
			}
			catch( Exception aExpection )
			{
				Debug.LogWarning( "UnauthorizedAccessException:" + directoryInfoSelf.FullName );
			}
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
			Toggle();

			if( isDisplayChildren == true )
			{
				if( haveChildren == true )
				{
					if( childList.Count == 0 )
					{
						DirectoryInfo[] lDirectoryInfoArray = directoryInfoSelf.GetDirectories( "*", SearchOption.TopDirectoryOnly );

						for( int i = 0; i < lDirectoryInfoArray.Length; i++ )
						{
							ViewDirectoryTree lViewDirectoryTree = new ViewDirectoryTree( root, lDirectoryInfoArray[i] );
							
							childList.Add( lViewDirectoryTree );
						}
					}

					GUILayout.BeginHorizontal();
					{
						GUILayout.Space( 10.0f );

						GUILayout.BeginVertical();
						{
							for( int i = 0; i < childList.Count; i++ )
							{
								childList[i].OnGUI();
							}
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
				}
			}
		}

		private void Toggle()
		{
			GUILayout.BeginHorizontal();
			{
				if( haveChildren == true )
				{
					isDisplayChildren = GUILayout.Toggle( isDisplayChildren, new GUIContent( "", "StyleList.ToggleOpenClose" ), GuiStyleSet.StyleList.toggleOpenClose, GUILayout.Width( 16.0f ) );
				}
				else
				{
					GUILayout.Toggle( true, new GUIContent( "", "StyleGeneral.None" ), GuiStyleSet.StyleGeneral.none, GUILayout.Width( 16.0f ) );
				}

				if( directoryInfoSelf.FullName == root.DirectoryInfoSelected.FullName )
				{
					GUILayout.Toggle( true, new GUIContent( directoryInfoSelf.Name, "StyleList.ToggleLine " ), GuiStyleSet.StyleList.toggleLine );
				}
				else
				{
					bool isSeledted = GUILayout.Toggle( false, new GUIContent( directoryInfoSelf.Name, "StyleList.ToggleLine " ), GuiStyleSet.StyleList.toggleLine );

					if( isSeledted == true )
					{
						root.DirectoryInfoSelected = directoryInfoSelf;
					}
				}
			}
			GUILayout.EndHorizontal();
		}
	}
}
