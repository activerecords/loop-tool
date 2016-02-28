using System;
using System.Collections.Generic;

using Monoamp.Common.Utility;
using Monoamp.Common.Data;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
	public class MasterStatus
	{
		private MidiVolume midiVolume;
		private MidiPitch midiPitch;
		private FilterChorus chorusFilter;
		private FilterReverb reverbFilter;

		public MasterStatus()
		{
			midiVolume.Init();
			midiPitch.Init();
			reverbFilter.Init();
			chorusFilter.Init();
		}

		public void Init()
		{
			midiVolume.Init();
			midiPitch.Init();
			reverbFilter.Init();
			chorusFilter.Init();
		}

		public void SetVolume( UInt16 aData )
		{
			midiVolume.SetVolumeMsb( ( Byte )( aData >> 8 & 0xFF ) );
			midiVolume.SetVolumeLsb( ( Byte )( aData & 0xFF ) );
		}

		public float GetVolume()
		{
			return midiVolume.GetVolume();
		}

		public float GetVolumeRate()
		{
			return midiVolume.GetVolumeRate();
		}

		public void SetFineTuningMsb( Byte aData )
		{
			midiPitch.SetFineTuningMsb( aData );
		}

		public void SetFineTuningLsb( Byte aData )
		{
			midiPitch.SetFineTuningLsb( aData );
		}

		public double GetFineTuning()
		{
			return midiPitch.GetFineTuning();
		}

		public void SetCoarseTuningMsb( Byte aData )
		{
			midiPitch.SetCoarseTuningMsb( aData );
		}

		public void SetCoarseTuningLsb( Byte aData )
		{
			midiPitch.SetCoarseTuningLsb( aData );
		}

		public double GetCoarseTuning()
		{
			return midiPitch.GetCoarseTuning();
		}
	}
}
