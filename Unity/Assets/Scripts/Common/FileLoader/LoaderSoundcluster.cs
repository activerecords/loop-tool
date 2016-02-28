using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.AdaptedData;
using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Blst;
using Curan.Common.FormalizedData.File.Riff;

namespace Curan.Common.FileLoader.Soundcluster
{
	public static class LoaderSoundcluster
	{
		public static SoundclusterBase Load( string aPath )
		{
			SoundclusterBase lBank = null;

			string lExtension = Path.GetExtension( aPath ).ToLower();

			FileStream lFileStream = new FileStream( aPath, FileMode.Open, FileAccess.Read );

			switch( lExtension )
			{
			case ".txt":
				lBank = new SoundclusterSfz( new BlstFile( lFileStream ) );
				break;

			case ".dls":
				lBank = new SoundclusterDls( new RiffFile( lFileStream ) );
				break;

			case ".sf2":
				lBank = new SoundclusterSfbk( new RiffFile( lFileStream ) );
				break;

			default:
				break;

			}

			return lBank;
		}
	}
}
