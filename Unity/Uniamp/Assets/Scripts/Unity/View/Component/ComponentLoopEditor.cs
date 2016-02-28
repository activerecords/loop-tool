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
	public class ComponentLoopEditor
	{
        private IPlayer player;
		private string title;
		private bool mouseButtonPrevious;
		
		private readonly ComponentLoopSelector componentLoopSelector;
		private readonly ComponentWaveform componentWaveform;
		private readonly ComponentWaveformSmall componentWaveformSmall;
		private readonly ComponentGui componentGui;
		
		public delegate void ChangeMusicPrevious();
		public delegate void ChangeMusicNext();

		public ChangeMusicPrevious changeMusicPrevious;
		public ChangeMusicNext changeMusicNext;

		public Rect Rect{ get; set; }
		
		private bool isOnFrameLoopStart;
		private bool isOnFrameLoopEnd;
		private bool isOnFrameLoopRange;
		private Vector2 positionMousePrevious;
		private Texture2D textureCursorMove;
		private Texture2D textureCursorHorizontal;

		private int positionInBuffer;

		public float scale;
		public float scaleRate;
		private float positionWaveform;

		private PlayMusicInformation playMusicInformation;

		public ComponentLoopEditor( ChangeMusicPrevious aChangeMusicPrevious, ChangeMusicNext aChangeMusicNext )
		{
			mouseButtonPrevious = false;

			title = "";
			player = new PlayerNull();

			changeMusicPrevious = aChangeMusicPrevious;
			changeMusicNext = aChangeMusicNext;

			positionInBuffer = 0;

			componentLoopSelector = new ComponentLoopSelector( this );
			componentWaveform = new ComponentWaveform( player, new sbyte[2] );
			componentWaveformSmall = new ComponentWaveformSmall( player, new sbyte[2] );
			componentGui = new ComponentGui();
			
			scale = 1.0f;
			scaleRate = 0.0f;
			positionWaveform = 0.0f;
			
			textureCursorMove = ( Texture2D ) Resources.Load( "Cursor/CursorHand" );
			textureCursorHorizontal = ( Texture2D ) Resources.Load( "Cursor/CursorLeftRight" );
		}
		
		public void SetPlayer( string aFilePath, PlayMusicInformation aMusicInformation )
		{
			bool lIsMute = player.IsMute;
			bool lIsLoop = player.IsLoop;
			float lVolume = player.Volume;

			title = Path.GetFileNameWithoutExtension( aFilePath );
			player = ConstructorCollection.ConstructPlayer( aFilePath );

			if( aMusicInformation.isSelected == true )
			{
				SetLoop( aMusicInformation.loopPoint );
			}

			player.IsMute = lIsMute;
			player.IsLoop = lIsLoop;
			player.Volume = lVolume;

			playMusicInformation = aMusicInformation;
			componentLoopSelector.SetPlayMusicInformation( aMusicInformation );
		}
		
		public void SetPlayer( IPlayer aPlayer, PlayMusicInformation aMusicInformation )
		{
			title = Path.GetFileNameWithoutExtension( aPlayer.FilePath );
			player = aPlayer;
			
			if( aMusicInformation.isSelected == true )
			{
				SetLoop( aMusicInformation.loopPoint );
			}

			playMusicInformation = aMusicInformation;
			componentLoopSelector.SetPlayMusicInformation( aMusicInformation );
		}

		public void UpdateMesh()
		{
			try
			{
				MusicPcm lMusicPcm = ( MusicPcm )player.Music;
		
				if( lMusicPcm != null )
				{
					RiffWaveRiff lRiffWaveRiff = new RiffWaveRiff( player.GetFilePath() );
					WaveformPcm lWaveform = new WaveformPcm( lRiffWaveRiff );
					sbyte[] lWaveformByte = lWaveform.data.sampleByteArray[0];
					
					componentWaveform.Set( player, lWaveformByte );
				 	componentWaveformSmall.Set( player, lWaveformByte );
				}
				
				componentWaveform.UpdateVertex( player, scale, positionWaveform );
			}
			catch( Exception e )
			{
				Logger.Warning( "Execpstion:" + e );
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
			EditLoop();

			GUILayout.BeginVertical( GuiStyleSet.StylePlayer.box );
			{
				GUILayout.TextArea( title, GuiStyleSet.StylePlayer.labelTitle );

				GUILayout.Label( new GUIContent( "", "StyleGeneral.None" ), GuiStyleSet.StyleGeneral.none, GUILayout.Height( 0.0f ) );
				
				Rect rectScrollbar = new Rect( 0.0f, GuiSettings.GuiSettingLoopEditor.seekbarTop + GuiStyleSet.StylePlayer.seekbar.fixedHeight - 12.0f, Screen.width, GuiStyleSet.StyleLoopTool.waveformbar.fixedHeight );

				if( scaleRate != 0.0f )
				{
					float lPositionWaveformAfter = GUI.HorizontalScrollbar( rectScrollbar, positionWaveform, 1.0f / scale, 0.0f, 1.0f, GuiStyleSet.StyleLoopTool.waveformbar );

					if( lPositionWaveformAfter != positionWaveform )
					{
						positionWaveform = lPositionWaveformAfter;
						componentWaveform.ChangeScale( player.Loop, scale, positionWaveform );
						componentWaveform.UpdateVertex( player, scale, positionWaveform );
					}
				}
				else
				{
					GUI.HorizontalScrollbar( rectScrollbar, positionWaveform, 1.0f / scale, 0.0f, 1.0f, GuiStyleSet.StyleLoopTool.nullbar );
				}
				
				Rect rectSeekbar = new Rect( 0.0f, GuiSettings.GuiSettingLoopEditor.seekbarTop, Screen.width, GuiStyleSet.StylePlayer.seekbar.fixedHeight );
				float lPositionFloat = ( float )player.PositionRate;
				float lPositionAfter = GUI.HorizontalSlider( rectSeekbar, lPositionFloat, 0.0f, 1.0f, GuiStyleSet.StylePlayer.SeekbarEditor, GuiStyleSet.StylePlayer.seekbarThumbLoopEditor );
				
				if( lPositionAfter != lPositionFloat )
				{
					player.PositionRate = lPositionAfter;
				}

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label( new GUIContent( player.GetTPosition().MMSS, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );
					GUILayout.FlexibleSpace();
					GUILayout.Label( new GUIContent( player.GetLength().MMSS, "StylePlayer.LabelTime" ), GuiStyleSet.StylePlayer.labelTime );

				}
				GUILayout.EndHorizontal();
				
				GUILayout.BeginArea( new Rect( GuiSettings.GuiSettingLoopEditor.buttonsLeft, GuiSettings.GuiSettingLoopEditor.buttonsTop, Screen.width, Screen.height ) );
				{
					GUILayout.BeginHorizontal();
					{
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
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndArea();
			}
			GUILayout.EndVertical();

			// Volume
			GUILayout.BeginArea( new Rect( 0, GuiSettings.GuiSettingLoopEditor.volumeTop, Screen.width / 2.0f - GuiSettings.GuiSettingLoopEditor.volumeRight, Screen.height ) );
			{
				GUILayout.BeginHorizontal();
				{
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

			// Scale
			GUILayout.BeginArea( new Rect( Screen.width / 2.0f + GuiSettings.GuiSettingLoopEditor.scaleLeft, GuiSettings.GuiSettingLoopEditor.scaleTop, Screen.width / 2.0f, Screen.height ) );
			{
				GUILayout.BeginHorizontal();
				{
					if( GUILayout.Button( new GUIContent ( "", "StyleGeneral.ButtonMinus" ), GuiStyleSet.StyleGeneral.buttonMinus ) == true )
					{
						
					}
					
					float lScaleRateAfter = GUILayout.HorizontalSlider( scaleRate, 0.0f, 1.0f, GuiStyleSet.StyleSlider.horizontalbar, GuiStyleSet.StyleSlider.horizontalbarThumb );
					
					if( lScaleRateAfter != scaleRate )
					{
						scaleRate = lScaleRateAfter;
						float lPositionPrevious = 0.5f;
						
						if( scale != 1.0f )
						{
							lPositionPrevious = positionWaveform + 1.0f / scale / 2.0f;
						}
						
						scale = ( float )( 1.0f + player.GetLength().sample * scaleRate * scaleRate * scaleRate * scaleRate / Screen.width );
						positionWaveform = lPositionPrevious - 1.0f / scale / 2.0f;
						componentWaveform.ChangeScale( player.Loop, scale, positionWaveform );
						componentWaveform.UpdateVertex( player, scale, positionWaveform );
					}
					
					if( GUILayout.Button( new GUIContent ( "", "StyleGeneral.ButtonPlus" ), GuiStyleSet.StyleGeneral.buttonPlus ) == true )
					{
						
					}
					
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			
			// Loop
			GUILayout.BeginArea( new Rect( Screen.width / 2.0f + GuiSettings.GuiSettingLoopEditor.loopLeft, GuiSettings.GuiSettingLoopEditor.loopTop, Screen.width / 2.0f, Screen.height ) );
			{
				player.IsLoop = GUILayout.Toggle( player.IsLoop, new GUIContent( "", "StylePlayer.ToggleLoop" ), GuiStyleSet.StylePlayer.toggleLoop );
			}
			GUILayout.EndArea();
		}

		private void EditLoop()
		{
			float lTop = GuiSettings.GuiSettingLoopEditor.seekbarTop;
			float lHeightFrame = 60.0f;

			if( GUI.Button( new Rect( ( float )( player.Loop.start.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ) - 8.0f, lTop + 8.0f, 16.0f, 16.0f ), new GUIContent( "", "StyleLoopTool.ButtonPoint" ), GuiStyleSet.StyleLoopTool.buttonPoint ) == true )
			{
				
			}
			
			if( GUI.Button( new Rect( ( float )( player.Loop.start.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ), lTop + 24.0f, ( float )( player.Loop.length.sample / player.GetLength().sample * Screen.width * scale ), lHeightFrame ), new GUIContent( "", "StyleLoopTool.Frame" ), GuiStyleSet.StyleLoopTool.frame ) == true )
			{
				
			}
			
			if( GUI.Button( new Rect( ( float )( player.Loop.end.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ) - 8.0f, lTop + 8.0f, 16.0f, 16.0f ), new GUIContent( "", "StyleLoopTool.ButtonPoint" ), GuiStyleSet.StyleLoopTool.buttonPoint ) == true )
			{
				
			}
			
			if( Input.GetMouseButton( 0 ) != mouseButtonPrevious )
			{
				if( Input.GetMouseButton( 0 ) == true )
				{
					Rect lFrameLoopStart = new Rect( ( float )( player.Loop.start.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ) - 8.0f, lTop + 8.0f, 16.0f, 16.0f );
					
					if( lFrameLoopStart.Contains( Event.current.mousePosition ) == true )
					{
						isOnFrameLoopStart = true;
						Cursor.SetCursor( textureCursorHorizontal, new Vector2( 16.0f, 16.0f ), CursorMode.Auto );
					}
					
					Rect lFrameLoopEnd = new Rect( ( float )( player.Loop.end.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ) - 8.0f, lTop + 8.0f, 16.0f, 16.0f );
					
					if( lFrameLoopEnd.Contains( Event.current.mousePosition ) == true )
					{
						isOnFrameLoopEnd = true;
						Cursor.SetCursor( textureCursorHorizontal, new Vector2( 16.0f, 16.0f ), CursorMode.Auto );
					}
					
					Rect lFrameLoopRange = new Rect( ( float )( player.Loop.start.sample / player.GetLength().sample * Screen.width * scale - positionWaveform * Screen.width * scale ), lTop + 24.0f, ( float )( player.Loop.length.sample / player.GetLength().sample * Screen.width * scale ), lHeightFrame );
					
					if( lFrameLoopRange.Contains( Event.current.mousePosition ) == true )
					{
						isOnFrameLoopRange = true;
						//meshRendererDetailLeft.material.color = new Color( 0.0f, 0.5f, 0.5f, 0.5f );
						Cursor.SetCursor( textureCursorMove, new Vector2( 16.0f, 16.0f ), CursorMode.Auto );
					}
				}
				else
				{
					isOnFrameLoopStart = false;
					isOnFrameLoopEnd = false;
					isOnFrameLoopRange = false;
					//meshRendererDetailLeft.material.color = new Color( 0.0f, 1.0f, 0.0f, 0.5f );
					Cursor.SetCursor( null, Vector2.zero, CursorMode.Auto );
				}
			}
			
			if( isOnFrameLoopStart == true )
			{
				double lPositionStart = player.Loop.start.sample;
				
				lPositionStart += ( Event.current.mousePosition.x - positionMousePrevious.x ) * player.GetLength().sample / Screen.width / scale;
				
				if( lPositionStart < 0 )
				{
					lPositionStart = 0;
				}
				
				if( lPositionStart > player.Loop.end.sample )
				{
					lPositionStart = player.Loop.end.sample;
				}
				
				if( lPositionStart != player.Loop.start.sample )
				{
					SetLoop( new LoopInformation( ( int )player.Loop.end.sampleRate, ( int )lPositionStart, ( int )player.Loop.end.sample ) );
					playMusicInformation.loopPoint = player.Loop;
					playMusicInformation.music.Loop = player.Loop;
					playMusicInformation.isSelected = true;
				}
			}
			
			if( isOnFrameLoopEnd == true )
			{
				double lPositionEnd = player.Loop.end.sample;
				
				lPositionEnd += ( Event.current.mousePosition.x - positionMousePrevious.x ) * player.GetLength().sample / Screen.width / scale;
				
				if( lPositionEnd < player.Loop.start.sample )
				{
					lPositionEnd = player.Loop.start.sample;
				}
				
				if( lPositionEnd >= player.GetLength().sample )
				{
					lPositionEnd = player.GetLength().sample - 1;
				}
				
				if( lPositionEnd != player.Loop.start.sample )
				{
					SetLoop( new LoopInformation( ( int )player.Loop.start.sampleRate, ( int )player.Loop.start.sample, ( int )lPositionEnd ) );
					playMusicInformation.loopPoint = player.Loop;
					playMusicInformation.music.Loop = player.Loop;
					playMusicInformation.isSelected = true;
				}
			}
			
			if( isOnFrameLoopRange == true )
			{
				double lPositionStart = player.Loop.start.sample;
				
				lPositionStart += ( Event.current.mousePosition.x - positionMousePrevious.x ) * player.GetLength().sample / Screen.width / scale;
				
				if( lPositionStart < 0.0d )
				{
					lPositionStart = 0.0d;
				}
				
				if( lPositionStart + player.Loop.length.sample > player.GetLength().sample + 2 )
				{
					lPositionStart = player.GetLength().sample - player.Loop.length.sample + 2;
				}
				
				if( lPositionStart != player.Loop.start.sample )
				{
					SetLoop( new LoopInformation( player.Loop.length.sampleRate, ( int )lPositionStart, ( int )lPositionStart + ( int )player.Loop.length.sample - 1 ) );
					playMusicInformation.loopPoint = player.Loop;
					playMusicInformation.music.Loop = player.Loop;
					playMusicInformation.isSelected = true;
				}
			}
			
			mouseButtonPrevious = Input.GetMouseButton( 0 );
			positionMousePrevious = Event.current.mousePosition;
		}

		public void OnRenderObject()
		{
			if( player != null && player.GetLength().Second != 0.0d )
			{
				componentGui.DrawSeekBar( new Rect( 0.0f, GuiSettings.GuiSettingLoopEditor.seekbarTop, Screen.width, GuiStyleSet.StylePlayer.seekbar.fixedHeight ), GuiStyleSet.StylePlayer.seekbar, ( float )( player.Loop.start / player.GetLength() ), ( float )( player.Loop.end / player.GetLength() ), ( float )player.PositionRate );
			}
			else
			{
				componentGui.DrawSeekBar( new Rect( 0.0f, GuiSettings.GuiSettingLoopEditor.seekbarTop, Screen.width, GuiStyleSet.StylePlayer.seekbar.fixedHeight ), GuiStyleSet.StylePlayer.seekbar, 0.0f, 0.0f, 0.0f );
			}

			float lWidthVolume = GuiStyleSet.StylePlayer.volumebar.fixedWidth;
			float lHeightVolume = GuiStyleSet.StylePlayer.volumebar.fixedHeight;
			//componentGui.DrawVolumeBar( new Rect( Screen.width / 2.0f - 280.0f, lY + GuiStyleSet.StylePlayer.seekbar.fixedHeight + 20.0f, lWidthVolume, lHeightVolume ), GuiStyleSet.StylePlayer.volumebar, player.Volume );
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			positionInBuffer = player.Update( aSoundBuffer, aChannels, aSampleRate, positionInBuffer );

			int lLength = aSoundBuffer.Length / aChannels;

			if( positionInBuffer != lLength && mouseButtonPrevious == false )
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

		public LoopInformation GetLoop()
		{
			return player.Loop;
		}

		public void SetLoop( LoopInformation aLoopInformation )
		{
			if( aLoopInformation.length.sample != player.Loop.length.sample )
			{
				componentWaveform.ChangeLoop( aLoopInformation, scale, positionWaveform );
			}

			player.SetLoop( aLoopInformation );
			componentWaveform.UpdateVertex( player, scale, positionWaveform );
		}
	}
}
