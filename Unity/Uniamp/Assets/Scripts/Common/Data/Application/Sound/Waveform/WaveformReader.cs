using System;
using System.IO;
using System.Collections.Generic;

using Monoamp.Common.system.io;
using Monoamp.Common.Struct;

using Monoamp.Boundary;

namespace Monoamp.Common.Data.Application.Sound
{
	public struct Int24
	{
		public const int MinValue = -8388608;
		public const int MaxValue = 8388607;
	}

	public class WaveformReader
	{
		private const int LENGTH_BUFFER = 1024 * 4;

		public WaweformFormat format{ get; private set; }
		public string filePath{ get; private set; }
		public int basePosition{ get; private set; }

		private readonly int bufferLength;
		private readonly float[][] sampleArray;

		private int startPosition;

		private object objectLock;
		private readonly AByteArray.Endian endian;

		public WaveformReader( WaweformFormat aFormat, string aFilePath, int aBasePosition, bool aIsOnMemory, AByteArray.Endian aEndian )
		{
			objectLock = new object();

			format = aFormat;
			filePath = aFilePath;
			basePosition = aBasePosition;

			if( aIsOnMemory == true || LENGTH_BUFFER == 0 )
			{
				bufferLength = aFormat.samples;
			}
			else
			{
				bufferLength = LENGTH_BUFFER;
			}

			sampleArray = new float[format.channels][];
			
			for( int i = 0; i < format.channels; i++ )
			{
				sampleArray[i] = new float[bufferLength];
			}

			startPosition = int.MaxValue;
			endian = aEndian;
		}

		public float GetSample( int aChannel, int aPositionSample )
		{
			lock( objectLock )
			{
				if( aPositionSample >= format.samples )
				{
					return 0.0f;
				}

				if( aPositionSample < startPosition || aPositionSample >= startPosition + bufferLength )
				{
					startPosition = aPositionSample;
					
					ReadSampleArray( startPosition );
				}
				
				if( aPositionSample - startPosition < 0 && aPositionSample - startPosition >= sampleArray[aChannel].Length )
				{
					UnityEngine.Debug.LogError( "Start:" + startPosition + ", Position:" + aPositionSample );
				}
				
				return sampleArray[aChannel % format.channels][aPositionSample - startPosition];
			}
		}

		private void ReadSampleArray( int aPointSample )
		{
			if( filePath != null )
			{
				using ( FileStream u = new FileStream( filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
				{
					AByteArray lByteArray = ( endian == AByteArray.Endian.Little ? ( AByteArray )( new ByteArrayLittle( u ) ) : ( AByteArray )( new ByteArrayBig( u ) ) );
					
					switch( format.sampleBits )
					{
						case 16:
							ReadSampleArray16( lByteArray, aPointSample );
							break;
							
						case 24:
							ReadSampleArray24( lByteArray, aPointSample );
							break;
							
						default:
							break;
					}
				}
			}
		}

		private void ReadSampleArray16( AByteArray aByteArray, int aPositionSample )
		{
			aByteArray.SetPosition( basePosition + 2 * format.channels * aPositionSample );
			
			for( int i = 0; i < bufferLength && i < format.samples - aPositionSample; i++ )
			{
				for( int j = 0; j < format.channels; j++ )
				{
					Int32 lSample = aByteArray.ReadInt16();
					sampleArray[j][i] = ( float )lSample / ( float )Int16.MaxValue;
				}
			}
		}
		
		private void ReadSampleArray24( AByteArray aByteArray, int aPositionSample )
		{
			aByteArray.SetPosition( basePosition + 3 * format.channels * aPositionSample );
			
			for( int i = 0; i < bufferLength && i < format.samples - aPositionSample; i++ )
			{
				for( int j = 0; j < format.channels; j++ )
				{
					Int32 lSample = aByteArray.ReadInt24();
					sampleArray[j][i] = ( float )lSample / ( float )Int24.MaxValue;
				}
			}
		}
	}
}
