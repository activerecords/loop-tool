using System;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Vmd
{
	public class VmdFile
	{
		public VmdHeader header;
		public VmdMotionChunk chunkMotion;
		public VmdSkinChunk chunkSkin;
		public VmdCameraChunk chunkCamera;
		public VmdLightChunk chunkLight;
		public VmdSelfShadowChunk chunkSelfShadow;

		public VmdFile( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayLittle( aStream );

			header = new VmdHeader( lByteArray );
			chunkMotion = new VmdMotionChunk( lByteArray );
			//chunkSkin = new VmdSkinChunk( lByteArray );
			//chunkCamera = new VmdCameraChunk( lByteArray );
			//chunkLight = new VmdLightChunk( lByteArray );
			//vmdSelfShadowCount = new VmdSelfShadowCount( lByteArray );
		}
	}
}
