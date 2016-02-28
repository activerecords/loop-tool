using System;

using Curan.Common.FormalizedData.File.Midi;

namespace Curan.Common.ApplicationComponent.Sound.Midi
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
