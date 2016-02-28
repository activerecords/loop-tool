using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.IO;
using System.Collections;

namespace Unity.View
{
	public class ViewGuiTest : IView
	{
		private ViewDirectoryTree viewDirectoryTree;
		private Vector2 positionScrollDirectory;
		
		public Rect Rect{ get; set; }

		public ViewGuiTest()
			: base()
		{
			viewDirectoryTree = new ViewDirectoryTree( new DirectoryInfo( Application.streamingAssetsPath ), new DirectoryInfo( Application.streamingAssetsPath ) );
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
			GUILayout.BeginVertical( GuiStyleSet.StyleGeneral.box );
			{
                // To Be Fixed.
                /*
				if( GUILayout.Button( ( Texture2D )PoolTexture.GetTexture( "Graphic/Start.png" ), GuiStyleSetGeneral.instance.button, GUILayout.Width( 120.0f ) ) == true )
				{

				}*/

				positionScrollDirectory = GUILayout.BeginScrollView( positionScrollDirectory );
				{
					viewDirectoryTree.OnGUI();
				}
				GUILayout.EndScrollView();
			}
			GUILayout.EndVertical();
		}
	}
}
