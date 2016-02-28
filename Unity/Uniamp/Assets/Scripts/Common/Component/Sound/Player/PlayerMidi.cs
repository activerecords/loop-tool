using System;
using System.Collections.Generic;

using Monoamp.Common.Component.Application.Sound;
using Monoamp.Common.Component.Sound.Midi;
using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Utility;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Sound.Player
{
	public class PlayerMidi : IPlayer
	{
		public string FilePath{ get; private set; }
		public IMusic Music{ get; private set; }
		public double PositionRate{ get{ return 0.0d; } set{} }
		public float Volume{ get; set; }
		public bool IsMute{ get; set; }
		public bool IsLoop{ get; set; }
		
		public LoopInformation Loop{ get{ return new LoopInformation( 44100, -1, -1 ); } }
		public int LoopNumberX{ get{ return 0; } }
		public int LoopNumberY{ get{ return 0; } }

		private MidiSequencer sequencer;

		private delegate void UpdatePlayer( float[] aSoundBuffer, int aChannels, int aSampleRate, float aVolume );
		private UpdatePlayer updatePlayCurrent;

		private string path;
        
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
        
		public PlayerMidi( string aFilePath )
		{
			FilePath = aFilePath;
			Music = ConstructorCollection.ConstructMusic( aFilePath );

			sequencer = new MidiSequencer( ( MusicMidi )Music, new MidiSynthesizer() );

			updatePlayCurrent = sequencer.UpdatePlay;
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
		
		public string GetFilePath()
		{
			return Music.Name;
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

		public SoundTime GetTPosition()
		{
			return new SoundTime( 44100, 0 );
		}

		public SoundTime GetElapsed()
		{
			return new SoundTime( 44100, 0 );
		}

		public SoundTime GetLength()
		{
			return new SoundTime( 44100, 0 );
		}

		public LoopInformation GetLoopPoint()
		{
			return new LoopInformation( 44100, 0, 0 );
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

		public int Update( float[] aSoundBuffer, int aChannels, int aSampleRate, int aPositionInBuffer )
		{
			float lVolume = ( float )( 40.0d * Math.Log10( Volume ) );

			updatePlayCurrent( aSoundBuffer, aChannels, aSampleRate, ( float )Math.Pow( 10.0d, lVolume / 20.0d ) * 2.0f );

			return aSoundBuffer.Length / aChannels;
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

		public void SetLoop( LoopInformation aLoopInformation )
		{
			
		}
	}
}
