using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	/*
	public class PmdMaterialData : PmdDataAbstract
	{
		public Vector3 diffuseColor;	// dr, dg, db // 減衰色
		public float alpha;
		public float specularity;
		public Vector3 specularColor;	// sr, sg, sb // 光沢色
		public Vector3 mirrorColor;		// mr, mg, mb // 環境色(ambient)
		public byte toonIndex;			// toon??.bmp // 0.bmp:0xFF, 1(01).bmp:0x00 ・・・ 10.bmp:0x09
		public byte edgeFlag;			// 輪郭、影
		public UInt32 faceVertCount;	// 面頂点数 // インデックスに変換する場合は、材質0から順に加算
		public string textureFileName;	// テクスチャファイル名またはスフィアファイル名 // 20バイトぎりぎりまで使える(終端の0x00は無くても動く)

		public PmdMaterialData( ByteArray aByteArray )
		{
			diffuseColor = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			alpha = aByteArray.ReadSingle();
			specularity = aByteArray.ReadSingle();
			specularColor = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			mirrorColor = new Vector3( aByteArray.ReadSingle(), aByteArray.ReadSingle(), aByteArray.ReadSingle() );
			toonIndex = aByteArray.ReadByte();
			edgeFlag = aByteArray.ReadByte();
			faceVertCount = aByteArray.ReadUInt32();
			textureFileName = aByteArray.ReadShiftJisString( 20 );
		}
	}*/
}
