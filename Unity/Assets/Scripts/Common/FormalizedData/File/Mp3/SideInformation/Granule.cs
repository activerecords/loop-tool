using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public class Granule
	{
		private UInt16 part23Length;
		private UInt16 bigValues;
		private Byte globalGain;
		private Byte scalefactorCompress;
		private Byte windowSwitchingFlag;
		private Byte preFlag;
		private Byte scalefactorScale;
		private Byte count1TableSelect;
		private IWindow window;

		public Granule( BitArray bitArray )
		{
			part23Length =  bitArray.ReadBits16( 12 );
			bigValues = bitArray.ReadBits16( 9 );
			globalGain = bitArray.ReadBits8( 8 );
			scalefactorCompress = bitArray.ReadBits8( 4 );
			windowSwitchingFlag = bitArray.ReadBits8( 1 );

			Logger.LogNormal( "Part23Length:" + part23Length );
			Logger.LogNormal( "BigValues:" + bigValues );
			Logger.LogNormal( "GlobalGain:" + globalGain );
			Logger.LogNormal( "ScalefactorCompress:" + scalefactorCompress );
			Logger.LogNormal( "WindowSwitchingFlag:" + windowSwitchingFlag );

			if( windowSwitchingFlag == 0 )
			{
				window = new Window1( bitArray );
			}
			else
			{
				window = new Window2( bitArray );
			}

			preFlag = bitArray.ReadBits8( 1 );
			scalefactorScale = bitArray.ReadBits8( 1 );
			count1TableSelect = bitArray.ReadBits8( 1 );

			Logger.LogNormal( "PreFlag:" + preFlag );
			Logger.LogNormal( "ScalefactorScale:" + scalefactorScale );
			Logger.LogNormal( "Count1TableSelect:" + count1TableSelect );
		}
		
		public int GetGuranuleLength()
		{
			return ( int )part23Length;
		}

		public int GetScalefactorCompress()
		{
			return ( int )scalefactorCompress;
		}

		public IWindow GetWindow()
		{
			return window;
		}
	}
}
