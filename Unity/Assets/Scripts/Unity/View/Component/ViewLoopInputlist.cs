using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FilePool;
using Curan.Common.Struct;

namespace Unity.View
{
	public class DataLoopInputlist
	{
		public DirectoryInfo directoryInfo;
		public List<FileInfo> fileInfoList;
		public List<double> progressList;
		public List<bool> isSelectedList;
		public List<LoopInformation> loopSetList;
		
		public delegate void PlayMusic( FileInfo aFileInfo );
		public delegate FileInfo GetPlayingMusic();
		
		public PlayMusic playMusic;
		public GetPlayingMusic getPlayingMusic;

		public DataLoopInputlist( DirectoryInfo aDirectoryInfo, PlayMusic aPlayMusic, GetPlayingMusic aGetPlayingMusic )
		{
			directoryInfo = aDirectoryInfo;
			playMusic = aPlayMusic;
			getPlayingMusic = aGetPlayingMusic;
		}
	}

	public class ViewLoopInputlist : IView
    {
		public DataLoopInputlist data;
		private Vector2 scrollPosition;
		private bool isSelectedAll;
		private string[] pathArray;

		private const string STRING_PROGRESS = "Progress";
		
		public Rect Rect{ get; set; }

		public ViewLoopInputlist( DirectoryInfo aDirectoryInfo, DataLoopInputlist.PlayMusic aPlayMusic, DataLoopInputlist.GetPlayingMusic aGetPlayingMusic )
		{
			data = new DataLoopInputlist( aDirectoryInfo, aPlayMusic, aGetPlayingMusic );

			UpdateFileList();
			
			isSelectedAll = false;
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
			
			float lWidthTable = ( float )( Math.Floor( Screen.width / 2.0f ) - 14.0f );

			GUILayout.BeginVertical( GUILayout.MinWidth( lWidthTable ) );
			{
				GUILayout.BeginScrollView( new Vector2( scrollPosition.x, 0.0f ), false, true, GuiStyleSet.StyleTable.horizontalbarHeader, GuiStyleSet.StyleTable.verticalbarHeader, GuiStyleSet.StyleGeneral.none );
				{
					GUILayout.BeginHorizontal();
					{
						bool lIsSelectedAll = isSelectedAll;
						isSelectedAll = GUILayout.Toggle( isSelectedAll, new GUIContent( "", "StyleTable.ToggleCheckHeader" ), GuiStyleSet.StyleTable.toggleCheckHeader );
						if( isSelectedAll != lIsSelectedAll )
						{
							for( int i = 0; i < data.isSelectedList.Count; i++ )
							{
								data.isSelectedList[i] = isSelectedAll;
							}
						}
						
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						GUILayout.Label( new GUIContent( "Name", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.MinWidth( 200.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						GUILayout.Label( new GUIContent( "Progress", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 140.0f ) );
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndScrollView();
				
				GUILayout.BeginHorizontal();
				{
					scrollPosition = GUILayout.BeginScrollView( scrollPosition, false, true, GuiStyleSet.StyleScrollbar.horizontalbar, GuiStyleSet.StyleScrollbar.verticalbar, GuiStyleSet.StyleScrollbar.view, GUILayout.MinWidth( lWidthTable ) );
					{
						GUIStyle[] lViewRow = { GuiStyleSet.StyleTable.viewRowA, GuiStyleSet.StyleTable.viewRowB };

						for( int i = 0; i < data.loopSetList.Count; i++ )
						{
							GUILayout.BeginHorizontal( lViewRow[i % 2] );
							{
								data.isSelectedList[i] = GUILayout.Toggle( data.isSelectedList[i], new GUIContent( "", "StyleGeneral.ToggleCheck" ), GuiStyleSet.StyleGeneral.toggleCheck );
								GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );

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
										data.playMusic( data.fileInfoList[i] );
									}
								}
								
								GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );

								if( data.isSelectedList[i] == true )
								{
									if( data.progressList[i] > 0.0f )
									{
										GUILayout.HorizontalScrollbar( 0.0f, ( float )data.progressList[i], 0.0f, 1.01f, "progressbar", GUILayout.Width( 132.0f ) );
									}
									else
									{
										GUILayout.Label( new GUIContent( "", "StyleProgressbar.Progressbar" ), GuiStyleSet.StyleProgressbar.progressbar, GUILayout.Width( 132.0f ) );
									}
								}
								else
								{
									GUILayout.Label( new GUIContent( "", "StyleGeneral.None" ), GuiStyleSet.StyleGeneral.none, GUILayout.Width( 140.0f ) );
								}
							}
							GUILayout.EndHorizontal();
						}

						GUILayout.BeginHorizontal();
						{
							GUILayout.BeginVertical( GUILayout.Width( 24.0f ) );
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
							
							GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
							
							GUILayout.BeginVertical();
							{
								GUILayout.FlexibleSpace();
							}
							GUILayout.EndVertical();
							
							GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
							
							GUILayout.BeginVertical( GUILayout.Width( 140.0f ) );
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
                
				data.fileInfoList = new List<FileInfo>();
				data.loopSetList = new List<LoopInformation>();
				data.isSelectedList = new List<bool>();
				data.progressList = new List<double>();

				for( int i = 0; i < pathArray.Length; i++ )
				{
					string extentionLower = Path.GetExtension( pathArray[i] ).ToLower();

					if( extentionLower == ".wav" || extentionLower == ".aif" || extentionLower == ".mp3" || extentionLower == ".ogg" )
                    {
						data.fileInfoList.Add( new FileInfo( pathArray[i] ) );
						data.loopSetList.Add( new LoopInformation( 44100, 0, 0 ) );
						data.isSelectedList.Add( false );
						data.progressList.Add( 0.0d );
					}
				}
			}
		}
		
		public void OnApplicationQuit()
		{
			for( int i = 0; i < data.isSelectedList.Count; i++ )
			{
				data.isSelectedList[i] = false;
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
					lIndex = data.loopSetList.Count - 1;
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

				if( lIndex >= data.loopSetList.Count )
				{
					lIndex = 0;
				}
				
				data.playMusic( data.fileInfoList[lIndex] );
			}
		}
	}
}
