using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Rgnh : RiffChunk
	{
		public const string ID = "rgnh";

		public readonly RgnRange rangeKey;
		public readonly RgnRange rangeVelocity;
		public readonly UInt16 options;
		public readonly UInt16 keyGroup;

		public RiffDls_Rgnh( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			informationList.Add( "Range Key:" );
			rangeKey = new RgnRange( aByteArray, informationList );

			informationList.Add( "Range Velocity:" );
			rangeVelocity = new RgnRange( aByteArray, informationList );

			options = aByteArray.ReadUInt16();
			keyGroup = aByteArray.ReadUInt16();

			informationList.Add( "Options:" + options );
			informationList.Add( "Key Group:" + keyGroup );
		}
	}

	public class RgnRange
	{
		public readonly UInt16 low;
		public readonly UInt16 high;

		public RgnRange( AByteArray aByteArray, List<string> aInformationList )
		{
			low = aByteArray.ReadUInt16();
			high = aByteArray.ReadUInt16();

			aInformationList.Add( "\tLow:" + low );
			aInformationList.Add( "\tHigh:" + high );
		}
	}
}
