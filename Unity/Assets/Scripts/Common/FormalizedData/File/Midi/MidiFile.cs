using System;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiFile
	{
		private MthdChunk mthdChunk;
		private MtrkChunk[] mtrkChunkArray;

		public MidiFile( Stream aStream )
		{
			ByteArray byteArray = new ByteArrayBig( aStream );

			ReadMidiHeader( byteArray );
			ReadMidiTrack( byteArray );
		}

		// MIDIヘッダを読み込む.
		private void ReadMidiHeader( ByteArray byteArray )
		{
			string lId = byteArray.ReadString( 4 );
			UInt32 lSize = byteArray.ReadUInt32();

			mthdChunk = new MthdChunk( lId, ( int )lSize );
			mthdChunk.Read( byteArray );
		}

		// MIDIトラックを読み込む.
		private void ReadMidiTrack( ByteArray byteArray )
		{
			mtrkChunkArray = new MtrkChunk[mthdChunk.GetTracks()];

			for( int i = 0; i < mthdChunk.GetTracks(); i++ )
			{
				string lId = byteArray.ReadString( 4 );
				UInt32 lSize = byteArray.ReadUInt32();

				mtrkChunkArray[i] = new MtrkChunk( lId, ( int )lSize );
				mtrkChunkArray[i].Read( byteArray );
			}
		}

		public MthdChunk GetMthdChunk()
		{
			return mthdChunk;
		}

		public MtrkChunk[] GetMtrkChunkArray()
		{
			return mtrkChunkArray;
		}
	}
}
