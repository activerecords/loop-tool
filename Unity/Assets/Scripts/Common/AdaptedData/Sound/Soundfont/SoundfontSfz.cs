using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FileLoader.Waveform;
using Curan.Common.FormalizedData.File.Sfz;
using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public class SoundfontSfz : SoundfontBase
	{
		public readonly string sample;

		public SoundfontSfz( SfzRegion sfzData, Dictionary<string, string> aPathWaveformDictionary )
		{
			byte lokey = sfzData.lokey;
			byte hikey = sfzData.hikey;
			bool loopMode = sfzData.loop_mode;
			int loopStart = sfzData.loop_start - 1;
			int loopEnd = sfzData.loop_end;
			int offset = sfzData.offset;
			int end = sfzData.end;
			int tune = sfzData.tune;
			int pitchKeyCenter = sfzData.pitch_keycenter;
			float volume = sfzData.volume;

			soundinfo = new Soundinfo( lokey, hikey, loopMode, loopStart, loopEnd, offset, end, tune, pitchKeyCenter, 0, 0, volume );
			ampeg = new Ampeg( sfzData.ampeg_delay, sfzData.ampeg_start / 100.0f, sfzData.ampeg_attack, sfzData.ampeg_hold, sfzData.ampeg_decay, sfzData.ampeg_sustain / 100.0f, sfzData.ampeg_release );

			if( aPathWaveformDictionary.ContainsKey( sfzData.sample ) == false )
			{
				Logger.LogError( "This waveform is not found:" + sfzData.sample );
			}
			else
			{
				waveform = LoaderWaveform.Load( aPathWaveformDictionary[sfzData.sample] );
			}
		}
	}
}
