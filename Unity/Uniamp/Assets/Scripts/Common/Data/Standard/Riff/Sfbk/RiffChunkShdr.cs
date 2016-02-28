using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Riff.Sfbk
{
	public class RiffChunkShdr : RiffChunk
	{
		public const string ID = "shdr";

		public readonly ShdrData[] shdrDataArray;

		public RiffChunkShdr( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			shdrDataArray = new ShdrData[Size / 46];

			for( int i = 0; i * 46 < Size; i++ )
			{
				shdrDataArray[i] = new ShdrData( aByteArray, informationList );
			}
		}
	}

	public class ShdrData
	{
		public enum SampleType
		{
			monoSample = 1,
			rightSample = 2,
			leftSample = 4,
			linkedSample = 8,
			RomMonoSample = 0x8001,
			RomRightSample = 0x8002,
			RomLeftSample = 0x8004,
			RomLinkedSample = 0x8008
		};

		public string sampleName;
		public UInt32 start;
		public UInt32 end;
		public UInt32 startLoop;
		public UInt32 endLoop;
		public UInt32 sampleRate;
		public Byte originalPitch;
		public SByte pitchCorrection;
		public UInt16 sampleLink;
		public SampleType sampleType;

		public ShdrData( AByteArray aByteArray, List<string> aInformationList )
		{
			sampleName = aByteArray.ReadString( 20 );
			start = aByteArray.ReadUInt32();
			end = aByteArray.ReadUInt32();
			startLoop = aByteArray.ReadUInt32();
			endLoop = aByteArray.ReadUInt32();
			sampleRate = aByteArray.ReadUInt32();
			originalPitch = aByteArray.ReadByte();
			pitchCorrection = aByteArray.ReadSByte();
			sampleLink = aByteArray.ReadUInt16();
			sampleType = ( SampleType )aByteArray.ReadUInt16();

			aInformationList.Add( "Sample Name:" + sampleName );
			aInformationList.Add( "Start:" + start );
			aInformationList.Add( "End:" + end );
			aInformationList.Add( "Start Loop:" + startLoop );
			aInformationList.Add( "End Loop:" + endLoop );
			aInformationList.Add( "Sample Rate:" + sampleRate );
			aInformationList.Add( "Original Pitch:" + originalPitch );
			aInformationList.Add( "Pitch Correction:" + pitchCorrection );
			aInformationList.Add( "Sample Link:" + sampleLink );
			aInformationList.Add( "Sample Type:" + sampleType );

			if( sampleType != SampleType.monoSample )
			{
				Logger.Warning( sampleName + "/" + "Not Mono Sample:" + sampleType );
			}
		}
	}
}
