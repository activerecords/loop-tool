using System;

using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Common.AdaptedData.Music;
using Curan.Common.FileLoader.Music;
using Curan.Common.Struct;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public class PlayerNsf : IPlayer
	{
		private NesState nesState;
		private MidiSynthesizer midiSynthesizer;

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
            set
            {
                volume = value;
            }
        }

		public PlayerNsf( string aFilePath )
		{
			MusicNsf lMusic = ( MusicNsf )LoaderMusic.Load( aFilePath );

			Logger.LogNormal( "Load" );

			midiSynthesizer = new MidiSynthesizer();
			midiSynthesizer.SetVolume( ( UInt16 )0x4000 );
			midiSynthesizer.MonoModeOn( 1 );

			nesState = new NesState( lMusic );
			NesCpu.InitNsf( nesState );

			float lVolume = ( float )( 40.0d * Math.Log10( 0.5f ) );
			volume = ( float )Math.Pow( 10.0d, lVolume / 20.0d );

			Init();
		}

		public void Init()
		{
			//Logger.LogNormal( "Init" );

			//Logger.LogNormal( "??:??????????\n" );
			//Logger.LogNormal( "??:?O??????????\n" );
			//Logger.LogNormal( "?X?y?[?X:?????t?@?C????????????\n" );
			//Logger.LogNormal( "ESC:?v???O???????I??\n" );

			nesState.memory.Init();	// ??????????????
		}

		public void Play()
		{
			nesState.nsf.IncrementMusicNumber();
			Init();	// ??????
		}

		public void Stop()
		{
			nesState.nsf.DecrementMusicNumber();
			Init();	// ??????
		}

		public void Pause()
		{

		}

		public bool GetFlagPlaying()
		{
			return true;
		}

		public void Record( string aPath )
		{

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

		public void SetVolume( float aVolume )
		{
			float lVolume = ( float )( 40.0d * Math.Log10( aVolume ) );
			volume = ( float )Math.Pow( 10.0d, lVolume / 20.0d );
		}

		public void Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			NesCpu.Update( nesState );

			NesApu.Update( nesState, midiSynthesizer );

			int lLength = aSoundBuffer.Length / aChannels;

			midiSynthesizer.Reflesh();

			for( int i = 0; i < lLength; i++ )
			{
				float[] lDataArray = new float[aChannels];

				midiSynthesizer.Update( lDataArray, aChannels, aSampleRate );

				for( int j = 0; j < aChannels; j++ )
				{
					aSoundBuffer[i * aChannels + j] = lDataArray[j] * volume;
				}
			}
		}

		/*
		public void Update( CoreSynthesizer[] midiSynthesizer )
		{
			NesCpu.Update( nesState );

			// Fixed:Affecter.NesApu.Update( nesState, midiSynthesizer );
			NesApu.Update( nesState, midiSynthesizer );
			/*
			int lLength = aSoundBuffer.Length / aChannels;

			// Fixed:midiSynthesizer.Reflesh();

			for( int i = 0; i < lLength; i++ )
			{
				float[] lDataArray = new float[aChannels];

				// Fixed:midiSynthesizer.Update( lDataArray, aChannels, aSampleRate );

				for( int j = 0; j < aChannels; j++ )
				{
					aSoundBuffer[i * aChannels + j] = lDataArray[j] * volume;
				}
			}*/
		//}

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
