using System;

using Curan.Common.FormalizedData.File.Midi;
using Curan.Common.ApplicationComponent.Sound.Synthesizer;
using Curan.Utility;

namespace Curan.Common.ApplicationComponent.Sound.Midi
{
	public class MidiEventExecutorSystemExclusive : MidiEventExecutorBase
	{
		private int length;
		private byte[] dataArray;

		public MidiEventExecutorSystemExclusive( MidiEventSystemExclusive aSysExEvent )
			: base( aSysExEvent )
		{
			MidiEventSystemExclusive lSystemExclusiveEvent = ( MidiEventSystemExclusive )midiEvent;

			length = lSystemExclusiveEvent.GetLength();
			dataArray = lSystemExclusiveEvent.GetDataArray();
		}

		public override void Execute( MidiSynthesizer aMidiSynthesizer, int aDivision, double aBpm )
		{
			switch( midiEvent.GetState() )
			{
			case 0xF0:
				ExecuteSystemExclusive0Event( aMidiSynthesizer );
				break;

			case 0xF7:
				break;

			default:
				Logger.LogError( "Undefined Ex Event:" + midiEvent.GetState() );
				Logger.LogException( new Exception() );
				break;
			}
		}

		public void ExecuteSystemExclusive0Event( MidiSynthesizer aMidiStatusArray )
		{
			switch( dataArray[0] )
			{
			case 0x40:
				Logger.LogError( "Kawai" );
				break;

			case 0x41:
				Logger.LogNormal( "Roland" );
				SysExRoland( aMidiStatusArray );
				break;

			case 0x42:
				Logger.LogError( "Korg" );
				break;

			case 0x43:
				Logger.LogNormal( "Yamaha" );
				SysExYamaha( aMidiStatusArray );
				break;

			case 0x44:
				Logger.LogError( "Casio" );
				break;

			case 0x45:
				Logger.LogError( "Undefined Maker" );
				break;

			case 0x46:
				Logger.LogError( "Kamiya" );
				break;

			case 0x7E:
				Logger.LogNormal( "Non Realtime" );
				SysExNonRealtime( aMidiStatusArray );
				break;

			case 0x7F:
				Logger.LogNormal( "Realtime" );
				SysExRealtime( aMidiStatusArray );
				break;

			default:
				Logger.LogError( "Undefined Message" );
				break;
			}
		}

		private void SysExRoland( MidiSynthesizer aMidiStatusArray )
		{
			if( dataArray[1] == 0x10 && dataArray[2] == 0x42 && dataArray[3] == 0x12 && dataArray[4] == 0x40 && dataArray[5] == 0x00 && dataArray[6] == 0x7F && dataArray[7] == 0x00 && dataArray[8] == 0x41 && dataArray[9] == 0xF7 )
			{
				Logger.LogNormal( "GS Mode On" );
			}
			else
			{
				if( dataArray[1] == 0x10 )
				{
					Logger.LogWarning( "デバイスID:" + dataArray[1].ToString( "X2" ) );
					Logger.LogWarning( "モデルID:" + dataArray[2].ToString( "X2" ) );
					Logger.LogWarning( "コマンドID:" + dataArray[3].ToString( "X2" ) );
					Logger.LogWarning( "アドレス:" + dataArray[4].ToString( "X2" ) + dataArray[5].ToString( "X2" ) + dataArray[6].ToString( "X2" ) );

					for( int i = 0; i < length - 9; i++ )
					{
						Logger.LogWarning( "データ" + i + ":" + dataArray[7 + i].ToString( "X2" ) );
					}

					Logger.LogWarning( "チェックサム:" + dataArray[length - 2].ToString( "X2" ) );
					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[length - 1].ToString( "X2" ) );
				}
				else
				{
					Logger.LogError( "Undefined Roland Message" );

					for( int i = 0; i < length; i++ )
					{
						Logger.LogWarning( dataArray[i].ToString( "X2" ) );
					}
				}
			}
		}

