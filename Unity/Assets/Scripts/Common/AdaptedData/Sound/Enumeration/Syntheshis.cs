using System;

namespace Curan.Common.AdaptedData
{
	public static class Syntheshis
	{
		// MIDIの楽器定義.
		public enum InstrumentNumber
		{
			// Piano
			eAcousticPiano = 0,		// 1	0x00	アコースティックピアノ
			eBrightPiano,			// 2	0x01	ブライトピアノ
			eElectricGrandPiano,	// 3	0x02	エレクトリックグランドピアノ
			eHonkyTonkPiano,		// 4	0x03	ホンキートンクピアノ
			eElectricPiano,			// 5	0x04	エレクトリックピアノ
			eElectricPiano2,		// 6	0x05	エレクトリックピアノ2
			eHarpsichord,			// 7	0x06	ハープシコード
			eClavi,					// 8	0x07	クラビネット

			// Chromatic Percussion
			eCelesta,				// 9	0x08	チェレスタ
			eGlockenspiel,			// 10	0x09	グロッケンシュピール
			eMusicalBox,			// 11	0x0A	オルゴール
			eVibraphone,			// 12	0x0B	ヴィブラフォン
			eMarimba,				// 13	0x0C	マリンバ
			eXylophone,				// 14	0x0D	シロフォン
			eTubularBell,			// 15	0x0E	チューブラーベル
			eDulcimer,				// 16	0x0F	ダルシマー

			// Organ
			eDrawbarOrgan,			// 17	0x10	ドローバーオルガン
			ePercussiveOrgan,		// 18	0x11	パーカッシブオルガン
			eRockOrgan,				// 19	0x12	ロックオルガン
			eChurchOrgan,			// 20	0x13	チャーチオルガン
			eReedOrgan,				// 21	0x14	リードオルガン
			eAccordion,				// 22	0x15	アコーディオン
			eHarmonica,				// 23	0x16	ハーモニカ
			eTangoAccordion,		// 24	0x17	タンゴアコーディオン

			// Guitar
			eAcousticGuitarNylon,	// 25	0x18	アコースティックギター（ナイロン弦）
			eAcousticGuitarSteel,	// 26	0x19	アコースティックギター（スチール弦）
			eElectricGuitarJazz,	// 27	0x1A	ジャズギター
			eElectricGuitarClean,	// 28	0x1B	クリーンギター
			eElectricGuitarMuted,	// 29	0x1C	ミュートギター
			eOverdrivenGuitar,		// 30	0x1D	オーバードライブギター
			eDistortionGuitar,		// 31	0x1E	ディストーションギター
			eGuitarHarmonics,		// 32	0x1F	ギターハーモニクス

			// Bass
			eAcousticBass,			// 33	0x20	アコースティックベース
			eElectricBassFinger,	// 34	0x21	フィンガー・ベース
			eElectricBassPick,		// 35	0x22	ピック・ベース
			eFretlessBass,			// 36	0x23	フレットレスベース
			eSlapBass1,				// 37	0x24	スラップベース1
			eSlapBass2,				// 38	0x25	スラップベース2
			eSynthBass1,			// 39	0x26	シンセベース1
			eSynthBass2,			// 40	0x27	シンセベース2

			// Strings
			eViolin,				// 41	0x28	ヴァイオリン
			eViola,					// 42	0x29	ヴィオラ
			eCello,					// 43	0x2A	チェロ
			eDoublebass,			// 44	0x2B	コントラバス
			eTremoloStrings,		// 45	0x2C	トレモロ
			ePizzicatoStrings,		// 46	0x2D	ピッチカート
			eOrchestralHarp,		// 47	0x2E	ハープ
			eTimpani,				// 48	0x2F	ティンパニ

			// Ensemble
			eStringEnsemble1,		// 49	0x30	ストリングアンサンブル1
			eStringEnsemble2,		// 50	0x31	ストリングアンサンブル2
			eSynthStrings1,			// 51	0x32	シンセストリングス1
			eSynthStrings2,			// 52	0x33	シンセストリングス2
			eVoiceAahs,				// 53	0x34	声「あー」
			eVoiceOohs,				// 54	0x35	声「おー」
			eSynthVoice,			// 55	0x36	シンセヴォイス
			eOrchestraHit,			// 56	0x37	オーケストラヒット

