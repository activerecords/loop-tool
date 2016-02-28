using System;

namespace Curan.Utility
{
	public static class MemoryTool
	{
		public static void memcpy( Byte[] aDataDestinationArray, int aStartDestination, Byte[] aDataSourceArray, int aStartSource, int aSize )
		{
			for( int i = 0; i < aSize; i++ )
			{
				aDataDestinationArray[aStartDestination + i] = aDataSourceArray[aStartSource + i];
			}
		}

		public static void memset( Byte[] aDataDestinationArray, Byte aData, int aStart, int aSize )
		{
			for( int i = 0; i < aSize; i++ )
			{
				aDataDestinationArray[aStart + i] = aData;
			}
		}
	}
}
