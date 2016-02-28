using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Dls
{
	public class RiffChunkVers : RiffChunk
	{
		public const string ID = "vers";

		public readonly UInt32 versionMs;
		public readonly UInt32 versionLs;

		public RiffChunkVers( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			versionMs = aByteArray.ReadUInt32();
			versionLs = aByteArray.ReadUInt32();

			informationList.Add( "Version Ms:" + versionMs );
			informationList.Add( "Version Ls:" + versionLs );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			aByteArray.WriteUInt32( versionMs );
			aByteArray.WriteUInt32( versionLs );
		}
	}
}
