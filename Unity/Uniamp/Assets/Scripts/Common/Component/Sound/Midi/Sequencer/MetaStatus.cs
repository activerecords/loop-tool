using System;

namespace Monoamp.Common.Component.Sound.Midi
{
	public class MetaStatus
	{
		private struct UnitTempoBpm
		{
			private double tempo;	// テンポ:4分音符の長さ（単位:マイクロセカンド）
			private double bpm;

			public void SetTempo( double aTempo )
			{
				tempo = aTempo;
				bpm = 60.0f * 1000000.0f / tempo;
			}

			public double GetTempo()
			{
				return tempo;
			}

			public void SetBpm( double aBpm )
			{
				bpm = aBpm;
				tempo = 60.0f * 1000000.0f / bpm;
			}

			public double GetBpm()
			{
				return bpm;
			}
		}

		private UnitTempoBpm unitTempoBpm;
		private int deltaPosition;

		public MetaStatus()
		{
			unitTempoBpm = new UnitTempoBpm();

			unitTempoBpm.SetBpm( 120.0d );
			deltaPosition = 0;
		}

		public void SetTempo( double aTempo )
		{
			unitTempoBpm.SetTempo( aTempo );
		}

		public double GetTempo()
		{
			return unitTempoBpm.GetTempo();
		}

		public void SetBpm( double aBpm )
		{
			unitTempoBpm.SetBpm( aBpm );
		}

		public double GetBpm()
		{
			return unitTempoBpm.GetBpm();
		}

		public void SetDelta( int aDelta )
		{
			deltaPosition = aDelta;
		}

		public int GetDelta()
		{
			return deltaPosition;
		}
	}
}
