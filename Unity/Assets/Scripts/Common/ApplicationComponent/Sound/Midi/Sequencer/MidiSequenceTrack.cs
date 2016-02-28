using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiSequenceTrack
	{
		private Dictionary<int, List<MetaEventAffecterBase>> metaEventAffecterListDictionary;
		private Dictionary<int, List<MidiEventExecutorBase>> midiEventAffecterListDictionary;

		public MidiSequenceTrack( MtrkChunk aMtrkChunkArray )
		{
			metaEventAffecterListDictionary = ReadMetaTrack( aMtrkChunkArray.GetMetaEventList() );
			midiEventAffecterListDictionary = ReadMidiTrack( aMtrkChunkArray.GetMidiEventList() );
		}

		private Dictionary<int, List<MetaEventAffecterBase>> ReadMetaTrack( List<MetaEventBase> aMetaEventList )
		{
			Dictionary<int, List<MetaEventAffecterBase>> lListDictionary = new Dictionary<int, List<MetaEventAffecterBase>>();

			for( int i = 0; i < aMetaEventList.Count; i++ )
			{
				MetaEventBase lMetaEvent = aMetaEventList[i];
				MetaEventAffecterBase lEventAffecter = null;

				if( lListDictionary.ContainsKey( lMetaEvent.GetDelta() ) == false )
				{
					lListDictionary.Add( lMetaEvent.GetDelta(), new List<MetaEventAffecterBase>() );
				}

				switch( lMetaEvent.GetCode() )
				{
				case 0x06:
					MetaEventText lTextEvent = ( MetaEventText )lMetaEvent;

					if( lTextEvent.GetText() == "LoopEnd" )
					{
						for( int j = 0; j < aMetaEventList.Count && j < i; j++ )
						{
							if( aMetaEventList[j].GetCode() == 0x06 )
							{
								MetaEventText lTextEventStart = ( MetaEventText )aMetaEventList[j];

								if( lTextEventStart.GetText() == "LoopStart" )
								{
									lEventAffecter = new MetaEventAffecterText( lTextEvent, lTextEventStart.GetDelta() );
								}
							}
						}
					}
					break;

				case 0x51:
					lEventAffecter = new MetaEventAffecterTempo( ( MetaEventTempo )lMetaEvent );
					break;

				case 0x58:
					lEventAffecter = new MetaEventAffecterTimeSignature( ( MetaEventTimeSignature )lMetaEvent );
					break;

				default:
					//Logger.LogWarning( "Null" );
					break;
				}

				lListDictionary[lMetaEvent.GetDelta()].Add( lEventAffecter );
			}

			return lListDictionary;
		}

		private Dictionary<int, List<MidiEventExecutorBase>> ReadMidiTrack( List<MidiEventBase> aMidiEventList )
		{
			Dictionary<int, List<MidiEventExecutorBase>> lListDictionary = new Dictionary<int, List<MidiEventExecutorBase>>();

			for( int i = 0; i < aMidiEventList.Count; i++ )
			{
				MidiEventBase lMidiEvent = aMidiEventList[i];
				MidiEventExecutorBase lEventAffecter = null;

				if( lListDictionary.ContainsKey( lMidiEvent.GetDelta() ) == false )
				{
					lListDictionary.Add( lMidiEvent.GetDelta(), new List<MidiEventExecutorBase>() );
				}

				switch( lMidiEvent.GetType().ToString() )
				{
				case "Curan.Common.FormalizedData.File.Midi.MidiEventNoteOff":
					lEventAffecter = new MidiEventExecutorNoteOff( ( MidiEventNoteOff )lMidiEvent );
					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventNoteOn":
					byte lVelocity = lMidiEvent.GetData2();

					if( lVelocity != 0 )
					{
						int lDelta = lMidiEvent.GetDelta();
						byte lChannel = lMidiEvent.GetChannel();
						byte lNote = lMidiEvent.GetData1();

						// オートオン情報を取得する.
						for( int j = i + 1; j < aMidiEventList.Count; j++ )
						{
							MidiEventBase lMidiEventNext = aMidiEventList[j];

							int lDeltaNext = lMidiEventNext.GetDelta();
							byte lStateNext = lMidiEventNext.GetState();
							byte lChannelNext = lMidiEventNext.GetChannel();
							byte lNoteNext = lMidiEventNext.GetData1();
							byte lVelocityNext = lMidiEventNext.GetData2();

							if( lDeltaNext != lDelta )
							{
								// 次にノートオフされるまでの長さを取得する.
								// ノートオフまたはVelocity0のノートオンメッセージの時.
								if( lChannelNext == lChannel && lNoteNext == lNote && ( lStateNext == 0x80 || ( lStateNext == 0x90 && lVelocityNext == 0 ) ) )
								{
									int lLength = lDeltaNext - lDelta;
									lEventAffecter = new MidiEventExecutorNoteOn( ( MidiEventNoteOn )lMidiEvent, lLength );
									break;
								}
							}
						}
					}

					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventControlChange":
					lEventAffecter = new MidiEventExecutorControlChange( ( MidiEventControlChange )lMidiEvent );
					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventProgramChange":
					lEventAffecter = new MidiEventExecutorProgramChange( ( MidiEventProgramChange )lMidiEvent );
					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventKeyPressure":
					lEventAffecter = new MidiEventExecutorKeyPressure( ( MidiEventKeyPressure )lMidiEvent );
					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventPitchWheelChange":
					lEventAffecter = new MidiEventExecutorPitchWheelChange( ( MidiEventPitchWheelChange )lMidiEvent );
					break;

				case "Curan.Common.FormalizedData.File.Midi.MidiEventSystemExclusive":
					lEventAffecter = new MidiEventExecutorSystemExclusive( ( MidiEventSystemExclusive )lMidiEvent );
					break;

				default:
					Logger.LogWarning( "Null" );
					break;
				}

				lListDictionary[lMidiEvent.GetDelta()].Add( lEventAffecter );
			}

			return lListDictionary;
		}

		public void ExecuteMetaEventOneDelta( MetaStatus aMetaStatus )
		{
			int lDelta = aMetaStatus.GetDelta();

			if( metaEventAffecterListDictionary.ContainsKey( lDelta ) )
			{
				for( int i = 0; i < metaEventAffecterListDictionary[lDelta].Count; i++ )
				{
					if( metaEventAffecterListDictionary[lDelta][i] != null )
					{
						metaEventAffecterListDictionary[lDelta][i].Execute( aMetaStatus );
					}
				}
			}
		}

		public void ExecuteMidiEventOneDelta( MetaStatus aMetaStatus, MidiSynthesizer aMidiSynthesizer, int aDivision )
		{
			int lDelta = aMetaStatus.GetDelta();

			if( midiEventAffecterListDictionary.ContainsKey( lDelta ) )
			{
				for( int i = 0; i < midiEventAffecterListDictionary[lDelta].Count; i++ )
				{
					midiEventAffecterListDictionary[lDelta][i].Execute( aMidiSynthesizer, aDivision, aMetaStatus.GetBpm() );
				}
			}
		}

		public void ExecuteMetaEventSeek( MetaStatus aMetaStatus, int aDelta )
		{
			for( int i = 0; i < aDelta; i++ )
			{
				if( metaEventAffecterListDictionary.ContainsKey( i ) )
				{
					for( int j = 0; j < metaEventAffecterListDictionary[i].Count; j++ )
					{
						metaEventAffecterListDictionary[i][j].Execute( aMetaStatus );
					}
				}
			}
		}

		public void ExecuteMidiEventSeek( MetaStatus aMetaStatus, MidiSynthesizer aMidiSynthesizer, int aDivision, int aDelta )
		{
			for( int i = 0; i < aDelta; i++ )
			{
				if( midiEventAffecterListDictionary.ContainsKey( i ) )
				{
					for( int j = 0; j < midiEventAffecterListDictionary[i].Count; j++ )
					{
						if( midiEventAffecterListDictionary[i][j].GetType().ToString() != "LayerMiddle.Sound.Controller.Midi.Affecter.MidiEventAffecterNoteOn" )
						{
							midiEventAffecterListDictionary[i][j].Execute( aMidiSynthesizer, aDivision, aMetaStatus.GetBpm() );
						}
					}
				}
			}
		}

		public List<MetaEventAffecterBase> GetMetaEventAffecterListArray( int aDelta )
		{
			if( metaEventAffecterListDictionary.ContainsKey( aDelta ) )
			{
				return metaEventAffecterListDictionary[aDelta];
			}
			else
			{
				return null;
			}
		}

		public List<MidiEventExecutorBase> GetMidiEventAffecterListArray( int aDelta )
		{
			if( midiEventAffecterListDictionary.ContainsKey( aDelta ) )
			{
				return midiEventAffecterListDictionary[aDelta];
			}
			else
			{
				return null;
			}
		}
	}
}
