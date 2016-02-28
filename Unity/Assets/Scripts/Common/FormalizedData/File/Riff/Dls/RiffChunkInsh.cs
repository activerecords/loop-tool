using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkInsh : RiffChunk
	{
		public const string ID = "insh";

		public readonly UInt32 regions;
		public readonly MidiLocal midiLocal;

		public RiffChunkInsh( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			regions = aByteArray.ReadUInt32();

			informationList.Add( "Regions:" + regions );

			midiLocal = new MidiLocal( aByteArray, informationList );
		}
	}

	public class MidiLocal
	{
		public readonly UInt32 bank;
		public readonly UInt32 instrument;

		public MidiLocal( ByteArray aByteArray, List<string> aInformationList )
		{
			bank = aByteArray.ReadUInt32();
			instrument = aByteArray.ReadUInt32();

			aInformationList.Add( "Bank:" + bank );
			aInformationList.Add( "Instrument:" + instrument );
		}
	}
}
