using System;

using Monoamp.Common.Data.Standard.Midi;

namespace Monoamp.Common.Component.Sound.Midi
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
