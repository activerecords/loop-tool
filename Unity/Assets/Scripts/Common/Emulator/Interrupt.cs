using System;

using Curan.Common.ApplicationComponent.Sound.Nsf;

namespace Curan.Common.Emulator
{
	class CInterrupt
	{
		public void Update( UInt16 interrupt, NesState aNesState, CPpu ppu )
		{
			if( ppu.IsSetNmi() == true )
			{
				NesCpu.Interrupt( aNesState, NesCpu.NMI );
			}
			//case CCpu::RESET:
			//case CCpu::IRQBRK:
		}
	}
}
