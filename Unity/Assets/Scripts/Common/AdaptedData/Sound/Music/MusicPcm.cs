using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public interface MusicPcm : IMusic
	{
		int Channels{ get; }
		int SampleLength{ get; }
		int SampleRate{ get; }

		float GetSample( int aChannel, int aPosition );
	}
}
