using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Form
{
	public abstract class FormChunk
	{
		public readonly string id;
		public readonly FormChunkList parent;
		public readonly UInt32 position;
		public readonly List<string> informationList;
		protected UInt32 _size;

		public virtual UInt32 size
		{
			get{ return _size; }
			protected set{ _size = value; }
		}

		protected FormChunk( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
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
