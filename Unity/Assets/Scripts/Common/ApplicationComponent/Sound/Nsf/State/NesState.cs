using System;

using Curan.Common.AdaptedData.Music;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Nsf
{
	public class NesState
	{
		public MusicNsf nsf;
		public NesMemory memory;
		public NesCpuRegister cpuRegister;
		public NesApuRegister apuRegister;

		public NesState( MusicNsf aMusicNsf )
		{
			nsf = aMusicNsf;
			memory = new NesMemory( nsf );
			cpuRegister = new NesCpuRegister();
			apuRegister = new NesApuRegister();
		}
	}
}
