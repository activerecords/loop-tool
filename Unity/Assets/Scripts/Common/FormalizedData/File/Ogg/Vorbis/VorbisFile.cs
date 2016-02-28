using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class VorbisFile
	{
		private List<VorbisPacket> vorbisPacketList;
		private VorbisHeader vorbisHeader;
		private ByteArray byteArray;

		private float[][] bufferArray;

		private int sampleLength;

		public VorbisFile( Stream aStream )
		{
			vorbisHeader = new VorbisHeader();

			byteArray = new ByteArrayLittle( aStream );

			vorbisPacketList = new List<VorbisPacket>();

		}

		public float[][] GetSampleArray()
		{
			int lIndex = 0;

			vorbisPacketList.Add( new VorbisPacket( byteArray, vorbisHeader, new Byte[0] ) );

			while( byteArray.Position < byteArray.Length )
			{
				vorbisPacketList.Add( new VorbisPacket( byteArray, vorbisHeader, vorbisPacketList[lIndex].nextSegmentArray ) );

				lIndex++;
			}

			int I = 0;

			while( vorbisHeader.identification == null )
			{
				for( int J = 0; J < vorbisPacketList[I].sizeSegmentList.Count; J++ )
				{
					vorbisPacketList[I].vorbisSegmentArray[J].Read( null );
				}

				I++;
			}

			sampleLength = ( int )vorbisPacketList[lIndex - 1].oggPageHeader.granulePosition;

			int lSamplePosition = 0;
			int lLeftWindowStartCurrent = 0;
			int lLeftWindowEndCurrent = 1024;
			int lRightWindowStartCurrent = 1024;
			int lRightWindowEndCurrent = 2048;

			bufferArray = new float[vorbisHeader.identification.audioChannels][];

			for( int i = 0; i < vorbisHeader.identification.audioChannels; i++ )
			{
				bufferArray[i] = new float[sampleLength];
			}

			double[][] bufferArrayArrayPrevious = new double[vorbisHeader.identification.audioChannels][];
			double[][] bufferArrayArrayCurrent = new double[vorbisHeader.identification.audioChannels][];

			for( ; I < vorbisPacketList.Count; I++ )
			{
				for( int J = 0; J < vorbisPacketList[I].sizeSegmentList.Count; J++ )
				{
					vorbisPacketList[I].vorbisSegmentArray[J].Read( bufferArrayArrayCurrent );

					if( vorbisPacketList[I].vorbisSegmentArray[J].window != null && vorbisPacketList[I].vorbisSegmentArray[J].window.samples != 0 )
					{
						int lRightWindowStartPre = lRightWindowStartCurrent;
						int lRightWindowEndPre = lRightWindowEndCurrent;

						lLeftWindowStartCurrent = vorbisPacketList[I].vorbisSegmentArray[J].window.left_window_start;
						lLeftWindowEndCurrent = vorbisPacketList[I].vorbisSegmentArray[J].window.left_window_end;
						lRightWindowStartCurrent = vorbisPacketList[I].vorbisSegmentArray[J].window.right_window_start;
						lRightWindowEndCurrent = vorbisPacketList[I].vorbisSegmentArray[J].window.right_window_end;

						if( lRightWindowEndPre - lRightWindowStartPre != lLeftWindowEndCurrent - lLeftWindowStartCurrent )
						{
							Logger.LogDebug( "                                Not Equal:" );
						}

						for( int i = 0; i < vorbisHeader.identification.audioChannels; i++ )
						{
							if( lRightWindowEndPre - lRightWindowStartPre == lLeftWindowEndCurrent - lLeftWindowStartCurrent )
							{
								for( int j = lLeftWindowStartCurrent; j < lLeftWindowEndCurrent && lSamplePosition + j - lLeftWindowStartCurrent < sampleLength; j++ )
								{
									bufferArray[i][lSamplePosition + j - lLeftWindowStartCurrent] = ( float )( bufferArrayArrayPrevious[i][lRightWindowStartPre + j - lLeftWindowStartCurrent] + bufferArrayArrayCurrent[i][j] );
								}
							}

							for( int j = lLeftWindowEndCurrent; j < lRightWindowStartCurrent && lSamplePosition + j - lLeftWindowStartCurrent < sampleLength; j++ )
							{
								bufferArray[i][lSamplePosition + j - lLeftWindowStartCurrent] = ( float )bufferArrayArrayCurrent[i][j];
							}
						}

						lSamplePosition += lRightWindowStartCurrent - lLeftWindowStartCurrent;
						Logger.LogDebug( "samples:" + lSamplePosition );

						bufferArrayArrayPrevious[0] = bufferArrayArrayCurrent[0];
						bufferArrayArrayPrevious[1] = bufferArrayArrayCurrent[1];
					}
				}
			}

			return bufferArray;
		}

		public int GetChannelLength()
		{
			return ( int )vorbisHeader.identification.audioChannels;
		}

		public int GetSampleLength()
		{
			return sampleLength;
		}

		public int GetSampleRate()
		{
			return ( int )vorbisHeader.identification.audioSampleRate;
		}

		public int GetSampleLoopStart()
		{
			return vorbisHeader.GetSampleLoopStart();
		}

		public int GetSampleLoopEnd()
		{
			return vorbisHeader.GetSampleLoopEnd();
		}
	}
}
