using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.Struct;

namespace Monoamp.Common.Component.Sound.LoopTool
{
	public class LoopSearchPointSoundfont
	{
		private const int NUMBER_OF_LOOP = 10;
		private const int POINTS_SAMPLE = 441;

		private int width;
		public int basePoint;
		private List<int> sadMinList;
		public List<int> samePointList;
		private SByte[] sourceSamples;
		private SByte[] compareSamples;

		public LoopSearchPointSoundfont( SByte[] aSampleArray, int aLength, int aWidth, int aBasePoint, int aMinLoopLength )
		{
			width = aWidth;
			basePoint = aBasePoint;
			sourceSamples = aSampleArray;
			compareSamples = new SByte[POINTS_SAMPLE];
			sadMinList = new List<int>();
			samePointList = new List<int>();

			for( int i = 0; i < POINTS_SAMPLE; i++ )
			{
				if( i < compareSamples.Length && basePoint + width * i / POINTS_SAMPLE < sourceSamples.Length )
				{
					compareSamples[i] = sourceSamples[basePoint + width * i / POINTS_SAMPLE];
				}
				else
				{
					UnityEngine.Debug.LogError( i.ToString() + "/" + compareSamples.Length );
					UnityEngine.Debug.LogError( ( basePoint + width * i / POINTS_SAMPLE ).ToString() + "/" + sourceSamples.Length );
				}
			}

			for( int i = 0; i < NUMBER_OF_LOOP; i++ )
			{
				sadMinList.Add( int.MaxValue );
				samePointList.Add( basePoint );
			}

			for( int i = basePoint + aMinLoopLength; i < aLength - width; i++ )
			{
				int lSad = 0;

				for( int j = 0; j < POINTS_SAMPLE && lSad <= sadMinList[NUMBER_OF_LOOP - 1]; j++ )
				{
					int lDiff = ( int )compareSamples[j] - ( int )aSampleArray[i + width * j / POINTS_SAMPLE];
					lSad += lDiff * lDiff;
				}

				if( lSad <= sadMinList[NUMBER_OF_LOOP - 1] )
				{
					for( int j = 0; j < NUMBER_OF_LOOP; j++ )
					{
						if( lSad <= sadMinList[j] )
						{
							sadMinList.Insert( j, lSad );
							samePointList.Insert( j, i - 1 );

							sadMinList.RemoveAt( NUMBER_OF_LOOP );
							samePointList.RemoveAt( NUMBER_OF_LOOP );

							break;
						}
					}
				}
			}
		}
	}
}
