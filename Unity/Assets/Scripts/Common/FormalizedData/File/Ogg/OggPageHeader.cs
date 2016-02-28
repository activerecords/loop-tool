using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Ogg
{
	public class OggPageHeader
	{
		private string id;
		private Byte version;
		private Byte headerType;
		public UInt64 granulePosition;
		private UInt32 bitstreamSerialNumber;
		private UInt32 pageSequenceNumber;
		private UInt32 crcChecksum;
		private Byte pageSegments;
		private Byte[] segmentTable;

		private int countSegments;
		private int sizePacket;
		private int sizeNextSegment;
		private List<int> sizeSegmentList;

		public OggPageHeader( ByteArray aByteArray )
		{
			countSegments = 0;
			sizePacket = 0;
			sizeSegmentList = new List<int>();
			int lPacketSize = 0;

			Logger.LogDebug( "Position:0x" + aByteArray.Position.ToString( "X16" ) + "." + aByteArray.GetBitPositionInByte().ToString() );

			UInt32 posStartHeader = ( UInt32 )aByteArray.Position;

			id = aByteArray.ReadString( 4 );

			if( id != "OggS" )
			{
				Logger.LogError( "The File Is Not a Ogg File:" + id );

				return;
				//throw new Exception();
			}

			version = aByteArray.ReadByte();
			headerType = aByteArray.ReadByte();
			granulePosition = aByteArray.ReadUInt64();
			bitstreamSerialNumber = aByteArray.ReadUInt32();
			pageSequenceNumber = aByteArray.ReadUInt32();

			UInt32 posCrc = ( UInt32 )aByteArray.Position;

			crcChecksum = aByteArray.ReadUInt32();
			pageSegments = aByteArray.ReadByte();
			segmentTable = aByteArray.ReadBytes( pageSegments );

			Logger.LogDebug( "ID:" + id );
			Logger.LogDebug( "Version:" + version.ToString() );
			Logger.LogDebug( "Header Type:" + headerType.ToString() );
			Logger.LogDebug( "Granule Position:" + granulePosition );
			Logger.LogDebug( "Bitstream Serial Number:0x" + bitstreamSerialNumber.ToString( "X8" ) );
			Logger.LogDebug( "Page Sequence Number:0x" + pageSequenceNumber.ToString( "X8" ) );
			Logger.LogDebug( "CRC Checksum:0x" + crcChecksum.ToString( "X8" ) );
			Logger.LogDebug( "Page Segments:" + pageSegments.ToString() );

			for( int i = 0; i < pageSegments; i++ )
			{
				sizePacket += segmentTable[i];
				lPacketSize += segmentTable[i];

				if( segmentTable[i] < 255 )
				{
					countSegments++;
					sizeSegmentList.Add( lPacketSize );
					lPacketSize = 0;
				}
				else if( i == pageSegments - 1 )
				{
					sizeNextSegment = lPacketSize;
					sizePacket -= sizeNextSegment;
				}
			}
			
			UInt32 posStartPacket = ( UInt32 )aByteArray.Position;
			UInt32 posEndPacket = ( UInt32 )( aByteArray.Position + sizePacket );

			aByteArray.SetPosition( ( int )posStartHeader );

			Byte[] lByteArray = aByteArray.ReadBytes( posEndPacket - posStartHeader );
			lByteArray[posCrc - posStartHeader + 0] = 0;
			lByteArray[posCrc - posStartHeader + 1] = 0;
			lByteArray[posCrc - posStartHeader + 2] = 0;
			lByteArray[posCrc - posStartHeader + 3] = 0;
			
			// CRC チェック
			UInt32 crc = 0;

			for( int i = 0; i < lByteArray.Length; i++ )
			{
				UInt32 lByte = ( UInt32 )( crc >> 24 ) ^ ( UInt32 )lByteArray[i];

				lByte ^= ( UInt32 )( lByte >> 6 );

				crc = ( UInt32 )(
					( UInt32 )( ( crc << 8 ) | lByte ) ^
					( UInt32 )( lByte << 26 ) ^
					( UInt32 )( lByte << 23 ) ^
					( UInt32 )( lByte << 22 ) ^
					( UInt32 )( lByte << 16 ) ^
					( UInt32 )( lByte << 12 ) ^
					( UInt32 )( lByte << 11 ) ^
					( UInt32 )( lByte << 10 ) ^
					( UInt32 )( lByte << 8 ) ^
					( UInt32 )( lByte << 7 ) ^
					( UInt32 )( lByte << 5 ) ^
					( UInt32 )( lByte << 4 ) ^
					( UInt32 )( lByte << 2 ) ^
					( UInt32 )( lByte << 1 )
					);
			}

			aByteArray.SetPosition( ( int )posStartPacket );

			if( crc == crcChecksum )
			{
				Logger.LogDebug( "Ok" );
			}
			else
			{
				//throw new Exception( "Error:" + crc.ToString( "X8" ) + "-" + crcChecksum.ToString( "X8" ) );
				Logger.LogError( "Error:" + crc.ToString( "X8" ) + "-" + crcChecksum.ToString( "X8" ) );
			}
		}

		public int GetPacketSize()
		{
			return sizePacket;
		}

		public List<int> GetSegmentSizeList()
		{
			return sizeSegmentList;
		}

		public int GetNextSegmentSize()
		{
			return sizeNextSegment;
		}
	}
}
