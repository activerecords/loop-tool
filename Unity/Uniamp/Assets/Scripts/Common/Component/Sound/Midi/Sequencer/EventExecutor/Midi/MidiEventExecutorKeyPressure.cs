using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;

namespace Monoamp.Common.Component.Sound.Midi
{
	public class MidiEventExecutorKeyPressure : MidiEventExecutorBase
	{
		public MidiEventExecutorKeyPressure( MidiEventKeyPressure aKeyPressure )
			: base( aKeyPressure )
		{

		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{

		}
	}
}
