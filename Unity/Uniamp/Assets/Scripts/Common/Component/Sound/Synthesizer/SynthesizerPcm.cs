using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Application.Sound;
using Monoamp.Common.Component.Sound.Utility;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
	public class SynthesizerPcm
	{
		private readonly WaveformReaderPcm waveform;
		
		public bool isLoop;
		public LoopInformation loop;
		
		public Dictionary<int, SoundTime> oneSampelList;
		public SoundTime Position{ get; private set; }
		public SoundTime Elapsed{ get; private set; }

		public double PositionRate
		{
			get { return Position.sample / waveform.format.samples; }
			set { Position = new SoundTime( waveform.format.sampleRate, waveform.format.samples * value ); }
		}

		public SynthesizerPcm( WaveformReaderPcm aWaveform, LoopInformation aLoop )
		{
			waveform = aWaveform;
			
			oneSampelList = new Dictionary<int, SoundTime>();
			Position = new SoundTime( waveform.format.sampleRate, 0 );
			Elapsed = new SoundTime( waveform.format.sampleRate, 0 );
			
			loop = aLoop;
			isLoop = false;
		}

		// Return: Ture if end.
		public bool Update( float[] aSoundBuffer, int aChannels, int aSampleRate )
		{
			SoundTime PositionPre = Position;
			Position += GetOneSample( aSampleRate );
			Elapsed += GetOneSample( aSampleRate );

			if( isLoop == true )
			{
				if( loop.length.sample > 0 )
				{
					if( PositionPre.sample < loop.end.sample + 1 && Position.sample >= loop.end.sample + 1 )
					{
						Position = new SoundTime( PositionPre.sampleRate, Position.sample - ( loop.end.sample + 1 - loop.start.sample ) );
					}
					else if( Position.sample >= waveform.format.samples )
					{
						Position = new SoundTime( Position.sampleRate, Position.sample - waveform.format.samples );
					}
				}
				else
				{
					if( Position.sample >= waveform.format.samples )
					{
						Position = new SoundTime( Position.sampleRate, PositionPre.sample - waveform.format.samples );
					}
				}
			}
			
			if( Position.sample + 1 < waveform.format.samples )
			{
				for( int i = 0; i < aChannels; i++ )
				{
					aSoundBuffer[i] = MeanInterpolation.Calculate( waveform, i, Position.sample );
				}
			}
			else if( Position.sample < waveform.format.samples )
			{
				for( int i = 0; i < aChannels; i++ )
				{
					if( loop.length.sample > 0 )
					{
						aSoundBuffer[i] = MeanInterpolation.Calculate( waveform, i, Position.sample, loop.start.sample );
					}
					else
					{
						aSoundBuffer[i] = waveform.reader.GetSample( i, ( int )Position.sample );
					}
				}
			}
			else // End position.
			{
				return true;
			}

			return false;
		}

		public SoundTime GetOneSample( int aSampleRate )
		{
			if( oneSampelList.ContainsKey( aSampleRate ) == false )
			{
				oneSampelList.Add( aSampleRate, new SoundTime( aSampleRate, 1 ) );
			}
			
			return oneSampelList[aSampleRate];
		}
	}
}