		private void SysExYamaha( MidiSynthesizer aMidiStatusArray )
		{
			if( dataArray[1] == 0x10 && dataArray[2] == 0x4C && dataArray[3] == 0x00 && dataArray[4] == 0x00 && dataArray[5] == 0x7E && dataArray[6] == 0x00 && dataArray[7] == 0xF7 )
			{
				Logger.LogNormal( "XG Mode On" );
			}
			else
			{
				if( dataArray[1] == 0x10 )
				{
					Logger.LogWarning( "XGネイティブ・パラメーター・チェンジ" );
					Logger.LogWarning( "XGモデルID:" + dataArray[2].ToString( "X2" ) );
					Logger.LogWarning( "アドレス:" + dataArray[3].ToString( "X2" ) + dataArray[4].ToString( "X2" ) + dataArray[5].ToString( "X2" ) );

					for( int i = 0; i < length - 7; i++ )
					{
						Logger.LogWarning( "データ" + i + ":" + dataArray[6 + i].ToString( "X2" ) );
					}

					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[length - 1].ToString( "X2" ) );
				}
				else
				{
					Logger.LogError( "Undefined Yamaha Message" );

					for( int i = 0; i < length; i++ )
					{
						Logger.LogWarning( dataArray[i].ToString( "X2" ) );
					}
				}
			}
		}

		private void SysExNonRealtime( MidiSynthesizer aStateMidi )
		{
			Logger.LogNormal( "Device ID:" + dataArray[1].ToString( "X2" ) );

			if( dataArray[2] == 0x09 )
			{
				Logger.LogNormal( "GM Message" );

				switch( dataArray[3] )
				{
				case 0x01:
					aStateMidi.Reset();
					Logger.LogNormal( "GM1 On" );
					break;

				case 0x02:
					aStateMidi.Reset();
					Logger.LogNormal( "GM Off" );
					break;

				case 0x03:
					Logger.LogNormal( "GM2 On" );
					break;

				default:
					Logger.LogError( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

					for( int i = 0; i < length; i++ )
					{
						Logger.LogWarning( dataArray[i].ToString( "X2" ) );
					}
					break;
				}
			}
			else
			{
				Logger.LogError( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

				for( int i = 0; i < length; i++ )
				{
					Logger.LogWarning( dataArray[i].ToString( "X2" ) );
				}
			}
		}

		private void SysExRealtime( MidiSynthesizer aMidiStatusArray )
		{
			Logger.LogNormal( "Device ID:" + dataArray[1].ToString( "X2" ) );

			switch( dataArray[2] )
			{
			case 0x04:
				Logger.LogWarning( "デバイス・コントロール" );

				switch( dataArray[3] )
				{
				case 0x01:
					Logger.LogWarning( "マスターボリューム" );
					Logger.LogWarning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.LogWarning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structVolume.SetVolume( dataArray[5] );
					break;

				case 0x02:
					Logger.LogWarning( "マスターバランス" );
					Logger.LogWarning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.LogWarning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structVolume.SetPan( dataArray[5] );
					break;

				case 0x03:
					Logger.LogWarning( "マスター・ファインチューニング" );
					Logger.LogWarning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.LogWarning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structPitch.SetCoarseTuningMsb( dataArray[5] );
					break;

				case 0x04:
					Logger.LogWarning( "マスター・コースチューニング" );
					Logger.LogWarning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.LogWarning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.LogWarning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structPitch.SetCoarseTuningMsb( dataArray[5] );
					break;

				case 0x05:
					Logger.LogNormal( "グローバル・パラメーター・コントロール" );
					Logger.LogWarning( "スロット・パスの長さ:" + dataArray[4].ToString( "X2" ) );
					Logger.LogWarning( "パラメーターID のサイズ:" + dataArray[5].ToString( "X2" ) );
					Logger.LogWarning( "パラメーター値のサイズ:" + dataArray[6].ToString( "X2" ) );
					Logger.LogNormal( "スロット・パスの MSB:" + dataArray[7].ToString( "X2" ) );
					Logger.LogNormal( "スロット・パスの LSB:" + dataArray[8].ToString( "X2" ) );
					Logger.LogNormal( "変更するパラメーター:" + dataArray[9].ToString( "X2" ) );
					Logger.LogNormal( "パラメーター値:" + dataArray[10].ToString( "X2" ) );
					Logger.LogNormal( "エンド･オブ･エクスクルーシブ:" + dataArray[11].ToString( "X2" ) );
					break;

				default:
					Logger.LogError( "Undefined Message:" + dataArray[3].ToString( "X2" ) );
					break;
				}
				break;

			case 0x09:
				Logger.LogNormal( "コントローラー・ディスティネーション・セッティング" );

				switch( dataArray[3] )
				{
				case 0x01:
					Logger.LogNormal( "コントローラー・タイプ01（チャンネル・プレッシャー）" );
					Logger.LogNormal( "MIDI チャンネル１～16:" + dataArray[4].ToString( "X2" ) );
					break;

				case 0x03:
					Logger.LogNormal( "コントローラー・タイプ03（コントロール・チェンジ）" );
					Logger.LogNormal( "MIDI チャンネル１～16:" + dataArray[4].ToString( "X2" ) );
					Logger.LogNormal( "コントローラ番号:" + dataArray[5].ToString( "X2" ) );
					break;

				default:
					Logger.LogError( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

					for( int i = 0; i < length; i++ )
					{
						Logger.LogWarning( dataArray[i].ToString( "X2" ) );
					}
					break;
				}
				break;

			default:
				Logger.LogError( "Undefined Message:" + dataArray[3].ToString( "X2" ) );
				for( int i = 0; i < length; i++ )
				{
					Logger.LogWarning( dataArray[i].ToString( "X2" ) );
				}
				break;
			}
		}
	}
}