			// Brass
			eTrumpet,				// 57	0x38	トランペット
			eTrombone,				// 58	0x39	トロンボーン
			eTuba,					// 59	0x3A	チューバ
			eMutedTrumpet,			// 60	0x3B	ミュートトランペット
			eFrenchHorn,			// 61	0x3C	フレンチ・ホルン
			eBrassSection,			// 62	0x3D	ブラスセクション
			eSynthBrass1,			// 63	0x3E	シンセブラス1
			eSynthBrass2,			// 64	0x3F	シンセブラス2

			// Reed
			eSopranoSax,			// 65	0x40	ソプラノサックス
			eAltoSax,				// 66	0x41	アルトサックス
			eTenorSax,				// 67	0x42	テナーサックス
			eBaritoneSax,			// 68	0x43	バリトンサックス
			eOboe,					// 69	0x44	オーボエ
			eEnglishHorn,			// 70	0x45	イングリッシュホルン
			eBassoon,				// 71	0x46	ファゴット
			eClarinet,				// 72	0x47	クラリネット

			// Pipe
			ePiccolo,				// 73	0x48	ピッコロ
			eFlute,					// 74	0x49	フルート
			eRecorder,				// 75	0x4A	リコーダー
			ePanFlute,				// 76	0x4B	パンフルート
			eBlownBottle,			// 77	0x4C	茶瓶
			eShakuhachi,			// 78	0x4D	尺八
			eWhistle,				// 79	0x4E	口笛
			eOcarina,				// 80	0x4F	オカリナ

			// Synth Lead
			eLead1Square,			// 81	0x50	矩形波
			eLead2Sawtooth,			// 82	0x51	ノコギリ波
			eLead3Calliope,			// 83	0x52	カリオペ
			eLead4Chiff,			// 84	0x53	チフ
			eLead5Charang,			// 85	0x54	チャランゴ
			eLead6Voice,			// 86	0x55	声
			eLead7Fifths,			// 87	0x56	フィフスズ
			eLead8BassLead,			// 88	0x57	バス+リード

			// Synth Pad
			ePad1Fantasia,			// 89	0x58	ファンタジア
			ePad2Warm,				// 90	0x59	ウォーム
			ePad3Polysynth,			// 91	0x5A	ポリシンセ
			ePad4Choir,				// 92	0x5B	クワイア
			ePad5Bowed,				// 93	0x5C	ボウ
			ePad6Metallic,			// 94	0x5D	メタリック
			ePad7Halo,				// 95	0x5E	ハロー
			ePad8Sweep,				// 96	0x5F	スウィープ

			// Synth Effects
			eFX1Rain,				// 97	0x60	雨
			eFX2Soundtrack,			// 98	0x61	サウンドトラック
			eFX3Crystal,			// 99	0x62	クリスタル
			eFX4Atmosphere,			// 100	0x63	アトモスフィア
			eFX5Brightness,			// 101	0x64	ブライトネス
			eFX6Goblins,			// 102	0x65	ゴブリン
			eFX7Echoes,				// 103	0x66	エコー
			eFX8SciFi,				// 104	0x67	サイファイ

			// Ethnic
			eSitar,					// 105	0x68	シタール
			eBanjo,					// 106	0x69	バンジョー
			eShamisen,				// 107	0x6A	三味線
			eKoto,					// 108	0x6B	琴
			eKalimba,				// 109	0x6C	カリンバ
			eBagpipe,				// 110	0x6D	バグパイプ
			eFiddle,				// 111	0x6E	フィドル
			eShanai,				// 112	0x6F	シャハナーイ

			// Percussive
			eTinkleBell,			// 113	0x70	ティンクルベル
			eAgogo,					// 114	0x71	アゴゴ
			eSteelDrums,			// 115	0x72	スチールドラム
			eWoodBlock,				// 116	0x73	ウッドブロック
			eTaikoDrum,				// 117	0x74	太鼓
			eMelodicTom,			// 118	0x75	メロディックタム
			eSynthDrum,				// 119	0x76	シンセドラム
			eReverseCymbal,			// 120	0x77	逆シンバル

			// Soundeffects
			eGuitarFretNoise,		// 121	0x78	ギターフレットノイズ
			eBreathNoise,			// 122	0x79	ブレスノイズ
			eSeasHore,				// 123	0x7A	海岸
			eBirdTweet,				// 124	0x7B	鳥の囀り
			eTelephoneRing,			// 125	0x7C	電話のベル
			eHelicopter,			// 126	0x7D	ヘリコプター
			eApplause,				// 127	0x7E	拍手
			eGunshot,				// 128	0x7F	銃声

