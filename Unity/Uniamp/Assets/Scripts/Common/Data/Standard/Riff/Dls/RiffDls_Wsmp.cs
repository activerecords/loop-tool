using System;
using System.Collections.Generic;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Riff.Dls
{
	public class RiffDls_Wsmp : RiffChunk
	{
		public const string ID = "wsmp";

		public readonly UInt32 lsize;
		public readonly UInt16 unityNote;
		public readonly Int16 fineTune;
		public readonly Int32 attenuation;
		public readonly UInt32 options;
		public readonly UInt32 sampleLoops;
		public readonly WaveSampleLoop[] waveSampleLoop;

		public RiffDls_Wsmp( string aId, UInt32 aSize, AByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			lsize = aByteArray.ReadUInt32();
			unityNote = aByteArray.ReadUInt16();
			fineTune = aByteArray.ReadInt16();
			attenuation = aByteArray.ReadInt32();
			options = aByteArray.ReadUInt32();
			sampleLoops = aByteArray.ReadUInt32();

			informationList.Add( "Size:" + lsize );
			informationList.Add( "Unity Note:" + unityNote );
			informationList.Add( "Fine Tune:" + fineTune );
			informationList.Add( "Attenuation:" + attenuation );
			informationList.Add( "Options:" + options );
			informationList.Add( "Sample Loops:" + sampleLoops );

			waveSampleLoop = new WaveSampleLoop[sampleLoops];

			for( int i = 0; i < sampleLoops; i++ )
			{
				informationList.Add( "Wave Sample Loop:" );

				waveSampleLoop[i] = new WaveSampleLoop( aByteArray, informationList );
			}
		}

		public UInt32 GetLoopType()
		{
			for( int i = 0; i < sampleLoops; i++ )
			{
				return waveSampleLoop[i].loopStart;
			}

			return 0;
		}

		public UInt32 GetLoopStart()
		{
			for( int i = 0; i < sampleLoops; i++ )
			{
				return waveSampleLoop[i].loopStart;
			}

			return 0;
		}

		public UInt32 GetLoopLength()
		{
			for( int i = 0; i < sampleLoops; i++ )
			{
				return waveSampleLoop[i].loopLength;
			}

			return 0;
		}

		public override void WriteByteArray( AByteArray aByteArrayRead, AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( Size );
			aByteArray.WriteUInt16( unityNote );
			aByteArray.WriteUInt16( ( UInt16 )fineTune );
			aByteArray.WriteUInt32( ( UInt32 )attenuation );
			aByteArray.WriteUInt32( options );
			aByteArray.WriteUInt32( sampleLoops );
			aByteArray.WriteUInt16( unityNote );

			for( int i = 0; i < sampleLoops; i++ )
			{
				waveSampleLoop[i].WriteByteArray( aByteArray );
			}
		}
	}

	public class WaveSampleLoop
	{
		public readonly UInt32 size;
		public readonly UInt32 loopType;
		public readonly UInt32 loopStart;
		public readonly UInt32 loopLength;

		public WaveSampleLoop( AByteArray aByteArray, List<string> aInformationList )
		{
			size = aByteArray.ReadUInt32();
			loopType = aByteArray.ReadUInt32();
			loopStart = aByteArray.ReadUInt32();
			loopLength = aByteArray.ReadUInt32();

			aInformationList.Add( "\tSize:" + size );
			aInformationList.Add( "\tLoop Type:" + loopType );
			aInformationList.Add( "\tLoop Start:" + loopStart );
			aInformationList.Add( "\tLoop Length:" + loopLength );
		}

		public void WriteByteArray( AByteArray aByteArray )
		{
			aByteArray.WriteUInt32( loopType );
			aByteArray.WriteUInt32( loopStart );
			aByteArray.WriteUInt32( loopLength );
		}
	}
}
