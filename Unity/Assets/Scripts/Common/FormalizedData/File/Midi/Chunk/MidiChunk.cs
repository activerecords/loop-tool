using System;

using Curan.Common.system.io;

namespace Curan.Common.FormalizedData.File.Midi
{
	public class MidiChunk
	{
		private string id;
		protected int size;

		protected MidiChunk()
		{

		}

		public MidiChunk( string aId, int aSize )
		{
			id = aId;
			size = aSize;
		}

		public string GetId()
		{
			return id;
		}

		public int GetSize()
		{
			return ( int )size;
		}
	}
}
