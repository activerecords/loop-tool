using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Adx;
using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicAdx : MusicPcm
	{
		private float[][] sampleArray;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicAdx( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicAdx( Stream aStream )
			: this( new AdxFile( aStream ) )
		{

		}

		public MusicAdx( AdxFile aAdxFile )
		{
			Channels = aAdxFile.GetAdxHeader().GetChannelLength();
			SampleLength = ( int )aAdxFile.GetAdxHeader().GetSampleLength();
			SampleRate = ( int )aAdxFile.GetAdxHeader().GetSampleRate();
			Loop[0][0] = new LoopInformation( SampleRate, ( int )aAdxFile.GetAdxHeader().GetSampleLoopStart(), ( int )aAdxFile.GetAdxHeader().GetSampleLoopEnd() );

			sampleArray = aAdxFile.GetSampleArray();
		}

		public float GetSample( int aChannel, int aPosition )
		{
			return sampleArray[aChannel][aPosition];
		}
	}
}
