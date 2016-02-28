using System;

using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	// ?X?e?[?^?X???W?X?^
	public class NesCpuStatusRegister
	{
		public int N;
		public int V;
		public int R;
		public int B;
		public int D;
		public int I;
		public int Z;
		public int C;

		public void Init()
		{
			N = 0;
			V = 0;
			R = 0;
			B = 0;
			D = 0;
			I = 0;
			Z = 0;
			C = 0;
		}

		public NesCpuStatusRegister GetCopy()
		{
			NesCpuStatusRegister lCStatusRegister = new NesCpuStatusRegister();

			lCStatusRegister.N = N;
			lCStatusRegister.V = V;
			lCStatusRegister.R = R;
			lCStatusRegister.B = B;
			lCStatusRegister.D = D;
			lCStatusRegister.I = I;
			lCStatusRegister.Z = Z;
			lCStatusRegister.C = C;

			return lCStatusRegister;
		}

		public bool GetFlagN()
		{
			return ( N == 0 ) ? false : true;
		}

		public bool GetFlagV()
		{
			return ( V == 0 ) ? false : true;
		}

		public bool GetFlagR()
		{
			return ( R == 0 ) ? false : true;
		}

		public bool GetFlagB()
		{
			return ( B == 0 ) ? false : true;
		}

		public bool GetFlagD()
		{
			return ( D == 0 ) ? false : true;
		}

		public bool GetFlagI()
		{
			return ( I == 0 ) ? false : true;
		}

		public bool GetFlagZ()
		{
			return ( Z == 0 ) ? false : true;
		}

		public bool GetFlagC()
		{
			return ( C == 0 ) ? false : true;
		}
	}

	// CPU???W?X?^
	public class NesCpuRegister
	{
		public Byte A;				// 8bit ?A?L???[?????[?^
		public Byte X;				// 8bit ?C???f?b?N?X???W?X?^
		public Byte Y;				// 8bit ?C???f?b?N?X???W?X?^
		public Byte S;				// 8bit ?X?^?b?N?|?C???^
		public Byte P;				// 8bit ?X?e?[?^?X???W?X?^
		public UInt16 PC;			// 16bit ?v???O?????J?E???^
		public UInt16 NPC;			// 16bit ?l?N?X?g?v???O?????J?E???^
		public UInt32 CLK;			// 32bit ?N???b?N
		public NesCpuStatusRegister SR = new NesCpuStatusRegister();	// ?X?e?[?^?X???W?X?^

		public void Init()
		{
			A = 0;
			X = 0;
			Y = 0;
			S = 0;
			P = 0;
			PC = 0;
			NPC = 0;
			CLK = 0;
			SR.Init();
		}

		public NesCpuRegister GetCopy()
		{
			NesCpuRegister lCCpuRegister = new NesCpuRegister();

			lCCpuRegister.A = A;
			lCCpuRegister.X = X;
			lCCpuRegister.Y = Y;
			lCCpuRegister.S = S;
			lCCpuRegister.P = P;
			lCCpuRegister.PC = PC;
			lCCpuRegister.NPC = NPC;
			lCCpuRegister.CLK = CLK;
			lCCpuRegister.SR = SR.GetCopy();

			return lCCpuRegister;
		}

		public bool GetFlagA()
		{
			return ( A == 0 ) ? false : true;
		}

		public bool GetFlagX()
		{
			return ( X == 0 ) ? false : true;
		}

		public bool GetFlagY()
		{
			return ( Y == 0 ) ? false : true;
		}

		public bool GetFlagS()
		{
			return ( S == 0 ) ? false : true;
		}

		public bool GetFlagP()
		{
			return ( P == 0 ) ? false : true;
		}
	}
}