			// Drums
			eDrums					// 129	0x80	ドラムセット
		};

		public static string[] instrumentName =
		{
			"1   アコースティックピアノ",
			"2   ブライトピアノ",
			"3   エレクトリックグランドピアノ",
			"4   ホンキートンクピアノ",
			"5   エレクトリックピアノ",
			"6   エレクトリックピアノ2",
			"7   ハープシコード",
			"8   クラビネット",

			// Chromatic Percussion
			"9   チェレスタ",
			"10  グロッケンシュピール",
			"11  オルゴール",
			"12  ヴィブラフォン",
			"13  マリンバ",
			"14  シロフォン",
			"15  チューブラーベル",
			"16  ダルシマー",

			// Organ
			"17  ドローバーオルガン",
			"18  パーカッシブオルガン",
			"19  ロックオルガン",
			"20  チャーチオルガン",
			"21  リードオルガン",
			"22  アコーディオン",
			"23  ハーモニカ",
			"24  タンゴアコーディオン",

			// Guitar
			"25  アコースティックギター（ナイロン弦）",
			"26  アコースティックギター（スチール弦）",
			"27  ジャズギター",
			"28  クリーンギター",
			"29  ミュートギター",
			"30  オーバードライブギター",
			"31  ディストーションギター",
			"32  ギターハーモニクス",

			// Bass
			"33  アコースティックベース",
			"34  フィンガー・ベース",
			"35  ピック・ベース",
			"36  フレットレスベース",
			"37  スラップベース1",
			"38  スラップベース2",
			"39  シンセベース1",
			"40  シンセベース2",

			// Strings
			"41  ヴァイオリン",
			"42  ヴィオラ",
			"43  チェロ",
			"44  コントラバス",
			"45  トレモロ",
			"46  ピッチカート",
			"47  ハープ",
			"48  ティンパニ",

			// Ensemble
			"49  ストリングアンサンブル1",
			"50  ストリングアンサンブル2",
			"51  シンセストリングス1",
			"52  シンセストリングス2",
			"53  声「あー」",
			"54  声「おー」",
			"55  シンセヴォイス",
			"56  オーケストラヒット",

			// Brass
			"57  トランペット",
			"58  トロンボーン",
			"59  チューバ",
			"60  ミュートトランペット",
			"61  フレンチ・ホルン",
			"62  ブラスセクション",
			"63  シンセブラス1",
			"64  シンセブラス2",

			// Reed
			"65  ソプラノサックス",
			"66  アルトサックス",
			"67  テナーサックス",
			"68  バリトンサックス",
			"69  オーボエ",
			"70  イングリッシュホルン",
			"71  ファゴット",
			"72  クラリネット",

			// Pipe
			"73  ピッコロ",
			"74  フルート",
			"75  リコーダー",
			"76  パンフルート",
			"77  茶瓶",
			"78  尺八",
			"79  口笛",
			"80  オカリナ",

			// Synth Lead
			"81  矩形波",
			"82  ノコギリ波",
			"83  カリオペ",
			"84  チフ",
			"85  チャランゴ",
			"86  声",
			"87  フィフスズ",
			"88  バス+リード",

			// Synth Pad
			"89  ファンタジア",
			"90  ウォーム",
			"91  ポリシンセ",
			"92  クワイア",
			"93  ボウ",
			"94  メタリック",
			"95  ハロー",
			"96  スウィープ",

			// Synth Effects
			"97  雨",
			"98  サウンドトラック",
			"99  クリスタル",
			"100 アトモスフィア",
			"101 ブライトネス",
			"102 ゴブリン",
			"103 エコー",
			"104 サイファイ",

			// Ethnic
			"105 シタール",
			"106 バンジョー",
			"107 三味線",
			"108 琴",
			"109 カリンバ",
			"110 バグパイプ",
			"111 フィドル",
			"112 シャハナーイ",

			// Percussive
			"113 ティンクルベル",
			"114 アゴゴ",
			"115 スチールドラム",
			"116 ウッドブロック",
			"117 太鼓",
			"118 メロディックタム",
			"119 シンセドラム",
			"120 逆シンバル",

			// Soundeffects
			"121 ギターフレットノイズ",
			"122 ブレスノイズ",
			"123 海岸",
			"124 鳥の囀り",
			"125 電話のベル",
			"126 ヘリコプター",
			"127 拍手",
			"128 銃声"
		};
	}
}
