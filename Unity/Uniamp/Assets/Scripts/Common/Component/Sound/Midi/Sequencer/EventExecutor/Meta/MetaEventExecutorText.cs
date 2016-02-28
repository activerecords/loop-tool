using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Sound.Midi
{
	public class MetaEventAffecterText : MetaEventAffecterBase
	{
		private int deltaStart;

		public MetaEventAffecterText( MetaEventText aText, int aDeltaStart )
			: base( aText )
		{
			deltaStart = aDeltaStart;
		}

		public override void Execute( MetaStatus aMetaStatus )
		{
			MetaEventText lTextEvent = ( MetaEventText )metaEvent;

			aMetaStatus.SetDelta( deltaStart );

			Logger.Warning( "Marker:" + lTextEvent.GetText() );
		}
	}
}
