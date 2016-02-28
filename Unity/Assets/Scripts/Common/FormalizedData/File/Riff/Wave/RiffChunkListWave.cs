using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkListWave : RiffChunkList
	{
		public const string TYPE = "WAVE";

		public static readonly Dictionary<string,Type> bodyTypeDictionary;
		public static readonly Dictionary<string,Type> chunkTypeDictionary;

		public RiffChunkCue_ cue_Chunk;
		public RiffChunkData dataChunk;
		public RiffChunkFmt_ fmt_Chunk;
		public RiffChunkSmpl smplChunk;

		static RiffChunkListWave()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			chunkTypeDictionary.Add( RiffChunkBext.ID, typeof( RiffChunkBext ) );
			chunkTypeDictionary.Add( RiffChunkCue_.ID, typeof( RiffChunkCue_ ) );
			chunkTypeDictionary.Add( RiffChunkData.ID, typeof( RiffChunkData ) );
			chunkTypeDictionary.Add( RiffChunkDisp.ID, typeof( RiffChunkDisp ) );
			chunkTypeDictionary.Add( RiffChunkFact.ID, typeof( RiffChunkFact ) );
			chunkTypeDictionary.Add( RiffChunkFile.ID, typeof( RiffChunkFile ) );
			chunkTypeDictionary.Add( RiffChunkFmt_.ID, typeof( RiffChunkFmt_ ) );
			chunkTypeDictionary.Add( RiffChunkInst.ID, typeof( RiffChunkInst ) );
			chunkTypeDictionary.Add( RiffChunkLabl.ID, typeof( RiffChunkLabl ) );
			chunkTypeDictionary.Add( RiffChunkLgwv.ID, typeof( RiffChunkLgwv ) );
			chunkTypeDictionary.Add( RiffChunkLtxt.ID, typeof( RiffChunkLtxt ) );
			chunkTypeDictionary.Add( RiffChunkNote.ID, typeof( RiffChunkNote ) );
			chunkTypeDictionary.Add( RiffChunkPlst.ID, typeof( RiffChunkPlst ) );
			chunkTypeDictionary.Add( RiffChunkSmpl.ID, typeof( RiffChunkSmpl ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
			//chunkTypeDictionary.Add( RiffChunkList.ID, typeof( RiffChunkList ) );
			bodyTypeDictionary.Add( RiffChunkListAdtl.TYPE, typeof( RiffChunkListAdtl ) );
			bodyTypeDictionary.Add( RiffChunkListInfo.TYPE, typeof( RiffChunkListInfo ) );
		}

		public RiffChunkListWave( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( chunkTypeDictionary, bodyTypeDictionary, aId, aSize, aByteArray, aParent )
		{
			type = TYPE;

			cue_Chunk = ( RiffChunkCue_ )GetChunk( RiffChunkCue_.ID );
			dataChunk = ( RiffChunkData )GetChunk( RiffChunkData.ID );
			fmt_Chunk = ( RiffChunkFmt_ )GetChunk( RiffChunkFmt_.ID );
			smplChunk = ( RiffChunkSmpl )GetChunk( RiffChunkSmpl.ID );
		}

		public void AddCuePoint( int aStart, int aEnd )
		{
			if( cue_Chunk == null )
			{
				List<CuePoint> lCuePointList = new List<CuePoint>();

				lCuePointList.Add( new CuePoint( 1, ( UInt32 )aStart, "data", 0, 0, ( UInt32 )aStart ) );
				lCuePointList.Add( new CuePoint( 2, ( UInt32 )aEnd, "data", 0, 0, ( UInt32 )aEnd ) );

				RiffChunkCue_ lCue_Body = new RiffChunkCue_( lCuePointList );

				AddChunk( lCue_Body );
				cue_Chunk = lCue_Body;
			}
			else
			{
				List<CuePoint> lCuePointList = cue_Chunk.cuePoints;

				lCuePointList.Add( new CuePoint( cue_Chunk.points + 1, ( UInt32 )aStart, "data", 0, 0, ( UInt32 )aStart ) );
				lCuePointList.Add( new CuePoint( cue_Chunk.points + 2, ( UInt32 )aEnd, "data", 0, 0, ( UInt32 )aEnd ) );

				RiffChunkCue_ lCue_Body = new RiffChunkCue_( lCuePointList );
				OverrideChunk( lCue_Body );
			}
		}

		public void AddSampleLoop( int aStart, int aEnd )
		{
			if( smplChunk == null )
			{
				List<SampleLoop> lSampleLoopList = new List<SampleLoop>();

				lSampleLoopList.Add( new SampleLoop( 0, 0, ( UInt32 )aStart, ( UInt32 )aEnd, 0, 0 ) );

				RiffChunkSmpl lChunkSmpl = new RiffChunkSmpl( 0, 0, 0, 60, 0, 0, 0, 1, 0, lSampleLoopList );
				AddChunk( lChunkSmpl );
				smplChunk = lChunkSmpl;
			}
			else
			{
				List<SampleLoop> lSmplLoopList = smplChunk.sampleLoopList;

				lSmplLoopList.Add( new SampleLoop( 0, 0, ( UInt32 )aStart, ( UInt32 )aEnd, 0, 0 ) );

				OverrideChunk( new RiffChunkSmpl( smplChunk, lSmplLoopList ) );
			}
		}

		public void SetDataArray( Byte[] aSampleData )
		{
			if( dataChunk == null )
			{
				// Error.
			}
			else
			{
				MemoryStream lMemoryStream = new MemoryStream( aSampleData );
				ByteArray lByteArray = new ByteArrayLittle( lMemoryStream );
				lByteArray.WriteBytes( new byte[dataChunk.position] );
				OverrideChunk( new RiffChunkData( RiffChunkData.ID, ( UInt32 )aSampleData.Length, lByteArray, this ) );
			}
		}
	}
}
