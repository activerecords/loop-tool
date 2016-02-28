using System;

using Monoamp.Common.Data.Application.Sound;
using Monoamp.Boundary;

namespace Monoamp.Common.Component.Application.Sound
{
    public class MidiOscillator
    {
		private FilterVibrato filterVibrato;
        private GeneratorEnvelope generatorEnvelope;
        private GeneratorPortament generatorPortament;

		public MidiOscillator( byte aNote, byte aInstrument, byte aVelocity, SoundfontBase aSoundfont, ref MidiVolume aMidiVolume, ref MidiPitch aMidiPitch, double aSecondsSustain = 0.0d )
        {
			generatorEnvelope = new GeneratorEnvelope( aSoundfont, aVelocity, aSecondsSustain );
			filterVibrato = new FilterVibrato( ref aMidiPitch );
			generatorPortament = new GeneratorPortament( aNote, aSoundfont.soundinfo.GetPitch() );
        }

		public void Portament( byte aNote, byte aVelocity, SoundfontBase aSoundfont, ref MidiVolume aMidiVolume, ref MidiPitch aMidiPitch, double aSecondsSustain = 0.0d )
        {
			Logger.Normal( "Portament" );

			generatorEnvelope.Set( aSoundfont, aVelocity, aSecondsSustain );
			generatorPortament.Portament( aNote, aSoundfont.soundinfo.GetPitch(), ref aMidiPitch );
        }

		public void Oscillate( double[] aBuffer, int aSampleRate, ref MidiVolume aMidiVolume, ref MidiPitch aMidiPitch )
		{
			generatorPortament.Update( aSampleRate );

			double lSampleSpeed = 1.0d / ( double )aSampleRate;
			double lAddSamples = generatorPortament.noteFrequency * aMidiPitch.GetFrequency() * lSampleSpeed;

			filterVibrato.Filter( ref lAddSamples, aSampleRate, ref aMidiPitch );

			generatorEnvelope.Generate( aBuffer, lAddSamples, lSampleSpeed );
		}

        public bool GetFlagNoteOn()
        {
			return generatorEnvelope.generatorEnvelopeCurrent.GetFlagNoteOn();
        }

        public bool GetFlagEnd()
        {
			return generatorEnvelope.generatorEnvelopeCurrent.GetFlagEnd();
        }

        public bool GetFlagSustain()
        {
			return generatorEnvelope.generatorEnvelopeCurrent.GetFlagSustain();
        }

        public void NoteOff()
        {
			generatorEnvelope.generatorEnvelopeCurrent = GeneratorEnvelopeSet.generatorEnvelopeRelease;
        }

        public void SoundOff()
        {
			generatorEnvelope.generatorEnvelopeCurrent = GeneratorEnvelopeSet.generatorEnvelopeRelease;
        }
    }
}
