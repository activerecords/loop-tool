using System;

using Curan.Common.FormalizedData.File.Xm;

using Curan.Common.AdaptedData;

namespace Curan.Common.ApplicationComponent.Sound.Xm
{
	public class XmNote
	{
		public int note;
		//public int instrument;

		public bool isEnd;
		public double position;

		public int envelopeIndex;
		public int envelopePoint;
		public int fadeOutPoint;

		public InstrumentChunk instrumentChunk;
		public SampleChunk sampleChunk;
		public UInt16 flags;

		private static UInt16[] periodTable = new UInt16[]
		{
			907, 900, 894, 887, 881, 875, 868, 862, 856, 850, 844, 838, 832, 826, 820, 814,
			808, 802, 796, 791, 785, 779, 774, 768, 762, 757, 752, 746, 741, 736, 730, 725,
			720, 715, 709, 704, 699, 694, 689, 684, 678, 675, 670, 665, 660, 655, 651, 646,
			640, 636, 632, 628, 623, 619, 614, 610, 604, 601, 597, 592, 588, 584, 580, 575,
			570, 567, 563, 559, 555, 551, 547, 543, 538, 535, 532, 528, 524, 520, 516, 513,
			508, 505, 502, 498, 494, 491, 487, 484, 480, 477, 474, 470, 467, 463, 460, 457
		};

		public void NoteOn( int aNote, InstrumentChunk aInstrumentChunk, UInt16 aFlags )
		{
			isEnd = false;
			position = 0.0d;

			envelopeIndex = 0;
			envelopePoint = 0;
			fadeOutPoint = 0;

			note = aNote;

			if( note == 97 )
			{
				isEnd = true;
			}

			instrumentChunk = aInstrumentChunk;
			int lSampleNumber = ( int )instrumentChunk.GetSampleNumberForAllNotes()[note];
			sampleChunk = instrumentChunk.GetSampleChunkArray()[lSampleNumber];

			flags = aFlags;
		}

		public void FadeOut()
		{
			fadeOutPoint++;
		}

		public void IncrementEnvelope()
		{
			envelopePoint++;

			while( instrumentChunk.GetPointsForVolumeEnvelopeX()[envelopeIndex] < envelopePoint && envelopeIndex < instrumentChunk.GetNumberOfVolumePoints() )
			{
				envelopeIndex++;
			}
		}

		public void Synthesis( float[,] aData )
		{
			if( sampleChunk.GetWaveData() == null || note == 0x00 )
			{
				return;
			}

			float lVolumeEnvelope = ( float )instrumentChunk.GetPointsForVolumeEnvelopeY()[envelopeIndex] / 64.0f;
			float lSampleVolume = ( float )sampleChunk.GetVolume() / 64.0f;
			float lFadeoutVolume = 1.0f - ( float )( instrumentChunk.GetVolumeFadeout() * fadeOutPoint ) / 65536.0f;

			if( lFadeoutVolume < 0.0f )
			{
				lFadeoutVolume = 0;
			}

			float waveRateLeft = 1.0f / 4.0F * lVolumeEnvelope * lSampleVolume * lFadeoutVolume;
			float waveRateRight = 1.0f / 4.0F * lVolumeEnvelope * lSampleVolume * lFadeoutVolume;
			double addPosition = GetFrequency() / 44100.0d;

			for( int i = 0; i < aData.GetLength( 1 ); i++ )
			{
				// 波形の最後に来ているか判定する.
				if( ( int )position >= sampleChunk.GetWaveData().Length )
				{
					isEnd = true;
					return;
				}

				float lWave = sampleChunk.GetWaveData()[( int )position];

				if( ( int )position + 1 < sampleChunk.GetWaveData().Length )
				{
					lWave = Interpolation( lWave, sampleChunk.GetWaveData()[( int )position + 1], position - ( int )position );
				}

				aData[0, i] += lWave * waveRateLeft;
				aData[1, i] += lWave * waveRateRight;

				position += addPosition;

				// ループの場合は再生箇所を変更する.
				if( sampleChunk.GetSampleLoopLength() > 0 )
				{
					if( ( int )position >= sampleChunk.GetSampleLoopStart() + sampleChunk.GetSampleLoopLength() )
					{
						position = ( double )sampleChunk.GetSampleLoopStart();
					}
				}
			}
		}

		public float Interpolation( double a, double b, double aPosition )
		{
			return ( float )( a + ( b - a ) * aPosition );
		}

		public float GetFrequency()
		{
			float period;
			float frequency;

			int realNote = note + sampleChunk.GetRelativeNoteNumber();

			if( ( flags & 0x01 ) == 0x01 )
			{
				// Linear frequence table:
				period = 10 * 12 * 16 * 4 - realNote * 16 * 4 - sampleChunk.GetFinetune() / 2;

				frequency = 8363 * ( float )Math.Pow( 2.0d, ( double )( 6 * 12 * 16 * 4 - period ) / ( 12 * 16 * 4 ) );
			}
			else
			{
				// Amiga frequence table:
				int pos = ( realNote % 12 ) * 8 + sampleChunk.GetFinetune() / 16;
				float frac = ( float )sampleChunk.GetFinetune() / 16 - ( int )( ( float )sampleChunk.GetFinetune() / 16 );

				period = ( periodTable[pos] * ( 1 - frac ) + periodTable[pos + 1] * frac ) * 16 / ( float )Math.Pow( 2.0d, ( double )note / 12.0d );

				frequency = 8363.0f * 1712.0f / period;
			}

			return frequency;
		}
	}
}
