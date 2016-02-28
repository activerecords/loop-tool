using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Riff.Sfbk;

namespace Curan.Common.AdaptedData
{
	public class SoundfontSfbk : SoundfontBase
	{
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

			waveform = new WaveformSfbk( sdtaBodyList, shdrData, startAddrsOffset, endAddrsOffset, aName );
		}
	}
}
