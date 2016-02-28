using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public interface ISideInformation
	{
		Granule GetGranule1Left();
		Granule GetGranule1Right();
		Granule GetGranule2Left();
		Granule GetGranule2Right();
		byte GetScalefactorSelectionInformation();
	}

	public class SideInformationStereo : ISideInformation
	{
		private UInt16 mainDataBegin;
		private Byte privateBits;
		private Byte scalefactorSelectionInformation;
		private Granule granule1Left;
		private Granule granule1Right;
		private Granule granule2Left;
		private Granule granule2Right;

		public SideInformationStereo( BitArray bitArray )
		{
			mainDataBegin = bitArray.ReadBits16( 9 );
			privateBits = bitArray.ReadBits8( 3 );
			scalefactorSelectionInformation = bitArray.ReadBits8( 8 );

			Logger.LogNormal( "MainDataBegin:" + mainDataBegin );
			Logger.LogNormal( "PrivateBits:" + privateBits );
			Logger.LogNormal( "ScalefactorSelectionInformation:" + scalefactorSelectionInformation );

			granule1Left = new Granule( bitArray );
			granule1Right = new Granule( bitArray );
			granule2Left = new Granule( bitArray );
			granule2Right = new Granule( bitArray );
		}

		public int GetGuranuleLength()
		{
			return granule1Left.GetGuranuleLength();
		}

		public Granule GetGranule1Left()
		{
			return granule1Left;
		}

		public Granule GetGranule1Right()
		{
			return granule1Right;
		}

		public Granule GetGranule2Left()
		{
			return granule2Left;
		}

		public Granule GetGranule2Right()
		{
			return granule2Right;
		}

		public byte GetScalefactorSelectionInformation()
		{
			return ( byte )scalefactorSelectionInformation;
		}
	}
}
