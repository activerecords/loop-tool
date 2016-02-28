using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Sound.Midi;
using Monoamp.Common.Struct;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Music
{
	public class MusicMidi : IMusic
	{
		public const string FILE_EXTENTION = ".mid";
		
		public string Name{ get; private set; }
		public SoundTime Length{ get; private set; }
		public LoopInformation Loop{ get; set; }

		public readonly MtrkChunk[] mtrkChunkArray;

		public readonly int tracks;
		public readonly int division;
		public readonly int deltaMax;
		
		public List<List<LoopInformation>> loopList{ get; private set; }
		
		public int GetCountLoopX()
		{
			return 0;
		}
		
		public int GetCountLoopY( int aIndexX )
		{
			return 0;
		}
		
		public LoopInformation GetLoop( int aIndexX, int aIndexY )
		{
			return new LoopInformation( 44100, -1, -1 );
		}

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
