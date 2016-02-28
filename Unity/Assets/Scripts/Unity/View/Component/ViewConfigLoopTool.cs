using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View
{
	public class ViewConfigLoopTool : IView
	{
		public delegate void CloseWindow( int[] aConfig );
		private CloseWindow closeWindow;
        
        private string[] captions;
        private int grid;
		
		public Rect Rect{ get; set; }

		public ViewConfigLoopTool( CloseWindow aCloseWindow )
		{
			closeWindow = aCloseWindow;
            
            captions = new string[2] { "On", "Off" };
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

		public void OnGUI()
		{
			GUILayout.BeginVertical();
            {
				GUILayout.Label( new GUIContent( "Loop Play", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
                GUILayout.BeginHorizontal();
                {
					grid = GUILayout.SelectionGrid( grid, captions, 1, GuiStyleSet.StyleGeneral.toggleRadio );
                }
                GUILayout.EndHorizontal();

				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );

				GUILayout.Label( new GUIContent( "File Out Option", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
                GUILayout.BeginHorizontal();
                {
					grid = GUILayout.SelectionGrid( grid, captions, 1, GuiStyleSet.StyleGeneral.toggleRadio );
                }
                GUILayout.EndHorizontal();

				GUILayout.Label( new GUIContent( "", "StyleGeneral.PartitionHorizontal" ), GuiStyleSet.StyleGeneral.partitionHorizontal );

				GUILayout.Label( new GUIContent( "Children Folder", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
                GUILayout.BeginHorizontal();
                {
					grid = GUILayout.SelectionGrid( grid, captions, 1, GuiStyleSet.StyleGeneral.toggleRadio );
                }
                GUILayout.EndHorizontal();
				
				GUILayout.FlexibleSpace();

				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					
					if( GUILayout.Button( new GUIContent( "OK", "StyleGeneral.Button " ), GuiStyleSet.StyleGeneral.button ) == true )
					{
						closeWindow( new int[]{} );
					}
					if( GUILayout.Button( new GUIContent( "Cancel", "StyleGeneral.Button " ), GuiStyleSet.StyleGeneral.button ) == true )
					{
						closeWindow( new int[]{} );
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}
	}
}
