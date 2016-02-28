using System;
using System.IO;

namespace Curan.Common.FormalizedData.File.Sfz
{
	public class SfzRegion : ICloneable
	{
		public string sample;
		public byte lokey = 0;
		public byte hikey = 127;
		public bool loop_mode = false;
		public int loop_start = 0;
		public int loop_end = 0x0fffffff;
		public int offset = 0;
		public int end = 0x0fffffff;
		public int tune = 0;
		public int pitch_keycenter = 60;
		public float volume = 0.0F;

		public float pitcheg_delay;
		public float pitcheg_start;
		public float pitcheg_attack;
		public float pitcheg_hold;
		public float pitcheg_decay;
		public float pitcheg_sustain;
		public float pitcheg_release;
		public float pitcheg_depth;
		public float pitcheg_vel2delay;
		public float pitcheg_vel2attack;
		public float pitcheg_vel2hold;
		public float pitcheg_vel2decay;
		public float pitcheg_vel2sustain;
		public float pitcheg_vel2release;
		public float pitcheg_vel2depth;

		public float ampeg_delay = 0.0f;
		public float ampeg_start = 0.0f;
		public float ampeg_attack = 0.0f;
		public float ampeg_hold = 0.0f;
		public float ampeg_decay = 0.0f;
		public float ampeg_sustain = 100.0f;
		public float ampeg_release = 0.25F;
		public float ampeg_vel2delay;
		public float ampeg_vel2attack;
		public float ampeg_vel2hold;
		public float ampeg_vel2decay;
		public float ampeg_vel2sustain;
		public float ampeg_vel2release;
		public float ampeg_delayccN;
		public float ampeg_startccN;
		public float ampeg_attackccN;
		public float ampeg_holdccN;
		public float ampeg_decayccN;
		public float ampeg_sustainccN;
		public float ampeg_releaseccN;

		private SfzRegion()
		{

		}

		public SfzRegion( StreamReader streamReader )
		{
			Read( streamReader );
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public void Read( StreamReader streamReader )
		{
			string line = streamReader.ReadLine();

			while( line != "" && line != null )
			{
				ReadLine( line );

				line = streamReader.ReadLine();
			}
		}

		private void ReadLine( string line )
		{
			if( line.IndexOf( "//" ) != 0 )
			{
				string opcode = line.Split( '=' )[0];
				string value = line.Split( '=' )[1];

				switch( opcode )
				{
				case "sample":
					sample = value;
					break;

				case "loop_mode":
					if( value == "loop_continuous" )
					{
						loop_mode = true;
					}
					else
					{
						loop_mode = false;
					}
					break;

				case "loop_start":
					loop_start = Convert.ToInt32( value );
					break;

				case "loop_end":
					loop_end = Convert.ToInt32( value );
					break;

				case "offset":
					offset = Convert.ToInt32( value );
					loop_start = Convert.ToInt32( value );
					break;

				case "end":
					end = Convert.ToInt32( value );
					loop_end = Convert.ToInt32( value );
					break;

				case "tune":
					tune = Convert.ToInt32( value );
					break;

				case "pitch_keycenter":
					pitch_keycenter = Convert.ToInt32( value );
					break;

				case "volume":
					volume = ( float )Convert.ToDouble( value );
					break;

				case "key":
					lokey = Convert.ToByte( value );
					hikey = Convert.ToByte( value );
					break;

				case "lokey":
					lokey = Convert.ToByte( value );
					break;

				case "hikey":
					hikey = Convert.ToByte( value );
					break;
					/*
				case "pitcheg_delay":
					pitcheg_delay = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_start":
					pitcheg_start = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_attack":
					pitcheg_attack = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_hold":
					pitcheg_hold = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_decay":
					pitcheg_decay = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_sustain":
					pitcheg_sustain = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_release":
					pitcheg_release = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_depth":
					pitcheg_depth = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2delay":
					pitcheg_vel2delay = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2attack":
					pitcheg_vel2attack = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2hold":
					pitcheg_vel2hold = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2decay":
					pitcheg_vel2decay = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2sustain":
					pitcheg_vel2sustain = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2release":
					pitcheg_vel2release = ( float )Convert.ToDouble( value );
					break;

				case "pitcheg_vel2depth":
					pitcheg_vel2depth = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_delay":
					ampeg_delay = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_start":
					ampeg_start = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_attack":
					ampeg_attack = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_hold":
					ampeg_hold = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_decay":
					ampeg_decay = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_sustain":
					ampeg_sustain = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_release":
					ampeg_release = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2delay":
					ampeg_vel2delay = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2attack":
					ampeg_vel2attack = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2hold":
					ampeg_vel2hold = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2decay":
					ampeg_vel2decay = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2sustain":
					ampeg_vel2sustain = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_vel2release":
					ampeg_vel2release = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_delayccN":
					ampeg_delayccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_startccN":
					ampeg_startccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_attackccN":
					ampeg_attackccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_holdccN":
					ampeg_holdccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_decayccN":
					ampeg_decayccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_sustainccN":
					ampeg_sustainccN = ( float )Convert.ToDouble( value );
					break;

				case "ampeg_releaseccN":
					ampeg_releaseccN = ( float )Convert.ToDouble( value );
					break;
					*/
				default:
					// 未定義.
					break;
				}
			}
		}
	}
}
