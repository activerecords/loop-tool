using System;
using System.Collections.Generic;

using Curan.Common.Struct;

namespace Curan.Common.AdaptedData.Music
{
	public interface IMusic
	{
		List<List<LoopInformation>> Loop{ get; }
	}
}
