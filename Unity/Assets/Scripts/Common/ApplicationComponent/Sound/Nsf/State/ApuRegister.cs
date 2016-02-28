using System;
			
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public class NesApuRegister
	{
		public UInt16 REG4002;
		public UInt16 REG4006;
		public UInt16 REG400a;

		public NesApuRegister()
		{
			REG4002 = 0;
			REG4006 = 0;
			REG400a = 0;
		}
	}
}
