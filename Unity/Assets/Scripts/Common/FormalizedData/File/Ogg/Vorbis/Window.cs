using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class Window
	{
		public int n;

		public int samples;

		public int left_window_start;
		public int left_window_end;
		public int left_n;

		public int right_window_start;
		public int right_window_end;
		public int right_n;

		public double[] windowArray;

		public Window( ByteArray aByteArray, Byte aBlockFlag, int aBlockSize0, int aBlockSize1 )
		{
			//Logger.LogError( "Mode Block Flag:" + lModeBlockFlag );

			Byte lPreviousWindowFlag = 0x00;
			Byte lNextWindowFlag = 0x00;

			if( aBlockFlag == 0x00 )
			{
				n = aBlockSize0;
			}
			else
			{
				n = aBlockSize1;

				lPreviousWindowFlag = aByteArray.ReadBitsAsByte( 1 );
				lNextWindowFlag = aByteArray.ReadBitsAsByte( 1 );

				//Logger.LogError( "Previous Window Flag, Next Window Flag:" + lPreviousWindowFlag + ", " + lNextWindowFlag );
			}

			int lWindowCenter = n / 2;

			if( aBlockFlag == 0x01 && lPreviousWindowFlag == 0x00 )
			{
				left_window_start = aBlockSize1 / 4 - aBlockSize0 / 4;
				left_window_end = aBlockSize1 / 4 + aBlockSize0 / 4;
				left_n = aBlockSize0 / 2;
			}
			else
			{
				left_window_start = 0;
				left_window_end = lWindowCenter;
				left_n = n / 2;
			}

			if( aBlockFlag == 0x01 && lNextWindowFlag == 0x00 )
			{
				right_window_start = aBlockSize1 * 3 / 4 - aBlockSize0 / 4;
				right_window_end = aBlockSize1 * 3 / 4 + aBlockSize0 / 4;
				right_n = aBlockSize0 / 2;
			}
			else
			{
				right_window_start = lWindowCenter;
				right_window_end = n;
				right_n = n / 2;
			}

			windowArray = new double[n];

			for( int i = 0; i < left_window_start; i++ )
			{
				windowArray[i] = 0;
			}

			for( int i = left_window_start; i < left_window_end; i++ )
			{
				double w = Math.Sin( ( i - left_window_start + 0.5d ) / left_n * ( Math.PI / 2.0d ) );

				windowArray[i] = Math.Sin( ( Math.PI / 2.0d ) * w * w );
			}

			for( int i = left_window_end; i < right_window_start; i++ )
			{
				windowArray[i] = 1;
			}

			for( int i = right_window_start; i < right_window_end; i++ )
			{
				double w = Math.Sin( ( i - right_window_start + 0.5d ) / right_n * ( Math.PI / 2.0d ) + ( Math.PI / 2.0d ) );

				windowArray[i] = Math.Sin( ( Math.PI / 2.0d ) * w * w );
			}

			for( int i = right_window_end; i < n; i++ )
			{
				windowArray[i] = 0;
			}

			samples = right_window_start - left_window_start;
		}
	}
}
