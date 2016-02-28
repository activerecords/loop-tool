using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdHeader
	{
		public readonly string magic;		// "Pmd"
		public readonly float version;		// 00 00 80 3F == 1.00
		public readonly string modelName;
		public readonly string comment;

		public PmdHeader( ByteArray aByteArray )
		{
			magic = aByteArray.ReadShiftJisString( 3 );
			version = aByteArray.ReadSingle();
			modelName = aByteArray.ReadShiftJisString( 20 );
			comment = aByteArray.ReadShiftJisString( 256 );

			Logger.LogDebug( "magic:" + magic );
			Logger.LogDebug( "version" + version );
			Logger.LogDebug( "modelName:" + modelName );
			Logger.LogDebug( "comment:" + comment );
		}
	}
}
