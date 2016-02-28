using UnityEngine;

using System;
using System.IO;

namespace Unity.View
{
	public class LoopPlayer : IView
	{
		private ViewLoopPlayer viewLoopPlayer;
		private ViewLoopDisplay viewLoopDisplay;
		private ViewLoopPlaylist viewLoopPlaylist;
		private ViewChangeDirectory viewChangeDirectory;
		
		public Rect Rect{ get; set; }

		public LoopPlayer( DirectoryInfo aDirectoryInfo )
		{
			viewLoopPlayer = new ViewLoopPlayer( null, ChangeMusicPrevious, ChangeMusicNext );
			viewLoopDisplay = new ViewLoopDisplay( viewLoopPlayer.GetLoopPoint() );
			viewLoopPlaylist = new ViewLoopPlaylist( aDirectoryInfo, SetFileInfoPlaying, GetFileInfoPlaying );
			
			DirectoryInfo lDirectoryInfoRoot = new DirectoryInfo( Application.streamingAssetsPath );
			viewChangeDirectory = new ViewChangeDirectory( lDirectoryInfoRoot, aDirectoryInfo, SetDirectoryInfo );

			Rect = new Rect( 0.0f, 0.0f, 0.0f, 0.0f );
		}
		
		private void SetFileInfoPlaying( FileInfo aFileInfo )
		{
			viewLoopPlayer = new ViewLoopPlayer( aFileInfo, ChangeMusicPrevious, ChangeMusicNext );
			viewLoopPlayer.Awake();
		}
		
		private FileInfo GetFileInfoPlaying()
		{
			return viewLoopPlayer.GetFileInfo();
		}
		
		private void SetDirectoryInfo( DirectoryInfo aDirectoryInfo )
		{
			viewLoopPlaylist = new ViewLoopPlaylist( aDirectoryInfo, SetFileInfoPlaying, GetFileInfoPlaying );
		}

		public void ChangeMusicPrevious()
		{
			viewLoopPlaylist.ChangeMusicPrevious();
		}
		
		public void ChangeMusicNext()
		{
			viewLoopPlaylist.ChangeMusicNext();
		}
		
		public void Awake()
		{
			viewLoopPlayer.Awake();
		}

		public void Start()
		{

		}

		public void Update()
		{

		}

		public void OnGUI()
		{
			GUILayout.BeginVertical();
			{
				viewLoopPlayer.OnGUI();
				viewLoopDisplay.OnGUI();
				viewLoopPlaylist.OnGUI();
				viewChangeDirectory.OnGUI();
			}
			GUILayout.EndVertical();
		}
		
		public void OnApplicationQuit()
		{

		}

		public void OnRenderObject()
		{
			viewLoopPlayer.OnRenderObject();
		}

		public void OnAudioFilterRead( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			viewLoopPlayer.OnAudioFilterRead( aSoundBuffer, aChannels, aSampleRate );
		}
	}
}
