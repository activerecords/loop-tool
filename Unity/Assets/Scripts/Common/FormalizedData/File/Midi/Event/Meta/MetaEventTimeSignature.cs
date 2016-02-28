using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MetaEventTimeSignature : MetaEventBase
	{
		private Byte nn;	// 分子
		private Byte dd;	// 分母（2の(-dd)乗？、2分dd＝1、4分dd＝2、8分dd＝3？）
		private Byte cc;	// メトロノーム発音間隔設定、MIDIクロック数単位
		private Byte bb;	// 32分レベルビートサブディビジョンの状況、⇒4分音符1個の内部にある32分音符の個数 

		public MetaEventTimeSignature( int aDelta, Byte aType, ByteArray byteArray )
			: base( aDelta, aType )
		{
			int length = byteArray.ReadByte();

			if( length == 4 )
			{
				nn = byteArray.ReadByte();
				Byte shift = byteArray.ReadByte();
				cc = byteArray.ReadByte();
				bb = byteArray.ReadByte();

				for( int i = 0; i < shift; i++ )
				{
					dd *= 2;
				}
			}
			else
			{
				byteArray.AddPosition( 3 );
				Logger.LogException( new Exception() );
			}
		}

		public Byte GetN()
		{
			return nn;	// 分子
		}

		public Byte GetD()
		{
			return dd;	// 分母（2の(-dd)乗？、2分dd＝1、4分dd＝2、8分dd＝3？）
		}

		public Byte GetC()
		{
			return cc;	// メトロノーム発音間隔設定、MIDIクロック数単位
		}

		public Byte GetB()
		{
			return bb;	// 32分レベルビートサブディビジョンの状況、⇒4分音符1個の内部にある32分音符の個数 
		}
	}
}
