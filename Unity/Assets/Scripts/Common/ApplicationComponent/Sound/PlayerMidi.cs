using System;
using System.Collections.Generic;

using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Common.AdaptedData.Music;
using Curan.Common.FileLoader.Music;
using Curan.Common.Struct;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class PlayerMidi : IPlayer
	{
		private MidiSequencer sequencer;

		private delegate void UpdatePlayer( float[] aSoundBuffer, int aChannels, int aSampleRate, float aVolume );
		private UpdatePlayer updatePlayCurrent;

		private string path;
		private float volume;
        
        public double Position
        {
            get
            {
                return sequencer.GetPosition();
            }
            set
            {
                sequencer.SetPosition( value );
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

		public PlayerMidi( string aFilePath )
		{
			MusicMidi lMusic = ( MusicMidi )LoaderMusic.Load( aFilePath );

			sequencer = new MidiSequencer( lMusic, new MidiSynthesizer() );

			updatePlayCurrent = sequencer.UpdatePlay;

			float lVolume = ( float )( 40.0d * Math.Log10( 0.5f ) );
			volume = ( float )Math.Pow( 10.0d, lVolume / 20.0d ) * 2.0f;
		}

		public void Play()
		{
			updatePlayCurrent = sequencer.UpdatePlay;

			sequencer.Play();
		}

		public void Stop()
		{
			updatePlayCurrent = sequencer.UpdateSynth;

			sequencer.Stop();
		}

		public void Pause()
		{
			if( updatePlayCurrent == sequencer.UpdatePlay )
			{
				updatePlayCurrent = sequencer.UpdateSynth;

				sequencer.Pause();
			}
			else
			{
				updatePlayCurrent = sequencer.UpdatePlay;
			}
		}

		public void Record( string aPath )
		{
			updatePlayCurrent = sequencer.UpdateRecord;

			sequencer.Stop();

			path = aPath;
		}

		public bool GetFlagPlaying()
		{
			if( updatePlayCurrent == sequencer.UpdatePlay )
			{
				return true;
			}
			else
			{
				return false;
			}
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
			updatePlayCurrent( aSoundBuffer, aChannels, aSampleRate, volume );
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
