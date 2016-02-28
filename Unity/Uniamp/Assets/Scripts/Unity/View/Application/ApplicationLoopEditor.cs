using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Component.Sound.Player;
using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Component.Sound.LoopTool;
using Monoamp.Common.Utility;
using Monoamp.Common.Struct;

using Monoamp.Boundary;

namespace Unity.View.LoopEditor
{
	public class ApplicationLoopEditor : IView
	{
		public Rect Rect{ get; set; }

		public readonly List<DirectoryInfo> directoryInfoRecentInputList;
		public readonly List<DirectoryInfo> directoryInfoRecentOutputList;
		
		private IPlayer player;
		private readonly MenuBar menu;
		private readonly ComponentPlayer componentLoopPlayer;
		private readonly ComponentLoopEditor componentLoopEditor;
		private readonly ComponentLoopSelector componentLoopSelector;
		private readonly ComponentInputlist componentInputlist;
		private readonly ComponentPlaylist componentPlaylist;
		private readonly ComponentLoopSearch componentLoopSearch;
		private readonly ComponentLoopSave componentLoopSave;
		private readonly ComponentDirectoryBar componentDirectoryBarInput;
		private readonly ComponentDirectoryBar componentDirectoryBarOutput;

		private string tooltipPrevious;

		private enum Mode
		{
			Search,
			Editor,
		}

		private Mode mode;

