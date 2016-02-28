using System;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Riff;
using Monoamp.Common.Data.Standard.Riff.Sfbk;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class InstrumentSfbk : InstrumentBase
	{
		public override SoundfontBase[] soundfontArray{ get; set; }

		public InstrumentSfbk( RiffChunkListPdta pdtaList, RiffChunkListSdta[] sdtaBodyList, InstData instData, IbagData ibagData0, IbagData ibagData1, string aName )
			: base()
		{
			RiffInfoIbag ibagChunk = pdtaList.ibagBody;
			RiffInfoIgen igenChunk = pdtaList.igenBody;
			RiffInfoImod imodChunk = pdtaList.imodBody;

			ShdrData[] shdrDataArray = pdtaList.shdrBody.shdrDataArray;

			byte lokey = 0xFF;
			byte hikey = 0xFF;
			byte rootkey = 0;
			int fineTune = 0;
			bool loopMode = false;
			int startAddrsOffset = 0;
			int endAddrsOffset = 0;
			int startLoopAddrsOffset = 0;
			int endLoopAddrsOffset = 0;
			int startAddrsCoarseOffset = 0;
			int modEnvToPitch = 0;

			int gens = igenChunk.igenDataArray.Length - ibagData0.genNdx;
			int mods = imodChunk.imodDataArray.Length - ibagData0.modNdx;

			if( ibagData1 != null ) {
				gens = ibagData1.genNdx - ibagData0.genNdx;
				mods = ibagData1.modNdx - ibagData0.modNdx;
			}

			for( int j = 0; j < gens; j++ ) {
				IgenData igenData = igenChunk.igenDataArray[ibagData0.genNdx + j];
				Generator generator = igenData.amount;

				switch( igenData.genOper ) {
				case SFGenerator.StartAddrsOffset:
					startAddrsOffset = generator.GetInt16();
					Logger.Normal( instData.name + "/Start Addrs Offset:" + startAddrsOffset );
					break;

				case SFGenerator.EndAddrsOffset:
					endAddrsOffset = generator.GetInt16();
					Logger.Normal( instData.name + "/End Addrs Offset:" + endAddrsOffset );
					break;

				case SFGenerator.StartLoopAddrsOffset:
					startLoopAddrsOffset = generator.GetInt16();
					Logger.Normal( instData.name + "/Start Loop Addrs Offset:" + startLoopAddrsOffset );
					break;

				case SFGenerator.EndLoopAddrsOffset:
					endLoopAddrsOffset = generator.GetInt16();
					Logger.Normal( instData.name + "/End Loop Addrs Offset:" + endLoopAddrsOffset );
					break;

				case SFGenerator.StartAddrsCoarseOffset:
					startAddrsCoarseOffset = generator.GetInt16();
					Logger.Normal( "Start Addrs Coarse Offset:" + startAddrsCoarseOffset );
					break;

				case SFGenerator.ModLfoToPitch:
							//startAddrsCoarseOffset = generator.GetUInt16();
							//Log( "Start Addrs Coarse Offset:" + startAddrsCoarseOffset );
					break;

				case SFGenerator.VibLfoToPitch:
							//startAddrsCoarseOffset = generator.GetUInt16();
							//Log( "Start Addrs Coarse Offset:" + startAddrsCoarseOffset );
					break;

				case SFGenerator.ModEnvToPitch:
					modEnvToPitch = generator.GetInt16();
					Logger.Normal( instData.name + "/Mod Env To Pitch:" + modEnvToPitch );
					break;

				case SFGenerator.DelayModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Delay Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.AttackModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Attack Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.HoldModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Hold Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.DecayModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Decay Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.SustainModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Sustain Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.ReleaseModEnv:
							//modEnvToPitch = generator.GetInt16();
					Logger.Normal( "Release Mod Env:" + generator.GetInt16() );
					break;

				case SFGenerator.DelayVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Delay Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.AttackVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Attack Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.HoldVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Hold Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.DecayVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Decay Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.SustainVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Sustain Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.ReleaseVolEnv:
							//VolEnvToPitch = generator.GetInt16();
					Logger.Normal( "Release Vol Env:" + generator.GetInt16() );
					break;

				case SFGenerator.KeyRange:
					RangesType keyRange = generator.GetRangesType();
					lokey = keyRange.lo;
					hikey = keyRange.hi;

					Logger.Normal( "Key Range:" + lokey + "," + hikey );

					break;

				case SFGenerator.SampleId:
					UInt16 sampleId = generator.GetUInt16();

					AddSoundfont( new SoundfontSfbk( sdtaBodyList, shdrDataArray[sampleId], lokey, hikey, rootkey, fineTune, loopMode, startAddrsOffset, endAddrsOffset, startLoopAddrsOffset, endLoopAddrsOffset, modEnvToPitch, new Instrument(), aName ) );

					Logger.Normal( instData.name + "Sample Id:" + sampleId );

					lokey = 0xFF;
					hikey = 0xFF;
					rootkey = 0;
					fineTune = 0;
					loopMode = false;
					modEnvToPitch = 0;
					startAddrsOffset = 0;
					endAddrsOffset = 0;
					startLoopAddrsOffset = 0;
					endLoopAddrsOffset = 0;
					startAddrsCoarseOffset = 0;

					break;

				case SFGenerator.FineTune:
					fineTune = generator.GetInt16();

					Logger.Normal( "Fine Tune:" + fineTune );

					break;

				case SFGenerator.SampleModes:
					if( generator.GetUInt16() == 0 ) {
						loopMode = false;
					}
					else if( generator.GetUInt16() == 1 ) {
							loopMode = true;
						}
						else {
							loopMode = false;
						}

					Logger.Normal( "Sample Modes:" + loopMode );

					break;

				case SFGenerator.Instrument:
					Console.WriteLine( "Error" );
					break;

				case SFGenerator.OverridingRootKey:
					rootkey = ( byte )generator.GetUInt16();

					Logger.Normal( "Root key:" + rootkey );

					break;

				case SFGenerator.VelRange:
					RangesType VelRange = generator.GetRangesType();

					Logger.Normal( "    Vel Range:" + VelRange.lo + "," + VelRange.hi );
					break;

				default:
					Logger.Warning( "    " + igenData.genOper + ":" + generator.GetUInt16() );

					break;
				}
			}

			for( int j = 0; j < mods; j++ ) {
				ImodData imodData = imodChunk.imodDataArray[ibagData0.modNdx + j];
			}
		}

		public static Dictionary<string, Instrument> CreateInstrumentList( RiffChunkListPdta pdtaList )
		{
			Dictionary<string, Instrument> lInstrumentDictionary = new Dictionary<string, Instrument>();

			RiffChunkPhdr phdrChunk = pdtaList.phdrBody;
			RiffChunkPbag pbagChunk = pdtaList.pbagBody;
			RiffChunkPgen pgenChunk = pdtaList.pgenBody;
			RiffChunkPmod pmodChunk = pdtaList.pmodBody;

			for( int i = 0; i < phdrChunk.phdrDataArray.Length; i++ )
			{
				Instrument lInstrument = new Instrument();

				if( lInstrumentDictionary.ContainsKey( i.ToString() ) == false ) {
					lInstrumentDictionary.Add( i.ToString(), lInstrument );
				}
				else {
					Console.WriteLine( "Error" );
				}

				PbagData pbagData0 = pbagChunk.pbagDataArray[phdrChunk.phdrDataArray[i].bagNdx];

				int gens = pbagChunk.pbagDataArray.Length - pbagData0.genNdx;
				int mods = pmodChunk.pmodDataArray.Length - pbagData0.modNdx;

				if( i < phdrChunk.phdrDataArray.Length - 1 ) {
					PbagData pbagData1 = pbagChunk.pbagDataArray[phdrChunk.phdrDataArray[i + 1].bagNdx];

					gens = pbagData1.genNdx - pbagData0.genNdx;
					mods = pbagData1.modNdx - pbagData0.modNdx;
				}

				for( int j = 0; j < gens; j++ )
				{
					PgenData pgenData = pgenChunk.pgenDataArray[pbagData0.genNdx + j];
					Generator generator = pgenData.amount;

					switch( pgenData.genOper )
					{
					case SFGenerator.StartAddrsOffset:
						lInstrument.startAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/Start Addrs Offset:" + lInstrument.startAddrsOffset );
						break;

					case SFGenerator.EndAddrsOffset:
						lInstrument.endAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/End Addrs Offset:" + lInstrument.endAddrsOffset );
						break;

					case SFGenerator.StartLoopAddrsOffset:
						lInstrument.startLoopAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/Start Loop Addrs Offset:" + lInstrument.startLoopAddrsOffset );
						break;

					case SFGenerator.EndLoopAddrsOffset:
						lInstrument.endLoopAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/End Loop Addrs Offset:" + lInstrument.endLoopAddrsOffset );
						break;

					case SFGenerator.StartAddrsCoarseOffset:
						lInstrument.startAddrsCoarseOffset = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/Start Addrs Coarse Offset:" + lInstrument.startAddrsCoarseOffset );
						break;

					case SFGenerator.ModEnvToPitch:
						lInstrument.modEnvToPitch = generator.GetInt16();
						Logger.Normal( phdrChunk.phdrDataArray[i].name + "/Mod Env To Pitch:" + lInstrument.startAddrsCoarseOffset );
						break;

					case SFGenerator.DelayModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Delay Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.AttackModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Attack Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.HoldModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Hold Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.DecayModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Decay Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.SustainModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Sustain Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.ReleaseModEnv:
						//modEnvToPitch = generator.GetInt16();
						Logger.Normal( "Release Mod Env:" + generator.GetInt16() );
						break;

					case SFGenerator.DelayVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Delay Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.AttackVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Attack Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.HoldVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Hold Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.DecayVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Decay Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.SustainVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Sustain Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.ReleaseVolEnv:
						//VolEnvToPitch = generator.GetInt16();
						Logger.Normal( "Release Vol Env:" + generator.GetInt16() );
						break;

					case SFGenerator.KeyRange:
						RangesType keyRange = generator.GetRangesType();
						lInstrument.lokey = keyRange.lo;
						lInstrument.hikey = keyRange.hi;

						Logger.Warning( "Key Range:" + lInstrument.lokey + "," + lInstrument.hikey );
						Logger.Warning( phdrChunk.phdrDataArray[i].name + ":" + "Key Range:" + lInstrument.lokey + "," + lInstrument.hikey );
						break;

					case SFGenerator.FineTune:
						lInstrument.fineTune = generator.GetInt16();
						Logger.Warning( "Fine Tune:" + lInstrument.fineTune );

						break;

					case SFGenerator.Instrument:
						UInt16 instrument = generator.GetUInt16 ();

						Logger.Normal ("Number:" + i);
						phdrChunk.phdrDataArray [i].instrumentList.Add (instrument);
						lInstrumentDictionary [i.ToString ()].instrument = instrument;
						Logger.Warning ("Instrument:" + instrument);

						//instrumentDictionary[instrument]
						break;

					case SFGenerator.OverridingRootKey:
						lInstrument.rootKey = ( byte )generator.GetUInt16();
						Logger.Normal( "Root key:" + lInstrument.rootKey );

						break;

					default:
						//Log.LogWarning( pgenData.GetGenOper() + ":" + generator.GetUInt16() );
						break;
					}
				}

				for( int j = 0; j < mods; j++ )
				{
					PmodData pmodData = pmodChunk.pmodDataArray[( pbagData0.modNdx + j )  % pmodChunk.pmodDataArray.Length];
				}
			}

			return lInstrumentDictionary;
		}
	}
}
