using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Midi;
using Curan.Common.Struct;
using Curan.Utility;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicMidi : IMusic
	{
		public const string FILE_EXTENTION = ".mid";

		public readonly MtrkChunk[] mtrkChunkArray;

		public readonly int tracks;
		public readonly int division;
		public readonly int deltaMax;
		
		public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicMidi( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicMidi( Stream aStream )
			: this( new MidiFile( aStream ) )
		{

		}

		public MusicMidi( MidiFile aMidiFile )
		{
			mtrkChunkArray = aMidiFile.GetMtrkChunkArray();

			tracks = aMidiFile.GetMthdChunk().GetTracks();
			division = aMidiFile.GetMthdChunk().GetDivision();
			deltaMax = SearchDeltaMax( aMidiFile );
		}

		private int SearchDeltaMax( MidiFile aMidiFile )
		{
			int lDeltaMax = 0;

			for( int i = 0; i < tracks; i++ )
			{
				for( int j = 0; j < aMidiFile.GetMtrkChunkArray()[i].GetMidiEventList().Count; j++ )
				{
					int lDelta = aMidiFile.GetMtrkChunkArray()[i].GetMidiEventList()[j].GetDelta();

					if( lDelta > lDeltaMax )
					{
						lDeltaMax = lDelta;
					}
				}
			}

			return lDeltaMax;
		}
	}
}
