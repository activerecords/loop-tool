using System;
using System.Collections.Generic;

namespace Curan.Common.AdaptedData
{
	public class Ampeg
	{
		public readonly double ampegDelay;
		public readonly double ampegStart;
		public readonly double ampegAttack;
		public readonly double ampegHold;
		public readonly double ampegDecay;
		public readonly double ampegSustain;
		public readonly double ampegRelease;

		public Ampeg( double aAmpegDelay, double aAmpegStart, double aAmpegAttack, double aAmpegHold, double aAmpegDecay, double aAmpegSustain, double aAmpegRelease )
		{
			ampegDelay = aAmpegDelay;
			ampegStart = aAmpegStart;
			ampegAttack = aAmpegAttack;
			ampegHold = aAmpegHold;
			ampegDecay = aAmpegDecay;
			ampegSustain = aAmpegSustain;
			ampegRelease = aAmpegRelease;
		}
	}

	public class Soundinfo
	{
		public readonly byte lokey;
		public readonly byte hikey;
		public readonly bool loopMode;
		public readonly int loopStart;
		public readonly int loopEnd;
		public readonly int offset;
		public readonly int end;
		public readonly int tune;
		public readonly int pitchKeyCenter;
		public readonly int pitchAdd;
		public readonly int pitchEnvelope;
		public readonly float volume;

		public Soundinfo( byte aLokey, byte aHikey, bool aLoopMode, int aLoopStart, int aLoopEnd, int aOffset, int aEnd, int aTune, int aPitchKeyCenter, int aPitchAdd, int aPitchEnvelope, float aVolume )
		{
			lokey = aLokey;
			hikey = aHikey;
			loopMode = aLoopMode;
			loopStart = aLoopStart;
			loopEnd = aLoopEnd;
			offset = aOffset;
			end = aEnd;
			tune = aTune;
			pitchKeyCenter = aPitchKeyCenter;
			pitchAdd = aPitchAdd;
			pitchEnvelope = aPitchEnvelope;
			volume = aVolume;
		}

		public int GetPitch()
		{
			return tune + pitchAdd + pitchEnvelope;
		}
	}

	public abstract class SoundfontBase
	{
		public Ampeg ampeg{ get; protected set; }
		public Soundinfo soundinfo{ get; protected set; }
		public WaveformBase waveform{ get; protected set; }
	}
}
