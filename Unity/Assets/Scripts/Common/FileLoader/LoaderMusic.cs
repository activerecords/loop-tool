using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.AdaptedData.Music;
using Curan.Common.FilePool;
using Curan.Common.FormalizedData.File.Form;
using Curan.Common.FormalizedData.File.Riff;
using Curan.Common.FormalizedData.File.Mp3;

namespace Curan.Common.FileLoader.Music
{
	public class LoaderMusic : LoaderBase
	{
		private static Dictionary<string, Constructor> constructorDictionary;

		static LoaderMusic()
		{
			constructorDictionary = new Dictionary<string, Constructor>();

			constructorDictionary.Add( ".aif", ( string aPathFile ) => { return new MusicAiff( ( FormFile )PoolCollection.poolAif.Get( aPathFile ) ); } );
			constructorDictionary.Add( ".wav", ( string aPathFile ) => { return new MusicWave( ( RiffFile )PoolCollection.poolWav.Get( aPathFile ) ); } );
			constructorDictionary.Add( ".mp3", ( string aPathFile ) => { return new MusicMp3( ( Mp3File )PoolCollection.poolMp3.Get( aPathFile ) ); } );
			constructorDictionary.Add( ".mid", ( string aPathFile  ) => { return new MusicMidi( aPathFile ); } );
			constructorDictionary.Add( ".xm", ( string aPathFile ) => { return new MusicXm( aPathFile ); } );
			constructorDictionary.Add( ".adx", ( string aPathFile ) => { return new MusicAdx( aPathFile ); } );
			constructorDictionary.Add( ".ahx", ( string aPathFile ) => { return new MusicAhx( aPathFile ); } );
			constructorDictionary.Add( ".ogg", ( string aPathFile ) => { return new MusicVorbis( aPathFile ); } );
			constructorDictionary.Add( ".nsf", ( string aPathFile ) => { return new MusicNsf( aPathFile ); } );
		}

		public static IMusic Load( string aPathFile )
		{
			return ( IMusic )Load( constructorDictionary, aPathFile );
		}
	}
}
