using System;
using System.IO;
using System.Collections.Generic;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Form.Aiff;

namespace Curan.Common.FormalizedData.File.Form
{
	public class FormFile : FormChunkList
	{
		public const string ID = "FORM";

		public static readonly Dictionary<string,Type> bodyTypeDictionary;
		public static readonly Dictionary<string,Type> chunkTypeDictionary;
		
		public readonly string name;
		public FormChunkList formChunkList;

		static FormFile()
		{
			chunkTypeDictionary = new Dictionary<string, Type>();
			//			chunkTypeDictionary.Add( RiffChunkRiff.ID, typeof( RiffBodyList ) );

			bodyTypeDictionary = new Dictionary<string, Type>();
			bodyTypeDictionary.Add( Aiff.FormChunkListAiff.TYPE, typeof( Aiff.FormChunkListAiff ) );
		}

		public FormFile( FileStream aStream )
			: this( new ByteArrayBig( aStream ) )
		{

		}

		public FormFile( ByteArray aByteArray )
			: base( chunkTypeDictionary, bodyTypeDictionary, ID, ( UInt32 )aByteArray.Length, aByteArray, null )
		{
			name = aByteArray.GetName();
			formChunkList = ( FormChunkList )GetChunk( "FORM" );
		}

		public override void WriteByteArray( ByteArray aByteArrayRead, ByteArray aByteArray )
		{
			if( name != null && name != "" )
			{
				using ( FileStream u = new FileStream( name, FileMode.Open, FileAccess.Read ) )
				{
					ByteArray lByteArray = new ByteArrayLittle( u );

					foreach( FormChunk lChunk in chunkList )
					{
						lChunk.WriteByteArray( lByteArray, aByteArray );
					}
				}
			}
		}
	}
}
