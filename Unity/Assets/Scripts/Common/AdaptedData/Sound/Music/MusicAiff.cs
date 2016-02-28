using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Form;
using Curan.Common.FormalizedData.File.Form.Aiff;
using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicAiff : MusicPcm
	{
		public readonly int samples;
		public readonly int sampleBits;
		public readonly int sampleLoopStart;
		public readonly int sampleLoopEnd;
		
		private string name;
		public readonly float[][] sampleArray;
		public readonly float[] sampleMeanArray;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicAiff( FormFile aFormFile )
		{
			FormChunkListAiff lAiffForm = ( FormChunkListAiff )aFormFile.formChunkList;

			FormChunkSsnd lSsndChunk = lAiffForm.chunkSsnd;
			name = aFormFile.name;
			int position = ( int )lSsndChunk.position;
			int length = lSsndChunk.dataSize;

			FormChunkComm lChunkComm = lAiffForm.chunkComm;
			Channels = lChunkComm.numberOfChannels;
			SampleRate = ( int )lChunkComm.sampleRate;
			sampleBits = lChunkComm.bitsPerSamples;
			SampleLength = length / ( sampleBits / 8 ) / Channels;

			sampleMeanArray = new float[SampleLength];
			
			if( name != null )
			{
				using ( FileStream u = new FileStream( name, FileMode.Open, FileAccess.Read ) )
				{
					ByteArray lByteArray = new ByteArrayBig( u );

					lByteArray.SetPosition( position );

					for( int i = 0; i < SampleLength; i++ )
					{
						float value = 0.0f;

						for( int j = 0; j < Channels; j++ )
						{
							if( sampleBits == 16 )
							{
								value += ( float )lByteArray.ReadInt16() / ( float )0x8000;
							}
							else if( sampleBits == 24 )
							{
								value += ( float )lByteArray.ReadInt24() / ( float )0x800000;
							}
						}

						sampleMeanArray[i] = value / Channels;
					}
				}
			}

			int lSampleLoopStart = 0;
			int lSampleLoopEnd = 0;

			Loop = new List<List<LoopInformation>>();
			Loop.Add( new List<LoopInformation>() );
			Loop[0].Add( new LoopInformation( SampleRate, lSampleLoopStart, lSampleLoopEnd ) );
		}

		public float GetSample( int aChannel, int aPositionSample )
		{
			return sampleMeanArray[aPositionSample];
		}
	}
}
