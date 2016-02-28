using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Sfz;
using Monoamp.Common.Utility;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class SoundfontSfz : SoundfontBase
	{
		public Ampeg ampeg{ get; private set; }
		public Soundinfo soundinfo{ get; private set; }
		public WaveformReaderPcm waveform{ get; private set; }

		public SoundfontSfz( SfzRegion sfzData )
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
			
			waveform = new WaveformReaderPcm( PoolCollection.GetRiffWave( sfzData.sample ), true );
		}
	}
}
