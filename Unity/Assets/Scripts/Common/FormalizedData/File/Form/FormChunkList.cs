using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Form
{
	public abstract class FormChunkList : FormChunk
	{
		public readonly Dictionary<string,Type> chunkTypeDictionary;
		public readonly Dictionary<string,Type> listTypeDictionary;

		public readonly List<FormChunk> chunkList;
		public readonly Dictionary<string, List<FormChunkList>> listListDictionary;

		public string type;

		public override UInt32 size
		{
			get
			{
				_size = 4;

				foreach( FormChunk lChunk in chunkList )
				{
					_size += lChunk.size + 8;
				}

				return _size;
			}
			protected set
			{
				_size = value;
			}
		}

		protected FormChunkList( Dictionary<string, Type> aChunkTypeDictionary, Dictionary<string, Type> aBodyTypeDictionary, string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			chunkTypeDictionary = aChunkTypeDictionary;
			listTypeDictionary = aBodyTypeDictionary;

			chunkList = new List<FormChunk>();
			listListDictionary = new Dictionary<string, List<FormChunkList>>();

			int lStartPosition = aByteArray.Position;

			try
			{
				while( aByteArray.Position < lStartPosition + aSize - 4 )
				{
					ReadChunk( aByteArray );
				}
			}
			catch( Exception aExpection )
			{
				Logger.LogError( "Expection at RIFF Read:" + aExpection.ToString() );
			}

			aByteArray.SetPosition( lStartPosition + ( int )aSize - 4 );
		}

		private void ReadChunk( ByteArray aByteArray )
		{
			string lId = aByteArray.ReadString( 4 );
			Logger.LogNormal( "ID:" + lId );

			UInt32 lSize = aByteArray.ReadUInt32();

			int lPositionStart = aByteArray.Position;

			// Padding.
			if( lSize % 2 == 1 )
			{
				if( lPositionStart + lSize <= aByteArray.Length && aByteArray.ReadByte( ( int )( lPositionStart + lSize ) ) == 0x00 )
				{
					lSize++;

					Logger.LogWarning( "Padding:" + lSize );
				}

				aByteArray.SetPosition( lPositionStart );
			}

			FormChunk lRiffChunk = Construct( lId, lSize, aByteArray, this );

			chunkList.Add( lRiffChunk );
		}

		public FormChunk Construct( string aId, UInt32 aSize, ByteArray aByteArray, FormChunkList aParent )
		{
			Type[] lArgumentTypes = { typeof( string ), typeof( UInt32 ), typeof( ByteArray ), typeof( FormChunkList ) };
			object[] lArguments = { aId, aSize, aByteArray, aParent };

			Type lTypeChunk = typeof( FormChunkUnknown );

			if( aId == "FORM" || aId == "LIST" )
			{
				type = aByteArray.ReadString( 4 );

				if( listTypeDictionary.ContainsKey( type ) == true )
				{
					lTypeChunk = listTypeDictionary[type];
				}
				else
				{
					Logger.LogError( "Unknown:" + type );
				}
			}
			else
			{
				if( chunkTypeDictionary.ContainsKey( aId ) == true )
				{
					lTypeChunk = chunkTypeDictionary[aId];
				}
			}

			return ( FormChunk )lTypeChunk.GetConstructor( lArgumentTypes ).Invoke( lArguments );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			for( int i = 0; i < id.Length; i++ )
			{
				aByteArray.WriteUByte( ( Byte )id[i] );
			}

			aByteArray.WriteUInt32( ( UInt32 )size );

			for( int i = 0; i < type.Length; i++ )
			{
				aByteArray.WriteUByte( ( Byte )type[i] );
			}

			foreach( FormChunk lChunk in chunkList )
			{
				lChunk.WriteByteArray( aByteArrayRead, aByteArray );
			}
		}

		protected void OverrideChunk( FormChunk aChunk )
		{
			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aChunk.id )
				{
					chunkList[i] = aChunk;

					return;
				}
			}
		}

		protected FormChunk GetChunk( string aId )
		{
			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId )
				{
					return chunkList[i];
				}
			}

			return null;
		}

		protected List<FormChunkList> GetChunkListList( string aId, string aType )
		{
			if( listListDictionary.ContainsKey( aType ) == true )
			{
				return listListDictionary[aType];
			}

			List<FormChunkList> lListList = new List<FormChunkList>();

			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId )
				{
					FormChunkList lRiffList = ( FormChunkList )chunkList[i];

					if( lRiffList.type == aType )
					{
						lListList.Add( ( FormChunkList )chunkList[i] );
					}
				}
			}

			listListDictionary.Add( aType, lListList );

			return listListDictionary[aType];
		}

		protected FormChunkList GetChunkList( string aId, string aType )
		{
			if( listListDictionary.ContainsKey( aType ) == true )
			{
				return listListDictionary[aType][0];
			}

			List<FormChunkList> lListList = new List<FormChunkList>();

			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId )
				{
					FormChunkList lRiffList = ( FormChunkList )chunkList[i];

					if( lRiffList.type == aType )
					{
						lListList.Add( ( FormChunkList )chunkList[i] );
					}
				}
			}

			listListDictionary.Add( aType, lListList );

			if( listListDictionary[aType].Count < 1 )
			{
				Logger.LogErrorBreak( "List is not exist.");
			}
			else if( listListDictionary[aType].Count > 1 )
			{
				Logger.LogErrorBreak( "List exist lather than 1.");
			}

			return listListDictionary[aType][0];
		}

		protected void AddChunk( FormChunk aRiffChunk )
		{
			chunkList.Add( aRiffChunk );
		}
	}
}
