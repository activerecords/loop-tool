using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Riff.Sfbk;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundfontSfbk : SoundfontBase
	{
		public Ampeg ampeg{ get; private set; }
		public Soundinfo soundinfo{ get; private set; }
		public WaveformReaderPcm waveform{ get; private set; }

		public SoundfontSfbk( RiffChunkListSdta[] sdtaBodyList, ShdrData shdrData, byte aLowKey, byte aHighKey, byte aKeyCenter, int aTune, bool aLoopMode, int startAddrsOffset, int endAddrsOffset, int startLoopAddrsOffset, int endLoopAddrsOffset, int modEnvToPitch, Instrument instrument ,string aName )
		{
			byte lokey = aLowKey;
			byte hikey = aHighKey;
			bool loopMode = aLoopMode;
			int loopStart = ( int )( ( shdrData.startLoop + startLoopAddrsOffset ) - ( shdrData.start + startAddrsOffset ) );
			int loopEnd = ( int )( ( shdrData.endLoop + endLoopAddrsOffset ) - ( shdrData.start + startAddrsOffset ) );
			int pitchKeyCenter = shdrData.originalPitch;
			int tune = shdrData.pitchCorrection;
			int pitchEnvelope = instrument.modEnvToPitch;
			int pitchAdd = instrument.fineTune;
			float volume = 0.0f;

			if( instrument.rootKey != 0 )
			{
				pitchKeyCenter = instrument.rootKey;
			}

			if( aTune != 0 )
			{
				pitchAdd = aTune;
			}

			if( modEnvToPitch != 0 )
			{
				pitchEnvelope = modEnvToPitch;
			}

			if( aKeyCenter != 0 )
			{
				pitchKeyCenter = aKeyCenter;
			}

			soundinfo = new Soundinfo( lokey, hikey, loopMode, loopStart, loopEnd, 0, 0x7FFFFFFF, tune, pitchKeyCenter, 0, 0, volume );
			ampeg = new Ampeg( 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 1.0d, 0.25d );

			waveform = new WaveformReaderPcm( sdtaBodyList, shdrData, startAddrsOffset, endAddrsOffset, aName );
		}
	}
}
