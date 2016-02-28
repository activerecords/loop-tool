using UnityEngine;

using Unity.Data;
using Unity.GuiStyle;
using Unity.Function.Graphic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Monoamp.Common.Component.Sound.Player;
using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Data.Application.Sound;
using Monoamp.Common.Data.Standard.Riff.Wave;
using Monoamp.Common.Utility;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Unity.View
{
	public class ComponentPlayer : IView
	{
        private IPlayer player;

		private string title;
		private bool mouseButton;

		public delegate void ChangeMusicPrevious();
		public delegate void ChangeMusicNext();
		
		public ChangeMusicPrevious changeMusicPrevious;
		public ChangeMusicNext changeMusicNext;

		public Rect Rect{ get; set; }

		private int positionInBuffer;
		
		private readonly ComponentGui componentGui;

		public ComponentPlayer( ChangeMusicPrevious aChangeMusicPrevious, ChangeMusicNext aChangeMusicNext )
		{
			mouseButton = false;

			title = "";
			player = new PlayerNull();

			changeMusicPrevious = aChangeMusicPrevious;
			changeMusicNext = aChangeMusicNext;

			positionInBuffer = 0;
			componentGui = new ComponentGui();
		}
		
		public void SetPlayer( string aFilePath )
		{
			bool lIsMute = player.IsMute;
			bool lIsLoop = player.IsLoop;
			float lVolume = player.Volume;

			title = Path.GetFileNameWithoutExtension( aFilePath );
			player = ConstructorCollection.ConstructPlayer( aFilePath );

			player.IsMute = lIsMute;
			player.IsLoop = lIsLoop;
			player.Volume = lVolume;
		}
		
		public void SetPlayer( IPlayer aPlayer )
		{
			title = Path.GetFileNameWithoutExtension( aPlayer.FilePath );
			player = aPlayer;
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
			mouseButton = Input.GetMouseButton( 0 );

			GUILayout.BeginVertical( GuiStyleSet.StylePlayer.box );
			{
				GUILayout.TextArea( title, GuiStyleSet.StylePlayer.labelTitle );
				
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label( new GUIContent( player.GetTPosition().MMSS, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );

					float lPositionFloat = ( float )player.PositionRate;
					float lPositionAfter = GUILayout.HorizontalSlider( lPositionFloat, 0.0f, 1.0f, GuiStyleSet.StylePlayer.Seekbar, GuiStyleSet.StylePlayer.seekbarThumb );

					if( lPositionAfter != lPositionFloat )
					{
						player.PositionRate = lPositionAfter;
					}

					GUILayout.Label( new GUIContent( player.GetLength().MMSS, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );
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
			}
			GUILayout.EndVertical();
			
			float lHeightTitle = GuiStyleSet.StylePlayer.labelTitle.CalcSize( new GUIContent( title ) ).y;
			float lY = Rect.y + lHeightTitle + GuiStyleSet.StyleGeneral.box.margin.top + GuiStyleSet.StyleGeneral.box.padding.top + GuiStyleSet.StylePlayer.seekbar.fixedHeight;

			float lYVolume = Rect.y + lHeightTitle + GuiStyleSet.StylePlayer.seekbar.fixedHeight + 18;
			
			// Volume
			GUILayout.BeginArea( new Rect( 0, GuiSettings.GuiSettingLoopTool.volumeTop, Screen.width / 2.0f - GuiSettings.GuiSettingLoopTool.volumeRight, Screen.height ) );
			{
				GUILayout.BeginHorizontal();
				{
					GUILayout.FlexibleSpace();

					player.IsMute = GUILayout.Toggle( player.IsMute, new GUIContent( "", "StylePlayer.ToggleMute" ), GuiStyleSet.StylePlayer.toggleMute );
					
					if( player.IsMute == false )
					{
						player.Volume = GUILayout.HorizontalSlider( player.Volume, 0.0f, 1.0f, GuiStyleSet.StylePlayer.volumebar, GuiStyleSet.StyleSlider.horizontalbarThumb );
						
						if( player.Volume == 0.0f )
						{
							player.IsMute = true;
						}
					}
					else // isMute == true
					{
						float lVolume = GUILayout.HorizontalSlider( 0.0f, 0.0f, 1.0f, GuiStyleSet.StylePlayer.Volumebar, GuiStyleSet.StyleSlider.horizontalbarThumb );
						
						if( lVolume != 0.0f )
						{
							player.IsMute = false;
							player.Volume = lVolume;
						}
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			
			// Loop
			GUILayout.BeginArea( new Rect( Screen.width / 2.0f + GuiSettings.GuiSettingLoopTool.loopLeft, GuiSettings.GuiSettingLoopTool.loopTop, Screen.width / 2.0f, Screen.height ) );
			{
				player.IsLoop = GUILayout.Toggle( player.IsLoop, new GUIContent( "", "StylePlayer.ToggleLoop" ), GuiStyleSet.StylePlayer.toggleLoop );
			}
			GUILayout.EndArea();
		}

		public void OnRenderObject()
		{
			float lHeightTitle = GuiStyleSet.StylePlayer.labelTitle.CalcSize( new GUIContent( title ) ).y + GuiStyleSet.StylePlayer.labelTitle.margin.top + GuiStyleSet.StylePlayer.labelTitle.margin.bottom;
			float lY = Rect.y + lHeightTitle;

			if( player != null && player.GetLength().Second != 0.0d )
			{
				float lWidth = GuiStyleSet.StylePlayer.seekbar.fixedWidth;
				float lHeight = GuiStyleSet.StylePlayer.seekbar.fixedHeight;
				componentGui.DrawSeekBar( new Rect( Screen.width / 2 - lWidth / 2, lY, lWidth, lHeight ), GuiStyleSet.StylePlayer.seekbar, ( float )( player.Loop.start / player.GetLength() ), ( float )( player.Loop.end / player.GetLength() ), ( float )player.PositionRate );
			}
			else
			{
				float lWidth = GuiStyleSet.StylePlayer.seekbar.fixedWidth;
				float lHeight = GuiStyleSet.StylePlayer.seekbar.fixedHeight;
				componentGui.DrawSeekBar( new Rect( Screen.width / 2 - lWidth / 2, lY, lWidth, lHeight ), GuiStyleSet.StylePlayer.seekbar, 0.0f, 0.0f, 0.0f );
			}
			
			float lYVolume = Rect.y + lHeightTitle + GuiStyleSet.StylePlayer.seekbar.fixedHeight + 18;
			float lWidthVolume = GuiStyleSet.StylePlayer.volumebar.fixedWidth;
			float lHeightVolume = GuiStyleSet.StylePlayer.volumebar.fixedHeight;
			//componentGui.DrawVolumeBar( new Rect( Screen.width / 2.0f - GuiStyleSet.StylePlayer.buttonPrevious.fixedWidth / 2.0f - GuiStyleSet.StylePlayer.toggleStartPause.fixedWidth - lWidthVolume, lYVolume, lWidthVolume, lHeightVolume ), GuiStyleSet.StylePlayer.volumebar, player.Volume );
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			positionInBuffer = player.Update( aSoundBuffer, aChannels, aSampleRate, positionInBuffer );

			int lLength = aSoundBuffer.Length / aChannels;

			if( positionInBuffer != lLength && mouseButton == false )
			{
				changeMusicNext();

				positionInBuffer = player.Update( aSoundBuffer, aChannels, aSampleRate, positionInBuffer );
			}

			positionInBuffer %= lLength;
		}
		
		public void OnApplicationQuit()
		{
			
		}

		public string GetFilePath()
		{
			return player.GetFilePath();
		}
		
		public bool GetIsLoop()
		{
			return player.IsLoop;
		}
		
		public void SetIsLoop( bool aIsLoop )
		{
			player.IsLoop = aIsLoop;
		}
	}
}
