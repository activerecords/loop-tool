using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Form.Aiff;
using Monoamp.Common.Data.Standard.Riff.Wave;
using Monoamp.Common.Data.Standard.Riff.Dls;
using Monoamp.Common.Data.Standard.Riff.Sfbk;
using Monoamp.Common.Data.Standard.Blst;
using Monoamp.Common.Data.Application.Music;
using Monoamp.Common.Data.Application.Sound;
using Monoamp.Common.Component.Sound.Player;

namespace Monoamp.Common.Utility
{
	public static class ConstructorCollection
	{
		private static readonly Constructor constructorMusic;
		private static readonly Constructor constructorWaveform;
		private static readonly Constructor constructorPlayer;
		private static readonly Constructor constructorSoundcluster;

		static ConstructorCollection()
		{
			Dictionary<string, Constructor.DConstructor> lConstructorDictionaryMusic = new Dictionary<string, Constructor.DConstructor>();
			lConstructorDictionaryMusic.Add( ".aif", ( string aPathFile ) => { return new MusicAiff( aPathFile ); } );
			lConstructorDictionaryMusic.Add( ".aiff", ( string aPathFile ) => { return new MusicAiff( aPathFile ); } );
			lConstructorDictionaryMusic.Add( ".wav", ( string aPathFile ) => { return new MusicWave( aPathFile ); } );
			lConstructorDictionaryMusic.Add( ".wave", ( string aPathFile ) => { return new MusicWave( aPathFile ); } );
			lConstructorDictionaryMusic.Add( ".mid", ( string aPathFile ) => { return new MusicMidi( aPathFile ); } );
			lConstructorDictionaryMusic.Add( ".midi", ( string aPathFile ) => { return new MusicMidi( aPathFile ); } );
			constructorMusic = new Constructor( lConstructorDictionaryMusic );

			Dictionary<string, Constructor.DConstructor> lConstructorDictionaryWaveform = new Dictionary<string, Constructor.DConstructor>();
			lConstructorDictionaryWaveform.Add( ".aif", ( string aPathFile ) => { return new WaveformReaderPcm( new FormAiffForm( aPathFile ), false ); } );
			lConstructorDictionaryWaveform.Add( ".aiff", ( string aPathFile ) => { return new WaveformReaderPcm( new FormAiffForm( aPathFile ), false ); } );
			lConstructorDictionaryWaveform.Add( ".wav", ( string aPathFile ) => { return new WaveformReaderPcm( new RiffWaveRiff( aPathFile ), true ); } );
			lConstructorDictionaryWaveform.Add( ".wave", ( string aPathFile ) => { return new WaveformReaderPcm( new RiffWaveRiff( aPathFile ), true ); } );
			constructorWaveform = new Constructor( lConstructorDictionaryWaveform );
			
			Dictionary<string, Constructor.DConstructor> lConstructorDictionaryPlayer = new Dictionary<string, Constructor.DConstructor>();
			lConstructorDictionaryPlayer.Add( ".aif", ( string aPathFile ) => { return new PlayerPcm( aPathFile ); } );
			lConstructorDictionaryPlayer.Add( ".aiff", ( string aPathFile ) => { return new PlayerPcm( aPathFile ); } );
			lConstructorDictionaryPlayer.Add( ".wav", ( string aPathFile ) => { return new PlayerPcm( aPathFile ); } );
			lConstructorDictionaryPlayer.Add( ".wave", ( string aPathFile ) => { return new PlayerPcm( aPathFile ); } );
			lConstructorDictionaryPlayer.Add( ".mid", ( string aPathFile ) => { return new PlayerMidi( aPathFile ); } );
			lConstructorDictionaryPlayer.Add( ".midi", ( string aPathFile ) => { return new PlayerMidi( aPathFile ); } );
			constructorPlayer = new Constructor( lConstructorDictionaryPlayer );
			
			Dictionary<string, Constructor.DConstructor> lConstructorDictonarySoundcluster = new Dictionary<string, Constructor.DConstructor>();
			lConstructorDictonarySoundcluster.Add( ".dls", ( string aPathFile ) => { return new SoundclusterDls( new RiffDls_Riff( aPathFile ) ); } );
			lConstructorDictonarySoundcluster.Add( ".sf2", ( string aPathFile ) => { return new SoundclusterSfbk( new RiffChunkListSfbk( aPathFile ) ); } );
			lConstructorDictonarySoundcluster.Add( ".blst", ( string aPathFile ) => { return new SoundclusterSfz( new BlstFile( aPathFile ) ); } );
			constructorSoundcluster = new Constructor( lConstructorDictonarySoundcluster );
		}
		
		public static IMusic ConstructMusic( string aPathFile )
		{
			return ( IMusic )constructorMusic.Construct( aPathFile );
		}

		public static WaveformReaderPcm ConstructWaveform( string aPathFile )
		{
			return ( WaveformReaderPcm )constructorWaveform.Construct( aPathFile );
		}

		public static IPlayer ConstructPlayer( string aPathFile )
		{
			return ( IPlayer )constructorPlayer.Construct( aPathFile );
		}
		
		public static ISoundcluster ConstructSoundcluster( string aPathFile )
		{
			return ( ISoundcluster )constructorSoundcluster.Construct( aPathFile );
		}
	}
}
