using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public enum SFGenerator
		{
			StartAddrsOffset = 0,
			EndAddrsOffset,
			StartLoopAddrsOffset,
			EndLoopAddrsOffset,
			StartAddrsCoarseOffset,
			ModLfoToPitch,
			VibLfoToPitch,
			ModEnvToPitch,
			InitialFilterFc,
			InitialFilterQ,
			ModLfoToFilterFc,
			ModEnvToFilterFc,
			EndAddrsCoarseOffset,
			ModLfoToVolume,
			Unused1,
			ChorusEffectsSend,
			ReverbEffectsSend,
			Pan,
			Unused2,
			Unused3,
			Unused4,
			DelayModLfo,
			FreqModLfo,
			DelayVibLfo,
			FreqVibLfo,
			DelayModEnv,
			AttackModEnv,
			HoldModEnv,
			DecayModEnv,
			SustainModEnv,
			ReleaseModEnv,
			KeyNumToModEnvHold,
			KeyNumToModEnvDecay,
			DelayVolEnv,
			AttackVolEnv,
			HoldVolEnv,
			DecayVolEnv,
			SustainVolEnv,
			ReleaseVolEnv,
			KeynumToVolEnvHold,
			KeynumToVolEnvDecay,
			Instrument,
			Reserved1,
			KeyRange,
			VelRange,
			StartLoopAddrsCoarseOffset,
			KeyNum,
			Velocity,
			InitialAttenuation,
			Reserved2,
			EndLoopAddrsCoarseOffset,
			CoarseTune,
			FineTune,
			SampleId,
			SampleModes,
			Reserved3,
			ScaleTuning,
			ExclusiveClass,
			OverridingRootKey,
			Unused5,
			EndOper
	};

	public enum SFModulator
		{
			NoController = 0,
			NoteOnVelocity = 2,
			NoteOnKeyNumber = 3,
			PolyPressure = 10,
			ChannelPressure = 13,
			PitchWheel = 14,
			PitchWheelSensitivity = 16,
			Link = 127
		};

	public enum SFTransform
		{
			Linear = 0
		};

		/*
		protected enum SFModulator
		{
			Linear = 0,
			Concave,
			Convex,
			Switch
		};
		*/

		public class RangesType
		{
			public readonly Byte lo;
			public readonly Byte hi;

			public RangesType( Byte byte1, Byte byte2 )
			{
				lo = byte1;
				hi = byte2;
			}
		}

		public class Generator
		{
			public readonly Byte byte1;
			public readonly Byte byte2;

			public Generator( AByteArray aByteArray )
			{
				byte1 = aByteArray.ReadByte();
				byte2 = aByteArray.ReadByte();
			}

			public RangesType GetRangesType()
			{
				return new RangesType( byte1, byte2 );
			}

			public Int16 GetInt16()
			{
				return ( Int16 )( byte2 << 8 | byte1 );
			}

			public UInt16 GetUInt16()
			{
				return ( UInt16 )( byte2 << 8 | byte1 );
			}
	}

	public class Instrument
	{
		public byte lokey;
		public byte hikey;
		public byte rootKey;
		public int fineTune;
		public UInt16 instrument;
		public bool loopMode;
		public int startAddrsOffset;
		public int endAddrsOffset;
		public int startLoopAddrsOffset;
		public int endLoopAddrsOffset;
		public int startAddrsCoarseOffset;
		public int modEnvToPitch;

		public Instrument()
		{
			lokey = 0;
			hikey = 127;
			rootKey = 0;
			fineTune = 0;
			instrument = 0;
			loopMode = false;
			startAddrsOffset = 0;
			endAddrsOffset = 0;
			startLoopAddrsOffset = 0;
			endLoopAddrsOffset = 0;
			startAddrsCoarseOffset = 0;
			modEnvToPitch = 0;
		}
	}
}
