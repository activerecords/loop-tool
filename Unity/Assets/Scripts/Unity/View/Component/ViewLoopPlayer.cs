using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.ApplicationComponent.Sound;
using Curan.Common.FileLoader.Music;
using Curan.Common.Struct;
using Curan.Utility;

namespace Unity.View
{
	public class ViewLoopPlayer : IView
	{
        private IPlayer player;

		private bool isMute;
		private float volume;
		private float volumePre;
		private float position;
		private float positionPre;

		private FileInfo fileInfo;
		private string title;

		public delegate void ChangeMusicPrevious();
		public delegate void ChangeMusicNext();
		
		public ChangeMusicPrevious changeMusicPrevious;
		public ChangeMusicNext changeMusicNext;

		public Rect Rect{ get; set; }

		public ViewLoopPlayer( FileInfo aFileInfo, ChangeMusicPrevious aChangeMusicPrevious, ChangeMusicNext aChangeMusicNext )
		{
			fileInfo = aFileInfo;

			if( fileInfo == null )
			{
				title = "Title";
				player = new PlayerNull();
			}
			else
			{
				title = fileInfo.Name;
				player = PlayerLoader.Load( fileInfo.FullName );
			}

			changeMusicPrevious = aChangeMusicPrevious;
			changeMusicNext = aChangeMusicNext;

			isMute = false;
			volume = 0.5f;
			volumePre = 0.5f;
			position = 0.0f;
			positionPre = 0.0f;
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
			GUILayout.BeginVertical( GuiStyleSet.StyleGeneral.box );
			{
				GUILayout.TextArea( title, GuiStyleSet.StylePlayer.labelTitle );
				
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label( new GUIContent( player.GetTimePosition().MMSSmmm, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );

					if( positionPre == position )
					{
						positionPre = ( float )player.Position;
						position = GUILayout.HorizontalScrollbar( positionPre, 0.01f, 0.0f, 1.01f, "seekbar" );
					}
					else
					{
						position = GUILayout.HorizontalScrollbar( position, 0.01f, 0.0f, 1.01f, "seekbar" );

						if( Input.GetMouseButtonUp( 0 ) == true )
						{
							player.Position = position;
							positionPre = position;
						}
					}

					GUILayout.Label( new GUIContent( player.GetTimeElapsed().MMSSmmm, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
	                GUILayout.FlexibleSpace();

					if( GUILayout.Button( new GUIContent( "", "StylePlayer.ButtonPrevious" ), GuiStyleSet.StylePlayer.buttonPrevious ) == true )
					{
						changeMusicPrevious();
					}

					bool lIsPlaying = GUILayout.Toggle( player.GetFlagPlaying(), new GUIContent( "", "StylePlayer.ToggleStartPause" ), GuiStyleSet.StylePlayer.toggleStartPause );

					if( lIsPlaying != player.GetFlagPlaying() )
					{
						if( lIsPlaying == true )
						{
							player.Play();
						}
						else
						{
							player.Pause();
						}
					}

					if( GUILayout.Button( new GUIContent( "", "StylePlayer.ButtonNext" ), GuiStyleSet.StylePlayer.buttonNext ) == true )
					{
						changeMusicNext();
					}
	                
	                GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();

					bool lIsMutePre = isMute;
					
					isMute = GUILayout.Toggle( isMute, new GUIContent( "", "StylePlayer.ToggleMute" ), GuiStyleSet.StylePlayer.toggleMute );

					if( isMute != lIsMutePre )
					{
						if( isMute == true )
						{
							volumePre = volume;
							volume = 0.0f;
						}
						else
						{
							volume = volumePre;
						}
					}

					volume = GUILayout.HorizontalSlider( volume, 0.0f, 1.00f, GuiStyleSet.StyleSlider.horizontalbar, GuiStyleSet.StyleSlider.horizontalbarThumb );
					player.Volume = volume;
					
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();				
			}
			GUILayout.EndVertical();
		}
		
		public void OnRenderObject()
		{
			float lHeightMenu = GuiStyleSet.StyleMenu.button.CalcSize( new GUIContent( "" ) ).y;
			float lHeightTitle = GuiStyleSet.StylePlayer.labelTitle.CalcSize( new GUIContent( title ) ).y;
			float lY = lHeightMenu + lHeightTitle + GuiStyleSet.StyleGeneral.box.margin.top + GuiStyleSet.StyleGeneral.box.padding.top;

			if( player != null && player.GetTimeLength().Second != 0.0d )
			{
				float lWidth = GuiStyleSet.StylePlayer.seekbar.fixedWidth;
				float lHeight = GuiStyleSet.StylePlayer.seekbar.fixedHeight;
				Gui.DrawSeekBar( new Rect( Screen.width / 2 - lWidth / 2, lY + lHeight, lWidth, lHeight ), GuiStyleSet.StylePlayer.seekbarImage, ( float )( player.GetLoopPoint().start.Seconds / player.GetTimeLength().Seconds ), ( float )( player.GetLoopPoint().end.Seconds / player.GetTimeLength().Seconds ), ( float )player.Position );
			}
			else
			{
				float lWidth = GuiStyleSet.StylePlayer.seekbar.fixedWidth;
				float lHeight = GuiStyleSet.StylePlayer.seekbar.fixedHeight;
				Gui.DrawSeekBar( new Rect( Screen.width / 2 - lWidth / 2, lY + lHeight, lWidth, lHeight ), GuiStyleSet.StylePlayer.seekbarImage, 0.0f, 0.0f, 0.0f );
			}
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			player.Update( aSoundBuffer, aChannels, aSampleRate );
		}
		
		public void OnApplicationQuit()
		{
			
		}

		public LoopInformation GetLoopPoint()
		{
			LoopInformation lLoopPoint = player.GetLoopPoint();

			if( lLoopPoint == null )
			{
				lLoopPoint = new LoopInformation( 44100, 0, 0 );
			}

			return lLoopPoint;
		}

		public FileInfo GetFileInfo()
		{
			return fileInfo;
		}
	}
}
