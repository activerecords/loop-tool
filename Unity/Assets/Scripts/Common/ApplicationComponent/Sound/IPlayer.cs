using Curan.Common.Struct;

namespace Curan.Common.ApplicationComponent.Sound
{
	public interface IPlayer
	{
        double Position{ get; set; }
        float Volume{ set; }

		void Play();
		void Stop();
		void Pause();
		void Record( string aPath );
		bool GetFlagPlaying();
		void SetPreviousLoop();
		void SetNextLoop();
		void SetUpLoop();
		void SetDownLoop();
		SoundTime GetTimePosition();
		SoundTime GetTimeElapsed();
		SoundTime GetTimeLength();
		LoopInformation GetLoopPoint();
		int GetLoopNumberX();
		int GetLoopNumberY();
		void Update( float[] aSoundBuffer, int aChannels, int aSampleRate );
	}
}
