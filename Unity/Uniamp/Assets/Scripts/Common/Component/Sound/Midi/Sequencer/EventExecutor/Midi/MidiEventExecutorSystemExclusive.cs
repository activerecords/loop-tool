using System;

using Monoamp.Common.Data.Standard.Midi;
using Monoamp.Common.Component.Application.Sound;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Sound.Midi
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
				Logger.Error( "Undefined Ex Event:" + midiEvent.GetState() );
				Logger.Exception( new Exception() );
				break;
			}
		}

		public void ExecuteSystemExclusive0Event( MidiSynthesizer aMidiStatusArray )
		{
			switch( dataArray[0] )
			{
			case 0x40:
				Logger.Error( "Kawai" );
				break;

			case 0x41:
				Logger.Normal( "Roland" );
				SysExRoland( aMidiStatusArray );
				break;

			case 0x42:
				Logger.Error( "Korg" );
				break;

			case 0x43:
				Logger.Normal( "Yamaha" );
				SysExYamaha( aMidiStatusArray );
				break;

			case 0x44:
				Logger.Error( "Casio" );
				break;

			case 0x45:
				Logger.Error( "Undefined Maker" );
				break;

			case 0x46:
				Logger.Error( "Kamiya" );
				break;

			case 0x7E:
				Logger.Normal( "Non Realtime" );
				SysExNonRealtime( aMidiStatusArray );
				break;

			case 0x7F:
				Logger.Normal( "Realtime" );
				SysExRealtime( aMidiStatusArray );
				break;

			default:
				Logger.Error( "Undefined Message" );
				break;
			}
		}

		private void SysExRoland( MidiSynthesizer aMidiStatusArray )
		{
			if( dataArray[1] == 0x10 && dataArray[2] == 0x42 && dataArray[3] == 0x12 && dataArray[4] == 0x40 && dataArray[5] == 0x00 && dataArray[6] == 0x7F && dataArray[7] == 0x00 && dataArray[8] == 0x41 && dataArray[9] == 0xF7 )
			{
				Logger.Normal( "GS Mode On" );
			}
			else
			{
				if( dataArray[1] == 0x10 )
				{
					Logger.Warning( "デバイスID:" + dataArray[1].ToString( "X2" ) );
					Logger.Warning( "モデルID:" + dataArray[2].ToString( "X2" ) );
					Logger.Warning( "コマンドID:" + dataArray[3].ToString( "X2" ) );
					Logger.Warning( "アドレス:" + dataArray[4].ToString( "X2" ) + dataArray[5].ToString( "X2" ) + dataArray[6].ToString( "X2" ) );

					for( int i = 0; i < length - 9; i++ )
					{
						Logger.Warning( "データ" + i + ":" + dataArray[7 + i].ToString( "X2" ) );
					}

					Logger.Warning( "チェックサム:" + dataArray[length - 2].ToString( "X2" ) );
					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[length - 1].ToString( "X2" ) );
				}
				else
				{
					Logger.Error( "Undefined Roland Message" );

					for( int i = 0; i < length; i++ )
					{
						Logger.Warning( dataArray[i].ToString( "X2" ) );
					}
				}
			}
		}

		private void SysExYamaha( MidiSynthesizer aMidiStatusArray )
		{
			if( dataArray[1] == 0x10 && dataArray[2] == 0x4C && dataArray[3] == 0x00 && dataArray[4] == 0x00 && dataArray[5] == 0x7E && dataArray[6] == 0x00 && dataArray[7] == 0xF7 )
			{
				Logger.Normal( "XG Mode On" );
			}
			else
			{
				if( dataArray[1] == 0x10 )
				{
					Logger.Warning( "XGネイティブ・パラメーター・チェンジ" );
					Logger.Warning( "XGモデルID:" + dataArray[2].ToString( "X2" ) );
					Logger.Warning( "アドレス:" + dataArray[3].ToString( "X2" ) + dataArray[4].ToString( "X2" ) + dataArray[5].ToString( "X2" ) );

					for( int i = 0; i < length - 7; i++ )
					{
						Logger.Warning( "データ" + i + ":" + dataArray[6 + i].ToString( "X2" ) );
					}

					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[length - 1].ToString( "X2" ) );
				}
				else
				{
					Logger.Error( "Undefined Yamaha Message" );

					for( int i = 0; i < length; i++ )
					{
						Logger.Warning( dataArray[i].ToString( "X2" ) );
					}
				}
			}
		}

		private void SysExNonRealtime( MidiSynthesizer aStateMidi )
		{
			Logger.Normal( "Device ID:" + dataArray[1].ToString( "X2" ) );

			if( dataArray[2] == 0x09 )
			{
				Logger.Normal( "GM Message" );

				switch( dataArray[3] )
				{
				case 0x01:
					aStateMidi.Reset();
					Logger.Normal( "GM1 On" );
					break;

				case 0x02:
					aStateMidi.Reset();
					Logger.Normal( "GM Off" );
					break;

				case 0x03:
					Logger.Normal( "GM2 On" );
					break;

				default:
					Logger.Error( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

					for( int i = 0; i < length; i++ )
					{
						Logger.Warning( dataArray[i].ToString( "X2" ) );
					}
					break;
				}
			}
			else
			{
				Logger.Error( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

				for( int i = 0; i < length; i++ )
				{
					Logger.Warning( dataArray[i].ToString( "X2" ) );
				}
			}
		}

		private void SysExRealtime( MidiSynthesizer aMidiStatusArray )
		{
			Logger.Normal( "Device ID:" + dataArray[1].ToString( "X2" ) );

			switch( dataArray[2] )
			{
			case 0x04:
				Logger.Warning( "デバイス・コントロール" );

				switch( dataArray[3] )
				{
				case 0x01:
					Logger.Warning( "マスターボリューム" );
					Logger.Warning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.Warning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structVolume.SetVolume( dataArray[5] );
					break;

				case 0x02:
					Logger.Warning( "マスターバランス" );
					Logger.Warning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.Warning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structVolume.SetPan( dataArray[5] );
					break;

				case 0x03:
					Logger.Warning( "マスター・ファインチューニング" );
					Logger.Warning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.Warning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structPitch.SetCoarseTuningMsb( dataArray[5] );
					break;

				case 0x04:
					Logger.Warning( "マスター・コースチューニング" );
					Logger.Warning( "00:" + dataArray[4].ToString( "X2" ) );
					Logger.Warning( "パラメーター値:" + dataArray[5].ToString( "X2" ) );
					Logger.Warning( "エンド･オブ･エクスクルーシブ:" + dataArray[6].ToString( "X2" ) );
					// To Be Fixed.
					//MidiStatus.midiStatusMaster.structPitch.SetCoarseTuningMsb( dataArray[5] );
					break;

				case 0x05:
					Logger.Normal( "グローバル・パラメーター・コントロール" );
					Logger.Warning( "スロット・パスの長さ:" + dataArray[4].ToString( "X2" ) );
					Logger.Warning( "パラメーターID のサイズ:" + dataArray[5].ToString( "X2" ) );
					Logger.Warning( "パラメーター値のサイズ:" + dataArray[6].ToString( "X2" ) );
					Logger.Normal( "スロット・パスの MSB:" + dataArray[7].ToString( "X2" ) );
					Logger.Normal( "スロット・パスの LSB:" + dataArray[8].ToString( "X2" ) );
					Logger.Normal( "変更するパラメーター:" + dataArray[9].ToString( "X2" ) );
					Logger.Normal( "パラメーター値:" + dataArray[10].ToString( "X2" ) );
					Logger.Normal( "エンド･オブ･エクスクルーシブ:" + dataArray[11].ToString( "X2" ) );
					break;

				default:
					Logger.Error( "Undefined Message:" + dataArray[3].ToString( "X2" ) );
					break;
				}
				break;

			case 0x09:
				Logger.Normal( "コントローラー・ディスティネーション・セッティング" );

				switch( dataArray[3] )
				{
				case 0x01:
					Logger.Normal( "コントローラー・タイプ01（チャンネル・プレッシャー）" );
					Logger.Normal( "MIDI チャンネル１～16:" + dataArray[4].ToString( "X2" ) );
					break;

				case 0x03:
					Logger.Normal( "コントローラー・タイプ03（コントロール・チェンジ）" );
					Logger.Normal( "MIDI チャンネル１～16:" + dataArray[4].ToString( "X2" ) );
					Logger.Normal( "コントローラ番号:" + dataArray[5].ToString( "X2" ) );
					break;

				default:
					Logger.Error( "Undefined Message:" + dataArray[3].ToString( "X2" ) );

					for( int i = 0; i < length; i++ )
					{
						Logger.Warning( dataArray[i].ToString( "X2" ) );
					}
					break;
				}
				break;

			default:
				Logger.Error( "Undefined Message:" + dataArray[3].ToString( "X2" ) );
				for( int i = 0; i < length; i++ )
				{
					Logger.Warning( dataArray[i].ToString( "X2" ) );
				}
				break;
			}
		}
	}
}
