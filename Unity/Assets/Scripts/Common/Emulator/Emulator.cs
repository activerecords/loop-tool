﻿using System;

using Curan.Common.ApplicationComponent.Sound.Nsf;
using Curan.Utility;

namespace Curan.Common.Emulator
{
	public class CEmulator
	{
		NesRom rom;
		NesMemory mem;
		CVram vram;
		// To Be Fixed.
		//NesCpu cpu;
		CPpu ppu;
		CInterrupt interrupt;
		UInt32 time;

		public CEmulator( Byte[] aDataArray )
		{
			time = 0;

			Logger.LogNormal( "Load" );

			rom = new NesRom( aDataArray );	// rom?f?[?^??????????
			// To Be Fixed.
			//mem = new NesMemory( rom );		// ???????????[?h?J?n?A?h???X????rom?f?[?^???i?[
			vram = new CVram( rom );	// ???????????[?h?J?n?A?h???X????rom?f?[?^???i?[
		}

		public void Init()
		{
			mem.Init();		// ??????????????

			// To Be Fixed.
			//cpu.InitMem();	// CPU????????

			ppu.Init();		// PPU????????

			//apu.Init();	// APU????????

			// To Be Fixed.
			//time = timeGetTime();
		}

		// To Be Fixed.
		/*
		public void Execute()
		{
			// 1?t???[??????????????s?????B
			while( cpu.GetRegister().CLK < 1790000 / 60 )
			{
				cpu.ExecuteInstruction( mem );

				ppu.Update( mem, vram );

				//apu.Update( mem );
			}

			interrupt.Update( 0x0000, mem, cpu, ppu );
		}
		*/
		// ??????1?t???[?????i16ms?j?o???????X???[?v
		public void Wait()
		{
			// To Be Fixed.
			/*
			time = timeGetTime() - time;

			if( time < 16 )
			{
				Sleep( 16 - time );
			}
		
			time = timeGetTime();
			*/
		}
	}
}
