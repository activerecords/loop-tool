using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View.Player
{
	public class ScenePlayer : MonoBehaviour
	{
		private int sampleRate;
		private Dictionary<int, float[]> soundBuffer;

		private ApplicationPlayer applicationPlayer;
		private bool isSetGuiStyle;

		void Awake()
		{
			isSetGuiStyle = false;

			Unity.Function.Graphic.Gui.camera = camera;
			GameObject obj = GameObject.Find( "GuiStyleSet" );
			GuiStyleSet.Reset( obj );

			AudioSettings.outputSampleRate = 44100;
			sampleRate = AudioSettings.outputSampleRate;

			applicationPlayer = new ApplicationPlayer( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music" ), GetComponent<MeshFilter>(), GetComponent<MeshRenderer>() );

			soundBuffer = new Dictionary<int, float[]>();
		}

		void Start()
		{
			applicationPlayer.Start();
			
			AudioClip myClip = AudioClip.Create("MySinoid", 8820, 2, 44100, false, true, OnAudioRead, OnAudioSetPosition);
			audio.clip = myClip;
			audio.Play();

			/*
			WWW www = new WWW( "file:///" + Application.streamingAssetsPath + "/Sound/Music/tam-n17.mp3" );
			
			if( www.error != null )
			{
				Debug.Log( www.error );
			}
			
			AudioClip audioClip = www.GetAudioClip( false, false );
			audio.clip = audioClip;
*/
			/*
			float[] data = new float[audioClip.samples  * audioClip.channels];
			audioClip.GetData( data, 0 );
				
				Debug.Log( data[0].ToString() );
				Debug.Log( data[1].ToString() );*/
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
					
			applicationPlayer.OnAudioFilterRead( soundBuffer[data.Length], 2, sampleRate );
					
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
			/*
			if( !audio.isPlaying && audio.clip.isReadyToPlay )
			{
				audio.Play();

				float[] data = new float[audio.clip.samples  * audio.clip.channels];
				audio.clip.GetData( data, 0 );
				
				Debug.Log( data[0].ToString() );
				Debug.Log( data[1].ToString() );
			}
*/
			applicationPlayer.Update();
		}

		void OnGUI()
		{
			GuiStyleSet.SetGuiStyles();

			GUILayout.BeginArea( new Rect( 0.0f, 0.0f, Screen.width, Screen.height ) );
			{
				applicationPlayer.OnGUI();
			}
			GUILayout.EndArea();
		}

		void OnRenderObject()
		{
			applicationPlayer.OnRenderObject();
		}
		
		void OnApplicationQuit()
		{
			applicationPlayer.OnApplicationQuit();
		}
	}
}
