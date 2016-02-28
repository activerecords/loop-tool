using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public class Scalefactor : ICloneable
	{
		private int[] slen1 =
		{
			0, 0, 0, 0, 3, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4
		};

		private int[] slen2 =
		{
			0, 1, 2, 3, 0, 1, 2, 3, 1, 2, 3, 1, 2, 3, 2, 3
		};

		private Byte[] scalefactor;

		private Scalefactor()
		{

		}

		public Scalefactor( BitArray bitArray, int scalefactorCompress )
		{
			scalefactor = new Byte[21];

			for( int i = 0; i < 11; i++ )
			{
				scalefactor[i] = bitArray.ReadBits8( slen1[scalefactorCompress] );
			}

			for( int i = 11; i < 21; i++ )
			{
				scalefactor[i] = bitArray.ReadBits8( slen2[scalefactorCompress] );
			}
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

	}
}
