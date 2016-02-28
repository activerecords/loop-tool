using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public static class MidiEventReader
	{
		public static MidiEventBase Execute( int aDelta, byte aState, ByteArray aByteArray )
		{
			MidiEventBase lMidiEvent = null;

			switch( aState & 0xF0 )
			{
			case 0x80:
				lMidiEvent = new MidiEventNoteOff( aDelta, aState, aByteArray );
				break;

			case 0x90:
				MidiEventNoteOn lNoteOn = new MidiEventNoteOn( aDelta, aState, aByteArray );

				if( lNoteOn.GetVelocity() == 0 ) {
					lMidiEvent = new MidiEventNoteOff( lNoteOn );
				}
				else {
					lMidiEvent = lNoteOn;
				}

				break;

			case 0xA0:
				lMidiEvent = new MidiEventKeyPressure( aDelta, aState, aByteArray );
				break;

			case 0xB0:
				lMidiEvent = new MidiEventControlChange( aDelta, aState, aByteArray );
				break;

			case 0xC0:
				lMidiEvent = new MidiEventProgramChange( aDelta, aState, aByteArray );
				break;

			case 0xD0:
				lMidiEvent = new MidiEventChannelPressure( aDelta, aState, aByteArray );
				break;

			case 0xE0:
				lMidiEvent = new MidiEventPitchWheelChange( aDelta, aState, aByteArray );
				break;

			case 0xF0:
				lMidiEvent = new MidiEventSystemExclusive( aDelta, aState, aByteArray );
				break;

			default:
				// 未定義のイベントメッセージ.
				Logger.LogError( "Undefined Midi Event:" + aState );
				throw new Exception();
			}

			return lMidiEvent;
		}
	}
}
