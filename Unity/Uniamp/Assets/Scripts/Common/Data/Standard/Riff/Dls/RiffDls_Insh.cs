using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Insh : RiffChunk
	{
		public const string ID = "insh";

		public readonly UInt32 regions;
		public readonly MidiLocal midiLocal;

		public RiffDls_Insh( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
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

		public MidiLocal( AByteArray aByteArray, List<string> aInformationList )
		{
			bank = aByteArray.ReadUInt32();
			instrument = aByteArray.ReadUInt32();

			aInformationList.Add( "Bank:" + bank );
			aInformationList.Add( "Instrument:" + instrument );
		}
	}
}
