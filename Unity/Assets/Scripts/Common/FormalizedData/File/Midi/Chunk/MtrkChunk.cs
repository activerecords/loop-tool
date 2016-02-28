using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MtrkChunk : MidiChunk
    {
		private List<MidiEventBase> midiEventList;
        private List<MetaEventBase> metaEventList;

		public MtrkChunk( string aId, int aSize )
			: base( aId, aSize )
		{
			midiEventList = new List<MidiEventBase>();
			metaEventList = new List<MetaEventBase>();
		}

		public void Read( ByteArray aByteArray )
		{
			CheckHeader();
            ReadTrack( aByteArray );
		}

		// ヘッダを読み込む.
		private void CheckHeader()
		{
			string id = GetId();

			if( id != "MTrk" )
			{
				Logger.LogError( "Undefined Header:" + id );

				throw new Exception();
			}
		}

        // トラックのイベントを読み込む.
        private void ReadTrack( ByteArray aByteArray )
        {
            int lDelta = 0;
            byte lStatePre = 0;

            int lPositionPre = aByteArray.Position;

            // トラック終了まで読み込む.
            while( aByteArray.Position < lPositionPre + size )
            {
                lDelta += GetVariableLengthByte( aByteArray );

                byte lState = aByteArray.ReadByte();

                // ランニングステータス対応.
                if( lState < 0x80 )
                {
                    lState = lStatePre;
                    aByteArray.SubPosition( 1 );
                }

                lStatePre = lState;

                if( lState != 0xFF )
                {
                    // MIDIイベント読み込み.
					MidiEventBase lMidiEvent = MidiEventReader.Execute( lDelta, lState, aByteArray );

                    midiEventList.Add( lMidiEvent );
                }
                else
                {
                    // メタイベント読み込み.
					MetaEventBase lMetaEvent = MetaEventReader.Execute( lDelta, aByteArray );

                    metaEventList.Add( lMetaEvent );
                }
            }
        }

        // 可変長バイトを取得する.
        public static int GetVariableLengthByte( ByteArray aByteArray )
        {
            byte temp = aByteArray.ReadByte();

            int length = ( int )( temp & 0x7F );

            // 最上位ビットが1であれば読み込みを続ける.
            while( ( temp & 0x80 ) == 0x80 )
            {
                temp = aByteArray.ReadByte();

                length <<= 7;
                length |= ( int )( temp & 0x7F );
            }

            return length;
        }

		public List<MidiEventBase> GetMidiEventList()
        {
            return midiEventList;
        }

		public List<MetaEventBase> GetMetaEventList()
        {
            return metaEventList;
        }
	}
}
