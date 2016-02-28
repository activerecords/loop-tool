using System;
using System.Collections.Generic;
using System.IO;

using Monoamp.Common.Data.Standard.Bnk;
using Monoamp.Common.Data.Standard.Sfz;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public class BankSfz : ABank
	{
		public BankSfz( BnkFile aBnkFile )
			: base()
		{
			for( int i = 0; i < aBnkFile.pathSfzArray.Length; i++ )
			{
				if( aBnkFile.pathSfzArray[i] != null )
				{
					using( FileStream uFileStream = new FileStream( aBnkFile.pathSfzArray[i], FileMode.Open, FileAccess.Read ) )
					{
						Logger.Debug( aBnkFile.pathSfzArray[i] );

						SfzFile lSfzFile = new SfzFile( uFileStream );

						instrumentArray[i] = new InstrumentSfz( lSfzFile );
					}
				}
			}
		}
	}
}
