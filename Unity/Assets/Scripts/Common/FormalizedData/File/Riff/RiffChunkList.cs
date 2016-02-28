using System;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Utility;

namespace Curan.Common.FormalizedData.File.Riff
{
	public abstract class RiffChunkList : RiffChunk
	{
		public readonly Dictionary<string,Type> chunkTypeDictionary;
		public readonly Dictionary<string,Type> listTypeDictionary;

		public readonly List<RiffChunk> chunkList;
		public readonly Dictionary<string, List<RiffChunkList>> listListDictionary;

		public string type;

		public override UInt32 size
		{
			get
			{
				_size = 4;

				foreach( RiffChunk lChunk in chunkList )
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

		protected RiffChunkList( Dictionary<string, Type> aChunkTypeDictionary, Dictionary<string, Type> aBodyTypeDictionary, string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
			: base( aId, aSize, aByteArray, aParent )
		{
			chunkTypeDictionary = aChunkTypeDictionary;
			listTypeDictionary = aBodyTypeDictionary;

			chunkList = new List<RiffChunk>();
			listListDictionary = new Dictionary<string, List<RiffChunkList>>();

			int lStartPosition = aByteArray.Position;

			try {
				while( aByteArray.Position < lStartPosition + aSize - 4 ) {
					ReadChunk( aByteArray );
				}
			}
			catch( Exception aExpection ) {
				Logger.LogError( "Expection at RIFF Read:" + aExpection.ToString() );
			}

			aByteArray.SetPosition( lStartPosition + ( int )aSize - 4 );
		}

		private void ReadChunk( ByteArray aByteArray )
		{
			string lId = aByteArray.ReadString( 4 );
			//Logger.LogNormal( "ID:" + lId );

			UInt32 lSize = aByteArray.ReadUInt32();

			int lPositionStart = aByteArray.Position;

			// Padding.
			if( lSize % 2 == 1 ) {
				if( lPositionStart + lSize <= aByteArray.Length && aByteArray.ReadByte( ( int )( lPositionStart + lSize ) ) == 0x00 ) {
					lSize++;

					Logger.LogWarning( "Padding:" + lSize );
				}

				aByteArray.SetPosition( lPositionStart );
			}

			RiffChunk lRiffChunk = Construct( lId, lSize, aByteArray, this );

			chunkList.Add( lRiffChunk );

			if (aByteArray.Position != lPositionStart + lSize) {
				Logger.LogWarning ("Modify Position:" + aByteArray.Position + "->" + lPositionStart + lSize);
				aByteArray.SetPosition (( int )( lPositionStart + lSize ));
			}
		}

		public RiffChunk Construct( string aId, UInt32 aSize, ByteArray aByteArray, RiffChunkList aParent )
		{
			Type[] lArgumentTypes = { typeof( string ), typeof( UInt32 ), typeof( ByteArray ), typeof( RiffChunkList ) };
			object[] lArguments = { aId, aSize, aByteArray, aParent };

			Type lTypeChunk = typeof( RiffChunkUnknown );

			if( aId == "RIFF" || aId == "LIST" ) {
				type = aByteArray.ReadString( 4 );

				if( listTypeDictionary.ContainsKey( type ) == true ) {
					lTypeChunk = listTypeDictionary[type];
				}
				else {
					Logger.LogError( "Unknown:" + type );
				}
			}
			else {
				if( chunkTypeDictionary.ContainsKey( aId ) == true ) {
					lTypeChunk = chunkTypeDictionary[aId];
				}
			}

			return ( RiffChunk )lTypeChunk.GetConstructor( lArgumentTypes ).Invoke( lArguments );
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

			foreach( RiffChunk lChunk in chunkList )
			{
				lChunk.WriteByteArray( aByteArrayRead, aByteArray );
			}
		}

		protected void OverrideChunk( RiffChunk aChunk )
		{
			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aChunk.id ) {
					chunkList[i] = aChunk;

					return;
				}
			}
		}

		protected RiffChunk GetChunk( string aId )
		{
			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId ) {
					return chunkList[i];
				}
			}

			return null;
		}

		protected List<RiffChunkList> GetChunkListList( string aId, string aType )
		{
			if( listListDictionary.ContainsKey( aType ) == true ) {
				return listListDictionary[aType];
			}

			List<RiffChunkList> lListList = new List<RiffChunkList>();

			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId) {
					RiffChunkList lRiffList = ( RiffChunkList )chunkList[i];

					if( lRiffList.type == aType ) {
						lListList.Add( ( RiffChunkList )chunkList[i] );
					}
				}
			}

			listListDictionary.Add( aType, lListList );

			return listListDictionary[aType];
		}

		protected RiffChunkList GetChunkList( string aId, string aType )
		{
			if( listListDictionary.ContainsKey( aType ) == true ) {
				return listListDictionary[aType][0];
			}

			List<RiffChunkList> lListList = new List<RiffChunkList>();

			for( int i = 0; i < chunkList.Count; i++ )
			{
				if( chunkList[i].id == aId) {
					RiffChunkList lRiffList = ( RiffChunkList )chunkList[i];

					if( lRiffList.type == aType ) {
						lListList.Add( ( RiffChunkList )chunkList[i] );
					}
				}
			}

			listListDictionary.Add( aType, lListList );

			if( listListDictionary[aType].Count < 1 ) {
				Logger.LogErrorBreak( "List is not exist.");
			}
			else if( listListDictionary[aType].Count > 1 ) {
				Logger.LogErrorBreak( "List exist more than 1.");
			}
			
			return listListDictionary[aType][0];
		}

		protected void AddChunk( RiffChunk aRiffChunk )
		{
			chunkList.Add( aRiffChunk );
		}
	}
}
