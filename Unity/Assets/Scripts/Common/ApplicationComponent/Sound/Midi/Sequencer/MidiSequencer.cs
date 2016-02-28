using System;
using System.Collections.Generic;

using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Common.AdaptedData.Music;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiSequencer
	{
		public readonly MusicMidi music;
		private readonly MidiSynthesizer synthesizer;

		private readonly MetaStatus metaStatus;
		private readonly MidiSequenceTrack[] sequenceTrackArray;

		private int deltaPosition;
		private int samplePosition;
		
		private float[] bufferArray;

		public MidiSequencer( MusicMidi aMusicMidi, MidiSynthesizer aMidiSynthesizer )
		{
			music = aMusicMidi;
			synthesizer = aMidiSynthesizer;

			metaStatus = new MetaStatus();
			sequenceTrackArray = new MidiSequenceTrack[music.tracks];

			for( int i = 0; i < music.tracks; i++ )
			{
				sequenceTrackArray[i] = new MidiSequenceTrack( music.mtrkChunkArray[i] );
			}

			deltaPosition = 0;
			samplePosition = 0;
			
			bufferArray = new float[2];

			Caching();
		}

		private void Caching()
		{
			MidiSynthesizer lMidiSynthesizer = new MidiSynthesizer();
			MetaStatus lMetaStatus = new MetaStatus();

			for( int i = 0; i < music.deltaMax; i++ )
			{
				lMetaStatus.SetDelta( i );

				for( int j = 0; j < music.tracks; j++ )
				{
					sequenceTrackArray[j].ExecuteMetaEventOneDelta( lMetaStatus );

					sequenceTrackArray[j].ExecuteMidiEventOneDelta( lMetaStatus, lMidiSynthesizer, music.division );
				}
			}
		}

		public void Play()
		{
			synthesizer.AllSoundOff();
			SetDelta( 0 );
		}

		public void Stop()
		{
			synthesizer.AllSoundOff();
			SetDelta( 0 );
		}

		public void Pause()
		{
			synthesizer.AllNoteOff();
		}

		public void SetPosition( double aPosition )
		{
			synthesizer.AllSoundOff();
			SetDelta( ( int )( aPosition * music.deltaMax ) );
		}

		public double GetPosition()
		{
			return ( double )GetDelta() / music.deltaMax;
		}

		public void SetDelta( int aDelta )
		{
			deltaPosition = ( int )aDelta;
			metaStatus.SetDelta( aDelta );
			samplePosition = 0;

			for( int i = 0; i < music.tracks; i++ )
			{
				sequenceTrackArray[i].ExecuteMetaEventSeek( metaStatus, aDelta );

				sequenceTrackArray[i].ExecuteMidiEventSeek( metaStatus, synthesizer, music.division, aDelta );
			}
		}

		public int GetDelta()
		{
			return metaStatus.GetDelta();
		}

		public double GetBpm()
		{
			return metaStatus.GetBpm();
		}

		public void UpdatePlay( float[] aSoundBuffer, int aChannels, int aSampleRate, float aVolume )
		{
			synthesizer.Reflesh();

			int lLength = aSoundBuffer.Length / aChannels;

			for( int i = 0; i < lLength; i++ )
			{
				Update( aSampleRate );
				synthesizer.Update( bufferArray, aChannels, aSampleRate );

				for( int j = 0; j < aChannels; j++ )
				{
					aSoundBuffer[i * aChannels + j] = bufferArray[j] * aVolume;
					bufferArray[j] = 0;
				}
			}
		}

		public void UpdateRecord( float[] aSoundBuffer, int aChannels, int aSampleRate, float aVolume )
		{
			List<float> lDataList = new List<float>();

			while( deltaPosition < music.deltaMax )
			{
				synthesizer.Reflesh();

				float[] lDataArray = new float[aChannels];

				Update( aSampleRate );
				synthesizer.Update( lDataArray, aChannels, aSampleRate );

				for( int j = 0; j < aChannels; j++ )
				{
					lDataList.Add( lDataArray[j] );
				}
			}

			//updatePlayCurrent = UpdateSynth;

			//WaveFileWriter.Store( lDataList, path );
		}

		public void UpdateSynth( float[] aSoundBuffer, int aChannels, int aSampleRate, float aVolume )
		{
			synthesizer.Reflesh();

			int lLength = aSoundBuffer.Length / aChannels;

			for( int i = 0; i < lLength; i++ )
			{
				float[] lDataArray = new float[aChannels];

				synthesizer.Update( lDataArray, aChannels, aSampleRate );

				for( int j = 0; j < aChannels; j++ )
				{
					aSoundBuffer[i * aChannels + j] = lDataArray[j] * aVolume;
				}
			}
		}

		private void Update()
		{
			int lDelta = metaStatus.GetDelta() + 1;

			for( int i = 0; i < music.tracks; i++ )
			{
				sequenceTrackArray[i].ExecuteMetaEventOneDelta( metaStatus );

				sequenceTrackArray[i].ExecuteMidiEventOneDelta( metaStatus, synthesizer, music.division );
			}

			metaStatus.SetDelta( lDelta );
			/*
			if( delta >= midiMusicFile.GetDeltaMax() )
			{
				Stop();
			}*/
		}

		private void Update( int aSampleRate )
		{
			int lDeltaPre = deltaPosition;
			double lDataLength = ( double )aSampleRate * 60.0d / metaStatus.GetBpm() / ( double )music.division;

			deltaPosition = ( int )( ( double )samplePosition / lDataLength );

			if( deltaPosition != lDeltaPre )
			{
				Update();
			}

			samplePosition++;
		}
	}
}
