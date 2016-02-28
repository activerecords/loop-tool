using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Mp3
{
	public interface IWindow
	{
		int GetRegion0Count();
		int GetRegion1Count();
	}

	public class Window1 : IWindow
	{
		private Byte tableSelectRegion0;	// Big Value領域の各リージョンのデコードで使用するハフマンテーブルのインデックス値.
		private Byte tableSelectRegion1;	// Big Value領域の各リージョンのデコードで使用するハフマンテーブルのインデックス値.
		private Byte tableSelectRegion2;	// Big Value領域の各リージョンのデコードで使用するハフマンテーブルのインデックス値.
		private Byte region0Count;			// Region 0領域のスケールファクターバンド数.
		private Byte region1Count;			// Region 1領域のスケールファクターバンド数.

		public Window1( BitArray bitArray )
		{
			tableSelectRegion0 = bitArray.ReadBits8( 5 );
			tableSelectRegion1 = bitArray.ReadBits8( 5 );
			tableSelectRegion2 = bitArray.ReadBits8( 5 );
			region0Count = bitArray.ReadBits8( 4 );
			region1Count = bitArray.ReadBits8( 4 );

			Logger.LogNormal( "TableSelectRegion0:" + tableSelectRegion0 );
			Logger.LogNormal( "TableSelectRegion1:" + tableSelectRegion1 );
			Logger.LogNormal( "TableSelectRegion2:" + tableSelectRegion2 );
			Logger.LogNormal( "Region0Count:" + region0Count );
			Logger.LogNormal( "Region1Count:" + region1Count );
		}

		public int GetRegion0Count()
		{
			return ( int )region0Count;
		}

		public int GetRegion1Count()
		{
			return ( int )region1Count;
		}
	}

	public class Window2 : IWindow
	{
		private Byte blockType;				// グラニュールのブロックタイプ.
		private Byte mixedBlockFlag;		// チャネルデータがミックスブロック構造である事を示す.
		private Byte tableSelectRegion0;	// [Big Values]パート内の各リージョンのデコードで使用するハフマンテーブルのインデックス値.
		private Byte tableSelectRegion1;	// [Big Values]パート内の各リージョンのデコードで使用するハフマンテーブルのインデックス値.
		private Byte subBlockGainWindow0;	// 各サブブロック（ショートブロックの各ウィンドウ）のゲイン.
		private Byte subBlockGainWindow1;	// 各サブブロック（ショートブロックの各ウィンドウ）のゲイン.
		private Byte subBlockGainWindow2;	// 各サブブロック（ショートブロックの各ウィンドウ）のゲイン.

		private Window2()
		{

		}

		public Window2( BitArray bitArray )
		{
			blockType = bitArray.ReadBits8( 2 );
			mixedBlockFlag = bitArray.ReadBits8( 1 );
			tableSelectRegion0 = bitArray.ReadBits8( 5 );
			tableSelectRegion1 = bitArray.ReadBits8( 5 );
			subBlockGainWindow0 = bitArray.ReadBits8( 3 );
			subBlockGainWindow1 = bitArray.ReadBits8( 3 );
			subBlockGainWindow2 = bitArray.ReadBits8( 3 );

			Logger.LogNormal( "BlockType:" + blockType );
			Logger.LogNormal( "MixedBlockFlag:" + mixedBlockFlag );
			Logger.LogNormal( "TableSelectRegion0:" + tableSelectRegion0 );
			Logger.LogNormal( "TableSelectRegion1:" + tableSelectRegion1 );
			Logger.LogNormal( "SubBlockGainWindow0:" + subBlockGainWindow0 );
			Logger.LogNormal( "SubBlockGainWindow1:" + subBlockGainWindow1 );
			Logger.LogNormal( "SubBlockGainWindow2:" + subBlockGainWindow2 );
		}

		public int GetRegion0Count()
		{
			return 0;
		}

		public int GetRegion1Count()
		{
			return 0;
		}
	}
}
