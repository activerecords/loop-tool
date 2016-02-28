using System;
using System.IO;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Adx
{
	public class AdxFile
	{
		private AdxHeader adxHeader;
		private AdxData adxData;

		private float[][] sampleArray;

		public AdxFile( Stream aStream )
		{
			ByteArray lByteArray = new ByteArrayBig( aStream );

			sampleArray = new float[2][];

			adxHeader = new AdxHeader( lByteArray );
			adxData = new AdxData( lByteArray, adxHeader );

			sampleArray = adxData.GetSampleArray();
		}

		public AdxHeader GetAdxHeader()
		{
			return adxHeader;
		}

		public AdxData GetAdxData()
		{
			return adxData;
		}

		public float[][] GetSampleArray()
		{
			return sampleArray;
		}
	}
}
