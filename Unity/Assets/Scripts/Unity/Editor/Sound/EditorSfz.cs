using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Collections.Generic;

namespace Curan.UnityEditorView.Sound
{
    public class EditorSfz : EditorWindow
    {
		private Vector2 vectorScrollList;
		private Vector2 vectorScrollEditor;
		private List<string> nameList;
		private Dictionary<string, string> sfzDictioinary;
		private string keyEditingSfz;
		
		private GUIStyle[] customStyles;

		[MenuItem( "Window/Sound/Sfz Editor" )]
        static void Open()
        {
			GetWindow<EditorSfz>( false, "Sfz Editor" );
        }

		void Awake()
		{
			nameList = new List<string>();
			sfzDictioinary = new Dictionary<string, string>();
			keyEditingSfz = "";
			vectorScrollList = Vector2.zero;
			vectorScrollEditor = Vector2.zero;
		}

        void Update()
        {

        }

        void OnInspectorUpdate()
        {

        }

        void OnGUI()
		{
            GUILayout.BeginVertical();
			{

				GUILayout.BeginHorizontal();
				{
					if( GUILayout.Button( "Create Bank Sfz" ) == true )
                    {
						CreateBankSfz();
					}
				
					if( GUILayout.Button( "Create Instrument Sfz" ) == true )
					{
						CreateInstrumentSfz();
					}
				}
				GUILayout.EndHorizontal();
				
				if( GUILayout.Button( "Clear" ) == true )
				{
					Clear();
				}

				if( GUILayout.Button( "Save" ) == true )
                {
					Save();
				}
				
				GUILayout.BeginHorizontal();
				{
					GUILayout.BeginVertical( "Box" );
					{
						GUILayout.Label( "List" );

						vectorScrollList = GUILayout.BeginScrollView( vectorScrollList, false, false );
						{
							if( nameList.Count != 0 )
							{
								for( int i = 0; i < nameList.Count; i++ )
								{
									if( GUILayout.Button( nameList[i] ) == true )
									{
										keyEditingSfz = nameList[i];
									}
								}
							}
						}
						GUILayout.EndScrollView();
					}
					GUILayout.EndVertical();
					
					GUILayout.BeginVertical( "Box" );
					{
						GUILayout.Label( "Edit:" + keyEditingSfz );

						vectorScrollEditor = GUILayout.BeginScrollView( vectorScrollEditor, false, false );
						{
							if( sfzDictioinary.ContainsKey( keyEditingSfz ) == true )
		                    {
								sfzDictioinary[keyEditingSfz] = GUILayout.TextArea( sfzDictioinary[keyEditingSfz] );
							}
						}
						GUILayout.EndScrollView();
					}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

		private void CreateBankSfz()
		{
			string lPathDirectory = EditorUtility.OpenFolderPanel( "Select Bank Folder", "/", "" );
			
			if( lPathDirectory != null )
			{
				string[] lPathDirectoryArray = Directory.GetDirectories( lPathDirectory, "*", SearchOption.TopDirectoryOnly );
				
				for( int i = 0; i < lPathDirectoryArray.Length; i++ )
				{
					string lName = Path.GetFileName( lPathDirectoryArray[i] ); 
					nameList.Add( lName );
					sfzDictioinary.Add( lName, CreateSfzList( lPathDirectoryArray[i] ) );
				}
			}
		}
		
		private void CreateInstrumentSfz()
		{
			string lPathDirectory = EditorUtility.OpenFolderPanel( "Select Instrument Folder", "/", "" );
			
			if( lPathDirectory != null )
			{
				string lName = Path.GetFileName( lPathDirectory ); 
				nameList.Add( lName );
				sfzDictioinary.Add( lName, CreateSfzList( lPathDirectory ) );
			}
		}

		private string CreateSfzList( string aDirectory )
		{
			Dictionary<int, string> lFileNameDictionary = new Dictionary<int, string>();
			Dictionary<int, int> lToneDictionary = new Dictionary<int, int>();
			
			string[] lFilePathArray = Directory.GetFiles( aDirectory, "*.wav", SearchOption.TopDirectoryOnly );

			for( int i = 0; i < lFilePathArray.Length; i++ )
			{
				string lFileName = Path.GetFileName( lFilePathArray[i] );
				string[] separatedStringArrayDot = lFileName.Split( '.' );
				int separatedNumDot = separatedStringArrayDot.Length;
				
				if( separatedNumDot >= 3 )
				{
					string pitchNotation = separatedStringArrayDot[separatedNumDot - 2];
					
					string[] separateStringArrayAtmark = pitchNotation.Split( '@' );
					int separatedNumAtmark = separateStringArrayAtmark.Length;
					
					if( separatedNumAtmark >= 1 )
					{
						int keycenterActual = GetPitchNotation( separateStringArrayAtmark[0] );
						int keycenterDestination = keycenterActual;
						
						if( separatedNumAtmark == 2 )
						{
							keycenterDestination = GetPitchNotation( separateStringArrayAtmark[1] );
						}
						
						if( lFileNameDictionary.ContainsKey( keycenterDestination ) == false )
						{
							lFileNameDictionary.Add( keycenterDestination, Path.GetFileName( lFilePathArray[i] ) );
						}
						
						if( lToneDictionary.ContainsKey( keycenterDestination ) == false )
						{
							lToneDictionary.Add( keycenterDestination, ( keycenterActual - keycenterDestination ) * 100 );
						}
					}
				}
			}
			
			string lSfz = "";

			lSfz += "<group>\n";
			lSfz += "loop_mode=no_loop\n";
			//sfz += "loop_mode=loop_continuous\n";
			lSfz += "ampeg_release=0.250\n";
			lSfz += "\n";
			
			int lokey = 0;
			int hikey = 127;
			
			for( int i = 0; i < 128; i++ )
			{
				if( lFileNameDictionary.ContainsKey( i ) == true )
				{
					lSfz += "<region>\n";
					lSfz += "sample=" + lFileNameDictionary[i] + "\n";
					
					lSfz += "pitch_keycenter=" + i + "\n";
					
					for( hikey = i + 1; hikey < 127; hikey++ )
					{
						if( lFileNameDictionary.ContainsKey( hikey ) == true )
						{
							hikey--;
							break;
						}
					}
					
					lSfz += "lokey=" + lokey + "\n";
					lSfz += "hikey=" + hikey + "\n";
					//sfz += "loop_start=\n";
					//sfz += "loop_end=\n";
					
					if( lToneDictionary.ContainsKey( i ) == true )
					{
						lSfz += "tune=" + lToneDictionary[i] + "\n";
					}
					else
					{
						lSfz += "tune=0\n";
					}
					
					lSfz += "\n";
					
					lokey = hikey + 1;
				}
			}

			return lSfz;
		}

		private int GetPitchNotation( string aPitchNotation )
		{
			int number = 24;
			
			int lLength = aPitchNotation.Length;
			int lDirection = 1;
			
			if( aPitchNotation[lLength - 2] == '-' )
			{
				lDirection = -1;
			}
			
			if( char.IsDigit( aPitchNotation[lLength - 1] ) == true )
			{
				number += int.Parse( aPitchNotation[lLength - 1].ToString() ) * 12 * lDirection;
			}
			
			if( aPitchNotation[1] == 's' || aPitchNotation[1] == 'S' || aPitchNotation[1] == '#' )
			{
				number += 1;
			}
			else if( aPitchNotation[1] == 'b' || aPitchNotation[1] == 'B' )
			{
				number -= 1;
			}
			
			switch( aPitchNotation[0] )
			{
			case 'C':
				number += 0;
				break;
				
			case 'D':
				number += 2;
				break;
				
			case 'E':
				number += 4;
				break;
				
			case 'F':
				number += 5;
				break;
				
			case 'G':
				number += 7;
				break;
				
			case 'A':
				number += 9;
				break;
				
			case 'B':
				number += 11;
				break;
				
			default:
				break;
			}
			
			return number;
		}
		
		private void Clear()
		{
			nameList = new List<string>();
			sfzDictioinary = new Dictionary<string, string>();
			keyEditingSfz = "";
			vectorScrollList = Vector2.zero;
			vectorScrollEditor = Vector2.zero;
		}

		private void Save()
		{
            string lPathDirectory = EditorUtility.OpenFolderPanel( "Select Folder", "/", "" );
			
			if( lPathDirectory != null && lPathDirectory != "" )
			{
				SaveSfzList( lPathDirectory );
            }
        }

		private void SaveSfzList( string aPathDirectory )
		{
			foreach( KeyValuePair<string, string> l in sfzDictioinary )
			{
				using( StreamWriter u = new StreamWriter( aPathDirectory + "/" + l.Key + ".sfz.txt" ) )
				{
					u.WriteLine( l.Value );
				}
			}
		}
	}
}
