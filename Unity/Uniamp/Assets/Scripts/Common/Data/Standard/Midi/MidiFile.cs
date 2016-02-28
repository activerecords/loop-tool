using System;
using System.IO;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MidiFile
	{
		private MthdChunk mthdChunk;
		private MtrkChunk[] mtrkChunkArray;

		public MidiFile( Stream aStream )
		{
			AByteArray byteArray = new ByteArrayBig( aStream );

			ReadMidiHeader( byteArray );
			ReadMidiTrack( byteArray );
		}

		// MIDIヘッダを読み込む.
		private void ReadMidiHeader( AByteArray byteArray )
		{
			string lId = byteArray.ReadString( 4 );
			UInt32 lSize = byteArray.ReadUInt32();

			mthdChunk = new MthdChunk( lId, ( int )lSize );
			mthdChunk.Read( byteArray );
		}

		// MIDIトラックを読み込む.
		private void ReadMidiTrack( AByteArray byteArray )
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
