using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Adx
{
	public class AdxHeader
	{
		private UInt64 byteStartData;	// +0 1l データの開始位置等
										// データの開始位置(x)-4+0x80000000になってます。
		private Byte[] nul;				// +4 3B
		private Byte channelLength;		// +7 1B モノラル/ステレオ
										// 1ならモノラル,2ならステレオです。
		private UInt32 sampleRate;		// +8 1l 周波数
		private UInt32 sampleLength;	// +0xc 1l 総サンプル数
		private UInt32 a;				// +0x10 1l
		private UInt32 b;				// +0x14 1l
		private UInt32 c;				// +0x18 1l
		private UInt32 sampleLoopStart;	// +0x1c 1l ループ先頭までのサンプル数
										// ステレオの場合左右合わせて1サンプルと勘定されてます。
		private UInt32 byteLoopStart;	// +0x20 1l ループ先頭位置
										// バイト単位のループの先頭位置です。ヘッダのギャップを使ってセクタの先頭にくるよう、調整されてるみたいです。
		private UInt32 sampleLoopEnd;	// +0x24 1l ループ最後までのサンプル数
		private UInt32 byteLoopEnd;		// +0x28 1l ループ最終のブロックの位置
										// バイト単位です。ステレオの場合は残り2ブロックの所を指してます。
		private Byte[] d;				// +0x2c ?B
		private Byte[] e;				// +x-6 6B "(c)CRI"
		private Byte[] data;				// +x データ開始位置

		public AdxHeader( ByteArray aByteArray )
		{
			byteStartData = ( UInt64 )aByteArray.ReadUInt32() + ( UInt64 )4 - ( UInt64 )0x80000000;
			nul = aByteArray.ReadBytes( 3 );
			channelLength = aByteArray.ReadByte();
			sampleRate = aByteArray.ReadUInt32();
			sampleLength = aByteArray.ReadUInt32();

			a = aByteArray.ReadUInt32();
			b = aByteArray.ReadUInt32();
			c = aByteArray.ReadUInt32();
			sampleLoopStart = aByteArray.ReadUInt32();
			byteLoopStart = aByteArray.ReadUInt32();
			sampleLoopEnd = aByteArray.ReadUInt32();
			byteLoopEnd = aByteArray.ReadUInt32();
			d = aByteArray.ReadBytes( ( UInt32 )( byteStartData - 0x2C - 6 ) );
			e = aByteArray.ReadBytes( 6 );
			//data = byteArray.ReadBytes( 4 );

			/*
			Logger.LogWarning( "byteStartData:" + byteStartData );
			//Logger.LogWarning( "nul" + nul.ToString( "X4" ) );
			Logger.LogWarning( "channelLength:" + channelLength );
			Logger.LogWarning( "sampleRate:" + sampleRate );
			Logger.LogWarning( "sampleLength:" + sampleLength );

			Logger.LogWarning( "a:" + a );
			Logger.LogWarning( "b:" + b );
			Logger.LogWarning( "c:" + c );
			Logger.LogWarning( "sampleLoopStart:" + sampleLoopStart );
			Logger.LogWarning( "byteLoopStart:" + byteLoopStart );
			Logger.LogWarning( "sampleLoopEnd:" + sampleLoopEnd );
			Logger.LogWarning( "byteLoopEnd:" + byteLoopEnd );
			//Logger.LogWarning( "d:" + d );
			//Logger.LogWarning( "e:" + e );
			*/
		}

		public UInt32 GetByteStartData()
		{
			return ( UInt32 )byteStartData;
		}

		public Byte GetChannelLength()
		{
			return channelLength;
		}

		public UInt32 GetSampleRate()
		{
			return sampleRate;
		}

		public UInt32 GetSampleLength()
		{
			return sampleLength;
		}

		public UInt32 GetSampleLoopStart()
		{
			return sampleLoopStart;
		}

		public UInt32 GetSampleLoopEnd()
		{
			return sampleLoopEnd;
		}
	}
}
