using System;
using System.Collections.Generic;

namespace Monoamp.Common.Data.Application.Sound
{
	public interface ISoundcluster
	{
		Dictionary<int, ABank> BankDictionary{ get; }
	}
}
