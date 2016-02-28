using System;

namespace Monoamp.Common.Component.Application.Sound
{
	public struct MidiPitch
	{
		private double modulationDepth;
		private double portamentTime;
		private Byte modulationDepthRangeMsb;
		private Byte modulationDepthRangeLsb;
		private int pitch;
		private Byte pitchBendSensitivityMsb;
		private Byte pitchBendSensitivityLsb;
		private Byte fineTuningMsb;
		private Byte fineTuningLsb;
		private Byte coarseTuningMsb;
		//private Byte coarseTuningLsb;

		private bool isOnPortament;

		private double frequency;
		public double lowFrequency;
		public double modHigh;
		public double modLow;
		public double mulBase;

		public void Init()
		{
			modulationDepth = 0.0d;
			portamentTime = 0.01d;
			pitch = 8192;
			pitchBendSensitivityMsb = 2;
			pitchBendSensitivityLsb = 0;
			fineTuningMsb = 0x40;
			fineTuningLsb = 0x00;
			coarseTuningMsb = 0x40;
			//coarseTuningLsb = 0x00;
			modulationDepthRangeMsb = 0x00;
			modulationDepthRangeLsb = 0x40;
			isOnPortament = false;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
			
			lowFrequency = 5.0d;
			double lModulationValue = GetModulationDepthRange() * GetModulationDepth();
			modHigh = Math.Pow( 2.0d, lModulationValue / 12.0d );
			modLow = Math.Pow( 2.0d, -lModulationValue / 12.0d );
			mulBase = 2 * lowFrequency * ( modHigh - modLow );
		}

		public void SetPitch( Byte aData1, Byte aData2 )
		{
			pitch = ( aData2 & 0x7F ) << 7 | ( aData1 & 0x7F );

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public double GetPitch()
		{
			return ( double )pitch - 8192.0d;
		}

		public void SetPitchBendSensitivityMsb( Byte aData )
		{
			if( aData > 12 )
			{
				pitchBendSensitivityMsb = 12;
			}
			else
			{
				pitchBendSensitivityMsb = aData;
			}

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public void SetPitchBendSensitivityLsb( Byte aData )
		{
			pitchBendSensitivityLsb = aData;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public int GetPitchBendSensitivity()
		{
			return pitchBendSensitivityMsb * 100 + pitchBendSensitivityLsb;
		}

		public void SetFineTuningMsb( Byte aData )
		{
			fineTuningMsb = aData;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public void SetFineTuningLsb( Byte aData )
		{
			fineTuningLsb = aData;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public double GetFineTuning()
		{
			return ( ( double )( fineTuningMsb << 7 | fineTuningLsb ) - 8192.0d ) * 100.0d / 8192.0d;
		}

		public void SetCoarseTuningMsb( Byte aData )
		{
			coarseTuningMsb = aData;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public void SetCoarseTuningLsb( Byte aData )
		{
			//coarseTuningLsb = aData;

			double lPitch = GetPitch() / ( 819200.0d / ( double )GetPitchBendSensitivity() ) + GetFineTuning() + GetCoarseTuning() /*+ midiStatusMaster.structPitch.GetFineTuning() + midiStatusMaster.structPitch.GetCoarseTuning()*/;
			frequency = Math.Pow( 2.0d, lPitch / 12.0d );
		}

		public double GetCoarseTuning()
		{
			return ( ( double )coarseTuningMsb - 64.0d ) * 100.0d;
		}

		public void SetModulationDepthRangeMsb( Byte aData )
		{
			modulationDepthRangeMsb = aData;

			lowFrequency = 5.0d;
			double lModulationValue = GetModulationDepthRange() * GetModulationDepth();
			modHigh = Math.Pow( 2.0d, lModulationValue / 12.0d );
			modLow = Math.Pow( 2.0d, -lModulationValue / 12.0d );
			mulBase = 2 * lowFrequency * ( modHigh - modLow );
		}

		public void SetModulationDepthRangeLsb( Byte aData )
		{
			modulationDepthRangeLsb = aData;

			lowFrequency = 5.0d;
			double lModulationValue = GetModulationDepthRange() * GetModulationDepth();
			modHigh = Math.Pow( 2.0d, lModulationValue / 12.0d );
			modLow = Math.Pow( 2.0d, -lModulationValue / 12.0d );
			mulBase = 2 * lowFrequency * ( modHigh - modLow );
		}

		public double GetModulationDepthRange()
		{
			return modulationDepthRangeMsb + ( double )modulationDepthRangeLsb / 128.0d;
		}

		public void SetModulationDepth( Byte aData )
		{
			if( aData > 127 )
			{
				aData = 127;
			}

			modulationDepth = ( double )aData / 127.0d;

			lowFrequency = 5.0f;
			double lModulationValue = GetModulationDepthRange() * GetModulationDepth();
			modHigh = Math.Pow( 2.0d, lModulationValue / 12.0d );
			modLow = Math.Pow( 2.0d, -lModulationValue / 12.0d );
			mulBase = 2 * lowFrequency * ( modHigh - modLow );
		}

		public double GetModulationDepth()
		{
			return modulationDepth;
		}

		public void SetPortamentTime( Byte aData )
		{
			if( aData > 127 )
			{
				aData = 127;
			}

			portamentTime = 1.0d - ( 1.0d - 0.01d ) * ( double )aData / 127.0d;
		}

		public double GetPortamentTime()
		{
			return portamentTime;
		}

		public void SetPortamentFlag( Byte aData )
		{
			if( aData < 64 )
			{
				// To Be Fixed.
				// 自分で設定可能とするため、一時的にコメントアウト.
				//isOnPortament = false;
			}
			else
			{
				isOnPortament = true;
			}
		}

		public bool GetPortamentFlag()
		{
			return isOnPortament;
		}

		public double GetFrequency()
		{
			return frequency;
		}
	}
}
