using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Sfbk
{
	public class RiffChunkPgen : RiffChunk
	{
		public const string ID = "pgen";

		public readonly PgenData[] pgenDataArray;

		public RiffChunkPgen( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			pgenDataArray = new PgenData[size / 4];

			for( int i = 0; i * 4 < size; i++ )
			{
				pgenDataArray[i] = new PgenData( aByteArray, informationList );
			}
		}
	}

	public class PgenData
	{
		public readonly SFGenerator genOper;
		public readonly Generator amount;

		public PgenData( ByteArray aByteArray, List<string> aInformationList )
		{
			genOper = ( SFGenerator )aByteArray.ReadUInt16();
			amount = new Generator( aByteArray );

			aInformationList.Add( "Gen Oper:" + genOper );
			aInformationList.Add( "Amount:" + amount );

			/*
			switch( genOper )
			{
			case SFGenerator.StartAddrsOffset:
				UInt16 startAddrsOffset = amount.GetUInt16();
				Logger.LogNormal( "Start Addrs Offset:" + startAddrsOffset );
				break;

			case SFGenerator.EndAddrsOffset:
				UInt16 endAddrsOffset = amount.GetUInt16();
				Logger.LogNormal( "End Addrs Offset:" + endAddrsOffset );
				break;

			case SFGenerator.StartLoopAddrsOffset:
				UInt16 startLoopAddrsOffset = amount.GetUInt16();
				Logger.LogNormal( "Start Loop Addrs Offset:" + startLoopAddrsOffset );
				break;

			case SFGenerator.EndLoopAddrsOffset:
				UInt16 endLoopAddrsOffset = amount.GetUInt16();
				Logger.LogNormal( "End Loop Addrs Offset:" + endLoopAddrsOffset );
				break;

			case SFGenerator.StartAddrsCoarseOffset:
				UInt16 startAddrsCoarseOffset = amount.GetUInt16();
				Logger.LogNormal( "Start Addrs Coarse Offset:" + startAddrsCoarseOffset );
				break;

			case SFGenerator.ModLfoToPitch:
				break;

			case SFGenerator.VibLfoToPitch:
				break;

			case SFGenerator.ModEnvToPitch:
				break;

			case SFGenerator.InitialFilterFc:
				break;

			case SFGenerator.InitialFilterQ:
				break;

			case SFGenerator.ModLfoToFilterFc:
				break;

			case SFGenerator.ModEnvToFilterFc:
				break;

			case SFGenerator.EndAddrsCoarseOffset:
				break;

			case SFGenerator.ModLfoToVolume:
				break;

			case SFGenerator.Unused1:
				break;

			case SFGenerator.ChorusEffectsSend:
				break;

			case SFGenerator.ReverbEffectsSend:
				break;

			case SFGenerator.Pan:
				Int16 pan = amount.GetInt16();
				Logger.LogNormal( "Pan:" + pan );
				break;

			case SFGenerator.Unused2:
				break;

			case SFGenerator.Unused3:
				break;

			case SFGenerator.Unused4:
				break;

			case SFGenerator.DelayModLfo:
				break;

			case SFGenerator.FreqModLfo:
				break;

			case SFGenerator.DelayVibLfo:
				break;

			case SFGenerator.FreqVibLfo:
				break;

			case SFGenerator.DelayModEnv:
				break;

			case SFGenerator.AttackModEnv:
				break;

			case SFGenerator.HoldModEnv:
				break;

			case SFGenerator.DecayModEnv:
				break;

			case SFGenerator.SustainModEnv:
				break;

			case SFGenerator.ReleaseModEnv:
				break;

			case SFGenerator.KeyNumToModEnvHold:
				break;

			case SFGenerator.KeyNumToModEnvDecay:
				break;

			case SFGenerator.DelayVolEnv:
				break;

			case SFGenerator.AttackVolEnv:
				break;

			case SFGenerator.HoldVolEnv:
				break;

			case SFGenerator.DecayVolEnv:
				break;

			case SFGenerator.SustainVolEnv:
				break;

			case SFGenerator.ReleaseVolEnv:
				break;

			case SFGenerator.KeynumToVolEnvHold:
				break;

			case SFGenerator.KeynumToVolEnvDecay:
				break;

			case SFGenerator.Instrument:
				UInt16 instrument = amount.GetUInt16();
				Logger.LogNormal( "Instrument:" + instrument );
				break;

			case SFGenerator.Reserved1:
				break;

			case SFGenerator.KeyRange:
				RangesType keyRange = amount.GetRangesType();
				Logger.LogNormal( "Ranges Type Lo:" + keyRange.lo );
				Logger.LogNormal( "Ranges Type Hi:" + keyRange.hi );
				break;

			case SFGenerator.VelRange:
				RangesType velRange = amount.GetRangesType();
				Logger.LogNormal( "Ranges Type Lo:" + velRange.lo );
				Logger.LogNormal( "Ranges Type Hi:" + velRange.hi );
				break;

			case SFGenerator.StartLoopAddrsCoarseOffset:
				break;

			case SFGenerator.KeyNum:
				UInt16 keyNum = amount.GetUInt16();
				Logger.LogNormal( "Key Num:" + keyNum );
				break;

			case SFGenerator.Velocity:
				UInt16 velocity = amount.GetUInt16();
				Logger.LogNormal( "Velocity:" + velocity );
				break;

			case SFGenerator.InitialAttenuation:
				break;

			case SFGenerator.Reserved2:
				break;

			case SFGenerator.EndLoopAddrsCoarseOffset:
				break;

			case SFGenerator.CoarseTune:
				Int16 coarseTune = amount.GetInt16();
				Logger.LogNormal( "Coarse Tune:" + coarseTune );
				break;

			case SFGenerator.FineTune:
				Int16 fineTune = amount.GetInt16();
				Logger.LogNormal( "Fine Tune:" + fineTune );
				break;

			case SFGenerator.SampleId:
				UInt16 sampleId = amount.GetUInt16();
				Logger.LogNormal( "Sample Id:" + sampleId );
				break;

			case SFGenerator.SampleModes:
				UInt16 sampleModes = amount.GetUInt16();
				Logger.LogNormal( "Sample Modes:" + sampleModes );
				break;

			case SFGenerator.Reserved3:
				break;

			case SFGenerator.ScaleTuning:
				Int16 scaleTuning = amount.GetInt16();
				Logger.LogNormal( "Scale Tuning:" + scaleTuning );
				break;

			case SFGenerator.ExclusiveClass:
				break;

			case SFGenerator.OverridingRootKey:
				break;

			case SFGenerator.Unused5:
				break;

			case SFGenerator.EndOper:
				break;

			default:
				UInt16 def = amount.GetUInt16();
				Logger.LogNormal( genOper + ":" + def );
				break;
			}
			*/
		}
	}
}
