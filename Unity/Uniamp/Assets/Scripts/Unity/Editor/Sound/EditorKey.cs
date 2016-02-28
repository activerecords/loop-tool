using System;

//using LayerMiddle.Graphic.Perticle;
//using LayerMiddle.Sound.Controller.Midi;

using UnityEngine;
using UnityEditor;

namespace LayerEditor.Sound.Piano
{
    /*
	public class KeyEditor : EditorWindow
	{
		private Material material;
		private bool[] isNoteOnArrayPre;
		private bool[] isNoteOnArrayNow;
		private RectPerticle[] rectPerticleArray;
		private Rect[] rectArray;
		private const int KEY_RANGE = 8;
		private const int KEY_HEIGHT = 24;

		private const int PITCH_CN = 0;
		private const int PITCH_CS = 1;
		private const int PITCH_DN = 2;
		private const int PITCH_DS = 3;
		private const int PITCH_EN = 4;
		private const int PITCH_FN = 5;
		private const int PITCH_FS = 6;
		private const int PITCH_GN = 7;
		private const int PITCH_GS = 8;
		private const int PITCH_AN = 9;
		private const int PITCH_AS = 10;
		private const int PITCH_BN = 11;

		private const int PITCH_NAME_C = 0;
		private const int PITCH_NAME_D = 1;
		private const int PITCH_NAME_E = 2;
		private const int PITCH_NAME_F = 3;
		private const int PITCH_NAME_G = 4;
		private const int PITCH_NAME_A = 5;
		private const int PITCH_NAME_B = 6;

		private int instrument;
		private int bank;

		private float[][] soundBuffer;
		private int soundBufferLength;
		private int soundBufferChannels;

		//private int sampleRate;

		//private Texture2D dotTextureBlack;
		private Texture2D dotTextureRed;

		private bool isMouseButton;

		[MenuItem( "Window/Sound/Key Window" )]
		public static void Init()
		{
			EditorWindow.GetWindow<KeyEditor>( false, "Key Window" );
		}

		void Awake()
		{
			isNoteOnArrayPre = new bool[128];
			isNoteOnArrayNow = new bool[128];
			rectPerticleArray = new RectPerticle[128];
			rectArray = new Rect[128];
			//SynthesizerMidi = Piano.SynthesizerMidi;

			//sampleRate = UnityEngine.AudioSettings.outputSampleRate;

			//dotTextureBlack = CreateDotTexture2d( TextureFormat.RGB24, Color.black );
			dotTextureRed = CreateDotTexture2d( TextureFormat.RGB24, Color.red );

			isMouseButton = false;

			for( int i = 0; i < 32; i++ )
			{
				isNoteOnArrayPre[i] = false;
				isNoteOnArrayNow[i] = false;
			}

			for( int i = 0; i < 128; i++ )
			{
				int pitchName = i % 12;
				int octarveNumber = i / 12;
				rectPerticleArray[i] = new RectPerticle( ( octarveNumber % 4 ) * 7 * KEY_RANGE, ( i / 48 ) * KEY_HEIGHT * 3, KEY_RANGE, KEY_HEIGHT );
				rectArray[i] = new Rect( octarveNumber * 7 * KEY_RANGE, 100, KEY_RANGE, KEY_HEIGHT );
				//rectPerticleArray[i] = new RectPerticle( octarveNumber * 7 * KEY_RANGE, 0, KEY_RANGE, KEY_HEIGHT );
				//rectPerticleArray[i] = new RectPerticle( 0, Screen.height - octarveNumber * 7 * KEY_RANGE, KEY_HEIGHT, -KEY_RANGE );

				switch( pitchName )
				{
				case PITCH_CN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_C * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_C * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_C * KEY_RANGE );
					break;

				case PITCH_CS:
					rectPerticleArray[i].SetPosition( PITCH_NAME_C * KEY_RANGE + KEY_RANGE / 2, KEY_HEIGHT );
					rectArray[i].x += PITCH_NAME_C * KEY_RANGE + KEY_RANGE / 2;
					rectArray[i].y -= KEY_HEIGHT;
					//rectPerticleArray[i].SetPosition( KEY_HEIGHT, Screen.height - ( PITCH_NAME_C * KEY_RANGE + KEY_RANGE / 2 ) );
					break;

				case PITCH_DN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_D * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_D * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_D * KEY_RANGE );
					break;

				case PITCH_DS:
					rectPerticleArray[i].SetPosition( PITCH_NAME_D * KEY_RANGE + KEY_RANGE / 2, KEY_HEIGHT );
					rectArray[i].x += PITCH_NAME_D * KEY_RANGE + KEY_RANGE / 2;
					rectArray[i].y -= KEY_HEIGHT;
					//rectPerticleArray[i].SetPosition( KEY_HEIGHT, Screen.height - ( PITCH_NAME_D * KEY_RANGE + KEY_RANGE / 2 ) );
					break;

				case PITCH_EN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_E * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_E * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_E * KEY_RANGE );
					break;

				case PITCH_FN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_F * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_F * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_F * KEY_RANGE );
					break;

				case PITCH_FS:
					rectPerticleArray[i].SetPosition( PITCH_NAME_F * KEY_RANGE + KEY_RANGE / 2, KEY_HEIGHT );
					rectArray[i].x += PITCH_NAME_F * KEY_RANGE + KEY_RANGE / 2;
					rectArray[i].y -= KEY_HEIGHT;
					//rectPerticleArray[i].SetPosition( KEY_HEIGHT, Screen.height - ( PITCH_NAME_F * KEY_RANGE + KEY_RANGE / 2 ) );
					break;

				case PITCH_GN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_G * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_G * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_G * KEY_RANGE );
					break;

				case PITCH_GS:
					rectPerticleArray[i].SetPosition( PITCH_NAME_G * KEY_RANGE + KEY_RANGE / 2, KEY_HEIGHT );
					rectArray[i].x += PITCH_NAME_G * KEY_RANGE + KEY_RANGE / 2;
					rectArray[i].y -= KEY_HEIGHT;
					//rectPerticleArray[i].SetPosition( KEY_HEIGHT, Screen.height - ( PITCH_NAME_G * KEY_RANGE + KEY_RANGE / 2 ) );
					break;

				case PITCH_AN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_A * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_A * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_A * KEY_RANGE );
					break;

				case PITCH_AS:
					rectPerticleArray[i].SetPosition( PITCH_NAME_A * KEY_RANGE + KEY_RANGE / 2, KEY_HEIGHT );
					rectArray[i].x += PITCH_NAME_A * KEY_RANGE + KEY_RANGE / 2;
					rectArray[i].y -= KEY_HEIGHT;
					//rectPerticleArray[i].SetPosition( KEY_HEIGHT, Screen.height - ( PITCH_NAME_A * KEY_RANGE + KEY_RANGE / 2 ) );
					break;

				case PITCH_BN:
					rectPerticleArray[i].SetPosition( PITCH_NAME_B * KEY_RANGE, 0 );
					rectArray[i].x += PITCH_NAME_B * KEY_RANGE;
					rectArray[i].y -= 0;
					//rectPerticleArray[i].SetPosition( 0, Screen.height - PITCH_NAME_B * KEY_RANGE );
					break;
				}
			}
		}

		void Start()
		{
			//CreateMaterial();
			instrument = 0;
			bank = 0;
		}

		void OnInspectorUpdate()
		{
			//Repaint();
		}

		void Update()
		{

		}

		private void UpdateKey( Event aCurrent )
		{
			for( int i = 0; i < 128; i++ )
			{
				isNoteOnArrayNow[i] = false;
			}

			if( aCurrent.type == EventType.MouseDown )
			{
				isMouseButton = true;
				Debug.Log( "MouseDown" );
			}

			if( aCurrent.type == EventType.MouseUp )
			{
				isMouseButton = false;
				Debug.Log( "MouseUp" );
			}

			if( isMouseButton == true )
			{
				for( int i = 0; i < 128; i++ )
				{
					//Rect rect = new Rect( rectPerticleArray[i].position.x + rectPerticleArray[i].rect.x, rectPerticleArray[i].position.y + rectPerticleArray[i].rect.y, rectPerticleArray[i].rect.width, rectPerticleArray[i].rect.height );

					if( Event.current.mousePosition.x >= rectArray[i].x && Event.current.mousePosition.x < rectArray[i].x + rectArray[i].width && Event.current.mousePosition.y >= rectArray[i].y && Event.current.mousePosition.y < rectArray[i].y + rectArray[i].height )
					{
						isNoteOnArrayNow[i] = true;
					}
				}
			}

			for( int i = 0; i < 128; i++ )
			{
				if( isNoteOnArrayPre[i] == false && isNoteOnArrayNow[i] == true )
				{
					// To Be Fixed.
					//MidiManager.NoteOn( 0, ( byte )i, 127, sampleRate );
					//SynthesizerMidi.NoteOnGlobal( ( byte )i, 127, sampleRate );
					Debug.Log( "On:" );
				}

				if( isNoteOnArrayPre[i] == true && isNoteOnArrayNow[i] == false )
				{
					// To Be Fixed.
					//MidiManager.NoteOff( 0, ( byte )i );
					//SynthesizerMidi.NoteOffGlobal( ( byte )i );
					Debug.Log( "Off:" );
				}

				isNoteOnArrayPre[i] = isNoteOnArrayNow[i];
			}
		}

		void OnGUI()
		{
			int temp = instrument;

			// 垂直方向にグループ化.
			GUILayout.BeginHorizontal( "Box" );
			{
				instrument = EditorGUILayout.IntSlider( "Instrument", instrument, 0, 127 );

				if( GUILayout.Button( "<" ) )
				{
					if( instrument > 0 )
					{
						instrument--;
					}
				}

				if( GUILayout.Button( ">" ) )
				{
					if( instrument < 127 )
					{
						instrument++;
					}
				}
			}
			GUILayout.EndHorizontal();

			if( instrument != temp )
			{
				// To Be Fixed.
				//SynthesizerMidi.GetMidiStatusGlobal().SetInstrument( ( byte )instrument );
			}

			temp = bank;

			// 垂直方向にグループ化.
			GUILayout.BeginHorizontal( "Box" );
			{
				bank = EditorGUILayout.IntSlider( "Bank", bank, 0, 1 );

				if( GUILayout.Button( "<" ) )
				{
					if( bank > 0 )
					{
						bank--;
					}
				}

				if( GUILayout.Button( ">" ) )
				{
					if( bank < 1 )
					{
						bank++;
					}
				}
			}
			GUILayout.EndHorizontal();

			if( bank != temp )
			{
				// To Be Fixed.
				//SynthesizerMidi.GetMidiStatusGlobal().SetBank( ( byte )bank * 0x7F00 );
			}

			if( isNoteOnArrayPre != null && isNoteOnArrayNow != null )
			{
				UpdateKey( Event.current );
			}

			for( int i = 0; i < 128; i++ )
			{
				DrawRectFrame( rectArray[i] );
			}
		}

		private static Texture2D CreateDotTexture2d( TextureFormat aTextureFormat, Color aColor )
		{
			Texture2D lTexture = new Texture2D( 1, 1, aTextureFormat, false );

			lTexture.SetPixel( 1, 1, aColor );
			lTexture.Apply();

			return lTexture;
		}

		private void DrawRectFrame( Rect aRect )
		{
			int lPositionX = ( int )aRect.x;
			int lPositionY = ( int )aRect.y;
			int lWidth = ( int )aRect.width;
			int lHeight = ( int )aRect.height;

			Rect lFrameRectTop = new Rect( lPositionX, lPositionY, lWidth, 1.0f );
			Rect lFrameRectBottom = new Rect( lPositionX, lPositionY + lHeight, lWidth, 1.0f );
			Rect lFrameRectLeft = new Rect( lPositionX, lPositionY, 1.0f, lHeight );
			Rect lFrameRectRight = new Rect( lPositionX + lWidth, lPositionY, 1.0f, lHeight );

			GUI.DrawTexture( lFrameRectTop, dotTextureRed );
			GUI.DrawTexture( lFrameRectBottom, dotTextureRed );
			GUI.DrawTexture( lFrameRectLeft, dotTextureRed );
			GUI.DrawTexture( lFrameRectRight, dotTextureRed );
		}
	}*/
}
