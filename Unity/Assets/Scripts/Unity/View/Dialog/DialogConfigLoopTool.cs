using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.IO;

namespace Unity.View
{
	public class DialogConfigLoopTool : ADragWindow
	{
		private ViewConfigLoopTool viewConfigLoopTool;
		
        public DialogConfigLoopTool( ViewConfigLoopTool.CloseWindow aCloseWindow )
            : base( null, new Rect( 10.0f, 10.0f, Screen.width / 2.0f, Screen.height * 2.0f / 3.0f ) )
		{
            viewConfigLoopTool = new ViewConfigLoopTool( aCloseWindow );
		}
        
        public override void OnGUI()
		{
			ResizeWindow();
			rectWindow = GUI.Window( 3, rectWindow, Window, "Config", GuiStyleSet.StyleWindow.window );
			viewConfigLoopTool.Rect = rectWindow;
		}
		
		private void Window( int windowID )
        {
            ControlWindow();
			viewConfigLoopTool.OnGUI();
			GUI.Label( new Rect( 0, 0, rectWindow.width, rectWindow.height ), new GUIContent( GUI.tooltip ), GuiStyleSet.StyleGeneral.tooltip );
		}
		
        public override void Awake()
		{
			
		}
		
        public override void Start()
		{
			
		}
		
        public override void Update()
		{
			
		}

        public override void OnRenderObject()
        {
            
        }

        public override void OnApplicationQuit()
        {
            
        }

        public override void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			
		}
	}
}
