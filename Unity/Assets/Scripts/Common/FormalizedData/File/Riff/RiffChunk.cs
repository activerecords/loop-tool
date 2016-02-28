using System;
using System.Collections.Generic;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Riff
{
	public abstract class RiffChunk
	{
		public readonly string id;
		public readonly RiffChunkList parent;
		public readonly UInt32 position;
		public readonly List<string> informationList;
		protected UInt32 _size;
		
		public virtual UInt32 size
		{
			get
			{
				return _size;
			}
			protected set
			{
				_size = value;
			}
		}

		protected RiffChunk( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
		{
			id = aId;
			size = aSize;
			parent = aParent;

			if( aByteArray != null )
			{
				position = ( UInt32 )aByteArray.Position;
			}

			informationList = new List<string>();
		}

		public virtual void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			for( int i = 0; i < id.Length; i++ )
			{
				aByteArray.WriteUByte( ( Byte )id[i] );
			}

			aByteArray.WriteUInt32( ( UInt32 )size );
			aByteArrayRead.SetPosition( ( int )position );
			aByteArray.WriteBytes( aByteArrayRead.ReadBytes( size ) );
		}
	}
}
