using System;

using Curan.Common.FormalizedData.File.Midi;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MetaEventAffecterTimeSignature : MetaEventAffecterBase
	{
		public MetaEventAffecterTimeSignature( MetaEventTimeSignature aTimeSignature )
			: base( aTimeSignature )
		{

		}

		public override void Execute( MetaStatus aMetaStatus )
		{
			//MidiSequencer.SetNumerator( metaMessageArray[index].nn );
			//MidiSequencer.SetDenominator( metaMessageArray[index].dd );
		}
	}
}
