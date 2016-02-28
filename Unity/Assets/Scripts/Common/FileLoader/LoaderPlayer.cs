using System;
using System.IO;

using Curan.Common.AdaptedData.Music;
using Curan.Common.ApplicationComponent.Sound;
using Curan.Common.ApplicationComponent.Sound.Pcm;
using Curan.Common.ApplicationComponent.Sound.Xm;
using Curan.Common.ApplicationComponent.Sound.Midi;
using Curan.Common.ApplicationComponent.Sound.Nsf;
using Curan.Utility;

namespace Curan.Common.FileLoader.Music
{
	public static class PlayerLoader
	{
		public static IPlayer Load( string aFilePath )
		{
			IPlayer lPlayer = null;

			string lFileExtension = Path.GetExtension( aFilePath ).ToLower();

			switch( lFileExtension )
			{
			case ".aif":
				lPlayer = new PlayerPcm( aFilePath );
				break;

			case ".wav":
				lPlayer = new PlayerPcm( aFilePath );
				break;

			case ".ogg":
				lPlayer = new PlayerPcm( aFilePath );
				break;
				
			case ".mp3":
				lPlayer = new PlayerPcm( aFilePath );
				break;

			case ".adx":
				lPlayer = new PlayerPcm( aFilePath );
				break;

			case ".ahx":
				lPlayer = new PlayerPcm( aFilePath );
				break;

			case ".mid":
				lPlayer = new PlayerMidi( aFilePath );
				break;

			case ".nsf":
				lPlayer = new PlayerNsf( aFilePath );
				break;

			case ".xm":
				lPlayer = new PlayerXm( aFilePath );
				break;
			
			default:
				//lPlayer = new PlayerNull();
				break;
			}

			return lPlayer;
		}
	}
}
