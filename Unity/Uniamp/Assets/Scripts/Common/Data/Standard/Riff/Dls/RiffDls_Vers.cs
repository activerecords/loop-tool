using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Vers : RiffChunk
	{
		public const string ID = "vers";

		public readonly UInt32 versionMs;
		public readonly UInt32 versionLs;

		public RiffDls_Vers( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			versionMs = aByteArray.ReadUInt32();
			versionLs = aByteArray.ReadUInt32();

			informationList.Add( "Version Ms:" + versionMs );
			informationList.Add( "Version Ls:" + versionLs );
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( versionMs );
			aByteArray.WriteUInt32( versionLs );
		}
	}
}
