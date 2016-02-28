using System;
using System.Collections.Generic;

using Curan.Common.AdaptedData.Music;
using Curan.Common.FileLoader.Music;
using Curan.Common.Struct;

namespace Curan.Common.ApplicationComponent.Sound.Xm
{
	public class PlayerXm : IPlayer
	{
		private XmSequencer sequencer;

		private float volume;
        
        public double Position
        {
            get
            {
                return 0.0d;
            }
            set
            {
                
            }
        }
        
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                float lVolume = ( float )( 40.0d * Math.Log10( value ) );
                volume = ( float )Math.Pow( 10.0d, lVolume / 20.0d ) * 2.0f;
            }
        }

		public PlayerXm( string aFilePath )
		{
			MusicXm lMusicXm = ( MusicXm )LoaderMusic.Load( aFilePath );

			sequencer = new XmSequencer( lMusicXm );

			float lVolume = ( float )( 40.0d * Math.Log10( 0.5f ) );
			volume = ( float )Math.Pow( 10.0d, lVolume / 20.0d ) * 2.0f;
		}

		public void Play()
		{

		}

		public void Stop()
		{

		}

		public void Pause()
		{

		}

		public void Record( string aPath )
		{

		}

		public bool GetFlagPlaying()
		{
			return true;
		}

		public SoundTime GetTimePosition()
		{
			return new SoundTime( 44100, 0 );
		}

		public SoundTime GetTimeElapsed()
		{
			return new SoundTime( 44100, 0 );
		}

		public SoundTime GetTimeLength()
		{
			return new SoundTime( 44100, 0 );
		}

		public LoopInformation GetLoopPoint()
		{
			return null;
		}

		public int GetLoopCount()
		{
			return 0;
		}

		public int GetLoopNumberX()
		{
			return 0;
		}

		public int GetLoopNumberY()
		{
			return 0;
		}

		public void Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			sequencer.Update( aSoundBuffer, aChannels, aSampleRate );

			for( int i = 0; i < aSoundBuffer.Length; i++ )
			{
				aSoundBuffer[i] *= volume;
			}
		}

		public void SetPreviousLoop()
		{

		}

		public void SetNextLoop()
		{

		}

		public void SetUpLoop()
		{

		}

		public void SetDownLoop()
		{

		}
	}
}
