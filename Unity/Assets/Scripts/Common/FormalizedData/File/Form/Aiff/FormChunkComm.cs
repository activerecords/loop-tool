using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Form.Aiff
{
	public class FormChunkComm : FormChunk
	{
		public const string ID = "COMM";

		public readonly UInt16 numberOfChannels;
		public readonly UInt32 numberOfFrames;
		public readonly UInt16 bitsPerSamples;
		public readonly Double sampleRate;

		public FormChunkComm( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			numberOfChannels = aByteArray.ReadUInt16();
			numberOfFrames = aByteArray.ReadUInt32();
			bitsPerSamples = aByteArray.ReadUInt16();
			sampleRate = aByteArray.ReadExtendedFloatPoint();

			informationList.Add( "Number Of Channels:" + numberOfChannels );
			informationList.Add( "Number Of Frames:" + numberOfFrames );
			informationList.Add( "Bits Per Samples:" + bitsPerSamples );
			informationList.Add( "Sample Rate:" + sampleRate );
		}
	}
}
