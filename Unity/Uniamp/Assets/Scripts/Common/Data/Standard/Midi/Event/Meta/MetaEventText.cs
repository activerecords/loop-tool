using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MetaEventText : MetaEventBase
	{
		private string text;

		public MetaEventText( int aDelta, byte aType, AByteArray byteArray )
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
