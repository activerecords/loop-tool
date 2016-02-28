using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.IO;

namespace Unity.View
{
	public class UiTest : IView
	{
		GameObject gameObject;
		Camera camera;

		float positionLoopStart;
		float positionLoopEnd;
		
		public Rect Rect{ get; set; }

		public UiTest()
		{

		}

		public void Awake()
		{
			positionLoopStart = 0.0f;
			positionLoopEnd = 0.0f;
		}

		public void Start()
		{
			gameObject = GameObject.Find( "Main Camera" );
			
			if( gameObject != null )
			{
				camera = gameObject.camera;
			}
		}

		public void Update()
		{

		}

		public void OnGUI()
		{
			GUILayout.Label( "Loop Start:", GuiStyleSet.StyleGeneral.label );
			positionLoopStart = GUILayout.HorizontalSlider( positionLoopStart, 0.0f, 1.00f, GuiStyleSet.StyleSlider.horizontalbar, GuiStyleSet.StyleSlider.horizontalbarThumb );
			GUILayout.Label( "Loop End:", GuiStyleSet.StyleGeneral.label );
			positionLoopEnd = GUILayout.HorizontalSlider( positionLoopEnd, 0.0f, 1.00f, GuiStyleSet.StyleSlider.horizontalbar, GuiStyleSet.StyleSlider.horizontalbarThumb );
		}

		public void OnRenderObject()
		{
			Vector2 positionStart = new Vector2( 0.0f, 0.0f );
			Vector2 positionEnd = new Vector2( Screen.width, Screen.height );

			//Line.DrawLine( camera, positionStart, positionEnd, 2.0f );

			if( camera != null )
			{
				Gui.DrawUiTexture( new Rect( 100.0f, 300.0f, 300.0f, 200.0f ), GuiStyleSet.StyleGeneral.button );
				
				//Line.DrawSeekBar( camera, new Rect( 50.0f, 250.0f, StylePlayer.instance.seekbar.fixedWidth, StylePlayer.instance.seekbar.fixedHeight ), StylePlayer.instance.seekbar, positionLoopStart, positionLoopEnd, 0.0f );
				//Line.DrawSeekBar( camera, new Rect( 50.0f, 200.0f, StylePlayer.instance.seekbar.fixedWidth, StylePlayer.instance.seekbar.fixedHeight ), StylePlayer.instance.seekbar, 0.45f, 0.55f, 1.0f );
				//Line.DrawSeekBar( camera, new Rect( 50.0f, 150.0f, StylePlayer.instance.seekbar.fixedWidth, StylePlayer.instance.seekbar.fixedHeight ), StylePlayer.instance.seekbar, 0.25f, 0.75f, 0.5f );
				//Line.DrawSeekBar( camera, new Rect( 50.0f, 100.0f, StylePlayer.instance.seekbar.fixedWidth, StylePlayer.instance.seekbar.fixedHeight ), StylePlayer.instance.seekbar, 0.125f, 0.875f, 0.125f );
				//Line.DrawSeekBar( camera, new Rect( 50.0f, 50.0f, StylePlayer.instance.seekbar.fixedWidth, StylePlayer.instance.seekbar.fixedHeight ), StylePlayer.instance.seekbar, 0.0f, 1.0f, 0.0f );
			}
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{

		}

		public void OnApplicationQuit()
		{
			
		}
	}
}
