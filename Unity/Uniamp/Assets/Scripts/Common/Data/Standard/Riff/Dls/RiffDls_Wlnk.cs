using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Wlnk : RiffChunk
	{
		public const string ID = "wlnk";

		public readonly UInt16 options;
		public readonly UInt16 phaseGroup;
		public readonly UInt32 channel;
		public readonly UInt32 tableIndex;

		public RiffDls_Wlnk( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			options = aByteArray.ReadUInt16();
			phaseGroup = aByteArray.ReadUInt16();
			channel = aByteArray.ReadUInt32();
			tableIndex = aByteArray.ReadUInt32();

			informationList.Add( "Options:" + options );
			informationList.Add( "Phase Group:" + phaseGroup );
			informationList.Add( "Channel:" + channel );
			informationList.Add( "Table Index:" + tableIndex );
		}
	}
}
