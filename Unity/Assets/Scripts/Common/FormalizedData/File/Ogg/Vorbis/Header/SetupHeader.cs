using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis.Header
{
	public class Setup
	{
		public VorbisCodebook codebook;
		public VorbisTimeDomainTransforms timeDomainTransforms;
		public VorbisFloor floor;
		public VorbisResidue residue;
		public VorbisMapping mapping;
		public VorbisMode mode;

		public Setup( ByteArray aByteArray )
		{
			Read( aByteArray );
		}

		private void Read( ByteArray aByteArray )
		{
			Logger.LogWarning( "ReadVorbisSetupHeader" );

			string lId = aByteArray.ReadString( 6 );

			Logger.LogWarning( "ID:" + lId );

			codebook = new VorbisCodebook( aByteArray );
			timeDomainTransforms = new VorbisTimeDomainTransforms( aByteArray );
			floor = new VorbisFloor( aByteArray );
			residue = new VorbisResidue( aByteArray );
			mapping = new VorbisMapping( aByteArray );
			mode = new VorbisMode( aByteArray );
		}
	}
}
