using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public static class MetaEventReader
	{
		public static MetaEventBase Execute( int aDelta, ByteArray aByteArray )
		{
			MetaEventBase lMetaEvent;
			byte lType = aByteArray.ReadByte();

			switch( lType )
			{
			case 0x01:	// Text.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x02:	// Copyright Notice.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x03:	// Sequense/Track Name.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x04:	// Instrument Name.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x05:	// Lylics.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x06:	// Marker.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x07:	// Queue Point.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			case 0x20:
				lMetaEvent = new MetaEventChannel( aDelta, lType, aByteArray );
				break;

			case 0x21:
				lMetaEvent = new MetaEventPort( aDelta, lType, aByteArray );
				break;

			case 0x2F:
				lMetaEvent = new MetaEventTrackEnd( aDelta, lType, aByteArray );
				break;

			case 0x51:
				lMetaEvent = new MetaEventTempo( aDelta, lType, aByteArray );
				break;

			case 0x54:
				lMetaEvent = new MetaEventSmpteOffset( aDelta, lType, aByteArray );
				break;

			case 0x58:
				lMetaEvent = new MetaEventTimeSignature( aDelta, lType, aByteArray );
				break;

			case 0x59:
				lMetaEvent = new KeySignature( aDelta, lType, aByteArray );
				break;

			case 0x7F:	// Sequencer Meta Event.
				lMetaEvent = new MetaEventText( aDelta, lType, aByteArray );
				break;

			default:
				// �����`�̃��^�C�x���g���b�Z�[�W.
				Logger.LogError( "Undefined Meta Event" );
				throw new Exception();
			}

			return lMetaEvent;
		}
	}
}
