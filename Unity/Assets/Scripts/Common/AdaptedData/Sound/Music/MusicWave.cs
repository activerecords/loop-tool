using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Riff.Wave;
using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicWave : MusicPcm
	{
        private const int LENGTH_BUFFER = 1024 * 4;

		private string nameFile;
		private int bytePosition;
		private int byteSize;
		private int sampleBits;
		private float[][] sampleArray;
		private int startPosition;
        private int lengthBuffer;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicWave( RiffFile aRiffFile )
		{
			RiffChunkListWave lWaveRiff = ( RiffChunkListWave )aRiffFile.riffChunkList;

			nameFile = aRiffFile.name;
			bytePosition = ( int )lWaveRiff.dataChunk.position;
			byteSize = ( int )lWaveRiff.dataChunk.size;

			Channels = lWaveRiff.fmt_Chunk.channels;
			SampleRate = ( int )lWaveRiff.fmt_Chunk.samplesPerSec;
			sampleBits = lWaveRiff.fmt_Chunk.bitsPerSample;
			SampleLength = ( int )( byteSize / ( sampleBits / 8 ) / Channels );

			sampleArray = new float[Channels][];

            if( LENGTH_BUFFER == 0 )
            {
                lengthBuffer = SampleLength;
            }
            else
            {
                lengthBuffer = LENGTH_BUFFER;
            }

			for( int i = 0; i < Channels; i++ )
			{
                sampleArray[i] = new float[lengthBuffer];
			}

			startPosition = 0x7FFFFFFF;

			if( lWaveRiff.smplChunk != null )
			{
				Loop = new List<List<LoopInformation>>();

				int lIndex = -1;
				int lLoopLength = -1;

				for( int i = 0; i < lWaveRiff.smplChunk.sampleLoops; i++ )
				{
					SampleLoop lLoop = lWaveRiff.smplChunk.sampleLoopList[i];

					if( ( int )( lLoop.end - lLoop.start ) == lLoopLength )
					{
						
					}
					else
					{
						Loop.Add( new List<LoopInformation>() );
						lLoopLength = ( int )( lLoop.end - lLoop.start );
						lIndex++;
					}

					Loop[lIndex].Add( new LoopInformation( SampleRate, ( int )lLoop.start, ( int )lLoop.end ) );
				}
			}
			else
			{
				Loop = new List<List<LoopInformation>>();
				Loop.Add( new List<LoopInformation>() );
				Loop[0].Add( new LoopInformation( SampleRate, 0, 0 ) );
			}
		}

		public float GetSample( int aChannel, int aPositionSample )
		{
            if( aPositionSample < startPosition || aPositionSample >= startPosition + lengthBuffer )
			{
				startPosition = aPositionSample;

				ReadSampleArray( startPosition );
			}

			return sampleArray[aChannel][aPositionSample - startPosition];
		}

		private void ReadSampleArray( int aPointSample )
		{
			if( nameFile != null )
			{
				using ( FileStream u = new FileStream( nameFile, FileMode.Open, FileAccess.Read ) )
                {
                    ByteArray lByteArray = new ByteArrayLittle( u );

        			switch( sampleBits )
        			{
        			case 16:
                            ReadSampleArray16( lByteArray, aPointSample );
        				break;

        			case 24:
                            ReadSampleArray24( lByteArray, aPointSample );
        				break;

        			default:
        				break;
        			}
                }
            }
		}

        private void ReadSampleArray16( ByteArray aByteArray, int aPositionSample )
		{
			aByteArray.SetPosition( bytePosition + 2 * Channels * aPositionSample );

            for( int i = 0; i < lengthBuffer && i < SampleLength - aPositionSample; i++ )
			{
				for( int j = 0; j < Channels; j++ )
				{
					Int32 sample = aByteArray.ReadInt16();
					sampleArray[j][i] = ( float )sample / ( float )0x8000;
				}
			}
		}

        private void ReadSampleArray24( ByteArray aByteArray, int aPositionSample )
		{
			aByteArray.SetPosition( bytePosition + 3 * Channels * aPositionSample );

            for( int i = 0; i < lengthBuffer && i < SampleLength - aPositionSample; i++ )
			{
				for( int j = 0; j < Channels; j++ )
				{
					Int32 sample = aByteArray.ReadInt24();
					sampleArray[j][i] = ( float )sample / ( float )0x800000;
				}
			}
		}
	}
}
