using System;

using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
	public struct MidiVolume
	{
		public int pan;

		public Byte volumeMsb;
		public Byte volumeLsb;
		public float volume;
		public float volumeRate;

		public Byte expressionMsb;
		public Byte expressionLsb;
		public float expression;

		public float[] volumeArray;

		public void Init()
		{
			volumeArray = new float[2];

			pan = 64;
			SetVolumeMsb( 0x80 );
			SetVolumeLsb( 0x00 );
			SetExpressionMsb( 0x80 );
			SetExpressionMsb( 0x00 );

			volume = 0.0f;
			expression = 0.0f;

			volumeArray[0] = ( float )Math.Pow( 10.0d, ( GetPanLeft() + GetVolume() + GetExpression() ) / 20.0d );
			volumeArray[1] = ( float )Math.Pow( 10.0d, ( GetPanRight() + GetVolume() + GetExpression() ) / 20.0d );
		}

		public void SetPan( int position )
		{
			pan = position;

			volumeArray[0] = ( float )Math.Pow( 10.0d, ( GetPanLeft() + GetVolume() + GetExpression() ) / 20.0d );
			volumeArray[1] = ( float )Math.Pow( 10.0d, ( GetPanRight() + GetVolume() + GetExpression() ) / 20.0d );
		}

		public float GetPanLeft()
		{
			return ( float )( 20.0d * Math.Log10( Math.Cos( Math.PI / 2.0d * ( double )Math.Max( 0, pan - 1 ) / 126.0d ) ) );
		}

		public float GetPanRight()
		{
			return ( float )( 20.0d * Math.Log10( Math.Sin( Math.PI / 2.0d * ( double )Math.Max( 0, pan - 1 ) / 126.0d ) ) );
		}

		public void SetVolumeMsb( Byte aData )
		{
			volumeMsb = aData;

			SetVolume();
		}

		public void SetVolumeLsb( Byte aData )
		{
			volumeLsb = aData;

			SetVolume();
		}

		public void SetVolume()
		{
			UInt16 lVolumeData = ( UInt16 )( ( volumeMsb << 8 ) | volumeLsb );
			double lVolume = ( double )lVolumeData / ( double )0x10000;

			volume = ( float )( 40.0d * Math.Log10( lVolume ) );
			volumeRate = ( float )Math.Pow( 10.0d, volume / 20.0d );

			volumeArray[0] = ( float )Math.Pow( 10.0d, ( GetPanLeft() + GetVolume() + GetExpression() ) / 20.0d );
			volumeArray[1] = ( float )Math.Pow( 10.0d, ( GetPanRight() + GetVolume() + GetExpression() ) / 20.0d );
		}

		public float GetVolume()
		{
			return volume;
		}

		public float GetVolumeRate()
		{
			return volumeRate;
		}

		public void SetExpressionMsb( Byte aData )
		{
			expressionMsb = aData;

			SetExpression();
		}

		public void SetExpressionLsb( Byte aData )
		{
			expressionLsb = aData;

			SetExpression();
		}

		public void SetExpression()
		{
			UInt16 lExpressionData = ( UInt16 )( ( expressionMsb << 8 ) | expressionLsb );
			double lExpression = ( double )lExpressionData / ( double )0x10000;

			expression = ( float )( 40.0d * Math.Log10( lExpression ) );

			volumeArray[0] = ( float )Math.Pow( 10.0d, ( GetPanLeft() + GetVolume() + GetExpression() ) / 20.0d );
			volumeArray[1] = ( float )Math.Pow( 10.0d, ( GetPanRight() + GetVolume() + GetExpression() ) / 20.0d );
		}

		public float GetExpression()
		{
			return expression;
		}
	}
}
