using System;

namespace Monoamp.Common.Component.Application.Sound
{
	public struct FilterVibrato
	{
		private double lowFrequency;
		private double lowFrequencyDirection;

		public FilterVibrato( ref MidiPitch aMidiPitch )
		{
			lowFrequency = aMidiPitch.lowFrequency;
			lowFrequencyDirection = aMidiPitch.mulBase;
			/*
			if( aMidiStatus.GetBank() == 0x7F00 )
			{
				lowFrequencyDirection = 0.0d;
			}*/
		}

		public void Filter( ref double lSampleSpeed, int aSampleRate, ref MidiPitch aMidiPitch )
		{
			if( lowFrequencyDirection != 0.0d )
			{
				lowFrequency += lowFrequencyDirection;

				if( lowFrequency >= aMidiPitch.modHigh * aSampleRate )
				{
					lowFrequencyDirection = -aMidiPitch.mulBase * aSampleRate;
				}
				else if( lowFrequency < aMidiPitch.modLow * aSampleRate )
				{
					lowFrequencyDirection = aMidiPitch.mulBase * aSampleRate;
				}

				lSampleSpeed *= lowFrequency;
			}
		}
	}
}
