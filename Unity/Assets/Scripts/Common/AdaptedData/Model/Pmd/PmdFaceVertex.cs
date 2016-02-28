using System;

using Curan.Common.system.io;
using Curan.Common.FormalizedData.File.Mmd.Pmd;

using UnityEngine;

namespace Curan.Common.AdaptedData.Model
{
	public class PmdFaceVertex
	{
		public UInt16 faceVertexIndex;

		public PmdFaceVertex( PmdFaceVertexData aPmdFaceVertexData )
		{
			faceVertexIndex = aPmdFaceVertexData.faceVertexIndex;
		}
	}
}
