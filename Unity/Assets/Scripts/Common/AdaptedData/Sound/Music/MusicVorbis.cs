using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Ogg.Vorbis;

using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicVorbis : MusicPcm
	{
		private float[][] sampleArray;
        
        public int Channels{ get; private set; }
        public int SampleLength{ get; private set; }
        public int SampleRate{ get; private set; }
        public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicVorbis( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicVorbis( Stream aStream )
			: this( new VorbisFile( aStream ) )
		{

		}

		public MusicVorbis( VorbisFile aVorbisFile )
		{
			sampleArray = aVorbisFile.GetSampleArray();
			Channels = aVorbisFile.GetChannelLength();
			SampleLength = aVorbisFile.GetSampleLength();
			SampleRate = aVorbisFile.GetSampleRate();
			Loop = new List<List<LoopInformation>>();
			Loop.Add( new List<LoopInformation>() );
			Loop[0].Add( new LoopInformation( SampleRate, aVorbisFile.GetSampleLoopStart(), aVorbisFile.GetSampleLoopEnd() ) );
		}

		public float GetSample( int aChannel, int aPosition )
		{
			return sampleArray[aChannel][aPosition];
		}
	}
}
