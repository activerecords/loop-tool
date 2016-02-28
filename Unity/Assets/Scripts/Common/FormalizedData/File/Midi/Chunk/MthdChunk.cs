using System;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MthdChunk : MidiChunk
	{
		private UInt16 format;
		private UInt16 tracks;
		private UInt16 division;

		// ヘッダ情報を読み込む.
		public MthdChunk( string aId, int aSize )
			: base( aId, aSize )
		{

		}

		public void Read( ByteArray byteArray )
		{
			CheckHeader();

			format = byteArray.ReadUInt16();
			tracks = byteArray.ReadUInt16();
			division = byteArray.ReadUInt16();
		}

		private void CheckHeader()
		{
			string id = GetId();

			if( id != "MThd" )
			{
				Logger.LogError( "Undefined Header:" + id );

				throw new Exception();
			}
		}

		public int GetFormat()
		{
			return ( int )format;
		}

		public int GetTracks()
		{
			return ( int )tracks;
		}

		public int GetDivision()
		{
			return ( int )division;
		}
	}
}
