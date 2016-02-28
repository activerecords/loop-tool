using System;

using Monoamp.Common.system.io;
using Monoamp.Boundary;

namespace Monoamp.Common.Data.Standard.Midi
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

		public void Read( AByteArray byteArray )
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
				Logger.Error( "Undefined Header:" + id );

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
