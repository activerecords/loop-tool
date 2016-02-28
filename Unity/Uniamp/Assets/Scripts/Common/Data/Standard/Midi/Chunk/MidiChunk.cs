using System;

using Monoamp.Common.system.io;

namespace Monoamp.Common.Data.Standard.Midi
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