		public ApplicationLoopEditor( DirectoryInfo aDirectoryInfoInput, DirectoryInfo aDirectoryInfoOutput )
		{
			player = new PlayerNull();
			directoryInfoRecentInputList = new List<DirectoryInfo>();
			directoryInfoRecentOutputList = new List<DirectoryInfo>();
			tooltipPrevious = "";

			try
			{
				using( StreamReader u = new StreamReader( Application.streamingAssetsPath + "/Config/LoopToolInput.ini" ) )
				{
					for( string line = u.ReadLine(); line != null; line = u.ReadLine() )
					{
						if( Directory.Exists( line ) == true )
						{
							directoryInfoRecentInputList.Add( new DirectoryInfo( line ) );
							
							if( directoryInfoRecentInputList.Count >= 5 )
							{
								break;
							}
						}
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.BreakDebug( "Exception:" + aExpection );

				using( StreamWriter u = new StreamWriter( Application.streamingAssetsPath + "/Config/LoopToolInput.ini" ) )
				{
					Logger.BreakDebug( "Create LoopToolInput.ini" );
				}
			}
			
			try
			{
				using( StreamReader u = new StreamReader( Application.streamingAssetsPath + "/Config/LoopToolOutput.ini" ) )
				{
					for( string line = u.ReadLine(); line != null; line = u.ReadLine() )
					{
						if( Directory.Exists( line ) == true )
						{
							directoryInfoRecentOutputList.Add( new DirectoryInfo( line ) );
							
							if( directoryInfoRecentOutputList.Count >= 5 )
							{
								break;
							}
						}
					}
				}
			}
			catch( Exception aExpection )
			{
				using( StreamWriter u = new StreamWriter( Application.streamingAssetsPath + "/Config/LoopToolOutput.ini" ) )
				{
					Logger.BreakDebug( "Exception:" + aExpection );
					Logger.BreakDebug( "Create LoopToolOutput.ini" );
				}
			}

			if( directoryInfoRecentInputList.Count == 0 )
			{
				directoryInfoRecentInputList.Add( aDirectoryInfoInput );
			}
			
			if( directoryInfoRecentOutputList.Count == 0 )
			{
				directoryInfoRecentOutputList.Add( aDirectoryInfoOutput );
			}

			mode = Mode.Search;
			menu = new MenuBar( Application.streamingAssetsPath + "/Language/LoopTool/Menu/MenuBar.language", this );
			componentLoopPlayer = new ComponentPlayer( ChangeMusicPrevious, ChangeMusicNext );
			componentLoopEditor = new ComponentLoopEditor( ChangeMusicPrevious, ChangeMusicNext );
			componentLoopSelector = new ComponentLoopSelector( componentLoopEditor );
			componentDirectoryBarInput = new ComponentDirectoryBar( SetInput, directoryInfoRecentInputList );
			componentDirectoryBarOutput = new ComponentDirectoryBar( SetOutput, directoryInfoRecentOutputList );

			componentInputlist = new ComponentInputlist( directoryInfoRecentInputList[0], PlayMusic, GetPlayingMusic );
			componentPlaylist = new ComponentPlaylist( directoryInfoRecentOutputList[0], PlayMusic, GetPlayingMusic );

			componentLoopSearch = new ComponentLoopSearch( componentInputlist, componentPlaylist );
			componentLoopSave = new ComponentLoopSave( componentPlaylist );

			SetInput( directoryInfoRecentInputList[0] );
			SetOutput( directoryInfoRecentOutputList[0] );
		}
		
		private void PlayMusic( string aFilePath )
		{
			if( componentPlaylist.musicInformationDictionary.ContainsKey( aFilePath ) == true )
			{
				bool lIsMute = player.IsMute;
				bool lIsLoop = player.IsLoop;
				float lVolume = player.Volume;
				
				player = ConstructorCollection.ConstructPlayer( aFilePath );
				
				player.IsMute = lIsMute;
				player.IsLoop = lIsLoop;
				player.Volume = lVolume;

				componentLoopEditor.SetPlayer( player, componentPlaylist.musicInformationDictionary[aFilePath] );
				componentLoopPlayer.SetPlayer( player );
				componentLoopSelector.SetPlayMusicInformation( componentPlaylist.musicInformationDictionary[aFilePath] );
			}
			else
			{
				if( componentInputlist.musicInformationDictionary.ContainsKey( aFilePath ) == true )
				{
					IMusic lMusic = componentInputlist.musicInformationDictionary[aFilePath].music;
					LoopInformation lLoop = componentInputlist.musicInformationDictionary[aFilePath].music.Loop;
					PlayMusicInformation l = new PlayMusicInformation( 0, false, lMusic, lLoop );

					bool lIsMute = player.IsMute;
					bool lIsLoop = player.IsLoop;
					float lVolume = player.Volume;

					player = ConstructorCollection.ConstructPlayer( aFilePath );

					player.IsMute = lIsMute;
					player.IsLoop = lIsLoop;
					player.Volume = lVolume;

					componentLoopEditor.SetPlayer( player, l );
					componentLoopPlayer.SetPlayer( player );
					componentLoopSelector.SetPlayMusicInformation( l );
				}
			}

			componentLoopEditor.UpdateMesh();
		}
		
		private string GetPlayingMusic()
		{
			return componentLoopEditor.GetFilePath();
		}
		
		public void SetInput( DirectoryInfo aDirectoryInfo )
		{
			componentInputlist.SetDirectory( aDirectoryInfo );

			for( int i = directoryInfoRecentInputList.Count - 1; i >= 0; i-- )
			{
				if( directoryInfoRecentInputList[i].FullName == aDirectoryInfo.FullName )
				{
					directoryInfoRecentInputList.RemoveAt( i );
				}
			}

			directoryInfoRecentInputList.Insert( 0, aDirectoryInfo );

			if( directoryInfoRecentInputList.Count > 5 )
			{
				directoryInfoRecentInputList.RemoveAt( 5 );
			}

			try
			{
				using( StreamWriter u = new StreamWriter( Application.streamingAssetsPath + "/Config/LoopToolInput.ini", false ) )
				{
					foreach( DirectoryInfo l in directoryInfoRecentInputList )
					{
						u.WriteLine( l.FullName );
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.BreakDebug( "Exception:" + aExpection );
				Logger.BreakDebug( "Failed write LoopToolInput.ini" );
			}
		}
		
		public void SetOutput( DirectoryInfo aDirectoryInfo )
		{
			componentPlaylist.SetDirectory( aDirectoryInfo );

			for( int i = directoryInfoRecentOutputList.Count - 1; i >= 0; i-- )
			{
				if( directoryInfoRecentOutputList[i].FullName == aDirectoryInfo.FullName )
				{
					directoryInfoRecentOutputList.RemoveAt( i );
				}
			}

			directoryInfoRecentOutputList.Insert( 0, aDirectoryInfo );
			
			if( directoryInfoRecentOutputList.Count > 5 )
			{
				directoryInfoRecentOutputList.RemoveAt( 5 );
			}

			try
			{
				using( StreamWriter u = new StreamWriter( Application.streamingAssetsPath + "/Config/LoopToolOutput.ini", false ) )
				{
					foreach( DirectoryInfo l in directoryInfoRecentOutputList )
					{
						u.WriteLine( l.FullName );
					}
				}
			}
			catch( Exception aExpection )
			{
				Logger.BreakDebug( "Exception:" + aExpection );
				Logger.BreakDebug( "Failed write LoopToolOutput.ini" );
			}
		}
		
		public void ChangeMusicPrevious()
		{
			componentInputlist.ChangeMusicPrevious();
			componentPlaylist.ChangeMusicPrevious();
		}
		
		public void ChangeMusicNext()
		{
			componentInputlist.ChangeMusicNext();
			componentPlaylist.ChangeMusicNext();
		}
		
		public void Awake()
		{
			componentLoopEditor.Awake();
		}
		
		public void Start()
		{
			GameObject.Find( "WaveformSmall" ).transform.position = new Vector3( 0, 0, -100 );
		}
		
		public void Update()
		{
			componentLoopEditor.Update();
		}
		
		public void OnGUI()
		{
			menu.OnGUI();

			componentLoopEditor.OnGUI();

			GUILayout.BeginArea( new Rect( GuiSettings.GuiSettingLoopEditor.searchLeft, GuiSettings.GuiSettingLoopEditor.searchTop, Screen.width, Screen.height ) );
			{
				componentLoopSearch.OnGUI();
			}
			GUILayout.EndArea();

			GUILayout.BeginArea( new Rect( GuiSettings.GuiSettingLoopEditor.textLeft, GuiSettings.GuiSettingLoopEditor.textTop, Screen.width, Screen.height ) );
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.BeginHorizontal( GuiStyleSet.StyleGeneral.box );
					{
						componentLoopSelector.Edit();
						componentLoopSave.OnGUI();
						
						if( mode == Mode.Search )
						{
							GUILayout.Toggle( true, "", GuiStyleSet.StyleLoopTool.toggleModeSearch );
							
							if( GUILayout.Toggle( false, "", GuiStyleSet.StyleLoopTool.toggleModeEditor ) == true )
							{
								Debug.Log( "Editor" );
								mode = Mode.Editor;
							}
						}
						else
						{
							if( GUILayout.Toggle( false, "", GuiStyleSet.StyleLoopTool.toggleModeSearch ) == true )
							{
								Debug.Log( "Search" );
								mode = Mode.Search;
							}
							
							GUILayout.Toggle( true, "", GuiStyleSet.StyleLoopTool.toggleModeEditor );
						}
					}
					GUILayout.EndHorizontal();

					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();

			GUILayout.BeginArea( new Rect( 0, GuiSettings.GuiSettingLoopEditor.tableTop, Screen.width, Screen.height ) );
			{
				GUILayout.BeginHorizontal();
				{
					if( mode == Mode.Search )
					{
						GUILayout.BeginVertical( GUILayout.Width( Screen.width - GuiSettings.GuiSettingLoopEditor.widthTableOutput ) );
						{
							GUILayout.BeginHorizontal();
							{
								GUILayout.Label( new GUIContent ( "Input", "StyleLoopTool.LabelInput" ), GuiStyleSet.StyleLoopTool.labelInput );
								componentDirectoryBarInput.OnGUI();
							}
							GUILayout.EndHorizontal();

							GUILayout.Label( new GUIContent ( "", "StyleLoopTool.BackgroundInput" ), GuiStyleSet.StyleLoopTool.backgroundInput );

							componentInputlist.OnGUI();
						}
						GUILayout.EndVertical();
					}

					GUILayout.BeginVertical( GUILayout.Width( GuiSettings.GuiSettingLoopEditor.widthTableOutput ) );
					{
						GUILayout.BeginHorizontal();
						{
							GUILayout.Label( new GUIContent ( "Output", "StyleLoopTool.LabelOutput" ), GuiStyleSet.StyleLoopTool.labelOutput );
							componentDirectoryBarOutput.OnGUI();
						}
						GUILayout.EndHorizontal();

						GUILayout.Label( new GUIContent ( "", "StyleLoopTool.BackgroundOutput" ), GuiStyleSet.StyleLoopTool.backgroundOutput );

						componentPlaylist.OnGUI();
					}
					GUILayout.EndVertical();
					
					if( mode == Mode.Editor )
					{
						GUILayout.BeginVertical( GUILayout.Width( Screen.width - GuiSettings.GuiSettingLoopEditor.widthTableOutput ) );
						{
							GUILayout.Label( new GUIContent ( "Edit", "StyleLoopTool.LabelOutput" ), GuiStyleSet.StyleLoopTool.labelInput );
							GUILayout.Label( new GUIContent ( "", "StyleLoopTool.BackgroundOutput" ), GuiStyleSet.StyleLoopTool.backgroundInput );
							componentLoopSelector.OnGUI();
						}
						GUILayout.EndVertical();
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			
			GUILayout.BeginArea( new Rect( Screen.width - 100, 32, 80, 32 ) );
			{
				GUILayout.BeginHorizontal();
				{
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();

			if( GUI.tooltip != "" && GUI.tooltip != tooltipPrevious )
			{
				Logger.BreakDebug( GUI.tooltip );
				tooltipPrevious = GUI.tooltip;
			}
		}

		public void OnRenderObject()
		{
			float lHeightMenubar = GuiStyleSet.StyleMenu.bar.fixedHeight;
			componentLoopPlayer.Rect = new Rect( 0.0f, lHeightMenubar, Screen.width, Screen.height - lHeightMenubar );
			componentLoopEditor.Rect = new Rect( 0.0f, lHeightMenubar, Screen.width, Screen.height - lHeightMenubar );

			componentLoopEditor.OnRenderObject();
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			componentLoopEditor.OnAudioFilterRead( aSoundBuffer, aChannels, aSampleRate );
		}
		
		public void OnApplicationQuit()
		{
			componentLoopSearch.OnApplicationQuit();
		}

		public bool GetIsCutLast()
		{
			return LoopSave.IsCutLast;
		}
		
		public void SetIsCutLast( bool aIsCutLast )
		{
			LoopSave.IsCutLast = aIsCutLast;
		}
	}
}
