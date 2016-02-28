using System;

using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public static class NesCpu
	{
		// ?????Z?b?g???`
		// ????
		// ?j?[???j?b?N:????[?l?K?e?B?u?t???O:::::?[???t???O]
		// ?A?h???b?V???O???[?h	?????R?[?h ?????? ?????N???b?N??

		// ------------------------------------------------------------------------
		// ???[?h????
		// LDA:??????????A?????[?h???????B[N:0:0:0:0:0:Z:0]
		public const Byte LDAI = 0xA9;	// Immediate		0xA9 2 2
		public const Byte LDAZ = 0xA5;	// Zeropage			0xA5 2 3
		public const Byte LDAZX = 0xB5;	// Zeropage, X		0xB5 2 4
		public const Byte LDAA = 0xAD;	// Absolute			0xAD 3 4
		public const Byte LDAAX = 0xBD;	// Absolute, X		0xBD 3 4
		public const Byte LDAAY = 0xB9;	// Absolute, Y		0xB9 3 4
		public const Byte LDAIX = 0xA1;	// (Indirect, X)	0xA1 2 6
		public const Byte LDAIY = 0xB1;	// (Indirect), Y	0xB1 2 5

		// LDX:??????????X?????[?h???????B[N:0:0:0:0:0:Z:0]
		public const Byte LDXI = 0xA2;	// Immediate		0xA2 2 2
		public const Byte LDXZ = 0xA6;	// Zeropage			0xA6 2 3
		public const Byte LDXZY = 0xB6;	// Zeropage, Y		0xB6 2 4
		public const Byte LDXA = 0xAE;	// Absolute			0xAE 3 4
		public const Byte LDXAY = 0xBE;	// Absolute, Y		0xBE 3 4

		// LDY:??????????Y?????[?h???????B[N:0:0:0:0:0:Z:0]
		public const Byte LDYI = 0xA0;	// Immediate		0xA0 2 2
		public const Byte LDYZ = 0xA4;	// Zeropage			0xA4 2 3
		public const Byte LDYZX = 0xB4;	// Zeropage, X		0xB4 2 4
		public const Byte LDYA = 0xAC;	// Absolute			0xAC 3 4
		public const Byte LDYAX = 0xBC;	// Absolute, X		0xBC 3 4

		// ------------------------------------------------------------------------
		// ?X?g?A????
		// STA:A?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
		public const Byte STAZ = 0x85;	// Zeropage			0x85 2 3
		public const Byte STAZX = 0x95;	// Zeropage, X		0x95 2 4
		public const Byte STAA = 0x8D;	// Absolute			0x8D 3 4
		public const Byte STAAX = 0x9D;	// Absolute, X		0x9D 3 5
		public const Byte STAAY = 0x99;	// Absolute, Y		0x99 3 5
		public const Byte STAIX = 0x81;	// (Indirect, X)	0x81 2 6
		public const Byte STAIY = 0x91;	// (Indirect), Y	0x91 2 6

		// STX:X?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
		public const Byte STXZ = 0x86;	// Zeropage			0x86 2 3
		public const Byte STXZY = 0x96;	// Zeropage, Y		0x96 2 4
		public const Byte STXA = 0x8E;	// Absolute			0x8E 3 4

		// STY:Y?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
		public const Byte STYZ = 0x84;	// Zeropage			0x84 2 3
		public const Byte STYZX = 0x94;	// Zeropage, X		0x94 2 4
		public const Byte STYA = 0x8C;	// Absolute			0x8C 3 4

		// ------------------------------------------------------------------------
		// ???W?X?^?R?s?[????
		// TAX:A??X???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TAX = 0xAA;	// Implied			0xAA 1 2

		// TAY:A??Y???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TAY = 0xA8;	// Implied			0xA8 1 2

		// TSX:S??X???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TSX = 0xBA;	// Implied			0xBA 1 2

		// TXA:X??A???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TXA = 0x8A;	// Implied			0x8A 1 2

		// TXS:X??S???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TXS = 0x9A;	// Implied			0x9A 1 2

		// TYA:Y??A???R?s?[???????B[N:0:0:0:0:0:Z:0]
		public const Byte TYA = 0x98;	// Implied			0x98 1 2

		// ---------------------------------------------------------------------------
		// ???Z????
		// ADC:(A + ?????? + ?L?????[?t???O) ?????Z??????????A???????????B[N:V:0:0:0:0:Z:C]
		public const Byte ADCI = 0x69;	// Immediate		0x69 2 2
		public const Byte ADCZ = 0x65;	// Zeropage			0x65 2 3
		public const Byte ADCZX = 0x75;	// Zeropage, X		0x75 2 4
		public const Byte ADCA = 0x6D;	// Absolute			0x6D 3 4
		public const Byte ADCAX = 0x7D;	// Absolute, X		0x7D 3 4
		public const Byte ADCAY = 0x79;	// Absolute, Y		0x79 3 4
		public const Byte ADCIX = 0x61;	// (Indirect, X)	0x61 2 6
		public const Byte ADCIY = 0x71;	// (Indirect), Y	0x71 2 5

		// AND:A???????????_??AND???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
		public const Byte ANDI = 0x29;	// Immediate		0x29 2 2
		public const Byte ANDZ = 0x25;	// Zeropage			0x25 2 3
		public const Byte ANDZX = 0x35;	// Zeropage, X		0x35 2 4
		public const Byte ANDA = 0x2D;	// Absolute			0x2D 3 4
		public const Byte ANDAX = 0x3D;	// Absolute, X		0x3D 3 4
		public const Byte ANDAY = 0x39;	// Absolute, Y		0x39 3 4
		public const Byte ANDIX = 0x21;	// (Indirect, X)	0x21 2 6
		public const Byte ANDIY = 0x31;	// (Indirect), Y	0x31 2 5

		// ASL:A???????????????????V?t?g???????B[N:0:0:0:0:0:Z:C]
		public const Byte ASLAC = 0x0A;	// Accumulator		0x0A 1 2
		public const Byte ASLZ = 0x06;	// Zeropage			0x06 2 5
		public const Byte ASLZX = 0x16;	// Zeropage, X		0x16 2 6
		public const Byte ASLAB = 0x0E;	// Absolute			0x0E 3 6
		public const Byte ASLAX = 0x1E;	// Absolute, X		0x1E 3 7

		// BIT:A???????????r?b?g???r???Z???????B[N:V:0:0:0:0:Z:0]
		public const Byte BITZ = 0x24;	// Zeropage			0x24 2 3
		public const Byte BITA = 0x2C;	// Absolute			0x2C 3 4

		// CMP:A?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
		public const Byte CMPI = 0xC9;	// Immediate		0xC9 2 2
		public const Byte CMPZ = 0xC5;	// Zeropage			0xC5 2 3
		public const Byte CMPZX = 0xD5;	// Zeropage, X		0xD5 2 4
		public const Byte CMPA = 0xCD;	// Absolute			0xCD 3 4
		public const Byte CMPAX = 0xDD;	// Absolute, X		0xDD 3 4
		public const Byte CMPAY = 0xD9;	// Absolute, Y		0xD9 3 4
		public const Byte CMPIX = 0xC1;	// (Indirect, X)	0xC1 2 6
		public const Byte CMPIY = 0xD1;	// (Indirect), Y	0xD1 2 5

		// CPX:X?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
		public const Byte CPXI = 0xE0;	// Immediate		0xE0 2 2
		public const Byte CPXZ = 0xE4;	// Zeropage			0xE4 2 3
		public const Byte CPXA = 0xEC;	// Absolute			0xEC 3 4

		// CPY:Y?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
		public const Byte CPYI = 0xC0;	// Immediate		0xC0 2 2
		public const Byte CPYZ = 0xC4;	// Zeropage			0xC4 2 3
		public const Byte CPYA = 0xCC;	// Absolute			0xCC 3 4

		// DEC:????????f?N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte DECZ = 0xC6;	// Zeropage			0xC6 2 5
		public const Byte DECZX = 0xD6;	// Zeropage, X		0xD6 2 6
		public const Byte DECA = 0xCE;	// Absolute			0xCE 3 6
		public const Byte DECAX = 0xDE;	// Absolute, X		0xDE 3 7

		// DEX:X???f?N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte DEX = 0xCA;	// Implied			0xCA 1 2

		// DEY:Y???f?N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte DEY = 0x88;	// Implied			0x88 1 2

		// EOR:A???????????_??XOR???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
		public const Byte EORI = 0x49;	// Immediate		0x49 2 2
		public const Byte EORZ = 0x45;	// Zeropage			0x45 2 3
		public const Byte EORZX = 0x55;	// Zeropage, X		0x55 2 4
		public const Byte EORA = 0x4D;	// Absolute			0x4D 3 4
		public const Byte EORAX = 0x5D;	// Absolute, X		0x5D 3 4
		public const Byte EORAY = 0x59;	// Absolute, Y		0x59 3 4
		public const Byte EORIX = 0x41;	// (Indirect, X)	0x41 2 6
		public const Byte EORIY = 0x51;	// (Indirect), Y	0x51 2 5

		// INC:?????????C???N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte INCZ = 0xE6;	// Zeropage			0xE6 2 5
		public const Byte INCZX = 0xF6;	// Zeropage, X		0xF6 2 6
		public const Byte INCA = 0xEE;	// Absolute			0xEE 3 6
		public const Byte INCAX = 0xFE;	// Absolute, X		0xFE 3 7

		// INX:X???C???N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte INX = 0xE8;	// Implied			0xE8 1 2

		// INY:Y???C???N???????g???????B[N:0:0:0:0:0:Z:0]
		public const Byte INY = 0xC8;	// Implied			0xC8 1 2


		// LSR:A???????????????E???V?t?g???????B[N:0:0:0:0:0:Z:C]
		public const Byte LSRAC = 0x4A;	// Accumulator		0x4A 1 2
		public const Byte LSRZ = 0x46;	// Zeropage			0x46 2 5
		public const Byte LSRZX = 0x56;	// Zeropage, X		0x56 2 6
		public const Byte LSRA = 0x4E;	// Absolute			0x4E 3 6
		public const Byte LSRAX = 0x5E;	// Absolute, X		0x5E 3 7

		// ORA:A???????????_??OR???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
		public const Byte ORAI = 0x09;	// Immediate		0x09 2 2
		public const Byte ORAZ = 0x05;	// Zeropage			0x05 2 3
		public const Byte ORAZX = 0x15;	// Zeropage, X		0x15 2 4
		public const Byte ORAA = 0x0D;	// Absolute			0x0D 3 4
		public const Byte ORAAX = 0x1D;	// Absolute, X		0x1D 3 4
		public const Byte ORAAY = 0x19;	// Absolute, Y		0x19 3 4
		public const Byte ORAIX = 0x01;	// (Indirect, X)	0x01 2 6
		public const Byte ORAIY = 0x11;	// (Indirect), Y	0x11 2 5

		// ROL:A?????????????????????[?e?[?g???????B[N:0:0:0:0:0:Z:C]
		public const Byte ROLAC = 0x2A;	// Accumulator		0x2A 1 2
		public const Byte ROLZ = 0x26;	// Zeropage			0x26 2 5
		public const Byte ROLZX = 0x36;	// Zeropage, X		0x36 2 6
		public const Byte ROLAB = 0x2E;	// Absolute			0x2E 3 6
		public const Byte ROLAX = 0x3E;	// Absolute, X		0x3E 3 7

		// ROR:A???????????????E?????[?e?[?g???????B[N:0:0:0:0:0:Z:C]
		public const Byte RORAC = 0x6A;	// Accumulator		0x6A 1 2
		public const Byte RORZ = 0x66;	// Zeropage			0x66 2 5
		public const Byte RORZX = 0x76;	// Zeropage, X		0x76 2 6
		public const Byte RORAB = 0x6E;	// Absolute			0x6E 3 6
		public const Byte RORAX = 0x7E;	// Absolute, X		0x7E 3 7

		// SBC:(A - ?????? - ?L?????[?t???O?????]) ?????Z??????????A???????????B[N:V:0:0:0:0:Z:C]
		public const Byte SBCI = 0xE9;	// Immediate		0xE9 2 2
		public const Byte SBCZ = 0xE5;	// Zeropage			0xE5 2 3
		public const Byte SBCZX = 0xF5;	// Zeropage, X		0xF5 2 4
		public const Byte SBCA = 0xED;	// Absolute			0xED 3 4
		public const Byte SBCAX = 0xFD;	// Absolute, X		0xFD 3 4
		public const Byte SBCAY = 0xF9;	// Absolute, Y		0xF9 3 4
		public const Byte SBCIX = 0xE1;	// (Indirect, X)	0xE1 2 6
		public const Byte SBCIY = 0xF1;	// (Indirect), Y	0xF1 2 5

		// ------------------------------------------------------------------------
		// ?X?^?b?N????
		// PHA:A???X?^?b?N???v?b?V???_?E?????????B[0:0:0:0:0:0:0:0]
		public const Byte PHA = 0x48;	// Implied			0x48 1 3

		// PHP:P???X?^?b?N???v?b?V???_?E?????????B[0:0:0:0:0:0:0:0]
		public const Byte PHP = 0x08;	// Implied			0x08 1 3

		// PLA:?X?^?b?N????A???|?b?v?A?b?v???????B[N:0:0:0:0:0:Z:0]
		public const Byte PLA = 0x68;	// Implied			0x68 1 4

		// PLP:?X?^?b?N????P???|?b?v?A?b?v???????B[N:V:R:B:D:I:Z:C]
		public const Byte PLP = 0x28;	// Implied			0x28 1 4

		// ------------------------------------------------------------------------
		// ?W?????v????
		// JMP:?A?h???X???W?????v???????B[0:0:0:0:0:0:0:0]
		public const Byte JMPA = 0x4C;	// Absolute			0x4C 3 3
		public const Byte JMPI = 0x6C;	// Indirect			0x6C 3 5

		// JSR:?T?u???[?`?????????o???????B[0:0:0:0:0:0:0:0]
		public const Byte JSR = 0x20;	// Absolute			0x20 3 6

		// RTS:?T?u???[?`?????????A???????B[0:0:0:0:0:0:0:0]
		public const Byte RTS = 0x60;	// Implied			0x60 1 6

		// RTI:???????????[?`?????????A???????B[N:V:R:B:D:I:Z:C]
		public const Byte RTI = 0x40;	// Implied			0x40 1 6

		// ------------------------------------------------------------------------
		// ?u?????`????
		// BCC:?L?????[?t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BCC = 0x90;	// Relative			0x90 2 2

		// BCS:?L?????[?t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BCS = 0xB0;	// Relative			0xB0 2 2

		// BEQ:?[???t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BEQ = 0xF0;	// Relative			0xF0 2 2

		// BMI:?l?K?e?B?u?t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BMI = 0x30;	// Relative			0x30 2 2

		// BNE:?[???t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BNE = 0xD0;	// Relative			0xD0 2 2

		// BPL:?l?K?e?B?u?t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BPL = 0x10;	// Relative			0x10 2 2

		// BVC:?I?[?o?[?t???[?t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BVC = 0x50;	// Relative			0x50 2 2

		// BVS:?I?[?o?[?t???[?t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
		public const Byte BVS = 0x70;	// Relative			0x70 2 2

		// ------------------------------------------------------------------------
		// ?t???O???X????
		// CLC:?L?????[?t???O???N???A???????B[0:0:0:0:0:0:0:C]
		public const Byte CLC = 0x18;	// Implied			0x18 1 2

		// CLD:BCD???[?h???????????[?h???????????B?t?@?~?R?????????????????????????B[0:0:0:0:D:0:0:0]
		public const Byte CLD = 0xD8;	// Implied			0xD8 1 2

		// CLI:IRQ?????????????????????B[0:0:0:0:0:I:0:0]
		public const Byte CLI = 0x58;	// Implied			0x58 1 2

		// CLV:?I?[?o?[?t???[?t???O???N???A???????B[0:V:0:0:0:0:0:0]
		public const Byte CLV = 0xB8;	// Implied			0xB8 1 2

		// SEC:?L?????[?t???O???Z?b?g???????B[0:0:0:0:0:0:0:C]
		public const Byte SEC = 0x38;	// Implied			0x38 1 2

		// SED:BCD???[?h?????????????B?t?@?~?R?????????????????????????B[0:0:0:0:D:0:0:0]
		public const Byte SED = 0xF8;	// Implied			0xF8 1 2

		// SEI:IRQ?????????????~???????B[0:0:0:0:0:I:0:0]
		public const Byte SEI = 0x78;	// Implied			0x78 1 2

		// ------------------------------------------------------------------------
		// ????????????
		// BRK:?\?t?g?E?F?A???????????N?????????B[0:0:0:B:0:0:0:0]
		public const Byte BRK = 0x00;	// Implied			0x00 1 7

		// NOP:?????????????s???????B[0:0:0:0:0:0:0:0]
		public const Byte NOP = 0xEA;	// Implied			0xEA 1 2

		// ------------------------------------------------------------------------
		// ?????????A?h???X?i?[?A?h???X
		public const UInt16 NMI = 0xFFFA;	// ?n?[?h?E?F?A?????M?? P[I]=1, P[B]=0
		public const UInt16 RESET = 0xFFFC;	// ?d?????????A???Z?b?g?? P[I]=1
		public const UInt16 IRQBRK = 0xFFFE;	// ?n?[?h?E?F?A?M??/BRK???? P[I]=1, P[B]=0/1

		public static void InitNsf( NesState aNesState )
		{
			aNesState.cpuRegister.Init();
			aNesState.cpuRegister.S = 0xFF;
			aNesState.cpuRegister.A = ( Byte )aNesState.nsf.GetMusicNumber();
			aNesState.cpuRegister.PC = aNesState.nsf.GetHeader().GetInitAddress();
			aNesState.cpuRegister.NPC = aNesState.nsf.GetHeader().GetInitAddress();
		}

		public static void Update( NesState aNesState )
		{
			// 1?t???[?????????????????????RTS???????o?????APC??0???????B
			while( aNesState.cpuRegister.NPC >= 0x8000 )
			{
				ExecuteInstruction( aNesState );

				// 1?t???[???????N???b?N???o???????????????????????????????A?????I???B
				if( aNesState.cpuRegister.CLK >= 1790000 / 60 )
				{
					break;
				}
			}

			aNesState.cpuRegister.CLK = 0;
			aNesState.cpuRegister.PC = aNesState.nsf.GetHeader().GetStartAddress();
			aNesState.cpuRegister.NPC = aNesState.nsf.GetHeader().GetStartAddress();
		}

		public static void ExecuteInstruction( NesState aNesState )
		{
			NesCpuRegister cpuRegisterPre = aNesState.cpuRegister.GetCopy();
			aNesState.cpuRegister.NPC = aNesState.cpuRegister.PC;

			aNesState.memory.SetAddressInstruction( aNesState.cpuRegister.PC );

			switch( aNesState.memory.GetOpecode() )
			{
			// --------------------------------------------------------------------
			// ???[?h????
			// LDA:??????????A?????[?h???????B[N:0:0:0:0:0:Z:0]
			case LDAI:
				aNesState.memory.LoadDataImmediate( ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case LDAZ:
				aNesState.memory.LoadDataZeropage( ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case LDAZX:
				aNesState.memory.LoadDataZeropageIndexX( aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDAA:
				aNesState.memory.LoadDataAbsolute( ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDAAX:
				aNesState.memory.LoadDataAbsoluteIndexX( aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case LDAAY:
				aNesState.memory.LoadDataAbsoluteIndexY( aNesState.cpuRegister.Y, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case LDAIX:
				aNesState.memory.LoadDataIndirectIndexX( aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case LDAIY:
				aNesState.memory.LoadDataIndirectIndexY( aNesState.cpuRegister.Y, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// LDX:??????????X?????[?h???????B[N:0:0:0:0:0:Z:0]
			case LDXI:
				aNesState.memory.LoadDataImmediate( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case LDXZ:
				aNesState.memory.LoadDataZeropage( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case LDXZY:
				aNesState.memory.LoadDataZeropageIndexY( aNesState.cpuRegister.Y, ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDXA:
				aNesState.memory.LoadDataAbsolute( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDXAY:
				aNesState.memory.LoadDataAbsoluteIndexY( aNesState.cpuRegister.Y, ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// LDY:??????????Y?????[?h???????B[N:0:0:0:0:0:Z:0]
			case LDYI:
				aNesState.memory.LoadDataImmediate( ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case LDYZ:
				aNesState.memory.LoadDataZeropage( ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case LDYZX:
				aNesState.memory.LoadDataZeropageIndexX( aNesState.cpuRegister.X, ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDYA:
				aNesState.memory.LoadDataAbsolute( ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case LDYAX:
				aNesState.memory.LoadDataAbsoluteIndexX( aNesState.cpuRegister.X, ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// --------------------------------------------------------------------
			// ?X?g?A????
			// STA:A?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
			case STAZ:
				aNesState.memory.StoreDataZeropage( ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case STAZX:
				aNesState.memory.StoreDataZeropageIndexX( ref aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case STAA:
				if( aNesState.memory.GetAddressAbsolute() < 0x8000 )
				{
					aNesState.memory.StoreDataAbsolute( ref aNesState.cpuRegister.A );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressAbsolute() );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case STAAX:
				if( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) < 0x8000 )
				{
					aNesState.memory.StoreDataAbsoluteIndexX( ref aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case STAAY:
				if( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) < 0x8000 )
				{
					aNesState.memory.StoreDataAbsoluteIndexY( ref aNesState.cpuRegister.Y, ref aNesState.cpuRegister.A );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case STAIX:
				if( aNesState.memory.GetAddressIndirectIndexX( aNesState.cpuRegister.X ) < 0x8000 )
				{
					aNesState.memory.StoreDataIndirectIndexX( ref aNesState.cpuRegister.X, ref aNesState.cpuRegister.A );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressIndirectIndexX( aNesState.cpuRegister.X ) );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case STAIY:
				if( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) < 0x8000 )
				{
					aNesState.memory.StoreDataIndirectIndexY( ref aNesState.cpuRegister.Y, ref aNesState.cpuRegister.A );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) );	// ?}?b?p?[???g?p????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// STX:X?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
			case STXZ:
				aNesState.memory.StoreDataZeropage( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case STXZY:
				aNesState.memory.StoreDataZeropageIndexY( ref aNesState.cpuRegister.Y, ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case STXA:
				if( aNesState.memory.GetAddressAbsolute() < 0x8000 )
				{
					aNesState.memory.StoreDataAbsolute( ref aNesState.cpuRegister.X );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressAbsolute() );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// STY:Y?????????????X?g?A???????B[0:0:0:0:0:0:0:0]
			case STYZ:
				aNesState.memory.StoreDataZeropage( ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case STYZX:
				aNesState.memory.StoreDataZeropageIndexX( ref aNesState.cpuRegister.X, ref aNesState.cpuRegister.Y );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case STYA:
				if( aNesState.memory.GetAddressAbsolute() < 0x8000 )
				{
					aNesState.memory.StoreDataAbsolute( ref aNesState.cpuRegister.Y );
				}
				else
				{
					Logger.LogNormal( "%04X\n" + aNesState.memory.GetAddressAbsolute() );	// ?}?b?p?[???g?p?????????A???????L?q
				}
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// --------------------------------------------------------------------
			// ???W?X?^?R?s?[????
			// TAX:A??X???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TAX:
				aNesState.cpuRegister.X = aNesState.cpuRegister.A;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// TAY:A??Y???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TAY:
				aNesState.cpuRegister.Y = aNesState.cpuRegister.A;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// TSX:S??X???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TSX:
				aNesState.cpuRegister.X = aNesState.cpuRegister.S;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// TXA:X??A???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TXA:
				aNesState.cpuRegister.A = aNesState.cpuRegister.X;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// TXS:X??S???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TXS:
				aNesState.cpuRegister.S = aNesState.cpuRegister.X;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.S >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.S == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// TYA:Y??A???R?s?[???????B[N:0:0:0:0:0:Z:0]
			case TYA:
				aNesState.cpuRegister.A = aNesState.cpuRegister.Y;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// --------------------------------------------------------------------
			// ???Z????
			// ADC:(A + ?????? + ?L?????[?t???O) ?????Z??????????A???????????B[N:V:0:0:0:0:Z:C]
			case ADCI:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataImmediate() + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataImmediate() & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataImmediate() & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case ADCZ:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataZeropage() + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataZeropage() & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataZeropage() & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case ADCZX:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ADCA:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataAbsolute() + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsolute() & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsolute() & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ADCAX:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ADCAY:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ADCIX:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ADCIY:
				aNesState.cpuRegister.A += ( Byte )( aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) + aNesState.cpuRegister.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x00 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x80 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// AND:A???????????_??AND???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
			case ANDI:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataImmediate();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case ANDZ:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataZeropage();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case ANDZX:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ANDA:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataAbsolute();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ANDAX:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ANDAY:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ANDIX:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ANDIY:
				aNesState.cpuRegister.A &= aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// ASL:A???????????????????V?t?g???????B[N:0:0:0:0:0:Z:C]
			case ASLAC:
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.A <<= 1;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case ASLZ:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.memory.WriteDataZeropage( ( Byte )( aNesState.memory.ReadDataZeropage() << 1 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case ASLZX:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.memory.WriteDataZeropageIndexX( aNesState.cpuRegister.X, ( Byte )( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) << 1 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ASLAB:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.memory.WriteDataAbsolute( ( Byte )( aNesState.memory.ReadDataAbsolute() << 1 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ASLAX:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.memory.WriteDataAbsoluteIndexX( aNesState.cpuRegister.X, ( Byte )( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) << 1 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// BIT:A???????????r?b?g???r???Z???????B[N:V:0:0:0:0:Z:0]
			case BITZ:
				aNesState.cpuRegister.SR.Z = ( ( aNesState.memory.ReadDataZeropage() & aNesState.cpuRegister.A ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.N = ( ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01 );
				aNesState.cpuRegister.SR.V = ( ( aNesState.memory.ReadDataZeropage() >> 6 ) & 0x01 );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case BITA:
				aNesState.cpuRegister.SR.Z = ( ( aNesState.memory.ReadDataAbsolute() & aNesState.cpuRegister.A ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.N = ( ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01 );
				aNesState.cpuRegister.SR.V = ( ( aNesState.memory.ReadDataAbsolute() >> 6 ) & 0x01 );
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// CMP:A?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
			case CMPI:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataImmediate() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case CMPZ:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataZeropage() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case CMPZX:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case CMPA:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataAbsolute() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case CMPAX:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case CMPAY:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case CMPIX:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case CMPIY:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.A - aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >= aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// CPX:X?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
			case CPXI:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.X - aNesState.memory.ReadDataImmediate() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.X >= aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case CPXZ:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.X - aNesState.memory.ReadDataZeropage() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.X >= aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case CPXA:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.X - aNesState.memory.ReadDataAbsolute() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.X >= aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// CPY:Y?????????????r???Z???????B[N:0:0:0:0:0:Z:C]
			case CPYI:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.Y - aNesState.memory.ReadDataImmediate() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.Y >= aNesState.memory.ReadDataImmediate() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case CPYZ:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.Y - aNesState.memory.ReadDataZeropage() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.Y >= aNesState.memory.ReadDataZeropage() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case CPYA:
				aNesState.cpuRegister.SR.N = ( ( aNesState.cpuRegister.Y - aNesState.memory.ReadDataAbsolute() ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.Y >= aNesState.memory.ReadDataAbsolute() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// DEC:?????????f?N???????g???????B[N:0:0:0:0:0:Z:0]
			case DECZ:
				aNesState.memory.DecDataZeropage();
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case DECZX:
				aNesState.memory.DecDataZeropageIndexX( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case DECA:
				aNesState.memory.DecDataAbsolute();
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case DECAX:
				aNesState.memory.DecDataAbsoluteIndexX( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// DEX:X???f?N???????g???????B[N:0:0:0:0:0:Z:0]
			case DEX:
				aNesState.cpuRegister.X--;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// DEY:Y???f?N???????g???????B[N:0:0:0:0:0:Z:0]
			case DEY:
				aNesState.cpuRegister.Y--;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// EOR:A???????????_??XOR???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
			case EORI:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataImmediate();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case EORZ:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataZeropage();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case EORZX:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case EORA:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataAbsolute();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case EORAX:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case EORAY:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case EORIX:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case EORIY:
				aNesState.cpuRegister.A ^= aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// INC:?????????C???N???????g???????B[N:0:0:0:0:0:Z:0]
			case INCZ:
				aNesState.memory.IncDataZeropage();
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case INCZX:
				aNesState.memory.IncDataZeropageIndexX( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case INCA:
				aNesState.memory.IncDataAbsolute();
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case INCAX:
				aNesState.memory.IncDataAbsoluteIndexX( ref aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// INX:X???C???N???????g???????B[N:0:0:0:0:0:Z:0]
			case INX:
				aNesState.cpuRegister.X++;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.X >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.X == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// INY:Y???C???N???????g???????B[N:0:0:0:0:0:Z:0]
			case INY:
				aNesState.cpuRegister.Y++;
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.Y >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.Y == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// LSR:A???????????????E???V?t?g???????B[N:0:0:0:0:0:Z:C]
			case LSRAC:
				aNesState.cpuRegister.SR.C = aNesState.cpuRegister.A & 0x01;
				aNesState.cpuRegister.A = ( Byte )( ( aNesState.cpuRegister.A >> 1 ) & 0x7F );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case LSRZ:
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataZeropage() & 0x01;
				aNesState.memory.WriteDataZeropage( ( Byte )( ( aNesState.memory.ReadDataZeropage() >> 1 ) & 0x7F ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case LSRZX:
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x01;
				aNesState.memory.WriteDataZeropageIndexX( aNesState.cpuRegister.X, ( Byte )( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 1 ) & 0x7F ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case LSRA:
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataAbsolute() & 0x01;
				aNesState.memory.WriteDataAbsolute( ( Byte )( ( aNesState.memory.ReadDataAbsolute() >> 1 ) & 0x7F ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case LSRAX:
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x01;
				aNesState.memory.WriteDataAbsoluteIndexX( aNesState.cpuRegister.X, ( Byte )( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 1 ) & 0x7F ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// ORA:A???????????_??OR???Z??????????A???????????B[N:0:0:0:0:0:Z:0]
			case ORAI:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataImmediate();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case ORAZ:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataZeropage();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case ORAZX:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ORAA:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataAbsolute();
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case ORAAX:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ORAAY:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case ORAIX:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ORAIY:
				aNesState.cpuRegister.A |= aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// ROL:A?????????????????????[?e?[?g???????B[N:0:0:0:0:0:Z:C]
			case ROLAC:
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.A = ( Byte )( ( aNesState.cpuRegister.A << 1 ) | cpuRegisterPre.SR.C );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case ROLZ:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.memory.WriteDataZeropage( ( Byte )( ( aNesState.memory.ReadDataZeropage() << 1 ) | cpuRegisterPre.SR.C ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropage() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case ROLZX:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.memory.WriteDataZeropageIndexX( aNesState.cpuRegister.X, ( Byte )( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) << 1 ) | cpuRegisterPre.SR.C ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ROLAB:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.memory.WriteDataAbsolute( ( Byte )( ( aNesState.memory.ReadDataAbsolute() << 1 ) | cpuRegisterPre.SR.C ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsolute() >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case ROLAX:
				aNesState.cpuRegister.SR.C = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.memory.WriteDataAbsoluteIndexX( aNesState.cpuRegister.X, ( Byte )( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) << 1 ) | cpuRegisterPre.SR.C ) );
				aNesState.cpuRegister.SR.N = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// ROR:A???????????????E?????[?e?[?g???????B[N:0:0:0:0:0:Z:C]
			case RORAC:
				aNesState.cpuRegister.SR.N = aNesState.cpuRegister.SR.C;
				aNesState.cpuRegister.SR.C = aNesState.cpuRegister.A & 0x01;
				aNesState.cpuRegister.A = ( Byte )( ( ( aNesState.cpuRegister.A >> 1 ) & 0x7F ) | ( cpuRegisterPre.SR.C << 7 ) );
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case RORZ:
				aNesState.cpuRegister.SR.N = aNesState.cpuRegister.SR.C;
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataZeropage() & 0x01;
				aNesState.memory.WriteDataZeropage( ( Byte )( ( ( aNesState.memory.ReadDataZeropage() >> 1 ) & 0x7F ) | ( cpuRegisterPre.SR.C << 7 ) ) );
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropage() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				break;
			case RORZX:
				aNesState.cpuRegister.SR.N = aNesState.cpuRegister.SR.C;
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x01;
				aNesState.memory.WriteDataZeropageIndexX( aNesState.cpuRegister.X, ( Byte )( ( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) >> 1 ) & 0x7F ) | ( cpuRegisterPre.SR.C << 7 ) ) );
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case RORAB:
				aNesState.cpuRegister.SR.N = aNesState.cpuRegister.SR.C;
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataAbsolute() & 0x01;
				aNesState.memory.WriteDataAbsolute( ( Byte )( ( ( aNesState.memory.ReadDataAbsolute() >> 1 ) & 0x7F ) | ( cpuRegisterPre.SR.C << 7 ) ) );
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsolute() == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case RORAX:
				aNesState.cpuRegister.SR.N = aNesState.cpuRegister.SR.C;
				aNesState.cpuRegister.SR.C = aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x01;
				aNesState.memory.WriteDataAbsoluteIndexX( aNesState.cpuRegister.X, ( Byte )( ( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) >> 1 ) & 0x7F ) | ( cpuRegisterPre.SR.C << 7 ) ) );
				aNesState.cpuRegister.SR.Z = ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 6;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// SBC:(A - ?????? - ?L?????[?t???O?????]) ????Z??????????A?????????B[N:V:0:0:0:0:Z:C]
			case SBCI:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataImmediate() + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataImmediate() & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataImmediate() & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 2;
				break;
			case SBCZ:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataZeropage() + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataZeropage() & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataZeropage() & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 3;
				break;
			case SBCZX:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataZeropageIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case SBCA:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataAbsolute() + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsolute() & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsolute() & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				break;
			case SBCAX:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataAbsoluteIndexX( cpuRegisterPre.X ) + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexX( aNesState.cpuRegister.X ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case SBCAY:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataAbsoluteIndexY( cpuRegisterPre.Y ) + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataAbsoluteIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 3 );
				aNesState.cpuRegister.CLK += 4;
				if( ( aNesState.memory.GetAddressAbsoluteIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( aNesState.memory.GetAddressAbsolute() >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;
			case SBCIX:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataIndirectIndexX( cpuRegisterPre.X ) + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataIndirectIndexX( aNesState.cpuRegister.X ) & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 6;
				break;
			case SBCIY:
				aNesState.cpuRegister.A -= ( byte )( aNesState.memory.ReadDataIndirectIndexY( cpuRegisterPre.Y ) + ( ( !aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0 ) );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( ( ( ( cpuRegisterPre.A & 0x80 ) == 0x00 ) && ( ( aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x80 ) && aNesState.cpuRegister.SR.GetFlagN() ) ||
									 ( ( ( cpuRegisterPre.A & 0x80 ) == 0x80 ) && ( ( aNesState.memory.ReadDataIndirectIndexY( aNesState.cpuRegister.Y ) & 0x80 ) == 0x00 ) && !aNesState.cpuRegister.SR.GetFlagN() ) ) ? 1 : 0;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.SR.C = ( aNesState.cpuRegister.A < cpuRegisterPre.A ) || ( ( aNesState.cpuRegister.A == cpuRegisterPre.A ) && aNesState.cpuRegister.SR.GetFlagC() ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 2 );
				aNesState.cpuRegister.CLK += 5;
				if( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) >> 8 ) != ( ( aNesState.memory.GetAddressIndirectIndexY( aNesState.cpuRegister.Y ) - aNesState.cpuRegister.Y ) >> 8 ) )
				{
					aNesState.cpuRegister.CLK++;
				}
				break;

			// --------------------------------------------------------------------
			// ?X?^?b?N????
			// PHA:A???X?^?b?N???v?b?V???_?E?????????B[0:0:0:0:0:0:0:0]
			case PHA:
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, aNesState.cpuRegister.A );
				aNesState.cpuRegister.S--;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 3;
				break;

			// PHP:P???X?^?b?N???v?b?V???_?E?????????B[0:0:0:0:0:0:0:0]
			case PHP:
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, aNesState.cpuRegister.P );
				aNesState.cpuRegister.S--;
				aNesState.cpuRegister.SR.B = 1;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 3;
				break;

			// PLA:?X?^?b?N????A???|?b?v?A?b?v???????B[N:0:0:0:0:0:Z:0]
			case PLA:
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.A = aNesState.memory.ReadDataStack( aNesState.cpuRegister.S );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.A >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.A == 0 ) ? 1 : 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// PLP:?X?^?b?N????P???|?b?v?A?b?v???????B[N:V:R:B:D:I:Z:C]
			case PLP:
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.P = aNesState.memory.ReadDataStack( aNesState.cpuRegister.S );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.P >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( aNesState.cpuRegister.P >> 6 ) & 0x01;
				aNesState.cpuRegister.SR.R = ( aNesState.cpuRegister.P >> 5 ) & 0x01;
				aNesState.cpuRegister.SR.B = ( aNesState.cpuRegister.P >> 4 ) & 0x01;
				aNesState.cpuRegister.SR.D = ( aNesState.cpuRegister.P >> 3 ) & 0x01;
				aNesState.cpuRegister.SR.I = ( aNesState.cpuRegister.P >> 2 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.P >> 1 ) & 0x01;
				aNesState.cpuRegister.SR.C = aNesState.cpuRegister.P & 0x01;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 4;
				break;

			// --------------------------------------------------------------------
			// ?W?????v????
			// JMP:?A?h???X???W?????v???????B[0:0:0:0:0:0:0:0]
			case JMPA:
				aNesState.cpuRegister.NPC = aNesState.memory.GetAddressAbsolute();
				aNesState.cpuRegister.CLK += 3;
				break;
			case JMPI:
				aNesState.cpuRegister.NPC = aNesState.memory.GetAddressIndirect();
				aNesState.cpuRegister.CLK += 5;
				break;

			// JSR:?T?u???[?`?????????o???????B[0:0:0:0:0:0:0:0]
			case JSR:
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( ( UInt16 )( aNesState.cpuRegister.PC + 2 ) ) >> 8 ) & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( UInt16 )( aNesState.cpuRegister.PC + 2 ) ) & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.cpuRegister.NPC = aNesState.memory.GetAddressAbsolute();
				aNesState.cpuRegister.CLK += 6;
				break;

			// RTS:?T?u???[?`?????????A???????B[0:0:0:0:0:0:0:0]
			case RTS:
				// nsf???????????A1???[?v???I?????i?X?^?b?N?|?C???^??255?????jRTS???????o???????B
				// 1???[?v???I??????????????NPC??0???????????B
				// nsf???????O?????A???????????????o?????????????????B
				if( aNesState.cpuRegister.S == 0xFF )
				{
					aNesState.cpuRegister.NPC = 0;
					aNesState.cpuRegister.CLK += 6;
					break;
				}

				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.NPC = aNesState.memory.ReadDataStack( aNesState.cpuRegister.S );
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.NPC = ( UInt16 )( ( ( UInt16 )aNesState.memory.ReadDataStack( aNesState.cpuRegister.S ) << 8 ) | aNesState.cpuRegister.NPC );
				aNesState.cpuRegister.NPC += 1;
				aNesState.cpuRegister.CLK += 6;
				break;

			// RTI:???????????[?`?????????A???????B[N:V:R:B:D:I:Z:C]
			case RTI:
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.P = aNesState.memory.ReadDataStack( aNesState.cpuRegister.S );
				aNesState.cpuRegister.SR.N = ( aNesState.cpuRegister.P >> 7 ) & 0x01;
				aNesState.cpuRegister.SR.V = ( aNesState.cpuRegister.P >> 6 ) & 0x01;
				aNesState.cpuRegister.SR.R = ( aNesState.cpuRegister.P >> 5 ) & 0x01;
				aNesState.cpuRegister.SR.B = ( aNesState.cpuRegister.P >> 4 ) & 0x01;
				aNesState.cpuRegister.SR.D = ( aNesState.cpuRegister.P >> 3 ) & 0x01;
				aNesState.cpuRegister.SR.I = ( aNesState.cpuRegister.P >> 2 ) & 0x01;
				aNesState.cpuRegister.SR.Z = ( aNesState.cpuRegister.P >> 1 ) & 0x01;
				aNesState.cpuRegister.SR.C = aNesState.cpuRegister.P & 0x01;
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.NPC = aNesState.memory.ReadDataStack( aNesState.cpuRegister.S );
				aNesState.cpuRegister.S++;
				aNesState.cpuRegister.NPC = ( UInt16 )( ( ( UInt16 )aNesState.memory.ReadDataStack( aNesState.cpuRegister.S ) << 8 ) | aNesState.cpuRegister.NPC );
				aNesState.cpuRegister.CLK += 6;
				break;

			// --------------------------------------------------------------------
			// ?u?????`????
			// BCC:?L?????[?t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BCC:
				if( aNesState.cpuRegister.SR.C == 0 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BCS:?L????[?t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BCS:
				if( aNesState.cpuRegister.SR.C == 1 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BEQ:?[???t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BEQ:
				if( aNesState.cpuRegister.SR.Z == 1 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BMI:?l?K?e?B?u?t??O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BMI:
				if( aNesState.cpuRegister.SR.N == 1 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BNE:?[???t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BNE:
				if( aNesState.cpuRegister.SR.Z == 0 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BPL:?l?K?e?B?u?t???O???N???A???????????????u????`???????B[0:0:0:0:0:0:0:0]
			case BPL:
				if( aNesState.cpuRegister.SR.N == 0 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BVC:?I?[?o?[?t???[?t???O???N???A???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BVC:
				if( aNesState.cpuRegister.SR.V == 0 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// BVS:?I?[?o?[?t???[?t???O???Z?b?g???????????????u?????`???????B[0:0:0:0:0:0:0:0]
			case BVS:
				if( aNesState.cpuRegister.SR.V == 1 )
				{
					aNesState.cpuRegister.NPC = aNesState.memory.GetAddressRerative();
					aNesState.cpuRegister.CLK += 1;

					if( ( aNesState.cpuRegister.NPC >> 8 ) != ( aNesState.cpuRegister.PC >> 8 ) )
					{
						aNesState.cpuRegister.CLK++;
					}
				}
				aNesState.cpuRegister.NPC += 2;
				aNesState.cpuRegister.CLK += 2;
				break;

			// --------------------------------------------------------------------
			// ?t???O???X????
			// CLC:?L?????[?t???O???N???A???????B[0:0:0:0:0:0:0:C]
			case CLC:
				aNesState.cpuRegister.SR.C = 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// CLD:BCD???[?h???????????[?h???????????B?t?@?~?R?????????????????????????B[0:0:0:0:D:0:0:0]
			case CLD:
				aNesState.cpuRegister.SR.D = 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// CLI:IRQ????????????????????B[0:0:0:0:0:I:0:0]
			case CLI:
				aNesState.cpuRegister.SR.I = 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// CLV:?I?[?o?[?t???[?t???O???N???A???????B[0:V:0:0:0:0:0:0]
			case CLV:
				aNesState.cpuRegister.SR.V = 0;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// SEC:?L?????[?t???O???Z?b?g???????B[0:0:0:0:0:0:0:C]
			case SEC:
				aNesState.cpuRegister.SR.C = 1;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// SED:BCD???[?h?????????????B?t?@?~?R?????????????????????????B[0:0:0:0:D:0:0:0]
			case SED:
				aNesState.cpuRegister.SR.D = 1;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// SEI:IRQ?????????????~???????B[0:0:0:0:0:I:0:0]
			case SEI:
				aNesState.cpuRegister.SR.I = 1;
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// --------------------------------------------------------------------
			// ????????????
			// BRK:?\?t?g?E?F?A???????????N?????????B[0:0:0:B:0:0:0:0]
			case BRK:
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			// NOP:?????????????s???????B[0:0:0:0:0:0:0:0]
			case NOP:
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;

			default:
				aNesState.cpuRegister.NPC = ( UInt16 )( aNesState.cpuRegister.PC + 1 );
				aNesState.cpuRegister.CLK += 2;
				break;
			}

			aNesState.cpuRegister.P = ( Byte )(
							( aNesState.cpuRegister.SR.N << 7 ) |
							( aNesState.cpuRegister.SR.V << 6 ) |
							( aNesState.cpuRegister.SR.R << 5 ) |
							( aNesState.cpuRegister.SR.B << 4 ) |
							( aNesState.cpuRegister.SR.D << 3 ) |
							( aNesState.cpuRegister.SR.I << 2 ) |
							( aNesState.cpuRegister.SR.Z << 1 ) |
							aNesState.cpuRegister.SR.C );

			aNesState.cpuRegister.PC = aNesState.cpuRegister.NPC;

			if( aNesState.memory.GetOpecode() == BRK )
			{
				// nsf????????I?t???O?????W???????s???????H
				//if( aNesState.cpuRegister.SR.I == 0 ){
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( ( Byte )( aNesState.cpuRegister.PC + 1 ) ) >> 8 ) & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( Byte )( aNesState.cpuRegister.PC + 1 ) ) & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( aNesState.cpuRegister.P | 0x10 ) );
				aNesState.cpuRegister.S--;
				aNesState.cpuRegister.P |= 0x4;
				aNesState.cpuRegister.SR.I = 1;
				aNesState.cpuRegister.PC = aNesState.memory.GetDataWord( IRQBRK );
				//}
			}
		}

		public static void Interrupt( NesState aNesState, UInt16 interrupt )
		{
			switch( interrupt )
			{
			case NMI:
				aNesState.cpuRegister.P &= 0xEF;
				aNesState.cpuRegister.SR.B = 0;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( aNesState.cpuRegister.PC >> 8 ) & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( aNesState.cpuRegister.PC & 0xFF ) );
				aNesState.cpuRegister.S--;
				aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, aNesState.cpuRegister.P );
				aNesState.cpuRegister.S--;
				aNesState.cpuRegister.P |= 0x4;
				aNesState.cpuRegister.SR.I = 1;
				aNesState.cpuRegister.PC = aNesState.memory.GetDataWord( NMI );
				break;
			case RESET:
				aNesState.cpuRegister.P &= 0xFB;
				aNesState.cpuRegister.SR.I = 0;
				aNesState.cpuRegister.PC = aNesState.memory.GetDataWord( RESET );
				break;
			case IRQBRK:
				if( aNesState.cpuRegister.SR.I == 0 )
				{
					aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( ( Byte )( aNesState.cpuRegister.PC + 1 ) ) >> 8 ) & 0xFF ) );
					aNesState.cpuRegister.S--;
					aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( ( ( Byte )( aNesState.cpuRegister.PC + 1 ) ) & 0xFF ) );
					aNesState.cpuRegister.S--;
					aNesState.memory.WriteDataStack( aNesState.cpuRegister.S, ( Byte )( aNesState.cpuRegister.P | 0x10 ) );
					aNesState.cpuRegister.S--;
					aNesState.cpuRegister.P |= 0x4;
					aNesState.cpuRegister.SR.I = 1;
					aNesState.cpuRegister.PC = aNesState.memory.GetDataWord( IRQBRK );
				}
				break;
			}

			aNesState.cpuRegister.P = ( Byte )(
							( aNesState.cpuRegister.SR.N << 7 ) |
							( aNesState.cpuRegister.SR.V << 6 ) |
							( aNesState.cpuRegister.SR.R << 5 ) |
							( aNesState.cpuRegister.SR.B << 4 ) |
							( aNesState.cpuRegister.SR.D << 3 ) |
							( aNesState.cpuRegister.SR.I << 2 ) |
							( aNesState.cpuRegister.SR.Z << 1 ) |
							aNesState.cpuRegister.SR.C );
		}
	}
}
