﻿using System;

using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public static class NesApu
	{
		private const Byte NOTE_NO_A4 = 69;				// ??????????'??'??????MIDI?m?[?g???????????????B
		private const UInt32 FREQUENCY_A4 = 442;		// ??????????'??'?????????g?????????????B?idouble???????A???????????|??????UInt32?????`?j

		private const UInt32 CPU_FREQUENCY = 1790000;	// CPU???N???b?N???g??

		private const Byte SHIFT_SQUARE = 5;			// ???`?g?????g?????v?Z?????????E?V?t?g?????l
		private const Byte SHIFT_TRYANGLE = 6;			// ?O?p?g?????Z?????v?Z?????????E?V?t?g?????l

		private static int[] notePre = { 60, 60, 60};

		public static void Update( NesState aNesState, MidiSynthesizer aSynthesizer )
		//public static void Update( NesState aNesState, CoreSynthesizer[] aSynthesizer )
		{
			aNesState.apuRegister.REG4002 = ( UInt16 )( aNesState.memory.GetDataWord( 0x4002 ) & 0x07FF );
			aNesState.apuRegister.REG4006 = ( UInt16 )( aNesState.memory.GetDataWord( 0x4006 ) & 0x07FF );
			aNesState.apuRegister.REG400a = ( UInt16 )( aNesState.memory.GetDataWord( 0x400a ) & 0x07FF );

			// ???`?g1????????
			if( aNesState.memory.IsWrite( 0x4002 ) == true || aNesState.memory.IsWrite( 0x4003 ) == true )
			{
				StartCannelSquare( aSynthesizer, aNesState.apuRegister.REG4002, 0x00 );
			}

			// ???`?g2????????
			if( aNesState.memory.IsWrite( 0x4006 ) == true || aNesState.memory.IsWrite( 0x4007 ) == true )
			{
				StartCannelSquare( aSynthesizer, aNesState.apuRegister.REG4006, 0x01 );
			}

			// ?O?p?g????????
			if( aNesState.memory.IsWrite( 0x400a ) == true || aNesState.memory.IsWrite( 0x400b ) == true )
			{
				StartCannelTryangle( aSynthesizer, aNesState.apuRegister.REG400a, 0x02 );
			}
		}

		// ???g???????m?[?g???????v?Z????
		private static Byte ConvertFrequencyToNoteNo( double frequency )
		{
			return ( Byte )( Math.Log10( frequency / ( double )FREQUENCY_A4 ) * 12 / Math.Log10( 2.0f ) + NOTE_NO_A4 );
		}

		// ???`?g?`?????l????MIDI??0???i?f?t?H???g???s?A?m?j????????
		public static void StartCannelSquare( MidiSynthesizer aSynthesizer, UInt32 cycle, Byte channel )
		//public static void StartCannelSquare( CoreSynthesizer[] aSynthesizer, UInt32 cycle, Byte channel )
		{
			Byte lVelocity = 0x70;	// ?????F0x70
			double lFrequency = ( CPU_FREQUENCY / ( cycle + 1 ) ) >> SHIFT_SQUARE;	// ???????????g?????v?Z
			Byte lNoteNo = ( Byte )( ConvertFrequencyToNoteNo( lFrequency ) + 12 );	// ???g???????m?[?g???????v?Z

			//Logger.LogWarning( "Square:" + lNoteNo );

			//aSynthesizer[channel].StopPlayNoteNumber( notePre[channel], channel );
			//notePre[channel] = lNoteNo;
			//aSynthesizer[channel].StartPlayNoteNumber( lNoteNo, channel );
			aSynthesizer.NoteOn( channel, lNoteNo, lVelocity );
		}

		// ?O?p?g?`?????l????MIDI??0???i?f?t?H???g???s?A?m?j????????
		public static void StartCannelTryangle( MidiSynthesizer aSynthesizer, UInt32 cycle, Byte channel )
		//public static void StartCannelTryangle( CoreSynthesizer[] aSynthesizer, UInt32 cycle, Byte channel )
		{
			Byte lVelocity = 0x70;	// ?????F0x70
			double lFrequency = ( CPU_FREQUENCY / ( cycle + 1 ) ) >> SHIFT_TRYANGLE;	// ???????????g?????v?Z
			Byte lNoteNo = ( Byte )( ConvertFrequencyToNoteNo( lFrequency ) + 12 );	// ???g???????m?[?g???????v?Z

			//Logger.LogWarning( "Tryangle:" + lNoteNo );

			//aSynthesizer[channel].StopPlayNoteNumber( notePre[channel], channel );
			//notePre[channel] = lNoteNo;
			//aSynthesizer[channel].StartPlayNoteNumber( lNoteNo, channel );
			aSynthesizer.NoteOn( channel, lNoteNo, lVelocity );
		}
	}
}