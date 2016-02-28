using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.Struct;
using Curan.Common.FormalizedData.File.Xm;

namespace Curan.Common.AdaptedData.Music
{
	public class MusicXm : IMusic
	{
		private XmFile xmFile;

		public Byte[][][] note;
		public Byte[][][] instrument;
		
		public List<List<LoopInformation>> Loop{ get; private set; }

		public MusicXm( string aPathFile )
			: this( new FileStream( aPathFile, FileMode.Open, FileAccess.Read ) )
		{

		}

		public MusicXm( Stream aStream )
			: this( new XmFile( aStream ) )
		{

		}

		public MusicXm( XmFile aXmFile )
		{
			xmFile = aXmFile;
			
			note = new Byte[xmFile.GetNumberOfChannels()][][];
			instrument = new Byte[xmFile.GetNumberOfChannels()][][];
			
			for( int i = 0; i < xmFile.GetNumberOfChannels(); i++ )
			{
				note[i] = new Byte[xmFile.GetNumberOfPatterns()][];
				instrument[i] = new Byte[xmFile.GetNumberOfPatterns()][];

				for( int j = 0; j < xmFile.GetNumberOfPatterns(); j++ )
				{
					int lLength = xmFile.GetPatternChunkArray()[j].GetNumberOfRowsInPattern();
					note[i][j] = new Byte[lLength];
					instrument[i][j] = new Byte[lLength];

					for( int k = 0; k < lLength; k++ )
					{
						note[i][j][k] = xmFile.GetPatternChunkArray()[j].GetNote()[k * xmFile.GetNumberOfChannels() + i];
						instrument[i][j][k] = xmFile.GetPatternChunkArray()[j].GetInstrument()[k * xmFile.GetNumberOfChannels() + i];
					}
				}
			}
		}

		public UInt16 GetSongLength()
		{
			return xmFile.GetSongLength();
		}

		public UInt16 GetRestartPosition()
		{
			return xmFile.GetRestartPosition();
		}

		public UInt16 GetNumberOfChannels()
		{
			return xmFile.GetNumberOfChannels();
		}

		public UInt16 GetNumberOfPatterns()
		{
			return xmFile.GetNumberOfPatterns();
		}

		public UInt16 GetNumberOfInstruments()
		{
			return xmFile.GetNumberOfInstruments();
		}

		public UInt16 GetFlags()
		{
			return xmFile.GetFlags();
		}

		public UInt16 GetDefaultTempo()
		{
			return xmFile.GetDefaultTempo();
		}

		public UInt16 GetDefaultBpm()
		{
			return xmFile.GetDefaultBpm();
		}

		public Byte[] GetPatternOrderTable()
		{
			return xmFile.GetPatternOrderTable();
		}

		public PatternChunk[] GetPatternChunkArray()
		{
			return xmFile.GetPatternChunkArray();
		}

		public InstrumentChunk[] GetInstrumentChunkArray()
		{
			return xmFile.GetInstrumentChunkArray();
		}
	}
}
