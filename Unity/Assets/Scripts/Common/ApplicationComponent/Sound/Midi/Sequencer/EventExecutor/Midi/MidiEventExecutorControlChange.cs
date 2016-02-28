using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorControlChange : MidiEventExecutorBase
	{
		public MidiEventExecutorControlChange( MidiEventControlChange aControlChange )
            : base( aControlChange )
		{

		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			MidiGenerator lMidiGenerator = aMidiSynthesizer.GetMidiGeneratorArray()[midiEvent.GetChannel()];

			switch( midiEvent.GetData1() )
			{
			case 0x00:
				lMidiGenerator.bankData = ( UInt16 )( midiEvent.GetData2() << 8 );
				break;

			case 0x20:
				lMidiGenerator.bankData |= ( UInt16 )midiEvent.GetData2();

				lMidiGenerator.Bank = lMidiGenerator.bankData;
				//Debug.LogWarning( channel + ":" + "Set Bank:" + bankData.ToString( "X4" ) );

				break;

			case 0x01:
				lMidiGenerator.midiPitch.SetModulationDepth( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Modulation Depth:" + aData );
				break;

			case 0x05:
				lMidiGenerator.midiPitch.SetPortamentTime( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Portamento Time:" + aData );
				break;

			case 0x06:
				SetRpnMsb( lMidiGenerator );
				break;

			case 0x26:
				SetRpnLsb( lMidiGenerator );
				break;

			case 0x07:
				lMidiGenerator.midiVolume.SetVolumeMsb( midiEvent.GetData2() );
				break;

			case 0x27:
				lMidiGenerator.midiVolume.SetVolumeLsb( midiEvent.GetData2() );
				break;

			case 0x0A:
				lMidiGenerator.midiVolume.SetPan( midiEvent.GetData2() );
				break;

			case 0x2A:
				lMidiGenerator.midiVolume.SetPan( midiEvent.GetData2() );
				break;

			case 0x0B:
				lMidiGenerator.midiVolume.SetExpressionMsb( midiEvent.GetData2() );
				break;

			case 0x2B:
				//Log.LogWarning( channel + ":" + "Expression2:" + aData );
				lMidiGenerator.midiVolume.SetExpressionLsb( midiEvent.GetData2() );
				break;

			case 0x40:
				////Log.LogNormal( channel + ":Hold 1:" + aData );
				break;

			case 0x41:
				lMidiGenerator.midiPitch.SetPortamentFlag( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Portament On/Off:" + aData );
				break;

			case 0x42:
				////Log.LogNormal( channel + ":Sostenute:" + aData );
				break;

			case 0x43:
				////Log.LogNormal( channel + ":Soft:" + aData );
				break;

			case 0x47:
				////Log.LogNormal( channel + ":Filter Resonance:" + aData );
				break;

			case 0x48:
				////Log.LogNormal( channel + ":Release Tim:" + aData );
				break;

			case 0x49:
				////Log.LogNormal( channel + ":Attack Tim:" + aData );
				break;

			case 0x4A:
				////Log.LogNormal( channel + ":Brightness:" + aData );
				break;

			case 0x4B:
				////Log.LogNormal( channel + ":Decay Time:" + aData );
				break;

			case 0x4D:
				////Log.LogNormal( channel + ":Vibrato Depth:" + aData );
				break;

			case 0x4E:
				////Log.LogNormal( channel + ":Vibrato Delay:" + aData );
				break;

			case 0x5B:
				lMidiGenerator.reverbFilter.SetReverbSendLevel( midiEvent.GetData2() );
				break;

			case 0x5D:
				lMidiGenerator.chorusFilter.SetChorusSendLevel( midiEvent.GetData2() );
				break;

			case 0x5E:
				////Log.LogNormal( channel + ":Effect Depth:" + aData );
				break;

			case 0x62:
				lMidiGenerator.nrpnLsb = midiEvent.GetData2();
				//Log.LogNormal( channel + ":NRPN LSB:" + aData );
				break;

			case 0x63:
				lMidiGenerator.nrpnMsb = midiEvent.GetData2();
				//Log.LogNormal( channel + ":NRPN MSB:" + aData );
				break;

			case 0x64:
				lMidiGenerator.rpnLsb = midiEvent.GetData2();
				//Log.LogNormal( channel + ":RPN LSB:" + aData );
				break;

			case 0x65:
				lMidiGenerator.rpnMsb = midiEvent.GetData2();
				//Log.LogNormal( channel + ":RPN MSB:" + aData );
				break;

			case 0x78:
				//AllSoundOff();
				//Log.LogWarning( channel + ":All Sound Off:" + aData );
				break;

			case 0x79:
				//Log.LogNormal( channel + ":Reset All Controller:" + aData );
				break;

			case 0x7B:
				aMidiSynthesizer.AllNoteOff();
				//Log.LogWarning( channel + ":All Note Off:" + aData );
				break;

			case 0x7C:
				aMidiSynthesizer.AllNoteOff();
				lMidiGenerator.OmniModeOff( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Omni Mode Off:" + aData );
				break;

			case 0x7D:
				aMidiSynthesizer.AllNoteOff();
				lMidiGenerator.OmniModeOn( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Omni Mode On:" + aData );
				break;

			case 0x7E:
				aMidiSynthesizer.AllNoteOff();
				lMidiGenerator.MonoModeOn( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Mono Mode On" + aData );
				break;

			case 0x7F:
				aMidiSynthesizer.AllNoteOff();
				lMidiGenerator.PolyModeOn( midiEvent.GetData2() );
				////Log.LogNormal( channel + ":Poly Mode On:" + aData );
				break;

			default:
				//Log.LogWarning( channel + ":Control:" + controlNumber + ":" + aData );
				break;
			}
		}

		public void SetRpnMsb( MidiGenerator aMidiGenerator )
		{
			if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x00 )
			{
				aMidiGenerator.midiPitch.SetPitchBendSensitivityMsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x01 )
			{
				aMidiGenerator.midiPitch.SetFineTuningMsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x02 )
			{
				aMidiGenerator.midiPitch.SetCoarseTuningMsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x05 )
			{
				//Log.LogNormal( channel + ":RPN Modulation Depth Range Msb:" + data );
			}
			else if( aMidiGenerator.rpnMsb == 0x7F && aMidiGenerator.rpnLsb == 0x7F )
			{
				//Log.LogNormal( channel + ":RPN Null:" + data );
				SetNrpn( aMidiGenerator );
			}
			else
			{
				//Debug.LogWarning( aMidiSynthesizerArray + ":Undefined RPN:" + data );
				SetNrpn( aMidiGenerator );
			}
		}

		public void SetRpnLsb( MidiGenerator aMidiGenerator )
		{
			if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x00 )
			{
				aMidiGenerator.midiPitch.SetPitchBendSensitivityLsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x01 )
			{
				aMidiGenerator.midiPitch.SetFineTuningLsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x02 )
			{
				aMidiGenerator.midiPitch.SetCoarseTuningLsb( midiEvent.GetData2() );
			}
			else if( aMidiGenerator.rpnMsb == 0x00 && aMidiGenerator.rpnLsb == 0x05 )
			{
				//Log.LogNormal( channel + ":RPN Modulation Depth Range Lsb:" + data );
			}
			else if( aMidiGenerator.rpnMsb == 0x7F && aMidiGenerator.rpnLsb == 0x7F )
			{
				//Log.LogNormal( channel + ":RPN Null:" + data );
				SetNrpn( aMidiGenerator );
			}
			else
			{
				//Debug.LogWarning( channel + ":Undefined RPN:" + data );
				SetNrpn( aMidiGenerator );
			}
		}

		public void SetNrpn( MidiGenerator aMidiGenerator )
		{
			if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x08 )
			{
				//Log.LogNormal( channel + ":NRPN ビブラート・レイト:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x09 )
			{
				//Log.LogNormal( channel + ":ビブラート・デプス:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x0A )
			{
				//Log.LogNormal( channel + ":NRPN ビブラート・ディレイ:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x20 )
			{
				//Log.LogNormal( channel + ":NRPN TVFカットオフ周波数:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x21 )
			{
				//Log.LogNormal( channel + ":NRPN TVFレゾナンス:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x63 )
			{
				//Log.LogNormal( channel + ":NRPN TVF&TVAエンベローブ・アタック・タイム:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x64 )
			{
				//Log.LogNormal( channel + ":NRPN TVF&TVAエンベローブ・ディケイ・タイム:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x01 && aMidiGenerator.nrpnLsb == 0x66 )
			{
				//Log.LogNormal( channel + ":NRPN TVF&TVAエンベローブ・リリース・タイム:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x18 )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・ピッチ・コース:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x1A )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・TVAレベル:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x1C )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・パンポット:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x1D )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・リバーブ・センド・レベル:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x1E )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・コーラス・センド・レベル:" + data );
			}
			else if( aMidiGenerator.nrpnMsb == 0x1F )
			{
				//Log.LogNormal( channel + ":NRPN ドラム・インストゥルメント・ディレイ・センド・レベル:" + data );
			}
			else
			{
				//Debug.LogWarning( channel + ":Undefined NRPN:" + data );
			}
		}
	}
}
