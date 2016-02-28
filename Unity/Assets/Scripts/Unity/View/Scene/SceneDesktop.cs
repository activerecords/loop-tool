using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

namespace Unity.View.Scene
{
	public class SceneDesktop : MonoBehaviour
	{
		private int sampleRate;
		private Dictionary<int, float[]> soundBuffer;

		private IView windowCurrent;
		private Dictionary<string, IView> windowDictionary;

		private string[] captions;
		private int grid;
		
		private bool isSetGuiStyle;
		private Dictionary<string, GUIStyle> guiStyleDictionary;

		MeshRenderer meshRenderer;
		TextMesh textMesh;
		
		public delegate void DelegateAudioFilter( float[] aSoundBuffer, int aChannels, int aSampleRate );
		public DelegateAudioFilter delegateAudioFilter;

		void Awake()
		{
			Unity.Function.Graphic.Gui.camera = GetComponent<Camera>();
			GameObject obj = GameObject.Find( "GuiStyleSet" );
			//GameObject obj = ( GameObject )Resources.Load( "Prefab/GuiStyleSet.prefab", typeof( GameObject ) );
			GuiStyleSet.Reset( obj );

			AudioSettings.outputSampleRate = 44100;
			sampleRate = AudioSettings.outputSampleRate;

			LoopPlayer lWindowLoopPlayer = new LoopPlayer( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmInput" ) );
			LoopTool lWindowLoopSearch = new LoopTool( new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmInput" ), new DirectoryInfo( Application.streamingAssetsPath + "/Sound/Music/BgmOutput" ) );
			UiTest lUiTest = new UiTest();

			windowDictionary = new Dictionary<string, IView>();
			windowDictionary.Add( "Loop Player", lWindowLoopPlayer );
			windowDictionary.Add( "Loop Tool", lWindowLoopSearch );
			windowDictionary.Add( "UI Test", lUiTest );

			captions = new string[] { "Loop Player", "Loop Tool", "UI Test" };
			grid = 1;
			windowCurrent = lWindowLoopSearch;

			foreach( KeyValuePair<string, IView> lKeyValuePair in windowDictionary )
			{
				lKeyValuePair.Value.Awake();
			}
			
			soundBuffer = new Dictionary<int, float[]>();

			isSetGuiStyle = false;
			guiStyleDictionary = new Dictionary<string, GUIStyle>();
			
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbar.name, GuiStyleSet.StyleScrollbar.verticalbar );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarThumb.name, GuiStyleSet.StyleScrollbar.verticalbarThumb );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarUpButton.name, GuiStyleSet.StyleScrollbar.verticalbarUpButton );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.verticalbarDownButton.name, GuiStyleSet.StyleScrollbar.verticalbarDownButton );
			
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbar.name, GuiStyleSet.StyleScrollbar.horizontalbar );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarThumb.name, GuiStyleSet.StyleScrollbar.horizontalbarThumb );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarLeftButton.name, GuiStyleSet.StyleScrollbar.horizontalbarLeftButton );
			guiStyleDictionary.Add( GuiStyleSet.StyleScrollbar.horizontalbarRightButton.name, GuiStyleSet.StyleScrollbar.horizontalbarRightButton );
			
			guiStyleDictionary.Add( GuiStyleSet.StylePlayer.seekbar.name, GuiStyleSet.StylePlayer.seekbar );
			guiStyleDictionary.Add( GuiStyleSet.StylePlayer.seekbarThumb.name, GuiStyleSet.StylePlayer.seekbarThumb );
			guiStyleDictionary.Add( GuiStyleSet.StylePlayer.seekbarLeftButton.name, GuiStyleSet.StylePlayer.seekbarLeftButton );
			guiStyleDictionary.Add( GuiStyleSet.StylePlayer.seekbarRightButton.name, GuiStyleSet.StylePlayer.seekbarRightButton );
			
			guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbar.name, GuiStyleSet.StyleProgressbar.progressbar );
			guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarThumb.name, GuiStyleSet.StyleProgressbar.progressbarThumb );
			guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarLeftButton.name, GuiStyleSet.StyleProgressbar.progressbarLeftButton );
			guiStyleDictionary.Add( GuiStyleSet.StyleProgressbar.progressbarRightButton.name, GuiStyleSet.StyleProgressbar.progressbarRightButton );
			
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeader.name, GuiStyleSet.StyleTable.verticalbarHeader );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderThumb.name, GuiStyleSet.StyleTable.verticalbarHeaderThumb );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderUpButton.name, GuiStyleSet.StyleTable.verticalbarHeaderUpButton );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.verticalbarHeaderDownButton.name, GuiStyleSet.StyleTable.verticalbarHeaderDownButton );
			
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeader.name, GuiStyleSet.StyleTable.horizontalbarHeader );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderThumb.name, GuiStyleSet.StyleTable.horizontalbarHeaderThumb );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderLeftButton.name, GuiStyleSet.StyleTable.horizontalbarHeaderLeftButton );
			guiStyleDictionary.Add( GuiStyleSet.StyleTable.horizontalbarHeaderRightButton.name, GuiStyleSet.StyleTable.horizontalbarHeaderRightButton );
		}

		void Start()
		{
			if( windowDictionary != null )
			{
				foreach( KeyValuePair<string, IView> l in windowDictionary )
				{
					l.Value.Start();
				}
			}
			
			AudioClip myClip = AudioClip.Create( "MySinoid", 8820, 2, 44100, false, true, OnAudioRead, OnAudioSetPosition );
			GetComponent<AudioSource>().clip = myClip;
			GetComponent<AudioSource>().Play();
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
			
			if( windowDictionary != null )
			{
				foreach( KeyValuePair<string, IView> l in windowDictionary )
				{
					for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
					{
						soundBuffer[data.Length][i] = 0.0f;
					}
					
					l.Value.OnAudioFilterRead( soundBuffer[data.Length], 2, sampleRate );
					
					for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
					{
						data[i] += soundBuffer[data.Length][i];
					}
				}
			}

            if( delegateAudioFilter != null )
			{
				for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
				{
					soundBuffer[data.Length][i] = 0.0f;
				}

				delegateAudioFilter( soundBuffer[data.Length], 2, sampleRate );
				
				for( int i = 0; i < soundBuffer[data.Length].Length; i++ )
				{
					data[i] += soundBuffer[data.Length][i];
				}
            }
		}

		void OnAudioSetPosition(int newPosition)
		{

		}

		void Update()
		{
			if( windowCurrent != null )
			{
				windowCurrent.Update();
			}

			// To Decide Specification.
			// バックグラウンドで実行させるかを検討する.
			// バックグラウンドで実行させる場合は別スレッドする？
			//foreach( KeyValuePair<string, WindowBase> l in windowDictionary )
			//{
			//	l.Value.Update();
			//}
		}

		void OnGUI()
		{
			SetGuiStyles();
			Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
			
			GUILayout.BeginVertical();
			{
				Rect lRectWindow = new Rect( 0.0f, 0.0f, Screen.width, Screen.height );
				
				GUILayout.BeginArea( lRectWindow );
				{
					if( windowCurrent != null )
					{
						windowCurrent.OnGUI();
					}
					
					GUILayout.FlexibleSpace();
					
					GUILayout.BeginHorizontal();
					{
						if( windowDictionary != null )
						{
							int lGridPre = grid;
							
							grid = GUILayout.SelectionGrid( grid, captions, 10, GuiStyleSet.StyleGeneral.tab );
							
							if( grid != lGridPre )
							{
								windowCurrent = windowDictionary[captions[grid]];
							}
						}
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndArea();
			}
			GUILayout.EndVertical();

			GUI.Label( new Rect( 0, 0, Screen.width, Screen.height ), new GUIContent( GUI.tooltip ), GuiStyleSet.StyleGeneral.tooltip );
		}
		
		private void SetGuiStyles()
		{
			if( isSetGuiStyle == false )
			{
				isSetGuiStyle = true;
				
				if( GUI.skin.GetStyle( GuiStyleSet.StylePlayer.seekbar.name ).name == "" )
				{
					GUIStyle[] lCustomStylesAfter = new GUIStyle[GUI.skin.customStyles.Length + guiStyleDictionary.Count];
					
					for( int i = 0; i < lCustomStylesAfter.Length; i++ )
					{
						lCustomStylesAfter[i] = lCustomStylesAfter[i];
					}
					
					int lIndex = 0;
					
					foreach( KeyValuePair<string, GUIStyle> l in guiStyleDictionary )
					{
						lCustomStylesAfter[GUI.skin.customStyles.Length + lIndex] = l.Value;
						lIndex++;
					}
					
					GUI.skin.customStyles = lCustomStylesAfter;
				}
				else
				{
					for( int i = 0; i < GUI.skin.customStyles.Length; i++ )
					{
						if( guiStyleDictionary.ContainsKey( GUI.skin.customStyles[i].name ) == true )
						{
							GUI.skin.customStyles[i] = guiStyleDictionary[GUI.skin.customStyles[i].name];
						}
					}
					
					GUI.skin.customStyles = GUI.skin.customStyles;
				}
			}
		}

		void OnRenderObject()
		{
			if( windowCurrent != null )
			{
				windowCurrent.OnRenderObject();
			}
		}
		
		void OnApplicationQuit()
		{
			if( windowDictionary != null )
			{
				foreach( KeyValuePair<string, IView> l in windowDictionary )
				{
					l.Value.OnApplicationQuit();
				}
			}
		}
	}
}
