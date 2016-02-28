using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Xm;

using Curan.Common.AdaptedData.Music;

namespace Curan.Common.ApplicationComponent.Sound.Xm
{
	public class XmSynthesizer
	{
		private MusicXm musicXm;
		private XmNote[] noteArray;

		private float[,] soundBuffer;
		private int soundBufferLength;

		public XmSynthesizer( MusicXm aMusicXm )
		{
			musicXm = aMusicXm;
			noteArray = new XmNote[aMusicXm.GetNumberOfChannels()];

			for( int i = 0; i < aMusicXm.GetNumberOfChannels(); i++ )
			{
				noteArray[i] = new XmNote();
			}
		}

		public void SynthesizeWaveform( float[] aData, int aChannels, int aSampleRate )
		{
			if( soundBuffer == null )
			{
				soundBufferLength = aData.Length / aChannels;
				soundBuffer = new float[aChannels, soundBufferLength];
			}

			for( int i = 0; i < noteArray.Length; i++ )
			{
				noteArray[i].Synthesis( soundBuffer );
			}

			for( int i = 0; i < aChannels; i++ )
			{
				for( int j = 0; j < soundBufferLength; j++ )
				{
					aData[j * aChannels + i] = soundBuffer[i, j];
					soundBuffer[i, j] = 0.0f;
				}
			}
		}

		public void NoteOn( int channel, int note, InstrumentChunk aInstrumentChunk )
		{
			noteArray[channel].NoteOn( note, aInstrumentChunk, musicXm.GetFlags() );
		}

		public void FadeOut( int channel )
		{
			noteArray[channel].FadeOut();
		}

		public void IncrementEnvelope( int channel )
		{
			noteArray[channel].IncrementEnvelope();
		}
	}
}
