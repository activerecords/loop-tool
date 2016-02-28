using System;

using Monoamp.Common.Data.Standard.Midi;

namespace Monoamp.Common.Component.Sound.Midi
{
	public abstract class MetaEventAffecterBase
	{
		protected MetaEventBase metaEvent;

		public MetaEventAffecterBase( MetaEventBase aMetaEvent )
		{
			metaEvent = aMetaEvent;
		}

		public abstract void Execute( MetaStatus aMetaStatus );

		public MetaEventBase GetMetaEvent()
		{
			return metaEvent;
		}

		public int GetDelta()
		{
			return metaEvent.GetDelta();
		}

		public byte GetCode()
		{
			return metaEvent.GetCode();
		}
	}
}
