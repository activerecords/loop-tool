using System;
using System.Collections.Generic;
using System.IO;

using Curan.Common.FormalizedData.File.Form;
using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Mp3;

namespace Curan.Common.FilePool
{
	public static class PoolCollection
	{
		public static readonly Pool poolWav;
		public static readonly Pool poolAif;
        public static readonly Pool poolMp3;
        public static readonly Pool poolTexture;

		static PoolCollection()
		{
			poolWav = new Pool( ( FileStream aFileStream ) => { return new RiffFile( aFileStream ); } );
			poolAif = new Pool( ( FileStream aFileStream ) => { return new FormFile( aFileStream ); } );
			poolMp3 = new Pool( ( FileStream aFileStream ) => { return new Mp3File( aFileStream ); } );
            poolTexture = new Pool( ( FileStream aFileStream ) => { return new FormFile( aFileStream ); } );
		}
	}
}
