using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View.LoopTool
{
	public class SceneLoopTool : MonoBehaviour
	{
		private int sampleRate;
		private Dictionary<int, float[]> soundBuffer;

		private ApplicationLoopTool applicationLoopTool;
		private bool isSetGuiStyle;

		void Awake()
		{
			isSetGuiStyle = false;

			Unity.Function.Graphic.Gui.camera = camera;
			GameObject obj = GameObject.Find( "GuiStyleSet" );
			GuiStyleSet.Reset( obj );

			AudioSettings.outputSampleRate = 44100;
			sampleRate = AudioSettings.outputSampleRate;

			applicationLoopTool = new ApplicationLoopTool( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music" ), new DirectoryInfo( Application.streamingAssetsPath + "/Sound" ) );

			soundBuffer = new Dictionary<int, float[]>();
		}

		void Start()
		{
			applicationLoopTool.Start();
			
			AudioClip myClip = AudioClip.Create("MySinoid", 8820, 2, 44100, false, true, OnAudioRead, OnAudioSetPosition);
			audio.clip = myClip;
			audio.Play();
		}
		
		void OnAudioRead( float[] data )
		{
			for( int i = 0; i < data.Length; i++ )
			{
				data[i] = 0;
			}
			
			if( soundBuffer.ContainsKey(data.Length) == false )
			{
				soundBuffer.Add( data.Length, new float[data.Length] );
			}

			for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
			{
				soundBuffer[data.Length][i] = 0.0f;
			}
					
			applicationLoopTool.OnAudioFilterRead( soundBuffer[data.Length], 2, sampleRate );
					
			for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
			{
				data[i] += soundBuffer[data.Length][i];
			}
		}

		void OnAudioSetPosition(int newPosition)
		{

		}

		void Update()
		{
			applicationLoopTool.Update();
		}

		void OnGUI()
		{
			GuiStyleSet.SetGuiStyles();

			GUILayout.BeginArea( new Rect( 0.0f, 0.0f, Screen.width, Screen.height ) );
			{
				applicationLoopTool.OnGUI();
			}
			GUILayout.EndArea();
		}

		void OnRenderObject()
		{
			applicationLoopTool.OnRenderObject();
		}
		
		void OnApplicationQuit()
		{
			applicationLoopTool.OnApplicationQuit();
		}
	}
}
