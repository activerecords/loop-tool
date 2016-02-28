using System;
using System.Collections.Generic;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public abstract class InstrumentBase
	{
		public abstract SoundfontBase[] soundfontArray{ get; set; }

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
					Logger.Normal( "Already Exist." );
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
