using System;
using System.Collections.Generic;

namespace Curan.Common.AdaptedData
{
	public abstract class SoundclusterBase
	{
		public readonly Dictionary<int, BankBase> bankDictionary;

		protected SoundclusterBase()
		{
			bankDictionary = new Dictionary<int, BankBase>();
		}
	}
}
