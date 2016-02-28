using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.AdaptedData.Music;
using Curan.Common.ApplicationComponent.Sound;
using Curan.Common.FileLoader.Music;
using Curan.Common.FilePool;
using Curan.Common.Struct;

namespace Unity.View
{
	public class DataLoopPlaylist
	{
		public DirectoryInfo directoryInfo;
		public DirectoryInfo directoryInfoRoot;

		public List<FileInfo> fileInfoList;
		public List<bool> isSelectedList;
		public List<LoopInformation> loopPointList;
		
		public delegate void PlayMusic( FileInfo aFileInfo );
		public delegate FileInfo GetPlayingMusic();
		
		public PlayMusic playMusic;
		public GetPlayingMusic getPlayingMusic;

		public DataLoopPlaylist( DirectoryInfo aDirectoryInfo, PlayMusic aSetPlayMusic, GetPlayingMusic aGetPlayingMusic )
		{
			directoryInfo = aDirectoryInfo;
			directoryInfoRoot = new DirectoryInfo( Application.streamingAssetsPath );

			playMusic = aSetPlayMusic;
			getPlayingMusic = aGetPlayingMusic;
		}
	}

	public class ViewLoopPlaylist : IView
	{
		public DataLoopPlaylist data;
        private Dictionary<string, IMusic> musicDictionary;
		private Vector2 scrollPosition;
		private string[] pathArray;
		
		public Rect Rect{ get; set; }

		public ViewLoopPlaylist( DirectoryInfo aDirectoryInfo, DataLoopPlaylist.PlayMusic aSetFileInfoPlaying, DataLoopPlaylist.GetPlayingMusic aGetFileInfoPlaying )
		{
			data = new DataLoopPlaylist( aDirectoryInfo, aSetFileInfoPlaying, aGetFileInfoPlaying );

			musicDictionary = new Dictionary<string, IMusic>();

			UpdateFileList();

			scrollPosition = Vector2.zero;
		}

		public void SetDirectory( DirectoryInfo aDirectoryInfo )
		{
			data.directoryInfo = aDirectoryInfo;
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
			float widthVerticalbar = GuiStyleSet.StyleScrollbar.verticalbar.fixedWidth;

			if( Event.current.type != EventType.Repaint )
			{
				UpdateFileList();
			}

			GUILayout.BeginVertical();
			{
				GUILayout.BeginScrollView( new Vector2( scrollPosition.x, 0.0f ), false, true, GuiStyleSet.StyleTable.horizontalbarHeader, GuiStyleSet.StyleTable.verticalbarHeader, GuiStyleSet.StyleGeneral.none );
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label( new GUIContent( "Name", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.MinWidth( 200.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						GUILayout.Label( new GUIContent( "Start", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 80.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						GUILayout.Label( new GUIContent( "End", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 80.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						GUILayout.Label( new GUIContent( "Length", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 80.0f ) );
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndScrollView();
				
				GUILayout.BeginHorizontal();
				{
					scrollPosition = GUILayout.BeginScrollView( scrollPosition, false, true, GuiStyleSet.StyleScrollbar.horizontalbar, GuiStyleSet.StyleScrollbar.verticalbar, GuiStyleSet.StyleScrollbar.view );
					{
						GUIStyle[] lViewRow = { GuiStyleSet.StyleTable.viewRowA, GuiStyleSet.StyleTable.viewRowB };

						for( int i = 0; i < data.fileInfoList.Count; i++ )
						{
							GUILayout.BeginHorizontal( lViewRow[i % 2] );
							{
								if( data.fileInfoList[i] == data.getPlayingMusic() )
								{
									if( GUILayout.Toggle( true, new GUIContent( data.fileInfoList[i].Name, "StyleTable.ToggleRow" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.MinWidth( 200.0f ) ) == false )
									{
										data.playMusic( data.fileInfoList[i] );
									}
								}
								else
								{
									if( GUILayout.Toggle( false, new GUIContent( data.fileInfoList[i].Name, "StyleTable.ToggleRow" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.MinWidth( 200.0f ) ) == true )
									{
										data.playMusic(  data.fileInfoList[i] );
									}
								}
								GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
								GUILayout.TextField( data.loopPointList[i].start.sample.ToString(), GuiStyleSet.StyleTable.textRow );
								GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
								GUILayout.TextField( data.loopPointList[i].end.sample.ToString(), GuiStyleSet.StyleTable.textRow );
								GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
								GUILayout.TextField( data.loopPointList[i].length.sample.ToString(), GuiStyleSet.StyleTable.textRow );
							}
							GUILayout.EndHorizontal();
						}

						GUILayout.BeginHorizontal();
						{
							GUILayout.BeginVertical();
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
							
							GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
							
							GUILayout.BeginVertical( GUILayout.Width( 80.0f ) );
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
							
							GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
							
							GUILayout.BeginVertical( GUILayout.Width( 80.0f ) );
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
							
							GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
							
							GUILayout.BeginVertical( GUILayout.Width( 80.0f ) );
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
						}
						GUILayout.EndHorizontal();
					}
					GUILayout.EndScrollView();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
		}

		private void UpdateFileList()
		{
			string[] lPathArray = PoolPathArray.Get( data.directoryInfo );

			if( lPathArray != pathArray )
			{
				pathArray = lPathArray;

				LoadLoop();
			}
		}

		private void LoadLoop()
		{
			data.fileInfoList = new List<FileInfo>();
			data.loopPointList = new List<LoopInformation>();

			for( int i = 0; i < pathArray.Length; i++ )
			{
				string lPath = pathArray[i];
				
				if( musicDictionary.ContainsKey( lPath ) == false )
				{
					IMusic lMusic = null;

					try
					{
						lMusic = LoaderMusic.Load( lPath );
					}
					catch( Exception aExpection )
					{
						Debug.LogWarning( "ViewLoopPlaylist Exception:" + aExpection.ToString() + ":" + lPath );
					}
				
					if( lMusic != null )
					{
						musicDictionary.Add( lPath, lMusic );
						data.fileInfoList.Add( new FileInfo( lPath ) );
						
						if( lMusic.Loop != null && lMusic.Loop.Count > 0 )
						{
							data.loopPointList.Add( lMusic.Loop[0][0] );
						}
						else
						{
							data.loopPointList.Add( new LoopInformation( 44100, 0, 0 ) );
						}
					}
				}
			}
		}

		public void ChangeMusicPrevious()
		{
			int lIndex = data.fileInfoList.IndexOf( data.getPlayingMusic() );

			if( lIndex >= 0 )
			{
				lIndex--;

				if( lIndex < 0 )
				{
					lIndex = data.fileInfoList.Count - 1;
				}

				data.playMusic( data.fileInfoList[lIndex] );
			}
		}

		public void ChangeMusicNext()
		{
			int lIndex = data.fileInfoList.IndexOf( data.getPlayingMusic() );

			if( lIndex >= 0 )
			{
				lIndex++;

				if( lIndex >= data.fileInfoList.Count )
				{
					lIndex = 0;
				}
				
				data.playMusic( data.fileInfoList[lIndex] );
			}
		}
    }
}

