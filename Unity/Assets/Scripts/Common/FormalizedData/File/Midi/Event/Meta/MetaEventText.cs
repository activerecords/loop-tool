using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MetaEventText : MetaEventBase
	{
		private string text;

		public MetaEventText( int aDelta, byte aType, ByteArray byteArray )
			: base( aDelta, aType )
		{
			int length = MtrkChunk.GetVariableLengthByte( byteArray );

			text = byteArray.ReadString( length );
		}

		public string GetText()
		{
			return text;
		}
	}
}
