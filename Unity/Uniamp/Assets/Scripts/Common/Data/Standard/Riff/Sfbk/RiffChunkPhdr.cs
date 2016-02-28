using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkPhdr : RiffChunk
	{
		public const string ID = "phdr";

		public readonly PhdrData[] phdrDataArray;

		public RiffChunkPhdr( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			phdrDataArray = new PhdrData[Size / 38];

			for( int i = 0; i * 38 < Size; i++ )
			{
				phdrDataArray[i] = new PhdrData( aByteArray, informationList );
			}
		}

		/*
		public void Display( RiffBodyPbag pbagChunk, RiffBodyPgen pgenChunk, RiffBodyPmod pmodChunk, Dictionary<int, List<Instrument>> instrumentListDictionary )
		{
			UInt16 instrument = 0;

			for( int i = 1; i * 38 < Size; i++ )
			{
				if( i == 1 )
				{
					Logger.Normal( "■Preset Headers:" + i );
				}

				PbagData pbagData1 = pbagChunk.pbagDataArray[phdrDataArray[i - 1].bagNdx];
				PbagData pbagData2 = pbagChunk.pbagDataArray[phdrDataArray[i].bagNdx];

				pbagData1.gens = pbagData2.genNdx - pbagData1.genNdx;
				pbagData1.mods = pbagData2.modNdx - pbagData1.modNdx;

				for( int j = 0; j < pbagData1.gens; j++ )
				{
					if( i <= 3 )
					{
						Logger.Normal( "■pgenData:" + j );
					}

					PgenData pgenData = pgenChunk.pgenDataArray[pbagData1.genNdx + j];
					Generator generator = pgenData.GetData();

					switch( pgenData.GetGenOper() )
					{
					case SFGenerator.StartAddrsOffset:
						instrumentListDictionary[instrument].startAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrDataArray[i - 1].name + "/Start Addrs Offset:" + instrumentListDictionary[instrument].startAddrsOffset );
						break;

					case SFGenerator.EndAddrsOffset:
						instrumentListDictionary[instrument].endAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrDataArray[i - 1].name + "/End Addrs Offset:" + instrumentListDictionary[instrument].endAddrsOffset );
						break;

					case SFGenerator.StartLoopAddrsOffset:
						instrumentListDictionary[instrument].startLoopAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrDataArray[i - 1].name + "/Start Loop Addrs Offset:" + instrumentListDictionary[instrument].startLoopAddrsOffset );
						break;

					case SFGenerator.EndLoopAddrsOffset:
						instrumentListDictionary[instrument].endLoopAddrsOffset = generator.GetInt16();
						Logger.Normal( phdrDataArray[i - 1].name + "/End Loop Addrs Offset:" + instrumentListDictionary[instrument].endLoopAddrsOffset );
						break;

					case SFGenerator.StartAddrsCoarseOffset:
						instrumentListDictionary[instrument].startAddrsCoarseOffset = generator.GetInt16();
						Logger.Normal( phdrDataArray[i - 1].name + "/Start Addrs Coarse Offset:" + instrumentListDictionary[instrument].startAddrsCoarseOffset );
						break;
						
					case SFGenerator.ModEnvToPitch:
						instrumentListDictionary[instrument].modEnvToPitch = generator.GetInt16();
						//.Log.LogNormal( phdrDataArray[i - 1].name + "/Mod Env To Pitch:" + instrumentListDictionary[instrument].modEnvToPitch );
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
						instrumentListDictionary[instrument].lokey = keyRange.lo;
						instrumentListDictionary[instrument].hikey = keyRange.hi;

						//Log.LogWarning( "Key Range:" + lokey + "," + hikey );
						//Log.LogWarning( phdrDataArray[i - 1].name + ":" + "Key Range:" + lokey + "," + hikey );

						break;

					case SFGenerator.FineTune:
						instrumentListDictionary[instrument].fineTune = generator.GetInt16();
						//Log.LogWarning( "Fine Tune:" + instrumentListDictionary[instrument].fineTune );

						break;

					case SFGenerator.Instrument:
						instrument = generator.GetUInt16();

						if( instrumentListDictionary.ContainsKey( instrument ) == false )
						{
							instrumentListDictionary.Add( instrument, new List<Instrument>() );
						}

						phdrDataArray[i - 1].instrumentList.Add( instrument );
						instrumentListDictionary[instrument].Add( new Instrument( instrument ) );
						//Log.LogWarning( "Instrument:" + instrument );
						
						break;

					case SFGenerator.OverridingRootKey:
						instrumentListDictionary[instrument].rootKey = ( byte )generator.GetUInt16();

						Logger.Normal( "Root key:" + instrumentListDictionary[instrument].rootKey );

						break;

					default:
						//Log.LogWarning( pgenData.GetGenOper() + ":" + generator.GetUInt16() );
						break;
					}
				}

				for( int j = 0; j < pbagData1.mods; j++ )
				{
					PmodData pmodData = pmodChunk.pmodDataArray[( pbagData2.modNdx + j )  % pmodChunk.pmodDataArray.Length];
				}
			}
		}*/

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			for( int i = 0; i < phdrDataArray.Length; i++ )
			{
				phdrDataArray[i].WriteByteArray( aByteArray );
			}
		}
	}

	public class PhdrData
	{
		public readonly string name;
		public readonly UInt16 preset;
		public readonly UInt16 bank;
		public readonly UInt16 bagNdx;
		public readonly UInt32 library;
		public readonly UInt32 genre;
		public readonly UInt32 morphology;
		public readonly string endOfPresets;

		public readonly UInt16 gens;
		public readonly UInt16 mods;

		public readonly List<UInt16> instrumentList;

		public PhdrData( AByteArray aByteArray, List<string> aInformationList )
		{
			name = aByteArray.ReadString( 20 );
			preset = aByteArray.ReadUInt16();
			bank = aByteArray.ReadUInt16();
			bagNdx = aByteArray.ReadUInt16();
			library = aByteArray.ReadUInt32();
			genre = aByteArray.ReadUInt32();
			morphology = aByteArray.ReadUInt32();

			instrumentList = new List<UInt16>();

			aInformationList.Add( "Name:" + name );
			aInformationList.Add( "Preset:" + preset );
			aInformationList.Add( "Bank:" + bank );
			aInformationList.Add( "Bag Ndx:" + bagNdx );
			aInformationList.Add( "Library:" + library );
			aInformationList.Add( "Genre:" + genre );
			aInformationList.Add( "Morphology:" + morphology );
			aInformationList.Add( "End Of Presets:" + endOfPresets );
		}

		public void WriteByteArray( AByteArray aByteArray )
		{
			aByteArray.WriteString( name );
			aByteArray.WriteUInt16( preset );
			aByteArray.WriteUInt16( bank );
			aByteArray.WriteUInt16( bagNdx );
			aByteArray.WriteUInt32( library );
			aByteArray.WriteUInt32( genre );
			aByteArray.WriteUInt32( morphology );
		}
	}
}
