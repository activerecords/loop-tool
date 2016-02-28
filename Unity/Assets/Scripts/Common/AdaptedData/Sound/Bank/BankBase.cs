using System;
using System.Collections.Generic;

namespace Curan.Common.AdaptedData
{
	public abstract class BankBase
	{
		public readonly InstrumentBase[] instrumentArray;

		protected BankBase()
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
