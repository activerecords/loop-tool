using UnityEngine;

namespace Curan.Common.AdaptedData.Graphic
{
	public class GraphicBase
	{
		public Texture2D texture;

		public GraphicBase()
		{

		}
        
        public GraphicBase( Texture2D aTexture )
        {
            texture = aTexture;
        }
	}
}
