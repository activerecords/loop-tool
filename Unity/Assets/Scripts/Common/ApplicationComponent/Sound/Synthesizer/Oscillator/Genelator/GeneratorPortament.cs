using System;

namespace Curan.Common.ApplicationComponent.Sound.Synthesizer
{
	public struct GeneratorPortament
	{
		public double portamentTime;
		public double noteFrequency;
		public double noteFrequencyDiff;
		public double noteFrequencyDestination;
		public double noteFrequencyAdd;

		public GeneratorPortament( int aNote, double aPitch )
		{
			portamentTime = 1.0d;
			noteFrequency = 440.0d * Math.Pow( 2.0d, ( ( double )aNote - 69.0d ) / 12.0d ) * Math.Pow( 2.0d, aPitch / 1200.0d );
			noteFrequencyDestination = noteFrequency;
			noteFrequencyDiff = 0.0d;
			noteFrequencyAdd = 0.0d;
		}

		public void Portament( int aNoteDestination, double aPitch, ref MidiPitch aMidiPitch )
		{
			if( /*aMidiStatus.GetBank() != 0x7F00 &&*/ aMidiPitch.GetPortamentFlag() == true )
			{
				portamentTime = aMidiPitch.GetPortamentTime();
				noteFrequencyDestination = 440.0d * Math.Pow( 2.0d, ( aNoteDestination - 69.0d ) / 12.0f ) * Math.Pow( 2.0d, aPitch / 1200.0d );
				noteFrequencyDiff = noteFrequencyDestination - noteFrequency;
			}
		}

		public void Update( int aSampleRate )
		{
			if( noteFrequencyDiff != 0.0d )
			{
				noteFrequencyAdd = noteFrequencyDiff / aSampleRate / portamentTime;

				noteFrequency += noteFrequencyAdd;

				if( ( noteFrequencyAdd > 0 && noteFrequency > noteFrequencyDestination ) || ( noteFrequencyAdd < 0 && noteFrequency < noteFrequencyDestination ) )
				{
					noteFrequency = noteFrequencyDestination;
				}
			}
		}
	}
}
