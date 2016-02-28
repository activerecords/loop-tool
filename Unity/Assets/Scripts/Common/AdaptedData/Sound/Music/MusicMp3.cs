using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mp3;
using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicMp3 : MusicPcm
	{
		private float[][] sampleArray;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicMp3( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicMp3( Stream aStream )
			: this( new Mp3File( aStream ) )
		{

		}

		public MusicMp3( Mp3File aMp3File )
		{
			sampleArray = aMp3File.GetSampleArray();
			Channels = aMp3File.GetChannelLength();
			SampleLength = aMp3File.GetSampleLength();
			SampleRate = aMp3File.GetSampleRate();
			Loop = new List<List<LoopInformation>>();
			Loop.Add( new List<LoopInformation>() );
			Loop[0].Add( new LoopInformation( SampleRate, aMp3File.GetSampleLoopStart(), aMp3File.GetSampleLoopEnd() ) );
		}

		public float GetSample( int aChannel, int aPosition )
		{
			return sampleArray[aChannel][aPosition];
		}
	}
}
