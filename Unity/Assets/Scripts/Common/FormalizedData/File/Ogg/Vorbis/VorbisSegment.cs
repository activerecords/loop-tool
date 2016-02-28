using System;
using System.Collections.Generic;

using Curan.Common.FormalizedData.File.Ogg.Vorbis.Header;
using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg.Vorbis
{
	public class VorbisSegment
	{
		private VorbisHeader vorbisHeader;

		private ByteArray byteArray;

		private ModeHeader modeHeader;
		private MappingHeader mappingHeader;

		private Byte[] noResidue;

		private double[][] residueArray;

		public Window window;

		public VorbisSegment( ByteArray aByteArray, VorbisHeader aVorbisHeader )
		{
			vorbisHeader = aVorbisHeader;

			residueArray = new double[2][];

			byteArray = aByteArray;
		}

		public void Read( double[][] aBufferArrayArray )
		{
			int packetType = byteArray.ReadBitsAsByte( 1 );

			Byte lTypeHeader = 0x00;

			if( packetType != 0x00 )
			{
				byteArray.SubBitPosition( 1 );
				lTypeHeader = byteArray.ReadBitsAsByte( 8 );
			}

			if( packetType == 0x00 )
			{
				Logger.LogDebug( "The Packet Type Is 0 (Audio)." );

				try
				{
					int lModeNumber = byteArray.ReadBitsAsByte( ilog( ( UInt32 )( vorbisHeader.setup.mode.count - 1 ) ) );
					modeHeader = vorbisHeader.setup.mode.header[lModeNumber];
					mappingHeader = vorbisHeader.setup.mapping.header[modeHeader.mapping];
					window = new Window( byteArray, modeHeader.blockFlag, vorbisHeader.identification.blockSize0, vorbisHeader.identification.blockSize1 );

					noResidue = new Byte[vorbisHeader.identification.audioChannels];

					DecodeFloor();

					Propagate();

					DecodeResidue();

					InverseCoupling();

					ComputeCurve( aBufferArrayArray );
				}
				catch
				{
					Logger.LogError( "■Error." );
				}
			}
			else
			{
				Logger.LogError( "The Packet Type Is Not 0." );

				vorbisHeader.Read( byteArray, lTypeHeader );
			}

			Logger.LogDebug( "Sub Packet Position:0x" + byteArray.Position.ToString( "X8" ) + "." + byteArray.GetBitPositionInByte().ToString() + "/" + byteArray.Length.ToString( "X8" ) );
		}

		private void DecodeFloor()
		{
			for( int i = 0; i < vorbisHeader.identification.audioChannels; i++ )
			{
				Byte lSubMapNumber = mappingHeader.mux[i];
				Byte lFloorNumber = mappingHeader.subMapFloor[lSubMapNumber];
				FloorHeader lFloorHeader = vorbisHeader.setup.floor.header[lFloorNumber];

				int lReturnCode = lFloorHeader.DecodePacket( byteArray, vorbisHeader.setup.codebook.headerArray );

				if( lReturnCode == 1 )
				{
					noResidue[i] = 0x01;
				}
				else
				{
					noResidue[i] = 0x00;
				}
			}
		}

		private void Propagate()
		{
			Logger.LogDebug( "■Propagate" );

			for( int i = 0; i < mappingHeader.couplingStepsAdd1; i++ )
			{
				if( noResidue[mappingHeader.magnitude[i]] == 0x00 || noResidue[mappingHeader.angle[i]] == 0x00 )
				{
					noResidue[mappingHeader.magnitude[i]] = 0x00;
					noResidue[mappingHeader.angle[i]] = 0x00;
				}
			}
		}

		private void DecodeResidue()
		{
			residueArray[0] = new double[window.n];
			residueArray[1] = new double[window.n];

			Logger.LogDebug( "■DecodeResidue" );
			
			Byte[] do_not_decode_flag = new Byte[2];

			for( int i = 0; i < mappingHeader.subMapsAdd1; i++ )
			{
				int ch = 0;

				for( int j = 0; j < vorbisHeader.identification.audioChannels; j++ )
				{
					if( mappingHeader.mux[j] == i )
					{
						if( noResidue[j] == 0x01 )
						{
							Logger.LogDebug( "noResidue" );

							do_not_decode_flag[ch] = 0x01;
						}
						else
						{
							Logger.LogDebug( "Residue" );

							do_not_decode_flag[ch] = 0x00;
						}

						ch++;
					}
					else
					{
						Logger.LogError( "■lMappingHeader.mux[j] != i" );
					}
				}

				int lResidueNumber = mappingHeader.subMapResidue[i];
				ResidueHeader lResidueHeader = vorbisHeader.setup.residue.header[lResidueNumber];
				lResidueHeader.DecodePacket( byteArray, vorbisHeader.identification.audioChannels, vorbisHeader.setup.codebook.headerArray, window.n, ch, do_not_decode_flag, residueArray );

				ch = 0;

				for( int j = 0; j < vorbisHeader.identification.audioChannels; j++ )
				{
					if( mappingHeader.mux[j] == i )
					{
						//i. residue vector for channel [j] is set to decoded residue vector [ch]
						ch++;
					}
				}
			}
		}

		private void InverseCoupling()
		{
			for( int i = mappingHeader.couplingStepsAdd1 - 1; i >= 0; i-- )
			{
				//Logger.LogError( "■M,A:" + mappingHeader.magnitude[i] + "," + mappingHeader.angle[i] );

				double[] magnitude_vector = residueArray[mappingHeader.magnitude[i]];
				double[] angle_vector = residueArray[mappingHeader.angle[i]];

				for( int j = 0; j < magnitude_vector.Length; j++ )
				{
					double M = 0;
					double A = 0;

					if( magnitude_vector[j] > 0 )
					{
						if( angle_vector[j] > 0 )
						{
							M = magnitude_vector[j];
							A = magnitude_vector[j] - angle_vector[j];
						}
						else
						{
							A = magnitude_vector[j];
							M = magnitude_vector[j] + angle_vector[j];
						}
					}
					else
					{
						if( angle_vector[j] > 0 )
						{
							M = magnitude_vector[j];
							A = magnitude_vector[j] + angle_vector[j];
						}
						else
						{
							A = magnitude_vector[j];
							M = magnitude_vector[j] - angle_vector[j];
						}
					}

					magnitude_vector[j] = M;
					angle_vector[j] = A;
				}
			}
		}

		private void ComputeCurve( double[][] aBufferArrayArray )
		{
			for( int i = 0; i < vorbisHeader.identification.audioChannels; i++ )
			{
				if( noResidue[i] == 0x00 )
				{
					Byte lSubMapNumber = mappingHeader.mux[i];
					Byte lFloorNumber = mappingHeader.subMapFloor[lSubMapNumber];
					FloorHeader lFloorHeader = vorbisHeader.setup.floor.header[lFloorNumber];

					double[] lFloorCurve = new double[window.n];
					lFloorHeader.ComputeCurve( lFloorCurve, window.n );

					for( int j = 0; j < window.n; j++ )
					{
						lFloorCurve[j] *= residueArray[i][j];
					}

					MdctFloat mdct = MdctFloat.GetMdctFloat( window.n );
					aBufferArrayArray[i] = new double[window.n];
					mdct.imdct( lFloorCurve, window.windowArray, aBufferArrayArray[i] );
				}
			}
		}

		private int ilog( UInt32 aX )
		{
			int lReturnValue = 0;

			while( aX > 0 )
			{
				lReturnValue++;

				aX >>= 1;
			}

			return lReturnValue;
		}
	}
}
