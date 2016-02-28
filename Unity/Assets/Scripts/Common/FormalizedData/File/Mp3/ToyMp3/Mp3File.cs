using UnityEngine;

using System;
using System.IO;
using System.Collections.Generic;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public class Mp3File
	{
		private int channels;
		private int sampleRate;
		private int sampleLength;
		private List<Int16> sampleList;

		public Mp3File( Stream aStream )
		{
			ToyMP3 mp3 = new ToyMP3( aStream );
			ToyMP3Frame frame = new ToyMP3Frame();
			ToyMP3Decoder decoder = new ToyMP3Decoder();
			
			channels = 2;
			sampleRate = 44100;
			sampleLength = 0;
			sampleList = new List<Int16>();

			try
			{
				while( mp3.SeekMP3Frame( frame ) )
				{
					decoder.DecodeFrame( frame );
					
					for( int i = 0; i < decoder.Pcm.Length; i++ )
					{
						sampleList.Add( decoder.Pcm[i] );
					}
					
					sampleLength += decoder.Pcm.Length / 2;
				}
			}
			catch( Exception e )
			{
				// Exist BitStream Error.
				Debug.LogError( "Mp3 Error:" + e );
			}

			Debug.Log( "clpped_samples:" + decoder.Clip );
		}
		
		public int GetChannelLength()
		{
			return 2;
		}
		
		public int GetSampleLength()
		{
			return sampleLength;
		}
		
		public int GetSampleRate()
		{
			return sampleRate;
		}
		
		public int GetSampleLoopStart()
		{
			return 0;
		}
		
		public int GetSampleLoopEnd()
		{
			return 1;
		}

		public float[][] GetSampleArray()
		{
			float[][] lSampleArray = new float[2][];
			
			lSampleArray[0] = new float[sampleLength];
			lSampleArray[1] = new float[sampleLength];

			for( int i = 0; i < sampleLength; i++ )
			{
				lSampleArray[0][i] = ( float )sampleList[i * 2 + 0] / ( float )0x0FFF;
				lSampleArray[1][i] = ( float )sampleList[i * 2 + 1] / ( float )0x0FFF;
			}

			return lSampleArray;
		}
	}
}
