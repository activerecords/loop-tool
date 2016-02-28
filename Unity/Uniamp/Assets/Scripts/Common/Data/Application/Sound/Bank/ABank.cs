using System;
using System.Collections.Generic;

namespace Monoamp.Common.Data.Application.Sound
{
	public abstract class ABank
	{
		public readonly InstrumentBase[] instrumentArray;

		protected ABank()
		{
			instrumentArray = new InstrumentBase[128];
		}

		public void AddInstrument( int aInstrument, InstrumentBase aInstrumentBase )
		{
			if( instrumentArray[aInstrument] == null ) {
				instrumentArray[aInstrument] = aInstrumentBase;
			}
			else {
				Console.WriteLine( "Add" );
			}
		}
	}
}
