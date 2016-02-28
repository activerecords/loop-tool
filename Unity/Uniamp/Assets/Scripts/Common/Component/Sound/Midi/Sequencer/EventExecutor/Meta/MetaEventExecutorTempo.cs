using System;

using Monoamp.Common.Data.Standard.Midi;

namespace Monoamp.Common.Component.Sound.Midi
{
	public class MetaEventAffecterTempo : MetaEventAffecterBase
	{
		public MetaEventAffecterTempo( MetaEventTempo aTempo )
			: base( aTempo )
		{

		}

		public override void Execute( MetaStatus aMetaStatus )
		{
			MetaEventTempo lTempoEvent = ( MetaEventTempo )metaEvent;

			aMetaStatus.SetTempo( lTempoEvent.GetTempo() );
		}
	}
}
