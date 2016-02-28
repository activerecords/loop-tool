using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Ahx;
using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicAhx : MusicPcm
	{
		private float[][] sampleArray;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicAhx( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicAhx( Stream aStream )
			: this( new AhxHeader( aStream ) )
		{

		}

		public MusicAhx( AhxHeader aAhxHeader )
		{
			Channels = aAhxHeader.GetChannelLength();
			SampleLength = ( int )aAhxHeader.GetSampleLength();
			SampleRate = ( int )aAhxHeader.GetSampleRate();
			Loop = new List<List<LoopInformation>>();
			Loop.Add( new List<LoopInformation>() );
			Loop[0].Add( new LoopInformation( SampleRate, ( int )aAhxHeader.GetSampleLoopStart(), ( int )aAhxHeader.GetSampleLoopEnd() ) );

			sampleArray = aAhxHeader.GetSampleArray();
		}

		public float GetSample( int aChannel, int aPosition )
		{
			return sampleArray[aChannel][aPosition];
		}
	}
}
