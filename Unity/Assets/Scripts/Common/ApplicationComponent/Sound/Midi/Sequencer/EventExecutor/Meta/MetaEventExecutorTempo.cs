using System;

using Curan.Common.FormalizedData.File.Midi;

namespace Curan.Common.ApplicationComponent.Sound.Midi
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
