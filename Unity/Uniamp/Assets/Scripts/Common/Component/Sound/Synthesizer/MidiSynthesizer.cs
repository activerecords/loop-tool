using System;

using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
	public class MidiSynthesizer
	{
		private const int CHANNELS = 16;

		private MasterStatus masterStatus;
		private MidiGenerator[] midiGeneratorArray;

		public MidiSynthesizer()
		{
			masterStatus = new MasterStatus();
			midiGeneratorArray = new MidiGenerator[CHANNELS];

			for( int i = 0; i < CHANNELS; i++ )
			{
				midiGeneratorArray[i] = new MidiGenerator( masterStatus );
			}

			midiGeneratorArray[9].Bank = 0x7F00;
			midiGeneratorArray[10].Bank = 0x7F00;
		}

		public void Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].Update( aSoundBuffer, aChannels, aSampleRate );
			}
		}

		public void SetVolume( UInt16 aData )
		{
			masterStatus.SetVolume( aData );
		}

		public void OmniModeOff( Byte aData )
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].OmniModeOff( aData );
			}
		}

		public void OmniModeOn( Byte aData )
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].OmniModeOn( aData );
			}
		}

		public void MonoModeOn( Byte aData )
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].MonoModeOn( aData );
			}
		}

		public void PolyModeOn( Byte aData )
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].PolyModeOn( aData );
			}
		}

		public void NoteOn( byte aChannel, byte aNote, byte aVelocity, double aSecondLength = 0.0d )
		{
			Logger.Debug( "Channel:" + aChannel.ToString() );
			midiGeneratorArray[aChannel].NoteOn( aNote, aVelocity, aSecondLength );
		}

		public void NoteOff( byte aChannel, byte aNote )
		{
			midiGeneratorArray[aChannel].NoteOff( aNote );
		}

		public void AllSoundOff()
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].AllSoundOff();
			}

			// メモ:リバーブを消音する処理を追加する.
		}

		public void AllNoteOff()
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].AllNoteOff();
			}
		}

		public void AllNoteReset()
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].AllNoteReset();
			}
		}

		public void Reflesh()
		{
			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].RefleshOscllatorList();
			}
		}

		public void Reset()
		{
			AllSoundOff();

			for( int i = 0; i < midiGeneratorArray.Length; i++ )
			{
				midiGeneratorArray[i].Init();
			}

			midiGeneratorArray[9].Bank = 0x7F00;
			midiGeneratorArray[10].Bank = 0x7F00;
		}

		public MidiGenerator[] GetMidiGeneratorArray()
		{
			return midiGeneratorArray;
		}
	}
}
