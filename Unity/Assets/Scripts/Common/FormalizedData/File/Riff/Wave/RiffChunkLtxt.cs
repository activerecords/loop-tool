using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff.Wave
{
	public class RiffChunkLtxt : RiffChunk
	{
		public const string ID = "ltxt";

		public readonly UInt32 name;
		public readonly UInt32 sampleLength;
		public readonly UInt32 purpose;
		public readonly UInt16 country;
		public readonly UInt16 language;
		public readonly UInt16 dialect;
		public readonly UInt16 codePage;
		public readonly Byte[] data;

		public RiffChunkLtxt( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			name = aByteArray.ReadUInt32();
			sampleLength = aByteArray.ReadUInt32();
			purpose = aByteArray.ReadUInt32();
			country = aByteArray.ReadUInt16();
			language = aByteArray.ReadUInt16();
			dialect = aByteArray.ReadUInt16();
			codePage = aByteArray.ReadUInt16();
			data = aByteArray.ReadBytes( size - 20 );

			informationList.Add( "    Name:" + name );
			informationList.Add( "    Sample Length:" + sampleLength );
			informationList.Add( "    Purpose:" + purpose );
			informationList.Add( "    Country:" + country );
			informationList.Add( "    Language:" + language );
			informationList.Add( "    Dialect:" + dialect );
			informationList.Add( "    CodePage:" + codePage );
			informationList.Add( "    Data:" + data.Length );
		}
	}
}
