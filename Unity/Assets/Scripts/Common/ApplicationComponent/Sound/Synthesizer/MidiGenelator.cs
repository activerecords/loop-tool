using System;
using System.Collections.Generic;

using Curan.Common.AdaptedData;
using Curan.Common.FileLoader.Soundcluster;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public class MidiGenerator
	{
		public static SoundclusterBase soundcluster;

		private byte instrument;
		private int bank;
		public int bankData;
		public byte rpnMsb;
		public byte rpnLsb;
		public byte nrpnMsb;
		public byte nrpnLsb;

		private int mode;
		private bool isOnChannel;

		private MasterStatus masterStatus;

		public MidiVolume midiVolume;
		public MidiPitch midiPitch;

		private List<int> noteOnList;
		private List<MidiOscillator> oscillatorList;

		public FilterChorus chorusFilter;
		public FilterReverb reverbFilter;
		
		private double[] sampleValue;
		private double[] sampleValueSum;

		static MidiGenerator()
		{
			//lSoundfontFile = "Sound/Soundfont/Sfz/Bank/MandolinOrchestra.blst.txt";
			//string lSoundfontFile = "/Users/nagi/Google ドライブ/Soundline/Sfz/BankList/Standard.blst.txt";
			//string lSoundfontFile = "/Users/nagi/Google ドライブ/Soundline/Sfz/BankList/Original.blst.txt";
			//lSoundfontFile = "Sound/Soundfont/gm.dls";
			//lSoundfontFile = "Sound/Soundfont/msgs.sf2";
			
			//soundcluster = LoaderSoundcluster.Load( UnityEngine.Application.streamingAssetsPath + "/Sound/Soundfont/gm.dls" );
			//soundcluster = LoaderSoundcluster.Load( UnityEngine.Application.streamingAssetsPath + "/Sound/Soundfont/msgs.sf2" );
			soundcluster = LoaderSoundcluster.Load( UnityEngine.Application.streamingAssetsPath + "/Sound/Soundfont/Original.blst.txt" );
		}

		public static void SetSoundcluster( string aPathFile )
		{
			soundcluster = LoaderSoundcluster.Load( aPathFile );
		}

		public MidiGenerator( MasterStatus aMasterStatus )
		{
			instrument = 0;
			bank = 0;
			bankData = 0;
			rpnMsb = 0;
			rpnLsb = 0;
			nrpnMsb = 0;
			nrpnLsb = 0;
			mode = 3;
			isOnChannel = true;

			midiVolume.Init();
			midiPitch.Init();

			noteOnList = new List<int>();
			oscillatorList = new List<MidiOscillator>();

			masterStatus = aMasterStatus;

			reverbFilter.Init();
			chorusFilter.Init();
			
			sampleValue = new double[2];
			sampleValueSum = new double[2];
		}

		public void Init()
		{
			instrument = 0;
			bank = 0;
			bankData = 0;
			rpnMsb = 0;
			rpnLsb = 0;
			nrpnMsb = 0;
			nrpnLsb = 0;
			mode = 3;

			midiVolume.Init();
			midiPitch.Init();

			reverbFilter.Init();
			chorusFilter.Init();
		}

		public Byte Instrument
		{
			get{ return instrument; }
			set
			{
				bank = bankData;
				instrument = value;
			}
		}

		public int Bank
		{
			get{ return bank; }

			set
			{
				bank = value;

				if( bank == 0x7900 )
				{
					bank = 0x0000;
				}

				if( bank == 0x7800 /* || ( bankData == 0x0000 /*&& channel == 9 )*/ )
				{
					bank = 0x7F00;
				}
			}
		}

		public void OmniModeOff( Byte aData )
		{

		}

		public void OmniModeOn( Byte aData )
		{

		}

		public void MonoModeOn( Byte aData )
		{
			mode = 4;
		}

		public void PolyModeOn( Byte aData )
		{
			mode = 3;
		}

		public void NoteOn( byte aNote, byte aVelocity, double aSecondLength = 0.0d )
		{
			if( isOnChannel == false )
			{
				return;
			}

			//note = aNote;
			//velocity = aVelocity;

			if( mode == 4 )
			{
				AllNoteOff();
			}

			if( midiPitch.GetPortamentFlag() == true )
			{
				Portament( bank, instrument, aNote, aVelocity, aSecondLength );
			}
			else
			{
				NoteOn( bank, instrument, aNote, aVelocity, aSecondLength );
			}
		}

		public void Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			for( int i = 0; i < oscillatorList.Count; i++ )
			{
				if( oscillatorList[i] != null )
				{
					oscillatorList[i].Oscillate( sampleValue, aSampleRate, ref midiVolume, ref midiPitch );

					sampleValueSum[0] += sampleValue[0];
					sampleValueSum[1] += sampleValue[1];
				}
				else
				{
					UnityEngine.Debug.Log( "Null!!!!!!!!!!!!!!!!!!" );
				}
			}

			for( int i = 0; i < aChannels; i++ )
			{
				if( chorusFilter.isOnChorus == true )
				{
					//chorusFilter.Filter( ref sampleValueSum[i], aSampleRate );
				}

				if( reverbFilter.isOnReverb == true )
				{
					//reverbFilter.Filter( ref sampleValueSum[i], aSampleRate );
				}

				aSoundBuffer[i] += ( float )sampleValueSum[i] * midiVolume.volumeArray[i] * masterStatus.GetVolumeRate();
				sampleValueSum[i] = 0.0d;
				//aWaveform[i] += ( float )lSampleValue[i] * masterStatus.GetVolumeRate();
			}
		}

		public void NoteOn( int aBank, Byte aInstrument, Byte aNote, Byte aVelocity, double aSecondLength = 0.0d )
		{
			int lBank = aBank;
			Byte lInstrument = aInstrument;

			if( soundcluster.bankDictionary.ContainsKey( lBank ) == false )
			{
				lBank = 0;
			}

			if( lBank == 0x7F00 )
			{
				if( lInstrument == 1 )
				{
					lInstrument = 8;
				}
				else if( lInstrument == 2 )
				{
					lInstrument = 16;
				}
				else if( lInstrument == 3 )
				{
					lInstrument = 24;
				}
				else if( lInstrument == 4 )
				{
					lInstrument = 25;
				}
				else if( lInstrument == 5 )
				{
					lInstrument = 32;
				}
				else if( lInstrument == 6 )
				{
					lInstrument = 40;
				}
				else if( lInstrument == 7 )
				{
					lInstrument = 48;
				}
			}

			if( soundcluster.bankDictionary.ContainsKey( lBank ) == true )
			{
				InstrumentBase lMidiInstrument = soundcluster.bankDictionary[lBank].instrumentArray[lInstrument];

				if( lMidiInstrument == null )
				{
					if( lBank == 0x7F00 )
					{
						Logger.LogWarning( "Change Instrument:" + lBank.ToString( "X4" ) + "/" + lInstrument );
						lInstrument = 0x0000;
					}
					else
					{
						Logger.LogWarning( "Change Bank:" + lBank.ToString( "X4" ) + "/" + lInstrument );
						lBank = 0x0000;
					}

					lMidiInstrument = soundcluster.bankDictionary[lBank].instrumentArray[lInstrument];
				}

				if( lMidiInstrument != null )
				{
					SoundfontBase lSoundfont = lMidiInstrument.soundfontArray[aNote];

					if( lSoundfont != null )
					{
						Logger.LogWarning( "Add" );
						noteOnList.Add( aNote );
						oscillatorList.Add( new MidiOscillator( aNote, lInstrument, aVelocity, lSoundfont, ref midiVolume, ref midiPitch, aSecondLength ) );
					}
					else
					{
						Logger.LogWarning( "Not Found Note:" + lBank.ToString( "X4" ) + "/" + lInstrument + "/" + aNote );
					}
				}
				else
				{
					Logger.LogWarning( "Not Found Instrument:" + lBank.ToString( "X4" ) + "/" + lInstrument );
				}
			}
			else
			{
				Logger.LogWarning( "Not Found Bank:" + lBank.ToString( "X4" ) );
			}
		}

		public void Portament( int aBank, Byte aInstrument, Byte aNote, Byte aVelocity, double aSecondsLength = 0.0d )
		{
			NoteOn( aBank, aInstrument, aNote, aVelocity, aSecondsLength );
		}

		public void NoteOff( byte aNote )
		{
			for( int i = 0; i < noteOnList.Count; i++ )
			{
				if( aNote == noteOnList[i] )
				{
					oscillatorList[i].NoteOff();
				}
			}
		}

		public void AllSoundOff()
		{
			for( int i = 0; i < oscillatorList.Count; i++ )
			{
				oscillatorList[i].SoundOff();
			}
		}

		public void AllNoteOff()
		{
			for( int i = 0; i < oscillatorList.Count; i++ )
			{
				oscillatorList[i].NoteOff();
			}
		}

		public void AllNoteReset()
		{
			noteOnList.Clear();
			oscillatorList.Clear();
		}

		public void RefleshOscllatorList()
		{
			for( int i = oscillatorList.Count - 1; i >= 0; i-- )
			{
				if( oscillatorList[i].GetFlagEnd() == true )
				{
					noteOnList.RemoveAt( i );
					oscillatorList.RemoveAt( i );
				}
			}
		}
	}
}
