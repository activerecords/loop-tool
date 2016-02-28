using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdHeader
	{
		public string vmdHeader;	// "Vocaloid Motion Data 0002"
		public string modelName;

		public VmdHeader( ByteArray aByteArray )
		{
			vmdHeader = aByteArray.ReadShiftJisString( 30 );
			modelName = aByteArray.ReadShiftJisString( 20 );

			Logger.LogNormal( "VmdHeader:" + vmdHeader );
			Logger.LogNormal( "modelName:" + modelName );
		}
	}
}
