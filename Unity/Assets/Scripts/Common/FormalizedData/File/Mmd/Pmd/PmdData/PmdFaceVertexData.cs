using System;
using System.Collections.Generic;

using Curan.Common.system.io;

using UnityEngine;

namespace Curan.Common.FormalizedData.File.Mmd.Pmd
{
	public class PmdFaceVertexData : PmdDataAbstract
	{
		public UInt16 faceVertexIndex;

		public PmdFaceVertexData( ByteArray aByteArray )
		{
			faceVertexIndex = aByteArray.ReadUInt16();
		}
	}
}
