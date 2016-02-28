using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;

using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Component.Sound;
using Monoamp.Common.Utility;
using Monoamp.Common.Struct;

using Monoamp.Boundary;

namespace Unity.View
{
	public class ComponentLoopSelector : IView
	{
		public Rect Rect{ get; set; }
		public Rect rect;

		private ComponentLoopEditor componentLoopEditor;

		private Vector2 scrollPosition;
		private LoopInformation[][] loopArrayArray;
		private PlayMusicInformation playMusicInformation;
		private int x;
		private int y;
		
		private bool isShow;

		public ComponentLoopSelector( ComponentLoopEditor aComponentPlayer )
		{
			componentLoopEditor = aComponentPlayer;
			scrollPosition = Vector2.zero;
			x = 0;
			y = 0;
			isShow = false;
			rect = new Rect( Screen.width - 340.0f, 210.0f, 320.0f, Screen.height - 230.0f );
		}

		public void SetPlayMusicInformation( PlayMusicInformation aPlayMusicInformation )
		{
			scrollPosition = Vector2.zero;
			x = 0;
			y = 0;

			playMusicInformation = aPlayMusicInformation;

			if( playMusicInformation != null )
			{
				loopArrayArray = new LoopInformation[playMusicInformation.music.GetCountLoopX()][];
				
				for( int i = 0; i < playMusicInformation.music.GetCountLoopX(); i++ )
				{
					loopArrayArray[i] = new LoopInformation[playMusicInformation.music.GetCountLoopY( i )];
					
					for( int j = 0; j < playMusicInformation.music.GetCountLoopY( i ); j++ )
					{
						loopArrayArray[i][j] = playMusicInformation.music.GetLoop( i, j );
					}
				}
			}
			else
			{
				loopArrayArray = null;
			}
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

		public void OnGUI()
		{
			int lX = x;
			int lY = y;
			
			int lStart = 0;
			int lEnd = 0;
			int lLength = 0;
			
			if( playMusicInformation != null )
			{
				lStart = ( int )componentLoopEditor.GetLoop().start.sample;
				lEnd = ( int )componentLoopEditor.GetLoop().end.sample;
				lLength = ( int )componentLoopEditor.GetLoop().length.sample;
			}
			
			GUILayout.BeginVertical();
			{
				float lHeightHeader = GuiStyleSet.StyleTable.labelHeaderTop.CalcSize( new GUIContent( "" ) ).y + GuiStyleSet.StyleTable.textHeader.CalcSize( new GUIContent( "" ) ).y + GuiStyleSet.StyleTable.partitionHorizontalHeader.fixedHeight;
				
				GUILayout.BeginScrollView( new Vector2( scrollPosition.x, 0.0f ), false, true, GuiStyleSet.StyleTable.horizontalbarHeader, GuiStyleSet.StyleTable.verticalbarHeader, GuiStyleSet.StyleGeneral.none, GUILayout.Height( lHeightHeader ) );
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label( new GUIContent( "Count", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.MinWidth( 80.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVerticalHeader );
						GUILayout.Label( new GUIContent( "Length", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 120.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVerticalHeader );
						GUILayout.Label( new GUIContent( "Start", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 120.0f ) );
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVerticalHeader );
						GUILayout.Label( new GUIContent( "End", "StyleTable.LabelHeader" ), GuiStyleSet.StyleTable.labelHeader, GUILayout.Width( 120.0f ) );
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndScrollView();
				
				scrollPosition = GUILayout.BeginScrollView( scrollPosition, false, true, GuiStyleSet.StyleScrollbar.horizontalbar, GuiStyleSet.StyleScrollbar.verticalbar, GuiStyleSet.StyleScrollbar.view );
				{
					if( playMusicInformation != null )
					{
						GUIStyle[] lViewRow = { GuiStyleSet.StyleTable.viewRowA, GuiStyleSet.StyleTable.viewRowB };
						
						int lCount = 0;

						for( int i = 0; i < loopArrayArray.Length && i < 128; i++ )
						{
							for( int j = 0; j < loopArrayArray[i].Length; j++ )
							{
								GUILayout.BeginHorizontal( lViewRow[lCount % 2] );
								{
									lCount++;

									bool lIsSelected = false;

									lIsSelected |= GUILayout.Button( new GUIContent( loopArrayArray[i].Length.ToString(), "StyleGeneral.Label" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.MinWidth( 80.0f ) );
									GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
									lIsSelected |= GUILayout.Button( new GUIContent( loopArrayArray[i][j].length.sample.ToString(), "StyleGeneral.Label" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.Width( 120.0f ) );
									GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
									lIsSelected |= GUILayout.Button( new GUIContent( loopArrayArray[i][j].start.sample.ToString(), "StyleGeneral.Label" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.Width( 120.0f ) );
									GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
									lIsSelected |= GUILayout.Button( new GUIContent( loopArrayArray[i][j].end.sample.ToString(), "StyleGeneral.Label" ), GuiStyleSet.StyleTable.toggleRow, GUILayout.Width( 120.0f ) );

									if( lIsSelected == true )
									{
										lX = i;
										lY = j;
									}
								}
								GUILayout.EndHorizontal();
							}
						}
					}
					
					GUILayout.BeginHorizontal();
					{
						GUILayout.BeginVertical( GUILayout.MinWidth( 80.0f ) );
						{
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndVertical();
						
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						
						GUILayout.BeginVertical( GUILayout.Width( 120.0f ) );
						{
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndVertical();
						
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						
						GUILayout.BeginVertical( GUILayout.Width( 120.0f ) );
						{
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndVertical();
						
						GUILayout.Label( new GUIContent( "", "StyleTable.PartitionVertical" ), GuiStyleSet.StyleTable.partitionVertical );
						
						GUILayout.BeginVertical( GUILayout.Width( 120.0f ) );
						{
							GUILayout.FlexibleSpace();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndScrollView();
			}
			GUILayout.EndVertical();
			
			if( x != lX || y != lY )
			{
				x = lX;
				y = lY;
				
				componentLoopEditor.SetLoop( loopArrayArray[x][y] );
				playMusicInformation.loopPoint = loopArrayArray[x][y];
				playMusicInformation.music.Loop = loopArrayArray[x][y];
				playMusicInformation.isSelected = true;
			}
		}
		
		public void Edit()
		{
			int lStart = 0;
			int lEnd = 0;
			int lLength = 0;
			
			if( playMusicInformation != null )
			{
				lStart = ( int )componentLoopEditor.GetLoop().start.sample;
				lEnd = ( int )componentLoopEditor.GetLoop().end.sample;
				lLength = ( int )componentLoopEditor.GetLoop().length.sample;
			}

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "Start", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
				GUILayout.TextField( lStart.ToString(), GuiStyleSet.StyleGeneral.textField );
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "End", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
				GUILayout.TextField( lEnd.ToString(), GuiStyleSet.StyleGeneral.textField );
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label( new GUIContent( "Length", "StyleGeneral.Label" ), GuiStyleSet.StyleGeneral.label );
				GUILayout.TextField( lLength.ToString(), GuiStyleSet.StyleGeneral.textField );
			}
			GUILayout.EndHorizontal();
		}

		public void OnRenderObject()
		{
			
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			
		}
		
		public void OnApplicationQuit()
		{
			
		}
    }
}

