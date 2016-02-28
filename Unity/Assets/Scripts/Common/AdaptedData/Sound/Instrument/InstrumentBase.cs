using System;
using System.Collections.Generic;

using Curan.Utility;

namespace Curan.Common.AdaptedData
{
	public abstract class InstrumentBase
	{
		public readonly SoundfontBase[] soundfontArray;

		protected InstrumentBase()
		{
			soundfontArray = new SoundfontBase[128];
		}

		protected InstrumentBase( SoundfontBase aSoundfont )
			: this()
		{
			AddSoundfont( aSoundfont );
		}

		protected InstrumentBase( List<SoundfontBase> aSoundfontList )
			: this()
		{
			AddSoundfont( aSoundfontList );
		}

		public void AddSoundfont( SoundfontBase aSoundfont )
		{
			for( int i = aSoundfont.soundinfo.lokey; i <= aSoundfont.soundinfo.hikey; i++ )
			{
				if( soundfontArray[i] == null )
				{
					soundfontArray[i] = aSoundfont;
				}
				else
				{
					Logger.LogNormal( "Already Exist." );
				}
			}
		}

		public void AddSoundfont( List<SoundfontBase> aSoundfontList )
		{
			for( int i = 0; i < aSoundfontList.Count; i++ )
			{
				SoundfontBase soundfont = aSoundfontList[i];

				AddSoundfont( soundfont );
			}
		}
	}
}
