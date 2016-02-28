using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Component.Application.Sound;

namespace Unity.View.LoopEditor
{
	public class SceneLoopEditor : MonoBehaviour
	{
		private int sampleRate;
		private Dictionary<int, float[]> soundBuffer;

		private ApplicationLoopEditor applicationLoopEditor;
		private bool isSetGuiStyle;
		
		MasterStatus masterStatus;
		MidiGenerator	midiGeneratorArray;

		void Awake()
		{
			isSetGuiStyle = false;
			camera.orthographicSize = Screen.height / 2.0f;

			Unity.Function.Graphic.Gui.camera = camera;
			GameObject objGuiStyleSet = GameObject.Find( "GuiStyleSet" );
			GameObject objGuiSettings = GameObject.Find( "GuiSettings" );
			GuiStyleSet.Reset( objGuiStyleSet );
			GuiSettings.Reset( objGuiSettings );

			AudioSettings.outputSampleRate = 44100;
			sampleRate = AudioSettings.outputSampleRate;

			applicationLoopEditor = new ApplicationLoopEditor( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music" ), new DirectoryInfo( Application.streamingAssetsPath + "/Sound" ) );

			soundBuffer = new Dictionary<int, float[]>();

			//masterStatus = new MasterStatus();
			//midiGeneratorArray = new MidiGenerator( masterStatus );
		}

		void Start()
		{
			applicationLoopEditor.Start();
			
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
				
			applicationLoopEditor.OnAudioFilterRead( soundBuffer[data.Length], 2, sampleRate );
					
			for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
			{
				data[i] += soundBuffer[data.Length][i];
			}

			/*
			for( int j = 0; j < soundBuffer[data.Length].Length / 2; j++ )
			{
				float[] ar = new float[2];

				midiGeneratorArray.Update( ar, 2, 44100 );
				
				data[j * 2 + 0] += ar[0];
				data[j * 2 + 1] += ar[1];
			}*/
		}

		void OnAudioSetPosition(int newPosition)
		{

		}

		void Update()
		{
			applicationLoopEditor.Update();
		}

		void OnGUI()
		{
			GuiStyleSet.SetGuiStyles();

			GUILayout.BeginArea( new Rect( 0.0f, 0.0f, Screen.width, Screen.height ) );
			{
				applicationLoopEditor.OnGUI();
			}
			GUILayout.EndArea();
		}

		void OnRenderObject()
		{
			applicationLoopEditor.OnRenderObject();
		}
		
		void OnApplicationQuit()
		{
			applicationLoopEditor.OnApplicationQuit();
		}
	}
}
