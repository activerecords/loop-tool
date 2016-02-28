using System;

namespace Monoamp.Common.Data.Standard.Midi
{
	public class MetaEventBase
	{
		private int delta;
		private byte code;

		public MetaEventBase( int aDelta, byte aType )
		{
			delta = aDelta;
			code = aType;
		}

		public int GetDelta()
		{
			return delta;
		}

		public byte GetCode()
		{
			return code;
		}
	}
}
