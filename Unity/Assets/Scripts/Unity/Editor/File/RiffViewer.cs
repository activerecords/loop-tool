using System;
using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

//using LayerBase.Common.Binary;
//using LayerBase.Data.File.Riff;
//using LayerBase.Data.File.Riff.Wave;

namespace LayerEditor.File
{
    /*
    public class RiffViewer : EditorWindow
    {
        private Vector2 scrollPosition;
		private RiffChunk riffChunk;
        private bool isFoldout = false;

        [MenuItem( "Window/File/Riff Viewer" )]
        public static void Init()
        {
            EditorWindow.GetWindow<RiffViewer>( false, "Riff Viewer" );
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView( scrollPosition, false, false );
            {
                GUILayout.BeginVertical( "Box" );
                {
                    if( GUILayout.Button( "Load" ) == true )
                    {
                        string lFilePath = EditorUtility.OpenFilePanel( "Select File", Application.streamingAssetsPath, "wav" );

						byte[] lData = System.IO.File.ReadAllBytes( lFilePath );
						RiffFile lRiffFile = new RiffFile( new ByteArrayLittle( lData ) );
						riffChunk = lRiffFile.riffChunk;
                    }

                    isFoldout = EditorGUILayout.Foldout( isFoldout, "?f?[?^" );

                    if( isFoldout == true )
					{
						DisplayList( riffChunk );
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
        }

        private void DisplayList( RiffChunk aRiffChunk )
        {
            if( aRiffChunk != null )
            {
                Dictionary<string, List<RiffChunk>> dictionary = aRiffChunk.GetRiffChunkListDictionary();

                if( dictionary != null )
                {
                    GUILayout.BeginVertical( "Box" );
                    {
                        foreach( KeyValuePair<string, List<RiffChunk>> keyValuePair in dictionary )
                        {
                            foreach( RiffChunk riffChunk in keyValuePair.Value )
                            {
								GUILayout.Label( keyValuePair.Key + ":" + riffChunk.GetType() );

                                DisplayList( riffChunk );
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }
            }
        }
    }*/
}
