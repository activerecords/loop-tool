using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
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

			Logger.LogWarning( "Marker:" + lTextEvent.GetText() );
		}
	}
}
