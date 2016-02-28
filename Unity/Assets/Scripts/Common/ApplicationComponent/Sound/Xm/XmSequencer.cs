using System;

using Curan.Common.AdaptedData.Music;
using Curan.Common.FormalizedData.File.Xm;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Xm
{
	public class XmSequencer
	{
		private MusicXm musicXm;
		private XmSynthesizer synthesizer;

		private int frame;
		private int tempo;
		private int index;
		private int patternIndex;
		private int patternNumber;

		public XmSequencer( MusicXm aMusicXm )
		{
			musicXm = aMusicXm;
			synthesizer = new XmSynthesizer( musicXm );

			frame = 0;
			tempo = 2;
			index = -1;
			patternIndex = 0;
			patternNumber = 0;
		}

		public void Update( float[] aData, int aChannels, int aSampleRate )
		{
			frame++;

			if( index != frame / tempo )
			{
				index = frame / tempo;

				if( index < musicXm.GetPatternChunkArray()[patternNumber].GetNumberOfRowsInPattern() )
				{
					for( int i = 0; i < musicXm.GetNumberOfChannels(); i++ )
					{
						if( musicXm.note[i][patternNumber][index] != 0 )
						{
							InstrumentChunk lInstrumentChunk = musicXm.GetInstrumentChunkArray()[( int )musicXm.instrument[i][patternNumber][index] - 1];
							synthesizer.NoteOn( i, ( int )musicXm.note[i][patternNumber][index], lInstrumentChunk );
						}
						else
						{
							synthesizer.FadeOut( i );
							synthesizer.IncrementEnvelope( i );
						}
					}
				}
				else
				{
					frame = 0;
					patternIndex++;
					patternNumber = musicXm.GetPatternOrderTable()[patternIndex];

					Logger.LogNormal( "■Pattern:" + patternNumber.ToString() );

					if( patternIndex >= musicXm.GetSongLength() )
					{
						patternIndex = musicXm.GetRestartPosition();

						Logger.LogNormal( "■Loop." );
					}
				}
			}

			synthesizer.SynthesizeWaveform( aData, aChannels, aSampleRate );
		}
	}
}
