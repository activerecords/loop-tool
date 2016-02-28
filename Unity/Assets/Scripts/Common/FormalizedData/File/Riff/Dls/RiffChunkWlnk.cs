using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkWlnk : RiffChunk
	{
		public const string ID = "wlnk";

		public readonly UInt16 options;
		public readonly UInt16 phaseGroup;
		public readonly UInt32 channel;
		public readonly UInt32 tableIndex;

		public RiffChunkWlnk( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
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
