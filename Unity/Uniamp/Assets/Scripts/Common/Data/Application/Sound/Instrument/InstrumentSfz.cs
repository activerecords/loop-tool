using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.Data.Standard.Sfz;

namespace Monoamp.Common.Data.Application.Sound
{
	public class InstrumentSfz : InstrumentBase
	{
		public override SoundfontBase[] soundfontArray{ get; set; }

		public InstrumentSfz( SfzFile aSfzFile )
			: base()
		{
			List<SoundfontBase> lSoundfontList = new List<SoundfontBase>();

			for( int i = 0; i < aSfzFile.sfzRegionList.Count; i++ )
			{
				SfzRegion lSfzData = aSfzFile.sfzRegionList[i];

				lSoundfontList.Add( new SoundfontSfz( lSfzData ) );
			}

			AddSoundfont( lSoundfontList );
		}
	}
}
