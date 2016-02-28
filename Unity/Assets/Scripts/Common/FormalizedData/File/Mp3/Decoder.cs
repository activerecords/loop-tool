using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public class Mp3Decoder
	{
		private BitArray bitArray;
		private FrameHeader frameHeader;
		private ISideInformation sideInformation;
		private Scalefactor scalefactor1Left;
		private Scalefactor scalefactor1Right;
		private Scalefactor scalefactor2Left;
		private Scalefactor scalefactor2Right;
		
		private int[,] scalefactorBandIndexLongBlock =
		{
			{0, 4, 8, 12, 16, 20, 24, 30, 36, 44, 52, 62, 74, 90, 110, 134, 162, 196, 238, 288, 342, 418, 576},
			{0, 4, 8, 12, 16, 20, 24, 30, 36, 42, 50, 60, 72, 88, 106, 128, 156, 190, 230, 276, 330, 384, 576},
			{0, 4, 8, 12, 16, 20, 24, 30, 36, 44, 54, 66, 82, 102, 126 , 156, 194, 240, 296, 364, 448, 550, 576}
		};

		private int[,] scalefactorBandIndexShortBlock =
		{
			{0, 4, 8, 12, 16, 22, 30, 40, 52, 66, 84, 106, 136, 192},
			{0, 4, 8, 12, 16, 22, 28, 38, 50, 64, 80, 100, 126, 192},
			{0, 4, 8, 12, 16, 22, 30, 42, 58, 78, 104, 138, 180, 192}
		};

		public Mp3Decoder( byte[] data )
		{
			bitArray = new BitArrayLittle( data );

			frameHeader = new FrameHeader( bitArray );

			if( frameHeader.GetChannels() == 2 )
			{
				sideInformation = new SideInformationStereo( bitArray );
			}
			else
			{
				Logger.LogError( "Monoral" );
			}

			scalefactor1Left = new Scalefactor( bitArray, sideInformation.GetGranule1Left().GetScalefactorCompress() );
			scalefactor1Right = new Scalefactor( bitArray, sideInformation.GetGranule1Right().GetScalefactorCompress() );

			scalefactor2Left = scalefactor1Left;
			scalefactor2Right = scalefactor1Right;

			int region1Start = scalefactorBandIndexLongBlock[0, sideInformation.GetGranule1Left().GetWindow().GetRegion0Count() + 1];
			int region2Start = scalefactorBandIndexLongBlock[0, sideInformation.GetGranule1Left().GetWindow().GetRegion0Count() + sideInformation.GetGranule1Left().GetWindow().GetRegion1Count() + 2];

			Logger.LogNormal( "Start1:" + region1Start + "," + "Start2:" + region2Start );
		}

		public int GetGuranuleLength()
		{
			return sideInformation.GetGranule1Left().GetGuranuleLength();
		}
	}
}
